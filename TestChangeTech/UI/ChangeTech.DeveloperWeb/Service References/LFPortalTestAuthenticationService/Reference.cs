﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.269
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="PortalResponse", Namespace="http://healthportalautentication.services.web.suppliers.sos.eu")]
    [System.SerializableAttribute()]
    public partial class PortalResponse : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int autenticatedField;
        
        private System.DateTime expirationDateField;
        
        private string insuranceField;
        
        private string nationalityField;
        
        private string ssnField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public int autenticated {
            get {
                return this.autenticatedField;
            }
            set {
                if ((this.autenticatedField.Equals(value) != true)) {
                    this.autenticatedField = value;
                    this.RaisePropertyChanged("autenticated");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public System.DateTime expirationDate {
            get {
                return this.expirationDateField;
            }
            set {
                if ((this.expirationDateField.Equals(value) != true)) {
                    this.expirationDateField = value;
                    this.RaisePropertyChanged("expirationDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string insurance {
            get {
                return this.insuranceField;
            }
            set {
                if ((object.ReferenceEquals(this.insuranceField, value) != true)) {
                    this.insuranceField = value;
                    this.RaisePropertyChanged("insurance");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string nationality {
            get {
                return this.nationalityField;
            }
            set {
                if ((object.ReferenceEquals(this.nationalityField, value) != true)) {
                    this.nationalityField = value;
                    this.RaisePropertyChanged("nationality");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public string ssn {
            get {
                return this.ssnField;
            }
            set {
                if ((object.ReferenceEquals(this.ssnField, value) != true)) {
                    this.ssnField = value;
                    this.RaisePropertyChanged("ssn");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="PortalAutenticationException", Namespace="http://healthportalautentication.services.web.suppliers.sos.eu")]
    [System.SerializableAttribute()]
    public partial class PortalAutenticationException : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
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
    [System.ServiceModel.ServiceContractAttribute(Namespace="http://healthportalautentication.services.web.suppliers.sos.eu/", ConfigurationName="LFPortalTestAuthenticationService.PortalAutenticationService")]
    public interface PortalAutenticationService {
        
        // CODEGEN: Generating message contract since element name requesterId from namespace  is not marked nillable
        [System.ServiceModel.OperationContractAttribute(Action="", ReplyAction="*")]
        [System.ServiceModel.FaultContractAttribute(typeof(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationException), Action="", Name="PortalAutenticationException")]
        ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateResponse autenticate(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticate request);
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class autenticate {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="autenticate", Namespace="http://healthportalautentication.services.web.suppliers.sos.eu/", Order=0)]
        public ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateBody Body;
        
        public autenticate() {
        }
        
        public autenticate(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class autenticateBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public string requesterId;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=1)]
        public string requesterPassword;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string ssn;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string nationality;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string insurance;
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=5)]
        public string insuranceCompany;
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public string insuranceProduct;
        
        public autenticateBody() {
        }
        
        public autenticateBody(string requesterId, string requesterPassword, string ssn, string nationality, string insurance, string insuranceCompany, string insuranceProduct) {
            this.requesterId = requesterId;
            this.requesterPassword = requesterPassword;
            this.ssn = ssn;
            this.nationality = nationality;
            this.insurance = insurance;
            this.insuranceCompany = insuranceCompany;
            this.insuranceProduct = insuranceProduct;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
    public partial class autenticateResponse {
        
        [System.ServiceModel.MessageBodyMemberAttribute(Name="autenticateResponse", Namespace="http://healthportalautentication.services.web.suppliers.sos.eu/", Order=0)]
        public ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateResponseBody Body;
        
        public autenticateResponse() {
        }
        
        public autenticateResponse(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateResponseBody Body) {
            this.Body = Body;
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    [System.Runtime.Serialization.DataContractAttribute(Namespace="")]
    public partial class autenticateResponseBody {
        
        [System.Runtime.Serialization.DataMemberAttribute(EmitDefaultValue=false, Order=0)]
        public ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalResponse @return;
        
        public autenticateResponseBody() {
        }
        
        public autenticateResponseBody(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalResponse @return) {
            this.@return = @return;
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface PortalAutenticationServiceChannel : ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class PortalAutenticationServiceClient : System.ServiceModel.ClientBase<ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationService>, ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationService {
        
        public PortalAutenticationServiceClient() {
        }
        
        public PortalAutenticationServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public PortalAutenticationServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortalAutenticationServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public PortalAutenticationServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
        ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateResponse ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationService.autenticate(ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticate request) {
            return base.Channel.autenticate(request);
        }
        
        public ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalResponse autenticate(string requesterId, string requesterPassword, string ssn, string nationality, string insurance, string insuranceCompany, string insuranceProduct) {
            ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticate inValue = new ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticate();
            inValue.Body = new ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateBody();
            inValue.Body.requesterId = requesterId;
            inValue.Body.requesterPassword = requesterPassword;
            inValue.Body.ssn = ssn;
            inValue.Body.nationality = nationality;
            inValue.Body.insurance = insurance;
            inValue.Body.insuranceCompany = insuranceCompany;
            inValue.Body.insuranceProduct = insuranceProduct;
            ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.autenticateResponse retVal = ((ChangeTech.DeveloperWeb.LFPortalTestAuthenticationService.PortalAutenticationService)(this)).autenticate(inValue);
            return retVal.Body.@return;
        }
    }
}