using System.Collections.Generic;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Tools.Models
{
    public class ExcelModel
    {
        public List<string> ExcelColumn { get; set; }
        public List<ExcelLlist> DyanamicColumn { get; set; }
        public List<SelectListItem> StaticColumn { get; set; }
    }
    public class ExcelLlist
    {
        public string StaticColumn { get; set; }

        public string DynamicColumn { get; set; }
    }
    public class ExcelActualTable
    {
        public string Name { get; set; }

        public string MemberType { get; set; }

        public string DOB { get; set; }
        public string Age { get; set; }
    }
    public class MembersndDependents
    {
        public List<ExcelActualTable> MembersList { get; set; }
        public double MembersAvergeAge { get; set; }
        public List<ExcelActualTable> DependentList { get; set; }
        public double DependenAvergeAge { get; set; }
    }
}