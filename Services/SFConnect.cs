﻿using ArdantOffical.IService;
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
        public static string Token = "R3y7G69dbiUr0ZEPINWk0kG6";
        const string sfdcConsumerKey = "3MVG9q4K8Dm94dAzDT_aDS743.EBFnz8.AnyCRpFg89H_0Nrdj2y10IpQK51JzGe0sQuvjsVgG9fCUJXkfKbx";
        const string sfdcConsumerSecret = "2C09BB6E4A252DA8C54164E0FCFCFCD49A165E5846D70A94D92DECDA9344200F";
       
        public static SalesforceClient client = new SalesforceClient();
        //public static UsernamePasswordAuthenticationFlow authFlow = new(sfdcConsumerKey, sfdcConsumerSecret, User, Password);
        var authflow = new UsernamePasswordAuthenticationFlow(sfdcConsumerKey, sfdcConsumerSecret, User, Password);

        //public static UsernamePasswordAuthenticationFlow authFlow = new UsernamePasswordAuthenticationFlow(sfdcConsumerKey, sfdcConsumerSecret, User, Password);

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
