﻿using ArdantOffical.Components.ClinicalData;
using ArdantOffical.Data.ModelVm.Invoices;
using ArdantOffical.Data.ModelVm;
using ArdantOffical.Data;
using ArdantOffical.Helpers.Enums;
using ArdantOffical.IService;
using ArdantOffical.Models;
using ArdantOffical.Services;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.JSInterop;
using Radzen.Blazor;
using Radzen;
using SalesforceSharp;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System;
using System.Threading.Tasks;
using System.IO;
using ArdantOffical.Helpers.Extensions;
using DocumentFormat.OpenXml.Presentation;
using static Microsoft.AspNetCore.Razor.Language.TagHelperMetadata;

namespace ArdantOffical.Pages.Invoices
{
    public partial class EditInvoice
    {
        [Parameter]
        public string InvoiceId { get; set; }
        [Parameter]
        public string JobID { get; set; }
        private int currentPage = 1;
        private int totalPageQuantity;
        private int totalCount = -1;
        public PaginationDTO paginationObj { get; set; } = new PaginationDTO();
        //public List<InvoicesVM> lstInvoices { get; set; }
        //public InvoicesVM Modal = new InvoicesVM();
        public InvoicesVMForTable ListOfRecord { get; set; }
        public List<InvoicesVM> PerPageInvoiceRecords { get; set; }
        public IQueryable<InvoicesVM> PerPageUserRecordIQueryable { get; set; }
        public List<InvoicesVM> AllUserRecords { get; set; }
        /*   Modal Popup Params */
        public bool showModal { get; set; } = false;
        public string Message { get; set; }
        public string title { get; set; }
        public EventCallback<bool> OnVisibilityChanged { get; set; }
        public bool responseDialogVisibility { get; set; }
        public string responseHeader { get; set; }
        public string responseBody { get; set; }
        public TostModel TostModelclass { get; set; } = new();
        public bool IsloaderShow { get; set; } = false;
        [Inject]
        AuthenticationStateProvider UserauthenticationStateProvider { get; set; }
        public CurrentUserInfoVM Userinfo { get; set; }
        public string ErrorMessage { get; set; }
        public InvoicesVM InvoiceModal = new InvoicesVM();
        public InvoiceItemsVm Modal = new InvoiceItemsVm();

        public string rootFolder;




        public List<SelectListItem> lstStatus = new List<SelectListItem>();
        public List<SelectListItem> lstAssignedJobs = new List<SelectListItem>();
        public List<NDIS> lstJobs = new List<NDIS>();
        public List<SelectListItem> ItemsList = new();
        public List<SelectListItem> RatesList = new();
        public List<SelectListItem> TaxesList = new();
        public List<SelectListItem> lstUoM = new();

        [Inject]
        public FGCDbContext Context { get; set; }
        [Inject]
        IWebHostEnvironment Environment { get; set; }
        private string baseUrl;

        protected override async Task OnInitializedAsync()
        {
            try
            {
                IsloaderShow = true;
                ConnectSalesforce();
                GetItems();
                GetUoM();
                GetStatus();
                GetTaxList();
                GetRateList();
                baseUrl = Navigator.BaseUri;
                rootFolder = Path.Combine(Environment.ContentRootPath, "wwwroot/Documents/");
                Userinfo = await UserauthenticationStateProvider.CurrentUser();
                InvoiceModal.OT = Userinfo.SalesforceID;
               await LoadAssignedJobs();
               await LoadInvoiceData();
                IsloaderShow = false;

            }
            catch (SalesforceException ex)
            {
                IsloaderShow = false;
                responseHeader = "ERROR";
                responseBody = string.Format("Authentication failed: {0} : {1}", ex.Error, ex.Message);
                responseDialogVisibility = true;

            }
        }

        protected override async Task OnParametersSetAsync()
        {
            try
            {
                if (!string.IsNullOrEmpty(InvoiceModal.JobID))
                {
                    GetReceipiantEmail();
                }
            }
            catch (SalesforceException ex)
            {
                IsloaderShow = false;
                responseHeader = "ERROR";
                responseBody = string.Format("Authentication failed: {0} : {1}", ex.Error, ex.Message);
                responseDialogVisibility = true;

            }

        }

        public async Task LoadAssignedJobs()
        {

            lstJobs = await IinvoiceServicesItems.GetAssignedNDISJobs();
        }
        public async Task LoadInvoiceData()
        {
            InvoiceModal = await IinvoiceServices.GetInvoiceItems(InvoiceId);
            SubTotal = InvoiceModal.InvoiceItemList.Sum(x => x.Amount).Value;
            TaxAmount = InvoiceModal.InvoiceItemList.Sum(x => x.TaxAmount);
            Total = InvoiceModal.InvoiceItemList.Sum(x => x.TotalAmount);
           
        }

        private void ConnectSalesforce()
        {
            try
            {
                if (!SFConnect.client.IsAuthenticated)
                {
                    SFConnect.OpenConnection();
                }
            }
            catch (SalesforceException ex)
            {
                IsloaderShow = false;
                responseHeader = "ERROR";
                responseBody = string.Format("Authentication failed: {0} : {1}", ex.Error, ex.Message);
                responseDialogVisibility = true;

            }
        }

        public void GetReceipiantEmail()
        {
            ConnectSalesforce();
            try
            {
                var record = SFConnect.client.Query<NDIS>("SELECT Id,PrimaryContactEmail__c, Job_Number__c,P_Firstname__c,P_Surname__c,Street_Address__c,Suburb__c,Postcode__c FROM NDIS_Job__c  WHERE Status__c= 'Assigned' AND Id='" + InvoiceModal.JobID + "' ");
                InvoiceModal.SentTo = record.FirstOrDefault().PrimaryContactEmail__c;
                InvoiceModal.Job_Number = record.FirstOrDefault().Job_Number__c;
                InvoiceModal.Customer_Name = record.FirstOrDefault().P_Firstname__c + ' ' + record.FirstOrDefault().P_Surname__c;
                InvoiceModal.Address = record.FirstOrDefault().Street_Address__c;
                InvoiceModal.Suburb = record.FirstOrDefault().Suburb__c;
                InvoiceModal.Postcode = record.FirstOrDefault().Postcode__c;
            }
            catch (Exception ex)
            {

                throw;
            }


        }
        public void GetRateList()
        {
            try
            {
                RatesList = IinvoiceServices.GetRateList();
            }
            catch (Exception ex)
            {
                responseHeader = "ERROR";
                responseBody = ex.Message;
                responseDialogVisibility = true;
            }
        }
        public void GetTaxList()
        {
            try
            {
                TaxesList = IinvoiceServices.GetTaxesList();
            }
            catch (Exception ex)
            {
                responseHeader = "ERROR";
                responseBody = ex.Message;
                responseDialogVisibility = true;
            }
        }

        //public void OnChangeState(ChangeEventArgs e)
        //{
        //    InvoiceModal.Status = e.Value.ToString();
        //}

        public void OnJobSelected(string value)
        {
            InvoiceModal.JobID = value;
            GetReceipiantEmail();
        }

        public void GetStatus()
        {
            lstStatus.Add(new SelectListItem() { Value = "Pending", Text = "Pending" });
            lstStatus.Add(new SelectListItem() { Value = "Paid", Text = "Paid" });
            lstStatus.Add(new SelectListItem() { Value = "Overdue", Text = "Overdue" });
            lstStatus.Add(new SelectListItem() { Value = "Sent", Text = "Sent" });
            lstStatus.Add(new SelectListItem() { Value = "Disputed", Text = "Disputed" });

        }

        private void GetItems()
        {
            ItemsList.Add(new SelectListItem() { Value = "NDIS Travel ", Text = "NDIS Travel" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Report", Text = "NDIS Report" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Communication", Text = "NDIS Communication" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Assessment of Participant", Text = "NDIS Assessment of Participant" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Meeting", Text = "NDIS Meeting" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Site Assessment", Text = "NDIS Site Assessment" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Equipment Trial", Text = "NDIS Equipment Trial" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Consumables", Text = "NDIS Consumables" });
            ItemsList.Add(new SelectListItem() { Value = "NDIS Intervention/ Therapy", Text = "NDIS Intervention/ Therapy" });

        }
        private void GetUoM()
        {
            lstUoM.Add(new SelectListItem() { Value = "Minutes", Text = "Minutes" });
            lstUoM.Add(new SelectListItem() { Value = "KM", Text = "KM" });


        }

        public string Id { get; set; }
        public string ActionName { get; set; } = "Update";
        //public async Task EditInvoice(string id)
        //{
        //    Id = id;
        //    Modal = await IinvoiceServicesItems.GetInvoiceByID(Id);
        //    ActionName = "Update";
        //}

        #region Radzden Data Grid

        RadzenDataGrid<InvoiceItemsVm> invoiceItemsGrid;
        DataGridEditMode editMode = DataGridEditMode.Multiple;

        List<InvoiceItemsVm> invoiceItemToInsert = new List<InvoiceItemsVm>();
        List<InvoiceItemsVm> invoiceItemToUpdate = new List<InvoiceItemsVm>();
        public decimal SubTotal { get; set; }
        public decimal TaxAmount { get; set; }
        public bool IsNDISTravel { get; set; } = false;
        public decimal Total { get; set; }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await DefaultRows();
               
            }
        }
        async Task DefaultRows()
        {
            //if (editMode == DataGridEditMode.Single)
            //{
            //    Reset();
            //}
          await  EditRow(InvoiceModal.InvoiceItemList);
        }
        void Reset()
        {
            invoiceItemToInsert.Clear();
            invoiceItemToInsert.Clear();
        }
        void OnChange(object value)
        {
            try
            {
                if (value != null)
                {
                    var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;
                    Console.WriteLine($"Value changed to {str}");
                }
            }
            catch (Exception ex)
            {

            }

        }

        //void OnItemChange(object value)
        //{
        //    try
        //    {
        //        if (value != null)
        //        {
        //            var str = value is IEnumerable<object> ? string.Join(", ", (IEnumerable<object>)value) : value;

        //            Modal.ItemName = str.ToString();
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //    }

        //}
        void Reset(InvoiceItemsVm invoiceItem)
        {
            invoiceItemToInsert.Remove(invoiceItem);
            invoiceItemToUpdate.Remove(invoiceItem);
        }

        async Task EditRow(List<InvoiceItemsVm> invoiceItemList)
        {
            //if (editMode == DataGridEditMode.Single && invoiceItemToInsert.Count() > 0)
            //{
            //    Reset();
            //}
            foreach (var invoiceItem in invoiceItemList)
            {
                invoiceItemToUpdate.Add(invoiceItem);
                await invoiceItemsGrid.EditRow(invoiceItem);
            }

        }

        void OnUpdateRow(InvoiceItemsVm invoiceItem)
        {
            Reset(invoiceItem);
            int index = InvoiceModal.InvoiceItemList.FindIndex(item => item.Id == invoiceItem.Id);

            if (index != -1)
            {
                // Replace the item at the found index with the updated item
                InvoiceModal.InvoiceItemList[index] = invoiceItem;
            }
        }

        async Task SaveRow(InvoiceItemsVm invoiceItem)
        {
            invoiceItem.UnitPrice = Convert.ToDecimal(invoiceItem.Rate);
            invoiceItem.Description = RemoveHtmlTags(invoiceItem.Description);

            SubTotal = InvoiceModal.InvoiceItemList.Sum(x => x.Amount).Value;
            TaxAmount = InvoiceModal.InvoiceItemList.Sum(x => x.TaxAmount);
            Total = InvoiceModal.InvoiceItemList.Sum(x => x.TotalAmount);
            //  await invoiceItemsGrid.UpdateRow(invoiceItem);
        }
        string RemoveHtmlTags(string input)
        {
            return Regex.Replace(input, "<.*?>", string.Empty);
        }
        void CancelEdit(InvoiceItemsVm invoiceItem)
        {
            Reset(invoiceItem);

            invoiceItemsGrid.CancelEditRow(invoiceItem);
            // Find the original item
            var originalItem = InvoiceModal.InvoiceItemList.FirstOrDefault(item => item.Id == invoiceItem.Id);

            if (originalItem != null)
            {
                // Check if the item was modified
                if (!invoiceItem.Equals(originalItem))
                {
                    // Update the edited item with the original values
                    int index = InvoiceModal.InvoiceItemList.IndexOf(invoiceItem);
                    if (index != -1)
                    {
                        InvoiceModal.InvoiceItemList[index] = originalItem;
                    }
                }
            }
        }

        async Task DeleteRow(InvoiceItemsVm invoiceItem)
        {
            Reset(invoiceItem);

            if (InvoiceModal.InvoiceItemList.Contains(invoiceItem))
            {
                InvoiceModal.InvoiceItemList.Remove(invoiceItem);
                await invoiceItemsGrid.Reload();
            }
            else
            {
                invoiceItemsGrid.CancelEditRow(invoiceItem);
                await invoiceItemsGrid.Reload();
            }
        }

        async Task InsertRow()
        {
            if (editMode == DataGridEditMode.Single)
            {
                Reset();
            }

            var invoiceItem = new InvoiceItemsVm();
            invoiceItemToInsert.Add(invoiceItem);
            await invoiceItemsGrid.InsertRow(invoiceItem);
        }

        void OnCreateRow(InvoiceItemsVm invoiceItem)
        {
            InvoiceModal.InvoiceItemList.Add(invoiceItem);
            invoiceItemToInsert.Remove(invoiceItem);
        }



        #endregion

        public bool DeleteConfirmationVisibility { get; set; }
        public void OnDeleteConfirmationVisibilityChangedModel(bool visibilityStatus)
        {
            DeleteConfirmationVisibility = visibilityStatus;

        }
        public bool IsDelete { get; set; } = false;
        async Task DeleteInvoice(string id)
        {
            try
            {
                DeleteConfirmationVisibility = true;
                Id = id;
            }
            catch (Exception ex)
            {
                responseHeader = "ERROR";
                responseBody = ex.Message;
                responseDialogVisibility = true;
            }

        }

       
        private DotNetObjectReference<JobsAttachments> dotNetObjectReference;
        string filename = "Invoice" + "_" + DateTime.Now.ToString("ddmmyyyyhhmmss") + ".pdf";

        public string fileUpload = "fileUploadId";
        private async Task BuisnessFileupdate(string id)
        {
            try
            {
                StateHasChanged();

                //await js.InvokeAsync<List<string>>("MyFileUploadFunctionForOnBoarding", dotNetObjectReference, id);
                await js.InvokeAsync<List<string>>("MyFileUploadFunctionForOnBoarding", dotNetObjectReference, fileUpload);
            }
            catch (Exception ex)
            {
                TostModelclass.AlertMessageShow = true;
                TostModelclass.AlertMessagebody = ex.Message;
                TostModelclass.Msgstyle = MessageColor.Error;
            }
        }
        
        public async Task SaveData()
        {
          
            ConnectSalesforce();
            // Call the create method to create the record
            try
            {
                //Modal.InvoiceId = InvoiceId;
                if (ActionName == "Update")
                {
                    InvoiceModal.Folder = DateTime.Now.Year.ToString() + "_documents";
                    InvoiceModal.Filename = filename;
                    // create a record using an anonymous class and returns the ID
                    // Add Invoice record first
                    // Create the attachmend PDF
                    string partialName = "/Views/InvoicePdf.cshtml";
                    htmlContent = RazorRendererHelper.RenderPartialToString(partialName, InvoiceModal);

                    InvoiceId = await IinvoiceServices.UpdateInvoice(InvoiceModal, InvoiceModal.JobID, htmlContent);
                    foreach (var item in InvoiceModal.InvoiceItemList)
                    {
                        item.InvoiceId = InvoiceId;
                    }
                    Exception registerResponse = await IinvoiceServicesItems.AddInvoiceItem(InvoiceModal.InvoiceItemList);
                    if (registerResponse.Message == "1")
                    {
                        TostModelclass.AlertMessageShow = true;
                        TostModelclass.AlertMessagebody = "Invoice Record Update";
                        TostModelclass.Msgstyle = MessageColor.Success;
                        ActionName = "Send Email";
                    }
                    else
                    {

                        TostModelclass.AlertMessageShow = true;
                        TostModelclass.AlertMessagebody = registerResponse.Message;
                        TostModelclass.Msgstyle = MessageColor.Error;

                    }
                }
                else if (ActionName == "Send Email")
                {
                    Modal.Id = Id;
                   
                    Exception registerResponse = await IinvoiceServices.SendInvoiceInEmail(htmlContent, filename, InvoiceModal);
                    if (registerResponse.Message == "1")
                    {
                        TostModelclass.AlertMessageShow = true;
                        TostModelclass.AlertMessagebody = "Invoice Sent to the customer";
                        TostModelclass.Msgstyle = MessageColor.Success;
                    }
                    else
                    {
                        TostModelclass.AlertMessageShow = true;
                        TostModelclass.AlertMessagebody = registerResponse.Message;
                        TostModelclass.Msgstyle = MessageColor.Error;
                    }
                }
               

            }
            catch (SalesforceException ex)
            {
                IsloaderShow = false;
                TostModelclass.AlertMessageShow = true;
                TostModelclass.AlertMessagebody = " Failed to save the invoice details : " + ex.Message;
                TostModelclass.Msgstyle = MessageColor.Error;

            }

        }



        public async void OnDeleteConfirmationSuccess(bool isAdded)
        {
            if (isAdded)
            {
                DeleteConfirmationVisibility = false;
                Exception registerResponse = await IinvoiceServicesItems.DeleteInvoiceItem(Id);
                if (registerResponse.Message == "1")
                {
                    TostModelclass.AlertMessageShow = true;
                    TostModelclass.AlertMessagebody = "Invoice Item Record Deleted";
                    TostModelclass.Msgstyle = MessageColor.Success;

                }
                //StateHasChanged();
            }
        }

        #region render Pdf Content

        public bool IsPdfPreview { get; set; }

        private string htmlContent;
        [Inject]
        public IRazorRendererHelper RazorRendererHelper { get; set; }
        private async Task LoadHtmlContent()
        {

            // var invoiceItems = await IinvoiceServicesItems.GetInvoiceItems(InvoiceId);
            string partialName = "/Views/InvoicePdf.cshtml";
            htmlContent = RazorRendererHelper.RenderPartialToString(partialName, InvoiceModal);
            IsPdfPreview = true;
            //htmlContent = await IinvoiceServices.GetInvoiceHtmlContent(baseUrl,InvoiceId);

        }
        public void HidePreviewPdf()
        {
            IsPdfPreview = false;
        }
    }

    #endregion
}

