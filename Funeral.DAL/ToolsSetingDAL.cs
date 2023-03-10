using Funeral.Model;
using System;
using System.Data;
using System.Data.SqlClient;
using Funeral.DAL.niws_partner;
using System.Text;

namespace Funeral.DAL
{
    public class ToolsSetingDAL
    {

        public static AdditionalApplicationSettingsModel ValidateServiceKey(AdditionalApplicationSettingsModel model)
        {
            //initialise all operations needed
            //---------------------------------------
            NIWS_PartnerClient client = new niws_partner.NIWS_PartnerClient();
            ValidateServiceKeyRequest ValidateServiceKeyRequest = new niws_partner.ValidateServiceKeyRequest();
            ServiceInfoList sil = new ServiceInfoList();
            StringBuilder Return_Result = new StringBuilder();

            //---------------------------------------

            //Populating request to validate
            //---------------------------------------

            //Add account number to MerchantAccount
            ValidateServiceKeyRequest.MerchantAccount = model.spNetcashAccountNumber;

            string SoftwareVendorKey = model.spSoftwareVendorKey;
            //string SoftwareVendorKey = "24ade73c-98cf-47b3-99be-cc7b867b3080";

            //Add Vendor key issued by Netcash
            ValidateServiceKeyRequest.SoftwareVendorKey = SoftwareVendorKey;



            //checks if field was populated
            if (model.spAccountServiceKey != String.Empty)
            {
                ServiceInfo si = new ServiceInfo();
                si.ServiceId = "5";
                si.ServiceKey = model.spAccountServiceKey;

                sil.Add(si);
            }

            if (model.spPaynowServiveKey != String.Empty)
            {
                ServiceInfo si = new ServiceInfo();

                si.ServiceId = "14";
                si.ServiceKey = model.spPaynowServiveKey;
                sil.Add(si);
            }

            if (model.spDebitOrderServicekey != String.Empty)
            {
                ServiceInfo si = new ServiceInfo();

                si.ServiceId = "1";
                si.ServiceKey = model.spDebitOrderServicekey;
                sil.Add(si);
            }




            //Add service info list to request
            ValidateServiceKeyRequest.ServiceInfoList = sil;

            //---------------------------------------
            //Calling the ValidateServiceKey method validating account number with the service keys added
            var Request = client.ValidateServiceKey(ValidateServiceKeyRequest);

            //Do a check on the response for Account Status
            //001 = valid
            if (Request.AccountStatus == "001")
            /*
             * 
            001	    Authenticated
            103	    No active partner found for this Software vendor key
            104	    No active client found for this Account number
            200	    General service error – contact Netcash support
            201	    Account locked out
             */

            {
                model.spNetcashAccountNumberMessage = "AccountStatus, Validated";
                //  Return_Result.Append("AccountStatus, Validated");
                //do something, eg. output if account active
                //then add service info to list to check on each service status
                ServiceInfoResponseList ServiceInfoResponseList = Request.ServiceInfo;
                foreach (var s in ServiceInfoResponseList)
                {
                    Return_Result.Append("");
                    string service = s.ServiceId;
                    switch (service)
                    /*
               001	    Validated
               105	    No active service found for this Account number/ServiceId combination
               106	    No active service key found for this Account number/ServiceId /Servicekey combination    
                 */
                    {


                        case "1": //do something //Debit orders
                            if (s.ServiceStatus == "011")
                                model.spDebitOrderServikeyMessage = ("Debit orders Service Key, Validated");

                            if (s.ServiceStatus == "105")
                                model.spDebitOrderServikeyMessage = ("Debit orders Service Key, No active service found for this Account number/ServiceId combination");
                            if (s.ServiceStatus == "106")
                                model.spDebitOrderServikeyMessage = ("Debit orders Service Key, No active service key found for this Account number/ServiceId /Servicekey combination");

                            break;
                        case "5": //do something //Account service
                            if (s.ServiceStatus == "011")
                                model.spAccountServiceKeyMessage = ("Account service Key, Validated");
                            if (s.ServiceStatus == "105")
                                model.spAccountServiceKeyMessage = ("Account service Key, No active service found for this Account number/ServiceId combination");
                            if (s.ServiceStatus == "106")
                                model.spAccountServiceKeyMessage = ("Account service Key, No active service key found for this Account number/ServiceId /Servicekey combination");
                            break;
                        case "14": //do something //Pay Now
                            if (s.ServiceStatus == "011")
                                model.spPaynowServiveKeyMessage = ("Pay Now service Key, Validated");
                            if (s.ServiceStatus == "105")
                                model.spPaynowServiveKeyMessage = ("Pay Now service Key, No active service found for this Account number/ServiceId combination");
                            if (s.ServiceStatus == "106")
                                model.spPaynowServiveKeyMessage = ("Pay Now service Key, No active service key found for this Account number/ServiceId /Servicekey combination");
                            break;

                    }
                }
            }
            else
            {
                /*

                   001	    Authenticated
                   103	    No active partner found for this Software vendor key
                   104	    No active client found for this Account number
                   200	    General service error – contact Netcash support
                   201	    Account locked out
                    */
                if (Request.AccountStatus == "103")
                {
                    model.spNetcashAccountNumberMessage = ("No active partner found for this Software vendor key");
                    model.spDebitOrderServikeyMessage = ("No active partner found for this Software vendor key");
                    model.spPaynowServiveKeyMessage = ("No active partner found for this Software vendor key");
                    model.spAccountServiceKeyMessage = ("No active partner found for this Software vendor key");
                    model.spSoftwareVendorKeyMessage = ("No active partner found for this Software vendor key");
                }

                if (Request.AccountStatus == "104")
                {

                    model.spNetcashAccountNumberMessage = ("No active client found for this Account number");
                    model.spDebitOrderServikeyMessage = ("No active client found for this Account number");
                    model.spPaynowServiveKeyMessage = ("No active client found for this Account number");
                    model.spAccountServiceKeyMessage = ("No active client found for this Account number");
                    model.spSoftwareVendorKeyMessage = ("No active client found for this Account number");
                }
                if (Request.AccountStatus == "200")
                {
                    model.spNetcashAccountNumberMessage = ("General service error – contact Netcash support");
                    model.spDebitOrderServikeyMessage = ("General service error – contact Netcash support");
                    model.spPaynowServiveKeyMessage = ("General service error – contact Netcash support");
                    model.spAccountServiceKeyMessage = ("General service error – contact Netcash support");
                    model.spSoftwareVendorKeyMessage = ("General service error – contact Netcash support");

                }
                if (Request.AccountStatus == "201")
                {
                    model.spNetcashAccountNumberMessage = ("Account locked out");
                    model.spDebitOrderServikeyMessage = ("Account locked out");
                    model.spPaynowServiveKeyMessage = ("Account locked out");
                    model.spAccountServiceKeyMessage = ("Account locked out");
                    model.spSoftwareVendorKeyMessage = ("Account locked out");
                }


                if (model.spAccountServiceKey == String.Empty)
                    model.spAccountServiceKeyMessage = ("");


                if (model.spDebitOrderServicekey == String.Empty)
                    model.spDebitOrderServikeyMessage = "";


                if (model.spPaynowServiveKey == String.Empty)
                    model.spPaynowServiveKeyMessage = "";

                if (model.spPaynowServiveKey == String.Empty)
                    model.spSoftwareVendorKeyMessage = "";




                //output if account not active
            }
            //Close client
            client.Close();

            return model;
        }
        public static DataTable GetAdditionalApplicationSettingsdt(int ApplicationID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ApplicationID", DbParameter.DbType.Int, 0, ApplicationID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAdditionalApplicationSettings_New", ObjParam);
        }
        public static DataTable GetAdditionalApplicationSettingsByParlourID(Guid ApplicationID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ApplicationID", DbParameter.DbType.UniqueIdentifier, 0, ApplicationID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAdditionalApplicationSettingsByParlourID_New", ObjParam);
        }
        public static int UploadApplicationLogo(ApplicationSettingsModel model)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ApplicationLogoPath", DbParameter.DbType.VarChar, 0, model.ApplicationLogoPath);
            ObjParam[1] = new DbParameter("@ApplicationLogo", DbParameter.DbType.Image, 0, model.ApplicationLogo);
            ObjParam[2] = new DbParameter("@pkiApplicationID", DbParameter.DbType.Int, 0, model.pkiApplicationID);
            //return 1;
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UploadApplicationLogo", ObjParam));
        }
        public static int RemoveApplicationLogo(int ApplicationID)
        {
            DbParameter[] ObjParam = new DbParameter[1];            
            ObjParam[0] = new DbParameter("@pkiApplicationID", DbParameter.DbType.Int, 0, ApplicationID);
            //return 1;
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "RemoveApplicationLogo", ObjParam));
        }
        public static int UploadUnderwriterLogo(UnderwriterSetupModel model)
        {
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@UnderwriterLogoPath", DbParameter.DbType.VarChar, 0, model.UnderwriterLogoPath);
            ObjParam[1] = new DbParameter("@UnderwriterLogo", DbParameter.DbType.Image, 0, model.UnderwriterLogo);
            ObjParam[2] = new DbParameter("@PkiUnderWriterSetupId", DbParameter.DbType.Int, 0, model.PkiUnderWriterSetupId);
            //return 1;
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "UploadUnderwriterLogo", ObjParam));
        }
        public static SqlDataReader SaveApplication(ApplicationSettingsModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveApplicationSetting";
            DbParameter[] ObjParam = new DbParameter[28];
            ObjParam[0] = new DbParameter("@pkiApplicationID", DbParameter.DbType.Int, 0, model.pkiApplicationID);
            ObjParam[1] = new DbParameter("@ApplicationName", DbParameter.DbType.NVarChar, 0, model.ApplicationName);
            ObjParam[2] = new DbParameter("@ApplicationLogoPath", DbParameter.DbType.NVarChar, 0, model.ApplicationLogoPath);
            ObjParam[3] = new DbParameter("@OwnerFirstName", DbParameter.DbType.NVarChar, 0, model.OwnerFirstName);
            ObjParam[4] = new DbParameter("@OwnerSurname", DbParameter.DbType.NVarChar, 0, model.OwnerSurname);
            ObjParam[5] = new DbParameter("@OwnerTelNumber", DbParameter.DbType.NVarChar, 0, model.OwnerTelNumber);
            ObjParam[6] = new DbParameter("@OwnerCellNumber", DbParameter.DbType.NVarChar, 0, model.OwnerCellNumber);
            ObjParam[7] = new DbParameter("@ManagerFirstName", DbParameter.DbType.NVarChar, 0, model.ManagerFirstName);
            ObjParam[8] = new DbParameter("@ManageSurname", DbParameter.DbType.NVarChar, 0, model.ManageSurname);
            ObjParam[9] = new DbParameter("@ManageTelNumber", DbParameter.DbType.NVarChar, 0, model.ManageTelNumber);
            ObjParam[10] = new DbParameter("@ManageCellNumber", DbParameter.DbType.NVarChar, 0, model.ManageCellNumber);
            ObjParam[11] = new DbParameter("@BusinessAddressLine1", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine1);
            ObjParam[12] = new DbParameter("@BusinessAddressLine2", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine2);
            ObjParam[13] = new DbParameter("@BusinessAddressLine3", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine3);
            ObjParam[14] = new DbParameter("@BusinessAddressLine4", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine4);
            ObjParam[15] = new DbParameter("@BusinessPostalCode", DbParameter.DbType.NVarChar, 0, model.BusinessPostalCode);
            ObjParam[16] = new DbParameter("@FSBNumber", DbParameter.DbType.NVarChar, 0, model.FSBNumber);
            ObjParam[17] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[18] = new DbParameter("@CereliaAPIKey", DbParameter.DbType.NVarChar, 0, model.CereliaAPIKey);
            ObjParam[19] = new DbParameter("@RegistrationNumber", DbParameter.DbType.NVarChar, 0, model.RegistrationNumber);
            ObjParam[20] = new DbParameter("@ManageSlogan", DbParameter.DbType.NVarChar, 0, model.ManageSlogan);
            ObjParam[21] = new DbParameter("@ManageEmail", DbParameter.DbType.NVarChar, 0, model.ManageEmail);
            ObjParam[22] = new DbParameter("@ManageFaxNumber", DbParameter.DbType.NVarChar, 0, model.ManageFaxNumber);
            ObjParam[23] = new DbParameter("@OwnerEmail", DbParameter.DbType.NVarChar, 0, model.OwnerEmail);
            ObjParam[24] = new DbParameter("@ApplicationRules", DbParameter.DbType.NVarChar, 0, model.ApplicationRules);
            ObjParam[25] = new DbParameter("@VatNo", DbParameter.DbType.NVarChar, 0, model.VatNo);
            ObjParam[26] = new DbParameter("@IsAutoGeneratedPolicyNo", DbParameter.DbType.Bit, 0, model.IsAutoGeneratedPolicyNo);
            ObjParam[27] = new DbParameter("@Currentparlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Currentparlourid);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam);

        }

        public static string GetNewReferenceNumber()
        {
            return DbConnection.GetScalarValue(CommandType.StoredProcedure, "GetNewReferenceNumber").ToString();
        }

        public static DataTable SaveApplicationdt(ApplicationSettingsModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveApplicationSetting_New_";//"SaveApplicationSetting";
            DbParameter[] ObjParam = new DbParameter[30];
            ObjParam[0] = new DbParameter("@pkiApplicationID", DbParameter.DbType.Int, 0, model.pkiApplicationID);
            ObjParam[1] = new DbParameter("@ApplicationName", DbParameter.DbType.NVarChar, 0, model.ApplicationName);
            ObjParam[2] = new DbParameter("@ApplicationLogoPath", DbParameter.DbType.NVarChar, 0, model.ApplicationLogoPath);
            ObjParam[3] = new DbParameter("@OwnerFirstName", DbParameter.DbType.NVarChar, 0, model.OwnerFirstName);
            ObjParam[4] = new DbParameter("@OwnerSurname", DbParameter.DbType.NVarChar, 0, model.OwnerSurname);
            ObjParam[5] = new DbParameter("@OwnerTelNumber", DbParameter.DbType.NVarChar, 0, model.OwnerTelNumber);
            ObjParam[6] = new DbParameter("@OwnerCellNumber", DbParameter.DbType.NVarChar, 0, model.OwnerCellNumber);
            ObjParam[7] = new DbParameter("@ManagerFirstName", DbParameter.DbType.NVarChar, 0, model.ManagerFirstName);
            ObjParam[8] = new DbParameter("@ManageSurname", DbParameter.DbType.NVarChar, 0, model.ManageSurname);
            ObjParam[9] = new DbParameter("@ManageTelNumber", DbParameter.DbType.NVarChar, 0, model.ManageTelNumber);
            ObjParam[10] = new DbParameter("@ManageCellNumber", DbParameter.DbType.NVarChar, 0, model.ManageCellNumber);
            ObjParam[11] = new DbParameter("@BusinessAddressLine1", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine1);
            ObjParam[12] = new DbParameter("@BusinessAddressLine2", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine2);
            ObjParam[13] = new DbParameter("@BusinessAddressLine3", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine3);
            ObjParam[14] = new DbParameter("@BusinessAddressLine4", DbParameter.DbType.NVarChar, 0, model.BusinessAddressLine4);
            ObjParam[15] = new DbParameter("@BusinessPostalCode", DbParameter.DbType.NVarChar, 0, model.BusinessPostalCode);
            ObjParam[16] = new DbParameter("@FSBNumber", DbParameter.DbType.NVarChar, 0, model.FSBNumber);
            ObjParam[17] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[18] = new DbParameter("@CereliaAPIKey", DbParameter.DbType.NVarChar, 0, model.CereliaAPIKey);
            ObjParam[19] = new DbParameter("@RegistrationNumber", DbParameter.DbType.NVarChar, 0, model.RegistrationNumber);
            ObjParam[20] = new DbParameter("@ManageSlogan", DbParameter.DbType.NVarChar, 0, model.ManageSlogan);
            ObjParam[21] = new DbParameter("@ManageEmail", DbParameter.DbType.NVarChar, 0, model.ManageEmail);
            ObjParam[22] = new DbParameter("@ManageFaxNumber", DbParameter.DbType.NVarChar, 0, model.ManageFaxNumber);
            ObjParam[23] = new DbParameter("@OwnerEmail", DbParameter.DbType.NVarChar, 0, model.OwnerEmail);
            ObjParam[24] = new DbParameter("@ApplicationRules", DbParameter.DbType.NVarChar, 0, model.ApplicationRules);
            ObjParam[25] = new DbParameter("@VatNo", DbParameter.DbType.NVarChar, 0, model.VatNo);
            ObjParam[26] = new DbParameter("@IsAutoGeneratedPolicyNo", DbParameter.DbType.Bit, 0, model.IsAutoGeneratedPolicyNo);
            ObjParam[27] = new DbParameter("@Currentparlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Currentparlourid);
            ObjParam[28] = new DbParameter("@EmailForClaimNotification", DbParameter.DbType.NVarChar, 0, model.EmailForClaimNotification);
            ObjParam[29] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, model.UserName);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);

        }

        public static DataTable GetPlaceholders()
        {
            DbParameter[] ObjParam = new DbParameter[0];
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetSMSPlaceholders", ObjParam);
        }

        public static SqlDataReader SaveAdditionalApplication(AdditionalApplicationSettingsModel model)
        {
            //call vaslifsation 
            ValidateServiceKey(model);


            string query = "AddUpdateAdditionalApplicationSettings_New";
            DbParameter[] ObjParam = new DbParameter[24];
            ObjParam[0] = new DbParameter("@pkiParlourid", DbParameter.DbType.UniqueIdentifier, 0, model.pkiParlourid);
            ObjParam[1] = new DbParameter("@spUPuser", DbParameter.DbType.NVarChar, 0, model.spUPuser);
            ObjParam[2] = new DbParameter("@spUPpass", DbParameter.DbType.NVarChar, 0, model.spUPpass);
            ObjParam[3] = new DbParameter("@spUPpinpad", DbParameter.DbType.NVarChar, 0, model.spUPpinpad);
            ObjParam[4] = new DbParameter("@spValuser", DbParameter.DbType.NVarChar, 0, model.spValuser);
            ObjParam[5] = new DbParameter("@spValpass", DbParameter.DbType.NVarChar, 0, model.spValpass);
            ObjParam[6] = new DbParameter("@spValpinpad", DbParameter.DbType.NVarChar, 0, model.spValpinpad);
            ObjParam[7] = new DbParameter("@spCCuser", DbParameter.DbType.NVarChar, 0, model.spCCuser);
            ObjParam[8] = new DbParameter("@spCCpass", DbParameter.DbType.NVarChar, 0, model.spCCpass);
            ObjParam[9] = new DbParameter("@spCCpinpad", DbParameter.DbType.NVarChar, 0, model.spCCpinpad);
            ObjParam[10] = new DbParameter("@GenerateMember", DbParameter.DbType.NVarChar, 0, model.GenerateMember);

            ObjParam[11] = new DbParameter("@spNetcashAccountNumber", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spNetcashAccountNumber) ? (object)DBNull.Value : (object)model.spNetcashAccountNumber);
            ObjParam[12] = new DbParameter("@spAccountServiceKey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spAccountServiceKey) ? (object)DBNull.Value : (object)model.spAccountServiceKey);
            ObjParam[13] = new DbParameter("@spDebitOrderServicekey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spDebitOrderServicekey) ? (object)DBNull.Value : (object)model.spDebitOrderServicekey);
            ObjParam[14] = new DbParameter("@spPaynowServiveKey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spPaynowServiveKey) ? (object)DBNull.Value : (object)model.spPaynowServiveKey);
            ObjParam[15] = new DbParameter("@spAccountServiceKeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spAccountServiceKeyMessage) ? (object)DBNull.Value : (object)model.spAccountServiceKeyMessage);
            ObjParam[16] = new DbParameter("@spDebitOrderServikeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spDebitOrderServikeyMessage) ? (object)DBNull.Value : (object)model.spDebitOrderServikeyMessage);
            ObjParam[17] = new DbParameter("@spPaynowServiveKeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spPaynowServiveKeyMessage) ? (object)DBNull.Value : (object)model.spPaynowServiveKeyMessage);
            ObjParam[18] = new DbParameter("@spNetcashAccountNumberMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spNetcashAccountNumberMessage) ? (object)DBNull.Value : (object)model.spNetcashAccountNumberMessage);
            ObjParam[19] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[20] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[21] = new DbParameter("@spSoftwareVendorKey", DbParameter.DbType.NVarChar, 0, model.spSoftwareVendorKey);
            ObjParam[22] = new DbParameter("@spSoftwareVendorKeyMessage", DbParameter.DbType.NVarChar, 0, model.spSoftwareVendorKeyMessage);
            ObjParam[23] = new DbParameter("@IsAutoGeneratedEasyPayNo", DbParameter.DbType.Bit, 0, model.GenerateEasyPay);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam);

        }
        public static DataTable SaveAdditionalApplicationdt(AdditionalApplicationSettingsModel model)
        {
            //call vaslifsation 
            ValidateServiceKey(model);

            string query = "AddUpdateAdditionalApplicationSettings_New_";
            DbParameter[] ObjParam = new DbParameter[25];
            ObjParam[0] = new DbParameter("@pkiParlourid", DbParameter.DbType.UniqueIdentifier, 0, model.pkiParlourid);
            ObjParam[1] = new DbParameter("@spUPuser", DbParameter.DbType.NVarChar, 0, model.spUPuser);
            ObjParam[2] = new DbParameter("@spUPpass", DbParameter.DbType.NVarChar, 0, model.spUPpass);
            ObjParam[3] = new DbParameter("@spUPpinpad", DbParameter.DbType.NVarChar, 0, model.spUPpinpad);
            ObjParam[4] = new DbParameter("@spValuser", DbParameter.DbType.NVarChar, 0, model.spValuser);
            ObjParam[5] = new DbParameter("@spValpass", DbParameter.DbType.NVarChar, 0, model.spValpass);
            ObjParam[6] = new DbParameter("@spValpinpad", DbParameter.DbType.NVarChar, 0, model.spValpinpad);
            ObjParam[7] = new DbParameter("@spCCuser", DbParameter.DbType.NVarChar, 0, model.spCCuser);
            ObjParam[8] = new DbParameter("@spCCpass", DbParameter.DbType.NVarChar, 0, model.spCCpass);
            ObjParam[9] = new DbParameter("@spCCpinpad", DbParameter.DbType.NVarChar, 0, model.spCCpinpad);
            ObjParam[10] = new DbParameter("@GenerateMember", DbParameter.DbType.NVarChar, 0, model.GenerateMember);

            ObjParam[11] = new DbParameter("@spNetcashAccountNumber", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spNetcashAccountNumber) ? (object)DBNull.Value : (object)model.spNetcashAccountNumber);
            ObjParam[12] = new DbParameter("@spAccountServiceKey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spAccountServiceKey) ? (object)DBNull.Value : (object)model.spAccountServiceKey);
            ObjParam[13] = new DbParameter("@spDebitOrderServicekey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spDebitOrderServicekey) ? (object)DBNull.Value : (object)model.spDebitOrderServicekey);
            ObjParam[14] = new DbParameter("@spPaynowServiveKey", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spPaynowServiveKey) ? (object)DBNull.Value : (object)model.spPaynowServiveKey);
            ObjParam[15] = new DbParameter("@spAccountServiceKeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spAccountServiceKeyMessage) ? (object)DBNull.Value : (object)model.spAccountServiceKeyMessage);
            ObjParam[16] = new DbParameter("@spDebitOrderServikeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spDebitOrderServikeyMessage) ? (object)DBNull.Value : (object)model.spDebitOrderServikeyMessage);
            ObjParam[17] = new DbParameter("@spPaynowServiveKeyMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spPaynowServiveKeyMessage) ? (object)DBNull.Value : (object)model.spPaynowServiveKeyMessage);
            ObjParam[18] = new DbParameter("@spNetcashAccountNumberMessage", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.spNetcashAccountNumberMessage) ? (object)DBNull.Value : (object)model.spNetcashAccountNumberMessage);
            ObjParam[19] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[20] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[21] = new DbParameter("@spSoftwareVendorKey", DbParameter.DbType.NVarChar, 0, model.spSoftwareVendorKey);
            ObjParam[22] = new DbParameter("@spSoftwareVendorKeyMessage", DbParameter.DbType.NVarChar, 0, model.spSoftwareVendorKeyMessage);
            ObjParam[23] = new DbParameter("@IsAutoGeneratedEasyPayNo", DbParameter.DbType.Bit, 0, model.GenerateEasyPay);
            ObjParam[24] = new DbParameter("@Currency", DbParameter.DbType.NVarChar, 0, String.IsNullOrEmpty(model.Currency) ? (object)DBNull.Value : (object)model.Currency);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam);

        }
        public static SqlDataReader SaveAgentInfo(AgentInfoSetupModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
            ObjParam[1] = new DbParameter("@surname", DbParameter.DbType.NVarChar, 0, model.Surname);
            ObjParam[2] = new DbParameter("@fullname", DbParameter.DbType.NVarChar, 0, model.Fullname);
            ObjParam[3] = new DbParameter("@percentage", DbParameter.DbType.Decimal, 0, model.percentage);
            ObjParam[4] = new DbParameter("@contactnumber", DbParameter.DbType.NVarChar, 0, model.ContactNumber);
            ObjParam[5] = new DbParameter("@code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[6] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, model.Email);
            ObjParam[7] = new DbParameter("@address1", DbParameter.DbType.NVarChar, 0, model.Address1);
            ObjParam[8] = new DbParameter("@address2", DbParameter.DbType.NVarChar, 0, model.Address2);
            ObjParam[9] = new DbParameter("@address3", DbParameter.DbType.NVarChar, 0, model.Address3);
            ObjParam[10] = new DbParameter("@address4", DbParameter.DbType.NVarChar, 0, model.Address4);
            ObjParam[11] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[12] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[13] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SaveAgentInfo", ObjParam);
        }

        public static DataTable SaveAgentInfodt(AgentInfoSetupModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
            ObjParam[1] = new DbParameter("@surname", DbParameter.DbType.NVarChar, 0, model.Surname);
            ObjParam[2] = new DbParameter("@fullname", DbParameter.DbType.NVarChar, 0, model.Fullname);
            ObjParam[3] = new DbParameter("@percentage", DbParameter.DbType.Decimal, 0, model.percentage);
            ObjParam[4] = new DbParameter("@contactnumber", DbParameter.DbType.NVarChar, 0, model.ContactNumber);
            ObjParam[5] = new DbParameter("@code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[6] = new DbParameter("@email", DbParameter.DbType.NVarChar, 0, model.Email);
            ObjParam[7] = new DbParameter("@address1", DbParameter.DbType.NVarChar, 0, model.Address1);
            ObjParam[8] = new DbParameter("@address2", DbParameter.DbType.NVarChar, 0, model.Address2);
            ObjParam[9] = new DbParameter("@address3", DbParameter.DbType.NVarChar, 0, model.Address3);
            ObjParam[10] = new DbParameter("@address4", DbParameter.DbType.NVarChar, 0, model.Address4);
            ObjParam[11] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[12] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[13] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SaveAgentInfo", ObjParam);
        }

        public static SqlDataReader SaveBankingDetail(BankingDetailSending Model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@AccountHolder", DbParameter.DbType.NVarChar, 0, Model.AccountHolder);
            ObjParam[1] = new DbParameter("@Bankname", DbParameter.DbType.NVarChar, 0, Model.Bankname);
            ObjParam[2] = new DbParameter("@AccountNumber", DbParameter.DbType.NVarChar, 0, Model.AccountNumber);
            ObjParam[3] = new DbParameter("@Accounttype", DbParameter.DbType.NVarChar, 0, Model.Accounttype);
            ObjParam[4] = new DbParameter("@Branch", DbParameter.DbType.NVarChar, 0, Model.Branch);
            ObjParam[5] = new DbParameter("@Branchcode", DbParameter.DbType.NVarChar, 0, Model.Branchcode);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, Model.parlourid);
            ObjParam[7] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, Model.LastModified);
            ObjParam[8] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, Model.ModifiedUser);


            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SaveBankingDetail", ObjParam);
        }

        public static DataTable SaveBankingDetaildt(BankingDetailSending Model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            DbParameter[] ObjParam = new DbParameter[15];
            ObjParam[0] = new DbParameter("@AccountHolder", DbParameter.DbType.NVarChar, 0, Model.AccountHolder);
            ObjParam[1] = new DbParameter("@Bankname", DbParameter.DbType.NVarChar, 0, Model.Bankname);
            ObjParam[2] = new DbParameter("@AccountNumber", DbParameter.DbType.NVarChar, 0, Model.AccountNumber);
            ObjParam[3] = new DbParameter("@Accounttype", DbParameter.DbType.NVarChar, 0, Model.Accounttype);
            ObjParam[4] = new DbParameter("@Branch", DbParameter.DbType.NVarChar, 0, Model.Branch);
            ObjParam[5] = new DbParameter("@Branchcode", DbParameter.DbType.NVarChar, 0, Model.Branchcode);
            ObjParam[6] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, Model.parlourid);
            ObjParam[7] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, Model.LastModified);
            ObjParam[8] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, Model.ModifiedUser);

            ObjParam[9] = new DbParameter("@Funeral_AccountHolder", DbParameter.DbType.NVarChar, 0, Model.Funeral_AccountHolder);
            ObjParam[10] = new DbParameter("@Funeral_Bankname", DbParameter.DbType.NVarChar, 0, Model.Funeral_Bankname);
            ObjParam[11] = new DbParameter("@Funeral_AccountNumber", DbParameter.DbType.NVarChar, 0, Model.Funeral_AccountNumber);
            ObjParam[12] = new DbParameter("@Funeral_Accounttype", DbParameter.DbType.NVarChar, 0, Model.Funeral_Accounttype);
            ObjParam[13] = new DbParameter("@Funeral_Branch", DbParameter.DbType.NVarChar, 0, Model.Funeral_Branch);
            ObjParam[14] = new DbParameter("@Funeral_Branchcode", DbParameter.DbType.NVarChar, 0, Model.Funeral_Branchcode);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SaveBankingDetail_New", ObjParam);
        }
        public static SqlDataReader GetUserAccessByID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetUserAccessByID", ObjParam);
        }
        public static DataTable GetUserAccessByIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetUserAccessByID", ObjParam);
        }

        public static SqlDataReader GetApplictionByParlourID(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ApplicationSelectByParlourId", ObjParam);
        }

        public static DataTable GetApplictionByParlourIDdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ApplicationSelectByParlourId", ObjParam);
        }
        public static SqlDataReader GetApplictionByID(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ApplicationSelectById", ObjParam);
        }

        public static DataTable GetApplictionByIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ApplicationSelectById", ObjParam);
        }


        public static SqlDataReader GetBankingByID(Guid ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "BankingSelectById", ObjParam);
        }

        public static DataTable GetBankingByIDdt(Guid ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "BankingSelectById", ObjParam);
        }

        public static SqlDataReader GetAgentByID(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "AgentSelectById", ObjParam);
        }

        public static DataTable GetAgentByIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "AgentSelectById", ObjParam);
        }
        public static SqlDataReader GetAllCompanys(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllCompanys", ObjParam);
        }

        public static DataTable GetAllCompanysdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllCompanys", ObjParam);
        }

        public static SqlDataReader GetAllAgentInfo(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllAgentInfo", ObjParam);
        }

        public static DataTable GetAllAgentInfodt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllAgentInfo", ObjParam);
        }

        public static int DeleteCompany(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiApplicationID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteCompany", ObjParam));
        }
        public static int DeleteAgent(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteAgent", ObjParam));
        }
        public static SqlDataReader EditsmsGroupbyID(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditsmsGroupbyID", ObjParam);
        }
        public static DataTable EditsmsGroupbyIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditsmsGroupbyID", ObjParam);
        }
        public static SqlDataReader GetsmsGroupbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetsmsGroupbyID", ObjParam);
        }

        public static DataTable GetsmsGroupbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetsmsGroupbyID", ObjParam);
        }

        public static int SaveSmsGroupDetails(smsSendingGroupModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveSmsGroupDetails";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
            ObjParam[1] = new DbParameter("@fkiCompanyID", DbParameter.DbType.Int, 0, model.fkiCompanyID);
            ObjParam[2] = new DbParameter("@fkismstempletID", DbParameter.DbType.Int, 0, model.fkismstempletID);
            ObjParam[3] = new DbParameter("@smstempletName", DbParameter.DbType.NVarChar, 0, model.smstempletName);
            ObjParam[4] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[5] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static SqlDataReader GetSecureGrouList()
        {
            DbParameter[] ObjParam = new DbParameter[0];
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetSecureGrouList", ObjParam);
        }
        public static DataTable GetSecureGrouListdt()
        {
            DbParameter[] ObjParam = new DbParameter[0];
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetSecureGrouList", ObjParam);
        }
        public static SqlDataReader GetUserByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetUserByID", ObjParam);
        }
        public static DataTable GetUserByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetUserByID", ObjParam);
        }

        public static SqlDataReader GetUserByEmailID(string ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetUserByEmailID", ObjParam);
        }
        public static DataTable GetUserByEmailIDdt(string ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetUserByEmailID", ObjParam);
        }

        public static int SaveUserDetails(SecureUsersModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveSecureUserDetails";
            DbParameter[] ObjParam = new DbParameter[23];
            ObjParam[0] = new DbParameter("@PkiUserID", DbParameter.DbType.Int, 0, model.PkiUserID);
            ObjParam[1] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, model.UserName);
            ObjParam[2] = new DbParameter("@Password", DbParameter.DbType.NVarChar, 0, model.Password);
            ObjParam[3] = new DbParameter("@EmployeeSurname", DbParameter.DbType.NVarChar, 0, model.EmployeeSurname);
            ObjParam[4] = new DbParameter("@EmployeeFullname", DbParameter.DbType.NVarChar, 0, model.EmployeeFullname);
            ObjParam[5] = new DbParameter("@EmployeeIDNumber", DbParameter.DbType.NVarChar, 0, model.EmployeeIDNumber);
            ObjParam[6] = new DbParameter("@EmployeeContactNumber", DbParameter.DbType.NVarChar, 0, model.EmployeeContactNumber);
            ObjParam[7] = new DbParameter("@EmployeeAddress1", DbParameter.DbType.NVarChar, 0, model.EmployeeAddress1);
            ObjParam[8] = new DbParameter("@EmployeeAddress2", DbParameter.DbType.NVarChar, 0, model.EmployeeAddress2);
            ObjParam[9] = new DbParameter("@EmployeeAddress3", DbParameter.DbType.NVarChar, 0, model.EmployeeAddress3);
            ObjParam[10] = new DbParameter("@EmployeeAddress4", DbParameter.DbType.NVarChar, 0, model.EmployeeAddress4);
            ObjParam[11] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[12] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[13] = new DbParameter("@Email", DbParameter.DbType.NVarChar, 0, model.Email);
            ObjParam[14] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[15] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[16] = new DbParameter("@AgentName", DbParameter.DbType.NVarChar, 0, model.AgentName);
            ObjParam[17] = new DbParameter("@AgentSurname", DbParameter.DbType.NVarChar, 0, model.AgentSurname);

            ObjParam[18] = new DbParameter("@BranchId", DbParameter.DbType.UniqueIdentifier, 0, model.BranchId);
            ObjParam[19] = new DbParameter("@CustomId1", DbParameter.DbType.NVarChar, 0, model.CustomId1);
            ObjParam[20] = new DbParameter("@CustomId2", DbParameter.DbType.NVarChar, 0, model.CustomId2);
            ObjParam[21] = new DbParameter("@CustomId3", DbParameter.DbType.NVarChar, 0, model.CustomId3);
            ObjParam[22] = new DbParameter("@Active", DbParameter.DbType.NVarChar, 0, model.Active);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int SaveUserGroupDetails(SecureUserGroupsModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveSecureUserGroupDetails";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pkiSecureUserGroups", DbParameter.DbType.Int, 0, model.pkiSecureUserGroups);
            ObjParam[1] = new DbParameter("@fkiSecureUserID", DbParameter.DbType.Int, 0, model.fkiSecureUserID);
            ObjParam[2] = new DbParameter("@fkiSecureGroupID", DbParameter.DbType.NVarChar, 0, model.fkiSecureGroupID);
            ObjParam[3] = new DbParameter("@sSecureUserGroupDesc", DbParameter.DbType.NVarChar, 0, model.sSecureUserGroupDesc);
            ObjParam[4] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[5] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static SqlDataReader GetAllUsers(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UserSelectAllByPage", ObjParam);
        }
        public static DataTable GetAllUsersdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UserSelectAllByPage", ObjParam);
        }
        public static int DeleteUser(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@PkiUserID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteUsers", ObjParam));
        }
        public static SqlDataReader EditUserbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "UserSelectbyID", ObjParam);
        }
        public static DataTable EditUserbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "UserSelectbyID", ObjParam);
        }
        public static SqlDataReader EditSecurUserbyID(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "ScureUserSelectbyID", ObjParam);
        }
        public static DataTable EditSecurUserbyIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "ScureUserSelectbyID", ObjParam);
        }
        public static SqlDataReader GetBranchByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetBranchByID", ObjParam);
        }
        public static DataTable GetBranchByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetBranchByID", ObjParam);
        }
        public static Guid SaveBranchDetails(BranchModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveBranchDetails";
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@Brancheid", DbParameter.DbType.UniqueIdentifier, 0, model.Brancheid);
            ObjParam[1] = new DbParameter("@BranchName", DbParameter.DbType.NVarChar, 0, model.BranchName);
            ObjParam[2] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.Parlourid);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[5] = new DbParameter("@Address1", DbParameter.DbType.NVarChar, 0, model.Address1);
            ObjParam[6] = new DbParameter("@Address2", DbParameter.DbType.NVarChar, 0, model.Address2);
            ObjParam[7] = new DbParameter("@Address3", DbParameter.DbType.NVarChar, 0, model.Address3);
            ObjParam[8] = new DbParameter("@Address4", DbParameter.DbType.NVarChar, 0, model.Address4);
            ObjParam[9] = new DbParameter("@Code", DbParameter.DbType.NVarChar, 0, model.Code);
            ObjParam[10] = new DbParameter("@TelNumber", DbParameter.DbType.NVarChar, 0, model.TelNumber);
            ObjParam[11] = new DbParameter("@CellNumber", DbParameter.DbType.NVarChar, 0, model.CellNumber);
            ObjParam[12] = new DbParameter("@BranchCode", DbParameter.DbType.NVarChar, 0, model.BranchCode);
            ObjParam[13] = new DbParameter("@Region", DbParameter.DbType.NVarChar, 0, model.Region);

            return new Guid(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam).ToString());

        }
        public static SqlDataReader GetAllBranches(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllBranches", ObjParam);
        }
        public static DataTable GetAllBranchesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllBranches", ObjParam);
        }
        public static SqlDataReader EditBranchbyID(Guid ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditBranchbyID", ObjParam);
        }
        public static DataTable EditBranchbyIDdt(Guid ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditBranchbyID", ObjParam);
        }
        public static int DeleteBranch(Guid id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@BranchID", DbParameter.DbType.UniqueIdentifier, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteBranch", ObjParam));
        }
        public static SqlDataReader GetAllSocietyes(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllSocietyes", ObjParam);
        }

        //public static DataTable GetAllSocietye_PaymentList(Guid ParlourId)
        //{
        //    DbParameter[] ObjParam = new DbParameter[1];
        //    ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
        //    return DbConnection.GetDataTable(CommandType.StoredProcedure, "SchemeSummary", ObjParam);
        //}

        //Test
        public static DataTable GetAllSocietye_PaymentList(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SchemeSummary_Test", ObjParam);
        }
        //Test End

        //public static DataSet GetAllSocietye_PaymentListdt(Guid ParlourId)
        //{
        //    DbParameter[] ObjParam = new DbParameter[1];
        //    ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
        //    return DbConnection.GetDataSet(CommandType.StoredProcedure, "SchemeSummary", ObjParam);
        //}
        public static DataTable GetGroupPayment_ByParlourId(Guid ParlourId, string ReferenceNumber)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@ReferenceNumber", DbParameter.DbType.NVarChar, 0, ReferenceNumber);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SchemeSummaryByParlourId", ObjParam);
        }

        public static DataTable GetGroupPayment_ByScheme(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetGroupPaymentByScheme", ObjParam);
        }

        public static DataTable GetAllSocietyesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllSocietyes", ObjParam);
        }
        public static SqlDataReader GetAllSocietye(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "getAllSociety", ObjParam);
        }
        public static DataTable GetAllSocietyedt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "getAllSociety", ObjParam);
        }

        public static SqlDataReader EditSocietybyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditSocietybyID", ObjParam);
        }
        public static DataTable EditSocietybyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditSocietybyID", ObjParam);
        }
        public static SqlDataReader GetSocietyByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetSocietyByID", ObjParam);
        }
        public static DataTable GetSocietyByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetSocietyByID", ObjParam);
        }
        public static int SaveSocietyDetails(SocietyModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveSocietyDetails";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@pkiSocietyID", DbParameter.DbType.Int, 0, model.pkiSocietyID);
            ObjParam[1] = new DbParameter("@SocietyName", DbParameter.DbType.NVarChar, 0, model.SocietyName);
            ObjParam[2] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int DeleteSociety(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiSocietyID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteSociety", ObjParam));
        }
        public static int DeleteFuneralService(int Id)
        {
            DbParameter[] ObjParams = new DbParameter[1];
            ObjParams[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, Id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteFuneral", ObjParams));
        }

        public static int DeleteTombstoneService(int Id)
        {
            DbParameter[] ObjParams = new DbParameter[1];
            ObjParams[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, Id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteTombstoneService", ObjParams));

        }
        public static SqlDataReader GetAllVendores(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllVendores", ObjParam);
        }
        public static DataTable GetAllVendoresdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllVendores", ObjParam);
        }
        public static SqlDataReader EditVendorbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditVendorbyID", ObjParam);
        }
        public static DataTable EditVendorbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditVendorbyID", ObjParam);
        }
        public static SqlDataReader GetVendorByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetVendorByID", ObjParam);
        }
        public static DataTable GetVendorByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetVendorByID", ObjParam);
        }
        public static int SaveVendorDetails(VendorModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveVendorDetails";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@pkiVendorID", DbParameter.DbType.Int, 0, model.pkiVendorID);
            ObjParam[1] = new DbParameter("@VendorName", DbParameter.DbType.NVarChar, 0, model.VendorName);
            ObjParam[2] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int DeleteVendor(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiVendorID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteVendor", ObjParam));
        }
        public static SqlDataReader GetAllExpenseses(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllExpenseses", ObjParam);
        }
        public static DataTable GetAllExpensesesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllExpenseses", ObjParam);
        }
        public static SqlDataReader EditExpensesbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditExpensesbyID", ObjParam);
        }
        public static DataTable EditExpensesbyIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditExpensesbyID", ObjParam);
        }
        public static SqlDataReader GetExpensesByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetExpensesByID", ObjParam);
        }
        public static DataTable GetExpensesByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetExpensesByID", ObjParam);
        }
        public static int SaveExpensesDetails(ExpensesModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveExpensesDetails";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@pkiExpenseCategoryID", DbParameter.DbType.Int, 0, model.pkiExpenseCategoryID);
            ObjParam[1] = new DbParameter("@Category", DbParameter.DbType.NVarChar, 0, model.Category);
            ObjParam[2] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int DeleteExpenses(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiExpenseCategoryID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteExpenses", ObjParam));
        }

        public static SqlDataReader GetAddonProductByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAddonProductByID", ObjParam);
        }
        public static DataTable GetAddonProductByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAddonProductByID", ObjParam);
        }
        public static Guid SaveAddonProductDetails(AddonProductsModal model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveAddonProductDetails_new";// "SaveAddonProductDetails";
            DbParameter[] ObjParam = new DbParameter[13];
            ObjParam[0] = new DbParameter("@pkiProductID", DbParameter.DbType.UniqueIdentifier, 0, model.pkiProductID);
            ObjParam[1] = new DbParameter("@ProductName", DbParameter.DbType.NVarChar, 0, model.ProductName);
            ObjParam[2] = new DbParameter("@Parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.SchemeID);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[5] = new DbParameter("@DateCreated", DbParameter.DbType.DateTime, 0, model.DateCreated);
            ObjParam[6] = new DbParameter("@ProductDesc", DbParameter.DbType.NVarChar, 0, model.ProductDesc);
            ObjParam[7] = new DbParameter("@ProductCost", DbParameter.DbType.Decimal, 0, model.ProductCost);
            ObjParam[8] = new DbParameter("@ProductCover", DbParameter.DbType.Decimal, 0, model.ProductCover);
            ObjParam[9] = new DbParameter("@IsProductOngoing", DbParameter.DbType.Int, 0, model.IsProductOngoing);
            ObjParam[10] = new DbParameter("@IsProductLaybye", DbParameter.DbType.Int, 0, model.IsProductLaybye);
            ObjParam[11] = new DbParameter("@UserId", DbParameter.DbType.NVarChar, 0, model.UserID);
            ObjParam[12] = new DbParameter("@UnderwriterPremium", DbParameter.DbType.Decimal, 0, model.UnderwriterPremium);
            return new Guid(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam).ToString());
        }
        public static SqlDataReader GetAllAddonProductes(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllAddonProductes", ObjParam);
        }
        public static DataTable GetAllAddonProductesdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllAddonProductes", ObjParam);
        }
        public static SqlDataReader EditAddonProductbyID(Guid ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditAddonProductbyID", ObjParam);
        }
        public static DataTable EditAddonProductbyIDdt(Guid ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.UniqueIdentifier, 0, ID);
            //ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditAddonProductbyID", ObjParam);
        }
        public static int DeleteAddonProduct(Guid id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiProductID", DbParameter.DbType.UniqueIdentifier, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeleteAddonProduct", ObjParam));
        }


        public static SqlDataReader GetAllPlans(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetAllPlans", ObjParam);
        }
        public static DataTable GetAllPlansdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            DbParameter[] ObjParam = new DbParameter[6];

            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllPlans", ObjParam);
        }
        public static SqlDataReader EditPlanbyID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "EditPlanbyID", ObjParam);
        }
        public static DataTable EditPlanbyIDdt(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            //ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditPlanbyID_NEW_", ObjParam);
        }
        public static DataTable EditPlanCreatorbyID(int PlanId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, PlanId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "EditPlanCreatorbyID_New", ObjParam);
        }
        public static SqlDataReader GetPlanByID(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetPlanByID", ObjParam);
        }
        public static DataTable GetPlanByIDdt(string ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.NVarChar, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanByID", ObjParam);
        }
        public static int SavePlanDetails(PlanModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SavePlanDetails";
            DbParameter[] ObjParam = new DbParameter[33];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, model.pkiPlanID);
            ObjParam[1] = new DbParameter("@PlanName", DbParameter.DbType.NVarChar, 0, model.PlanName);
            ObjParam[2] = new DbParameter("@PlanSubscription", DbParameter.DbType.Decimal, 0, model.PlanSubscription);
            ObjParam[3] = new DbParameter("@JoiningFee", DbParameter.DbType.Decimal, 0, model.JoiningFee);
            ObjParam[4] = new DbParameter("@PlanDesc", DbParameter.DbType.NVarChar, 0, model.PlanDesc);
            ObjParam[5] = new DbParameter("@PlanUnderwriter", DbParameter.DbType.NVarChar, 0, model.PlanUnderwriter);
            //ObjParam[6] = new DbParameter("@MainMember", DbParameter.DbType.Int, 0, model.MainMember);
            ObjParam[6] = new DbParameter("@Cover", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[7] = new DbParameter("@Spouse", DbParameter.DbType.Int, 0, model.Spouse);
            ObjParam[8] = new DbParameter("@Children", DbParameter.DbType.Int, 0, model.Children);
            ObjParam[9] = new DbParameter("@Cover0to5year", DbParameter.DbType.Decimal, 0, model.Cover0to5year);
            ObjParam[10] = new DbParameter("@Cover6to13year", DbParameter.DbType.Decimal, 0, model.Cover6to13year);
            ObjParam[11] = new DbParameter("@Cover14to21year", DbParameter.DbType.Decimal, 0, model.Cover14to21year);
            //ObjParam[12] = new DbParameter("@Cover22to40year", DbParameter.DbType.Decimal, 0, model.Cover22to40year);
            //ObjParam[13] = new DbParameter("@Cover41to59year", DbParameter.DbType.Decimal, 0, model.Cover41to59year);
            //ObjParam[14] = new DbParameter("@Cover60to65year", DbParameter.DbType.Decimal, 0, model.Cover60to65year);
            //ObjParam[15] = new DbParameter("@Cover66to75year", DbParameter.DbType.Decimal, 0, model.Cover66to75year);
            ObjParam[12] = new DbParameter("@Adults", DbParameter.DbType.Int, 0, model.Adults);
            ObjParam[13] = new DbParameter("@WaitingPeriod", DbParameter.DbType.Int, 0, model.WaitingPeriod);
            ObjParam[14] = new DbParameter("@PolicyLaps", DbParameter.DbType.Int, 0, model.PolicyLaps);
            ObjParam[15] = new DbParameter("@UnderwriterSplit", DbParameter.DbType.Decimal, 0, model.UnderwriterSplit);
            ObjParam[16] = new DbParameter("@AgentSplit", DbParameter.DbType.Decimal, 0, model.AgentSplit);
            ObjParam[17] = new DbParameter("@OfficeSplit", DbParameter.DbType.Decimal, 0, model.OfficeSplit);
            ObjParam[18] = new DbParameter("@CompanySplit", DbParameter.DbType.Decimal, 0, model.CompanySplit);
            ObjParam[19] = new DbParameter("@HeadManagerSplit", DbParameter.DbType.Decimal, 0, model.HeadManagerSplit);
            ObjParam[20] = new DbParameter("@AdminSplit", DbParameter.DbType.Decimal, 0, model.AdminSplit);
            ObjParam[21] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[22] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[23] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[24] = new DbParameter("@AdultCover", DbParameter.DbType.Decimal, 0, model.AdultCover);
            ObjParam[25] = new DbParameter("@SpouseCover", DbParameter.DbType.Decimal, 0, model.SpouseCover);
            ObjParam[26] = new DbParameter("@ChildCover", DbParameter.DbType.Decimal, 0, model.ChildCover);
            ObjParam[27] = new DbParameter("@Cover22to40year", DbParameter.DbType.Decimal, 0, model.Cover22to40year);
            ObjParam[28] = new DbParameter("@Cover41to59year", DbParameter.DbType.Decimal, 0, model.Cover41to59year);
            ObjParam[29] = new DbParameter("@Cover60to65year", DbParameter.DbType.Decimal, 0, model.Cover60to65year);
            ObjParam[30] = new DbParameter("@Cover66to75year", DbParameter.DbType.Decimal, 0, model.Cover66to75year);
            ObjParam[31] = new DbParameter("@UnderwriterId", DbParameter.DbType.Int, 0, model.UnderwriterId);
            ObjParam[32] = new DbParameter("@CashPayout", DbParameter.DbType.Decimal, 0, model.CashPayout);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }

        public static int NewSavePlanDetails(PlanModel model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SaveNewPlanDetails_dt";
            DbParameter[] ObjParam = new DbParameter[30];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, model.pkiPlanID);
            ObjParam[1] = new DbParameter("@PlanName", DbParameter.DbType.NVarChar, 0, model.PlanName);
            ObjParam[2] = new DbParameter("@PlanSubscription", DbParameter.DbType.Decimal, 0, model.PlanSubscription);
            ObjParam[3] = new DbParameter("@JoiningFee", DbParameter.DbType.Decimal, 0, model.JoiningFee);
            ObjParam[4] = new DbParameter("@PlanDesc", DbParameter.DbType.NVarChar, 0, model.PlanDesc);
            ObjParam[5] = new DbParameter("@PlanUnderwriter", DbParameter.DbType.NVarChar, 0, model.PlanUnderwriter);
            ObjParam[6] = new DbParameter("@Cover", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[7] = new DbParameter("@UnderwriterSplit", DbParameter.DbType.Decimal, 0, model.UnderwriterSplit);
            ObjParam[8] = new DbParameter("@AgentSplit", DbParameter.DbType.Decimal, 0, model.AgentSplit);
            ObjParam[9] = new DbParameter("@OfficeSplit", DbParameter.DbType.Decimal, 0, model.OfficeSplit);
            ObjParam[10] = new DbParameter("@CompanySplit", DbParameter.DbType.Decimal, 0, model.CompanySplit);
            ObjParam[11] = new DbParameter("@HeadManagerSplit", DbParameter.DbType.Decimal, 0, model.HeadManagerSplit);
            ObjParam[12] = new DbParameter("@AdminSplit", DbParameter.DbType.Decimal, 0, model.AdminSplit);
            ObjParam[13] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[14] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            ObjParam[15] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[16] = new DbParameter("@AdultCover", DbParameter.DbType.Decimal, 0, model.AdultCover);
            ObjParam[17] = new DbParameter("@SpouseCover", DbParameter.DbType.Decimal, 0, model.SpouseCover);
            ObjParam[18] = new DbParameter("@ChildCover", DbParameter.DbType.Decimal, 0, model.ChildCover);
            ObjParam[19] = new DbParameter("@UnderwriterId", DbParameter.DbType.Int, 0, model.UnderwriterId);
            ObjParam[20] = new DbParameter("@CashPayout", DbParameter.DbType.Decimal, 0, model.CashPayout);
            ObjParam[21] = new DbParameter("@WaitingPeriod", DbParameter.DbType.Int, 0, model.WaitingPeriod);
            ObjParam[22] = new DbParameter("@PolicyLaps", DbParameter.DbType.Int, 0, model.PolicyLaps);
            ObjParam[23] = new DbParameter("@OtherPartiesCommision", DbParameter.DbType.Decimal, 0, model.OtherPartiesCommision);
            ObjParam[24] = new DbParameter("@LoyaltyProgramme", DbParameter.DbType.Decimal, 0, model.LoyaltyProgramme);
            ObjParam[25] = new DbParameter("@NumberOfDependents", DbParameter.DbType.Decimal, 0, model.NumberOfDependents);
            ObjParam[26] = new DbParameter("@AgeFrom", DbParameter.DbType.Int, 0, model.AgeFrom);
            ObjParam[27] = new DbParameter("@AgeTo", DbParameter.DbType.Int, 0, model.AgeTo);
            ObjParam[28] = new DbParameter("@UnderwriterCover", DbParameter.DbType.Decimal, 0, model.UnderwriterCover);
            ObjParam[29] = new DbParameter("@UnderwriterPremium", DbParameter.DbType.Decimal, 0, model.UnderwriterPremium);


            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int SavePlanCreatorDetails(PlanCreator model)
        {
            AdditionalMemberInfoModel model1 = new AdditionalMemberInfoModel();
            string query = "SavePlanCreatorDetails_New";//"SavePlanCreatorDetails";
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@PlanID", DbParameter.DbType.Int, 0, model.PlanID);
            ObjParam[1] = new DbParameter("@UserTypeId", DbParameter.DbType.Int, 0, model.UserTypeId);
            ObjParam[2] = new DbParameter("@AgeFrom", DbParameter.DbType.Int, 0, model.AgeFrom);
            ObjParam[3] = new DbParameter("@AgeTo", DbParameter.DbType.Int, 0, model.AgeTo);
            ObjParam[4] = new DbParameter("@CoverAmount", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[5] = new DbParameter("@PremiumAmount", DbParameter.DbType.Decimal, 0, model.Premium);
            ObjParam[6] = new DbParameter("@UnderwriterCover", DbParameter.DbType.Decimal, 0, model.UnderwriterCover);
            ObjParam[7] = new DbParameter("@UnderwriterPremium", DbParameter.DbType.Decimal, 0, model.UnderwriterPremium);
            ObjParam[8] = new DbParameter("@IsActive", DbParameter.DbType.Bit, 0, model.IsActive);
            ObjParam[9] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, model.CreatedBy);
            ObjParam[10] = new DbParameter("@CreatedDate", DbParameter.DbType.DateTime, 0, model.CreatedDate);
            ObjParam[11] = new DbParameter("@PlanCreatorId", DbParameter.DbType.Int, 0, model.PlanCreatorId);
            ObjParam[12] = new DbParameter("@OfficePremium", DbParameter.DbType.Decimal, 0, model.OfficePremium);
            ObjParam[13] = new DbParameter("@ReinsurancePremium", DbParameter.DbType.Decimal, 0, model.ReinsurancePremium);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static int DeletePlan(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeletePlan", ObjParam));
        }
        public static int DeletePlanAndPlanCreator(int id)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiPlanID", DbParameter.DbType.Int, 0, id);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, "DeletePlanAndPlanCreator", ObjParam));
        }

        public static SqlDataReader GetAllMemberCellphon(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "SendsmsAllMembers", ObjParam);
        }
        public static DataTable GetAllMemberCellphondt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "SendsmsAllMembers", ObjParam);
        }
        public static SqlDataReader GetTemplateList(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetTemplateList", ObjParam);
        }
        public static DataTable GetTemplateListdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];

            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetTemplateList", ObjParam);
        }
        public static SqlDataReader GetEmailTemplateByID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetEmailTemplateByID", ObjParam);
        }
        public static DataTable GetEmailTemplateByIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetEmailTemplateByID", ObjParam);
        }
        public static int UpdatesmsTemplate(smsTempletModel model)
        {
            smsTempletModel model1 = new smsTempletModel();
            string query = "UpdatesmsTemplate";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, model.ID);
            ObjParam[1] = new DbParameter("@Name", DbParameter.DbType.NVarChar, 0, model.Name);
            ObjParam[2] = new DbParameter("@smsText", DbParameter.DbType.NVarChar, 0, model.smsText);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, model.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, model.ModifiedUser);
            ObjParam[5] = new DbParameter("@IsActive", DbParameter.DbType.Bit, 0, model.IsActive);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int SaveTermsAndCondition(ApplicationTnCModel Model1)
        {
            string query = "SaveTermsAndCondition_New";
            DbParameter[] ObjParam = new DbParameter[10];
            ObjParam[0] = new DbParameter("@pkiAppTC", DbParameter.DbType.Int, 0, Model1.pkiAppTC);
            ObjParam[1] = new DbParameter("@fkiApplicationID", DbParameter.DbType.Int, 0, Model1.fkiApplicationID);
            ObjParam[2] = new DbParameter("@TermsAndCondition", DbParameter.DbType.NVarChar, 0, Model1.TermsAndCondition);
            ObjParam[3] = new DbParameter("@LastModified", DbParameter.DbType.DateTime, 0, Model1.LastModified);
            ObjParam[4] = new DbParameter("@ModifiedUser", DbParameter.DbType.NVarChar, 0, Model1.ModifiedUser);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Model1.parlourid);
            ObjParam[6] = new DbParameter("@TermsAndConditionFuneral", DbParameter.DbType.NVarChar, 0, Model1.TermsAndConditionFuneral);
            ObjParam[7] = new DbParameter("@TermsAndConditionTombstone", DbParameter.DbType.NVarChar, 0, Model1.TermsAndConditionTombstone);
            ObjParam[8] = new DbParameter("@TermsAndConditionQuotation", DbParameter.DbType.NVarChar, 0, Model1.QuotationTermsAndCondition);
            ObjParam[9] = new DbParameter("@Declaration", DbParameter.DbType.NVarChar, 0, Model1.Declaration);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static SqlDataReader SelectApplicationTermsAndCondition(Guid parlourid)
        {
            string SP = "SelectApplicationTnCByParlourId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourid);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, SP, ObjParam);
        }
        public static DataTable SelectApplicationTermsAndConditiondt(Guid parlourid)
        {
            string SP = "SelectApplicationTnCByParlourId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourid);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam);
        }
        public static int UpdateAutoPolicyNo(ApplicationSettingsModel Model1)
        {
            string query = "update ApplicationSettings set IsAutoGeneratedPolicyNo=@IsAutoGeneratedPolicyNo where parlourid=@parlourid";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@IsAutoGeneratedPolicyNo", DbParameter.DbType.Bit, 0, Model1.IsAutoGeneratedPolicyNo);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Model1.parlourid);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));
        }
        public static SqlDataReader SelectFuneralManageServiceByParlID(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            string query = "SelectFuneralManageServiceByParlID";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable SelectFuneralManageServiceByParlIDdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            string query = "SelectFuneralManageServiceByParlID";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam));
        }

        public static SqlDataReader SelectTombstoneServiceByParlID(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            string query = "SelectTombstoneServiceByParlID";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataReader(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable SelectTombstoneServiceByParlIDdt(Guid ParlourId, int PageSize, int PageNum, string Keyword, string SortBy, string SortOrder)
        {
            string query = "SelectTombstoneServiceByParlID";
            DbParameter[] ObjParam = new DbParameter[6];
            ObjParam[0] = new DbParameter("@pagesize", DbParameter.DbType.Int, 0, PageSize);
            ObjParam[1] = new DbParameter("@pagenum", DbParameter.DbType.Int, 0, PageNum);
            ObjParam[2] = new DbParameter("@Keyword", DbParameter.DbType.NVarChar, 0, Keyword);
            ObjParam[3] = new DbParameter("@field", DbParameter.DbType.NVarChar, 0, SortBy);
            ObjParam[4] = new DbParameter("@orderby", DbParameter.DbType.NVarChar, 0, SortOrder);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, query, ObjParam));
        }
        public static int SaveFuneralManageService(FuneralServiceManageModel Model1)
        {
            string query = "SaveFuneralManageService";
            DbParameter[] ObjParam = new DbParameter[10];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, Model1.pkiServiceID);
            ObjParam[1] = new DbParameter("@ServiceName", DbParameter.DbType.NVarChar, 0, Model1.ServiceName);
            ObjParam[2] = new DbParameter("@ServiceDesc", DbParameter.DbType.NVarChar, 0, Model1.ServiceDesc);
            ObjParam[3] = new DbParameter("@ServiceCost", DbParameter.DbType.Money, 0, Model1.ServiceCost);
            ObjParam[4] = new DbParameter("@QTY", DbParameter.DbType.Int, 0, Model1.QTY);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, Model1.parlourid);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, Model1.ModifiedUser);
            ObjParam[7] = new DbParameter("@VendorId", DbParameter.DbType.Int, 0, Model1.VendorId);
            ObjParam[8] = new DbParameter("@CostOfSale", DbParameter.DbType.Money, 0, Model1.CostOfSale);
            ObjParam[9] = new DbParameter("@FuneralServiceType", DbParameter.DbType.Int, 0, Model1.FuneralServiceType);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }

        public static int SaveTombstoneServices(TombstoneServiceModel model1)
        {
            string query = "SaveTombstoneServices";
            DbParameter[] ObjParam = new DbParameter[9];
            ObjParam[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, model1.pkiTombstoneID);
            ObjParam[1] = new DbParameter("@ServiceName", DbParameter.DbType.NVarChar, 0, model1.ServiceName);
            ObjParam[2] = new DbParameter("@ServiceDesc", DbParameter.DbType.NVarChar, 0, model1.ServiceDesc);
            ObjParam[3] = new DbParameter("@ServiceCost", DbParameter.DbType.Money, 0, model1.ServiceCost);
            ObjParam[4] = new DbParameter("@QTY", DbParameter.DbType.Int, 0, model1.QTY);
            ObjParam[5] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, model1.parlourid);
            ObjParam[6] = new DbParameter("@ModifiedUser", DbParameter.DbType.VarChar, 0, model1.ModifiedUser);
            ObjParam[7] = new DbParameter("@VendorId", DbParameter.DbType.Int, 0, model1.VendorId);
            ObjParam[8] = new DbParameter("@CostOfSale", DbParameter.DbType.Money, 0, model1.CostOfSale);

            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));            
        }
        public static SqlDataReader SelectFuneralManageServiceByParlANdID(int pkiServiceID, Guid ParlourId)
        {
            string SpName = "SelectFuneralManageServiceByParlANdID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, pkiServiceID);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, SpName, ObjParam);
        }
        public static DataTable SelectFuneralManageServiceByParlANdIDdt(int pkiServiceID, Guid ParlourId)
        {
            string SpName = "SelectFuneralManageServiceByParlANdID";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, pkiServiceID);
            ObjParam[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, SpName, ObjParam);
        }

        public static SqlDataReader SelectTombstoneServiceById(int pkiTombstoneID ,Guid ParlourId)
        {
            string spName = "SelectTombstoneByIDAndParlour";
            DbParameter[] dbParameters = new DbParameter[2];
            dbParameters[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            dbParameters[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, spName, dbParameters);
        }
        public static DataTable SelectTombstoneServiceByIdAndParldt(int pkiTombstoneID, Guid ParlourId)
        {
            string spName = "SelectTombstoneByIDAndParlour";
            DbParameter[] dbParameters = new DbParameter[2];
            dbParameters[0] = new DbParameter("@pkiTombstoneID", DbParameter.DbType.Int, 0, pkiTombstoneID);
            dbParameters[1] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, spName, dbParameters);
        }
        public static int DeleteFuneralManageServiceById(int pkiServiceID)
        {
            string query = "Delete from FuneralServices Where pkiServiceID=@pkiServiceID";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@pkiServiceID", DbParameter.DbType.Int, 0, pkiServiceID);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.Text, query, ObjParam));

        }
        public static SqlDataReader GetAllApplicationList(Guid parlourid, int param, int AppID)
        {
            string SP = "SelectApplicationList";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            ObjParam[1] = new DbParameter("@param", DbParameter.DbType.Int, 0, param);
            ObjParam[2] = new DbParameter("@AppID", DbParameter.DbType.Int, 0, AppID);

            return DbConnection.GetDataReader(CommandType.StoredProcedure, SP, ObjParam);
        }
        public static DataTable GetAllApplicationListdt(Guid parlourid, int param, int AppID)
        {
            string SP = "SelectApplicationList";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@parlourid", DbParameter.DbType.UniqueIdentifier, 0, parlourid);
            ObjParam[1] = new DbParameter("@param", DbParameter.DbType.Int, 0, param);
            ObjParam[2] = new DbParameter("@AppID", DbParameter.DbType.Int, 0, AppID);

            return DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam);
        }
        public static SqlDataReader GetSuperUserAccessByID(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetSuperUserAccessByID", ObjParam);
        }

        public static DataTable GetSuperUserAccessByIDdt(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetSuperUserAccessByID", ObjParam);
        }
        public static DataTable GetSuperUserAccessByUserId(int ID)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetSuperUserAccessByUserId", ObjParam);
        }

        public static SqlDataReader GetAllPlansList(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];


            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetPlanByparlourId", ObjParam);
        }


        public static DataTable GetAllPlansListdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];


            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetPlanByparlourId", ObjParam);
        }



        public static SqlDataReader GetAllRolePlayer(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataReader(CommandType.StoredProcedure, "GetRolePlayerByparlourId", ObjParam);
        }
        public static DataTable GetAllRolePlayerdt(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetRolePlayerByparlourId", ObjParam);
        }

        public static DataTable CheckProgressStatus(int ID, Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@ID", DbParameter.DbType.Int, 0, ID);
            ObjParam[1] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "CheckProgressStatus", ObjParam);
        }
        public static DataTable GetAllUnderwriterList(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "getUnderwriterSetupListByParlourId", ObjParam);
        }
        public static DataTable GetVendorNameByParlourId(Guid Parlourid)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, Parlourid);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetVendorNameByParlourId", ObjParam);
        }
        public static DataTable GetAllSocietyesList(Guid ParlourId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetAllSocietyesList", ObjParam);
        }
        public static DataTable GetDashboardLableDetails(Guid ParlourId, bool IsAdministrator, bool IsSuperUser, string UserName)
        {
            DbParameter[] ObjParam = new DbParameter[4];
            ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
            ObjParam[1] = new DbParameter("@IsAdmin", DbParameter.DbType.Bit, 0, IsAdministrator);
            ObjParam[2] = new DbParameter("@IsSuperUser", DbParameter.DbType.Bit, 0, IsSuperUser);
            ObjParam[3] = new DbParameter("@UserName", DbParameter.DbType.NVarChar, 0, UserName);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimsDashboardTodayPayment", ObjParam);
        }
        public static DataTable GetClaimRightsCollectionByRoleId(int RoleId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@RoleID", DbParameter.DbType.Int, 0, RoleId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "GetClaimRightsByRoleId", ObjParam);
        }
        public static int SaveClaimRights(ClaimRightsList model)
        {
            string query = "SaveClaimRightsWorkflow";
            DbParameter[] ObjParam = new DbParameter[3];
            ObjParam[0] = new DbParameter("@ClaimStatusId", DbParameter.DbType.Int, 0, model.ClaimStatusId);
            ObjParam[1] = new DbParameter("@RoleId", DbParameter.DbType.Int, 0, model.RoleId);
            ObjParam[2] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, model.CreatedBy);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static int DeleteClaimRights(int RoleId, string CreatedBy)
        {
            string query = "DeleteClaimRightsWorkflow";
            DbParameter[] ObjParam = new DbParameter[2];
            ObjParam[0] = new DbParameter("@RoleId", DbParameter.DbType.Int, 0, RoleId);
            ObjParam[1] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, CreatedBy);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));

        }
        public static DataTable AddPlansAndGetPlanList_Staging(Guid newImportedId)
        {
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportId", DbParameter.DbType.UniqueIdentifier, 0, newImportedId);
            return DbConnection.GetDataTable(CommandType.StoredProcedure, "AddPlanStaging_and_getPlanList", ObjParam);
        }
        public static int SavePlanCreatorStagingDetails(PlanStagingList model)
        {
            string query = "SavePlanCreatorStaging";
            DbParameter[] ObjParam = new DbParameter[14];
            ObjParam[0] = new DbParameter("@PlanName", DbParameter.DbType.NVarChar, 0, model.PlanName);
            ObjParam[1] = new DbParameter("@UserTypeId", DbParameter.DbType.Int, 0, model.RelationTypeId);
            ObjParam[2] = new DbParameter("@AgeFrom", DbParameter.DbType.Int, 0, model.FromAge);
            ObjParam[3] = new DbParameter("@AgeTo", DbParameter.DbType.Int, 0, model.ToAge);
            ObjParam[4] = new DbParameter("@CoverAmount", DbParameter.DbType.Decimal, 0, model.Cover);
            ObjParam[5] = new DbParameter("@PremiumAmount", DbParameter.DbType.Decimal, 0, model.PremiumAmount);
            ObjParam[6] = new DbParameter("@UnderwriterCover", DbParameter.DbType.Decimal, 0, model.UnderwriterCover);
            ObjParam[7] = new DbParameter("@UnderwriterPremium", DbParameter.DbType.Int, 0, model.UnderwriterPremium);
            ObjParam[8] = new DbParameter("@IsActive", DbParameter.DbType.Bit, 0, true);
            ObjParam[9] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, model.CreatedBy);
            ObjParam[10] = new DbParameter("@OfficePremium", DbParameter.DbType.Decimal, 0, model.OfficePremium);
            ObjParam[11] = new DbParameter("@ReinsurancePremium", DbParameter.DbType.Decimal, 0, model.ReinsurancePremium);
            ObjParam[12] = new DbParameter("@PlanDesc", DbParameter.DbType.NVarChar, 0, model.PlanDesc);
            ObjParam[13] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, model.parlourid);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataSet GetImportedMemberList(Guid newImportedId, string columnName)
        {
            string query = "select [ID]," + columnName + ",IsExecuted,ExecutionError from MembersRowImport where ImportedId=@ImportedId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportedId", DbParameter.DbType.UniqueIdentifier, 0, newImportedId);
            return DbConnection.GetDataSet(CommandType.Text, query, ObjParam);
        }
        public static int SaveImportedHistory(ImportedHistory model)
        {
            string query = "SaveImportedHistory";
            DbParameter[] ObjParam = new DbParameter[8];
            ObjParam[0] = new DbParameter("@ImportedFilename", DbParameter.DbType.NVarChar, 0, model.ImportedFilename);
            ObjParam[1] = new DbParameter("@MappingColumn", DbParameter.DbType.Xml, 0, model.MappingColumn);
            ObjParam[2] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, model.parlourId);
            ObjParam[3] = new DbParameter("@ImportedBy", DbParameter.DbType.NVarChar, 0, model.ImportedBy);
            ObjParam[4] = new DbParameter("@IsImported", DbParameter.DbType.NVarChar, 0, model.IsImported);
            ObjParam[5] = new DbParameter("@ImportedId", DbParameter.DbType.Int, 0, model.ImportedId);
            ObjParam[6] = new DbParameter("@newImportedGuidId", DbParameter.DbType.UniqueIdentifier, 0, model.NewImportedId);
            ObjParam[7] = new DbParameter("@MemberType", DbParameter.DbType.NVarChar, 0, model.MemberType);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable GetImportedHistory(Guid ParlourId)
        {
            if (ParlourId == Guid.Empty)
                return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetImportedHistory_WithoutParlour"));
            else
            {
                DbParameter[] ObjParam = new DbParameter[1];
                ObjParam[0] = new DbParameter("@ParlourId", DbParameter.DbType.UniqueIdentifier, 0, ParlourId);
                return (DbConnection.GetDataTable(CommandType.StoredProcedure, "GetImportedHistory", ObjParam));
            }
        }
        public static DataTable GetImportedHistory_ByImportedId(int ImportedId)
        {
            string SP = "GetImportedHistory_ByImportedId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportedId", DbParameter.DbType.Int, 0, ImportedId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        }
        public static DataTable GetImportedHistory_ByNewImportedId(Guid ImportedId)
        {
            string SP = "GetImportedHistory_ByNewImportedId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportedId", DbParameter.DbType.UniqueIdentifier, 0, ImportedId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        }
        public static int SaveMappedDependents(string SystemType, string ExcelType, Guid parlourId, Guid newImportedId, string CreatedBy)
        {
            string query = "SaveDependentMapping";
            DbParameter[] ObjParam = new DbParameter[5];
            ObjParam[0] = new DbParameter("@SystemTypeName", DbParameter.DbType.NVarChar, 0, SystemType);
            ObjParam[1] = new DbParameter("@ExcelTypeName", DbParameter.DbType.NVarChar, 0, ExcelType);
            ObjParam[2] = new DbParameter("@parlourId", DbParameter.DbType.UniqueIdentifier, 0, parlourId);
            ObjParam[3] = new DbParameter("@ImportedId", DbParameter.DbType.UniqueIdentifier, 0, newImportedId);
            ObjParam[4] = new DbParameter("@CreatedBy", DbParameter.DbType.NVarChar, 0, CreatedBy);
            return Convert.ToInt32(DbConnection.GetScalarValue(CommandType.StoredProcedure, query, ObjParam));
        }
        public static DataTable GetMappedDependent(Guid ImportedId)
        {
            string SP = "GetMappedDependent_ByImportedId";
            DbParameter[] ObjParam = new DbParameter[1];
            ObjParam[0] = new DbParameter("@ImportedId", DbParameter.DbType.UniqueIdentifier, 0, ImportedId);
            return (DbConnection.GetDataTable(CommandType.StoredProcedure, SP, ObjParam));
        }
        public static void UpdateMemberStagingTable(DataTable dt)
        {
            string constr = DbConnection.sqlConnectionstring;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand("UpdateMemberStagingTable"))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Connection = con;
                    cmd.Parameters.AddWithValue("@MemberStagingType", dt);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
            }
        }
    }
}
