using Funeral.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Admin.Models.ViewModel
{ 
    public class FuneralServicePackageVM
    {
        public string PackageName { get; set; }
        public string PackageID { get; set; }
        public int pkiFuneralSelectionID { get; set; }
        public int fkiFuneralID { get; set; }
        public int fkiServiceID { get; set; }
        public int Quantity { get; set; }
        public decimal Amount { get; set; }
        public int pkiServiceID { get; set; }
        public string ServiceName { get; set; }
        public string ServiceDesc { get; set; }
        public decimal ServiceCost { get; set; }
        public int QTY { get; set; }
        public Guid parlourid { get; set; }
        public decimal ServiceRate { get; set; }
    }

    public class FuneralService
    {
        public int pkiFuneralID { get; set; }
        public string Tax { get; set; }
        public string DisCount { get; set; }
        public string InvoiceNumber { get; set; }
        public string Notes { get; set; }

    }
}