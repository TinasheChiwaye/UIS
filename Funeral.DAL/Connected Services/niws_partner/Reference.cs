﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Funeral.DAL.niws_partner {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidateServiceKeyRequest", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ValidateServiceKeyRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MerchantAccountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Funeral.DAL.niws_partner.ServiceInfoList ServiceInfoListField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SoftwareVendorKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MerchantAccount {
            get {
                return this.MerchantAccountField;
            }
            set {
                if ((object.ReferenceEquals(this.MerchantAccountField, value) != true)) {
                    this.MerchantAccountField = value;
                    this.RaisePropertyChanged("MerchantAccount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Funeral.DAL.niws_partner.ServiceInfoList ServiceInfoList {
            get {
                return this.ServiceInfoListField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceInfoListField, value) != true)) {
                    this.ServiceInfoListField = value;
                    this.RaisePropertyChanged("ServiceInfoList");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SoftwareVendorKey {
            get {
                return this.SoftwareVendorKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.SoftwareVendorKeyField, value) != true)) {
                    this.SoftwareVendorKeyField = value;
                    this.RaisePropertyChanged("SoftwareVendorKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ServiceInfoList", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS", ItemName="ServiceInfo")]
    [System.SerializableAttribute()]
    public class ServiceInfoList : System.Collections.Generic.List<Funeral.DAL.niws_partner.ServiceInfo> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceInfo", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ServiceInfo : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceId {
            get {
                return this.ServiceIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceIdField, value) != true)) {
                    this.ServiceIdField = value;
                    this.RaisePropertyChanged("ServiceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceKey {
            get {
                return this.ServiceKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceKeyField, value) != true)) {
                    this.ServiceKeyField = value;
                    this.RaisePropertyChanged("ServiceKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidateServiceKeyResponse", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ValidateServiceKeyResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AccountStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MerchantAccountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private Funeral.DAL.niws_partner.ServiceInfoResponseList ServiceInfoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SoftwareVendorKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AccountStatus {
            get {
                return this.AccountStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountStatusField, value) != true)) {
                    this.AccountStatusField = value;
                    this.RaisePropertyChanged("AccountStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MerchantAccount {
            get {
                return this.MerchantAccountField;
            }
            set {
                if ((object.ReferenceEquals(this.MerchantAccountField, value) != true)) {
                    this.MerchantAccountField = value;
                    this.RaisePropertyChanged("MerchantAccount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public Funeral.DAL.niws_partner.ServiceInfoResponseList ServiceInfo {
            get {
                return this.ServiceInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceInfoField, value) != true)) {
                    this.ServiceInfoField = value;
                    this.RaisePropertyChanged("ServiceInfo");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SoftwareVendorKey {
            get {
                return this.SoftwareVendorKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.SoftwareVendorKeyField, value) != true)) {
                    this.SoftwareVendorKeyField = value;
                    this.RaisePropertyChanged("SoftwareVendorKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.CollectionDataContractAttribute(Name="ServiceInfoResponseList", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS", ItemName="ServiceInfoResponse")]
    [System.SerializableAttribute()]
    public class ServiceInfoResponseList : System.Collections.Generic.List<Funeral.DAL.niws_partner.ServiceInfoResponse> {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ServiceInfoResponse", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ServiceInfoResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceKeyField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ServiceStatusField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceId {
            get {
                return this.ServiceIdField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceIdField, value) != true)) {
                    this.ServiceIdField = value;
                    this.RaisePropertyChanged("ServiceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceKey {
            get {
                return this.ServiceKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceKeyField, value) != true)) {
                    this.ServiceKeyField = value;
                    this.RaisePropertyChanged("ServiceKey");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ServiceStatus {
            get {
                return this.ServiceStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.ServiceStatusField, value) != true)) {
                    this.ServiceStatusField = value;
                    this.RaisePropertyChanged("ServiceStatus");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidateComplianceRequest", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ValidateComplianceRequest : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MerchantAccountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SoftwareVendorKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MerchantAccount {
            get {
                return this.MerchantAccountField;
            }
            set {
                if ((object.ReferenceEquals(this.MerchantAccountField, value) != true)) {
                    this.MerchantAccountField = value;
                    this.RaisePropertyChanged("MerchantAccount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SoftwareVendorKey {
            get {
                return this.SoftwareVendorKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.SoftwareVendorKeyField, value) != true)) {
                    this.SoftwareVendorKeyField = value;
                    this.RaisePropertyChanged("SoftwareVendorKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ValidateComplianceResponse", Namespace="http://schemas.datacontract.org/2004/07/NC.DG.TMS.C.WCF.NIWS")]
    [System.SerializableAttribute()]
    public partial class ValidateComplianceResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string AccountStatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MerchantAccountField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string SoftwareVendorKeyField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string AccountStatus {
            get {
                return this.AccountStatusField;
            }
            set {
                if ((object.ReferenceEquals(this.AccountStatusField, value) != true)) {
                    this.AccountStatusField = value;
                    this.RaisePropertyChanged("AccountStatus");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string MerchantAccount {
            get {
                return this.MerchantAccountField;
            }
            set {
                if ((object.ReferenceEquals(this.MerchantAccountField, value) != true)) {
                    this.MerchantAccountField = value;
                    this.RaisePropertyChanged("MerchantAccount");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string SoftwareVendorKey {
            get {
                return this.SoftwareVendorKeyField;
            }
            set {
                if ((object.ReferenceEquals(this.SoftwareVendorKeyField, value) != true)) {
                    this.SoftwareVendorKeyField = value;
                    this.RaisePropertyChanged("SoftwareVendorKey");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="niws_partner.INIWS_Partner")]
    public interface INIWS_Partner {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/BatchFileUpload", ReplyAction="http://tempuri.org/INIWS_Partner/BatchFileUploadResponse")]
        string BatchFileUpload(string serviceKey, string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/BatchFileUpload", ReplyAction="http://tempuri.org/INIWS_Partner/BatchFileUploadResponse")]
        System.Threading.Tasks.Task<string> BatchFileUploadAsync(string serviceKey, string file);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RequestFileUploadReport", ReplyAction="http://tempuri.org/INIWS_Partner/RequestFileUploadReportResponse")]
        string RequestFileUploadReport(string serviceKey, string fileToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RequestFileUploadReport", ReplyAction="http://tempuri.org/INIWS_Partner/RequestFileUploadReportResponse")]
        System.Threading.Tasks.Task<string> RequestFileUploadReportAsync(string serviceKey, string fileToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/ValidateServiceKey", ReplyAction="http://tempuri.org/INIWS_Partner/ValidateServiceKeyResponse")]
        Funeral.DAL.niws_partner.ValidateServiceKeyResponse ValidateServiceKey(Funeral.DAL.niws_partner.ValidateServiceKeyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/ValidateServiceKey", ReplyAction="http://tempuri.org/INIWS_Partner/ValidateServiceKeyResponse")]
        System.Threading.Tasks.Task<Funeral.DAL.niws_partner.ValidateServiceKeyResponse> ValidateServiceKeyAsync(Funeral.DAL.niws_partner.ValidateServiceKeyRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/AccountSupportMessage", ReplyAction="http://tempuri.org/INIWS_Partner/AccountSupportMessageResponse")]
        string AccountSupportMessage(string partnerServiceKey, string serviceKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/AccountSupportMessage", ReplyAction="http://tempuri.org/INIWS_Partner/AccountSupportMessageResponse")]
        System.Threading.Tasks.Task<string> AccountSupportMessageAsync(string partnerServiceKey, string serviceKey);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RequestAccountStatus", ReplyAction="http://tempuri.org/INIWS_Partner/RequestAccountStatusResponse")]
        string RequestAccountStatus(string partnerServiceKey, string pastelAccountNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RequestAccountStatus", ReplyAction="http://tempuri.org/INIWS_Partner/RequestAccountStatusResponse")]
        System.Threading.Tasks.Task<string> RequestAccountStatusAsync(string partnerServiceKey, string pastelAccountNo);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RetrieveAccountStatus", ReplyAction="http://tempuri.org/INIWS_Partner/RetrieveAccountStatusResponse")]
        string RetrieveAccountStatus(string ServiceKey, string FileToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RetrieveAccountStatus", ReplyAction="http://tempuri.org/INIWS_Partner/RetrieveAccountStatusResponse")]
        System.Threading.Tasks.Task<string> RetrieveAccountStatusAsync(string ServiceKey, string FileToken);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RetrievePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/RetrievePNSHandleResponse")]
        string RetrievePNSHandle(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/RetrievePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/RetrievePNSHandleResponse")]
        System.Threading.Tasks.Task<string> RetrievePNSHandleAsync(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/PartnerAuth", ReplyAction="http://tempuri.org/INIWS_Partner/PartnerAuthResponse")]
        string PartnerAuth(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/PartnerAuth", ReplyAction="http://tempuri.org/INIWS_Partner/PartnerAuthResponse")]
        System.Threading.Tasks.Task<string> PartnerAuthAsync(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/CreateOrUpdatePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/CreateOrUpdatePNSHandleResponse")]
        string CreateOrUpdatePNSHandle(string ServiceKey, int SystemUserID, string PNSHandle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/CreateOrUpdatePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/CreateOrUpdatePNSHandleResponse")]
        System.Threading.Tasks.Task<string> CreateOrUpdatePNSHandleAsync(string ServiceKey, int SystemUserID, string PNSHandle);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/DeletePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/DeletePNSHandleResponse")]
        string DeletePNSHandle(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/DeletePNSHandle", ReplyAction="http://tempuri.org/INIWS_Partner/DeletePNSHandleResponse")]
        System.Threading.Tasks.Task<string> DeletePNSHandleAsync(string ServiceKey, int SystemUserID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/ValidateComplianceStatus", ReplyAction="http://tempuri.org/INIWS_Partner/ValidateComplianceStatusResponse")]
        Funeral.DAL.niws_partner.ValidateComplianceResponse ValidateComplianceStatus(Funeral.DAL.niws_partner.ValidateComplianceRequest request);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/INIWS_Partner/ValidateComplianceStatus", ReplyAction="http://tempuri.org/INIWS_Partner/ValidateComplianceStatusResponse")]
        System.Threading.Tasks.Task<Funeral.DAL.niws_partner.ValidateComplianceResponse> ValidateComplianceStatusAsync(Funeral.DAL.niws_partner.ValidateComplianceRequest request);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface INIWS_PartnerChannel : Funeral.DAL.niws_partner.INIWS_Partner, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class NIWS_PartnerClient : System.ServiceModel.ClientBase<Funeral.DAL.niws_partner.INIWS_Partner>, Funeral.DAL.niws_partner.INIWS_Partner {
        
        public NIWS_PartnerClient() {
        }
        
        public NIWS_PartnerClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public NIWS_PartnerClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NIWS_PartnerClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public NIWS_PartnerClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public string BatchFileUpload(string serviceKey, string file) {
            return base.Channel.BatchFileUpload(serviceKey, file);
        }
        
        public System.Threading.Tasks.Task<string> BatchFileUploadAsync(string serviceKey, string file) {
            return base.Channel.BatchFileUploadAsync(serviceKey, file);
        }
        
        public string RequestFileUploadReport(string serviceKey, string fileToken) {
            return base.Channel.RequestFileUploadReport(serviceKey, fileToken);
        }
        
        public System.Threading.Tasks.Task<string> RequestFileUploadReportAsync(string serviceKey, string fileToken) {
            return base.Channel.RequestFileUploadReportAsync(serviceKey, fileToken);
        }
        
        public Funeral.DAL.niws_partner.ValidateServiceKeyResponse ValidateServiceKey(Funeral.DAL.niws_partner.ValidateServiceKeyRequest request) {
            return base.Channel.ValidateServiceKey(request);
        }
        
        public System.Threading.Tasks.Task<Funeral.DAL.niws_partner.ValidateServiceKeyResponse> ValidateServiceKeyAsync(Funeral.DAL.niws_partner.ValidateServiceKeyRequest request) {
            return base.Channel.ValidateServiceKeyAsync(request);
        }
        
        public string AccountSupportMessage(string partnerServiceKey, string serviceKey) {
            return base.Channel.AccountSupportMessage(partnerServiceKey, serviceKey);
        }
        
        public System.Threading.Tasks.Task<string> AccountSupportMessageAsync(string partnerServiceKey, string serviceKey) {
            return base.Channel.AccountSupportMessageAsync(partnerServiceKey, serviceKey);
        }
        
        public string RequestAccountStatus(string partnerServiceKey, string pastelAccountNo) {
            return base.Channel.RequestAccountStatus(partnerServiceKey, pastelAccountNo);
        }
        
        public System.Threading.Tasks.Task<string> RequestAccountStatusAsync(string partnerServiceKey, string pastelAccountNo) {
            return base.Channel.RequestAccountStatusAsync(partnerServiceKey, pastelAccountNo);
        }
        
        public string RetrieveAccountStatus(string ServiceKey, string FileToken) {
            return base.Channel.RetrieveAccountStatus(ServiceKey, FileToken);
        }
        
        public System.Threading.Tasks.Task<string> RetrieveAccountStatusAsync(string ServiceKey, string FileToken) {
            return base.Channel.RetrieveAccountStatusAsync(ServiceKey, FileToken);
        }
        
        public string RetrievePNSHandle(string ServiceKey, int SystemUserID) {
            return base.Channel.RetrievePNSHandle(ServiceKey, SystemUserID);
        }
        
        public System.Threading.Tasks.Task<string> RetrievePNSHandleAsync(string ServiceKey, int SystemUserID) {
            return base.Channel.RetrievePNSHandleAsync(ServiceKey, SystemUserID);
        }
        
        public string PartnerAuth(string ServiceKey, int SystemUserID) {
            return base.Channel.PartnerAuth(ServiceKey, SystemUserID);
        }
        
        public System.Threading.Tasks.Task<string> PartnerAuthAsync(string ServiceKey, int SystemUserID) {
            return base.Channel.PartnerAuthAsync(ServiceKey, SystemUserID);
        }
        
        public string CreateOrUpdatePNSHandle(string ServiceKey, int SystemUserID, string PNSHandle) {
            return base.Channel.CreateOrUpdatePNSHandle(ServiceKey, SystemUserID, PNSHandle);
        }
        
        public System.Threading.Tasks.Task<string> CreateOrUpdatePNSHandleAsync(string ServiceKey, int SystemUserID, string PNSHandle) {
            return base.Channel.CreateOrUpdatePNSHandleAsync(ServiceKey, SystemUserID, PNSHandle);
        }
        
        public string DeletePNSHandle(string ServiceKey, int SystemUserID) {
            return base.Channel.DeletePNSHandle(ServiceKey, SystemUserID);
        }
        
        public System.Threading.Tasks.Task<string> DeletePNSHandleAsync(string ServiceKey, int SystemUserID) {
            return base.Channel.DeletePNSHandleAsync(ServiceKey, SystemUserID);
        }
        
        public Funeral.DAL.niws_partner.ValidateComplianceResponse ValidateComplianceStatus(Funeral.DAL.niws_partner.ValidateComplianceRequest request) {
            return base.Channel.ValidateComplianceStatus(request);
        }
        
        public System.Threading.Tasks.Task<Funeral.DAL.niws_partner.ValidateComplianceResponse> ValidateComplianceStatusAsync(Funeral.DAL.niws_partner.ValidateComplianceRequest request) {
            return base.Channel.ValidateComplianceStatusAsync(request);
        }
    }
}
