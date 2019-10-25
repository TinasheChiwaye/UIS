using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Web;

namespace Funeral.Services
{
    [DataContract]
    public class FuneralServiceFault
    {
        private string _message;

        public FuneralServiceFault(string message)
        {
            _message = message;
        }

        [DataMember]
        public string Message { 
            get { return _message; } 
            set { _message = value; } 
        }

    }
}