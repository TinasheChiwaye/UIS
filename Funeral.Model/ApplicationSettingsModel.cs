﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Funeral.Model
{
    public class ApplicationSettingsModel
    {
        public ApplicationSettingsModel()
        {

            ApplicationName = string.Empty;
            ApplicationLogoPath = string.Empty;
            OwnerFirstName = string.Empty;
            OwnerSurname = string.Empty;
            OwnerTelNumber = string.Empty;
            OwnerCellNumber = string.Empty;
            ManagerFirstName = string.Empty;
            ManageSurname = string.Empty;
            ManageTelNumber = string.Empty;
            ManageCellNumber = string.Empty;
            BusinessAddressLine1 = string.Empty;
            BusinessAddressLine2 = string.Empty;
            BusinessAddressLine3 = string.Empty;
            BusinessAddressLine4 = string.Empty;
            BusinessPostalCode = string.Empty;
            FSBNumber = string.Empty;
            CereliaAPIKey = string.Empty;
            RegistrationNumber = string.Empty;
            ManageSlogan = string.Empty;
            ManageEmail = string.Empty;
            ManageFaxNumber = string.Empty;
            OwnerEmail = string.Empty;
            VatNo = string.Empty;
            EmailForClaimNotification = string.Empty;
            //IsAutoGeneratedPolicyNo = true; 

        }
        public int pkiApplicationID { get; set; }

        [Required(ErrorMessage = "Please enter Scheme Name")]
        public string ApplicationName { get; set; }

        public string ApplicationLogoPath { get; set; }

        [Required(ErrorMessage = "Please enter Owner First Name")]
        [RegularExpression(pattern: @"[a-zA-Z ]*$", ErrorMessage = "Owner FirstName Enter Only characters")]
        public string OwnerFirstName { get; set; }

        [Required(ErrorMessage = "Please enter Owner Last Name")]
        [RegularExpression(pattern: @"[a-zA-Z ]*$", ErrorMessage = "Owner SurName Enter Only characters")]
        public string OwnerSurname { get; set; }

        [Required(ErrorMessage = "Please enter Owners Telephone number")]
        [RegularExpression(pattern: @"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Owners Telephone Number Enter Only Number")]
        public string OwnerTelNumber { get; set; }

        [Required(ErrorMessage = "Please enter Owners cell phone number")]
        [RegularExpression(pattern: @"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Owners Cellphone Number Enter Only Number")]
        public string OwnerCellNumber { get; set; }

        [Required(ErrorMessage = "Please enter Owners Email Address")]
        [RegularExpression(pattern: @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter valid Owners email address")]
        public string OwnerEmail { get; set; }

        public string ManagerFirstName { get; set; }

        public string ManageSurname { get; set; }

        [Required(ErrorMessage = "Please enter cell Telephone number")]
        [RegularExpression(pattern: @"^([0-9\(\)\/\+ \-]*)$", ErrorMessage = "Telephone Number Enter Only Number")]
        public string ManageTelNumber { get; set; }

        public string ManageCellNumber { get; set; }

        [Required(ErrorMessage = "Please enter line 1")]
        public string BusinessAddressLine1 { get; set; }

        [Required(ErrorMessage = "Please enter line 2")]
        public string BusinessAddressLine2 { get; set; }

        public string BusinessAddressLine3 { get; set; }

        public string BusinessAddressLine4 { get; set; }

        [Required(ErrorMessage = "Please Enter Postal Code")]
        [RegularExpression(pattern: @"^[0-9]*$", ErrorMessage = "Postal Code Enter Only Number")]
        public string BusinessPostalCode { get; set; }

        public string FSBNumber { get; set; }

        public string CereliaAPIKey { get; set; }

        public string RegistrationNumber { get; set; }

        public string ManageSlogan { get; set; }

        [Required(ErrorMessage = "Please enter Email Address")]
        [RegularExpression(pattern: @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter valid email address")]
        public string ManageEmail { get; set; }

        public string ManageFaxNumber { get; set; }        

        public Guid parlourid { get; set; }

        public Byte[] ApplicationLogo { get; set; }

        public string ApplicationRules { get; set; }

        public Boolean IsAutoGeneratedPolicyNo { get; set; }

        public string VatNo { get; set; }

        public Guid Currentparlourid { get; set; }

        public ApplicationTnCModel TermAndConditions { get; set; }

        //public List<smsSendingGroupModel> SMSSettings { get; set; }
        public List<smsTempletModel> SMSSettings { get; set; }

        public BankingDetailSending BankDetails { get; set; }

        [Required(ErrorMessage = "Please Enter Claim Email Address")]
        [RegularExpression(pattern: @"\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*", ErrorMessage = "Please enter valid Claim email address")]
        public string EmailForClaimNotification { get; set; }


    }
    
}
