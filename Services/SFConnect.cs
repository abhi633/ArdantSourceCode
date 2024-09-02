using ArdantOffical.IService;
using ArdantOffical.Models;
using CsvHelper.Configuration;
using Microsoft.Extensions.Configuration;
using SalesforceSharp;
using SalesforceSharp.Security;
using System;
using System.Collections;
using System.Collections.Generic;
namespace ArdantOffical.Services
{
    public class SFConnect : ISFConnect
    {   
        public static string User = "access@ardant.com.au";
        public static string Password = "cArl0905$$$$";
        public static string Token = "3VwRdO5SZLa9xXsGrEDgJuCQ";
        const string sfdcConsumerKey = "3MVG9q4K8Dm94dAzDT_aDS743.P9QRZAcnaEAmyep1.iuO5yau7zk7oygLxR4XAtKu_5izSqVe7vvIE0nTbsB";
        const string sfdcConsumerSecret = "C7023AEEF43414D91F62472A996EC13BD5EDDC9F773ADB9CE741DBF007634206";
       
        public static SalesforceClient client = new SalesforceClient();
        public static UsernamePasswordAuthenticationFlow authFlow = new(sfdcConsumerKey, sfdcConsumerSecret, User, Password);
        public SFConnect()
        {
          //  OpenConnection();
        }

        public static void OpenConnection()
        {
            client.Authenticate(authFlow);

        }

        /// <summary>  
        /// For calculating age  
        /// </summary>  
        /// <param name="Dob">Enter Date of Birth to Calculate the age</param>  
        /// <returns> years, months,days, hours...</returns>  
       public static string CalculateAge(DateTime Dob)
        {
            DateTime Now = DateTime.Now;
            int Years = new DateTime(DateTime.Now.Subtract(Dob).Ticks).Year - 1;
            DateTime PastYearDate = Dob.AddYears(Years);
            int Months = 0;
            for (int i = 1; i <= 12; i++)
            {
                if (PastYearDate.AddMonths(i) == Now)
                {
                    Months = i;
                    break;
                }
                else if (PastYearDate.AddMonths(i) >= Now)
                {
                    Months = i - 1;
                    break;
                }
            }
            //int Days = Now.Subtract(PastYearDate.AddMonths(Months)).Days;
            //int Hours = Now.Subtract(PastYearDate).Hours;
            //int Minutes = Now.Subtract(PastYearDate).Minutes;
            //int Seconds = Now.Subtract(PastYearDate).Seconds;
            return String.Format("Age: {0} Y",
            Years);
        }


    }
}
