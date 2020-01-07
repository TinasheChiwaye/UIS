using Funeral.BAL;
using Funeral.Web.Areas.Admin.Controllers;
using Funeral.Web.Areas.Tools.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Funeral.Web.Areas.Tools.Controllers
{
    public class QuotationEngineController : BaseAdminController
    {
        // GET: Tools/QuotationEngine

        private static readonly List<string> memberTableColumns = new List<string>() { "Name", "MemberType", "DOB", "Age" };
        //[PageRightsAttribute(CurrentPageId = 15, Right = new isPageRight[] { isPageRight.HasAccess })]
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public JsonResult UploadExcelFile(HttpPostedFileBase FileUpload)
        {
            string partialHTML = string.Empty;
            if (FileUpload.ContentLength > 0)
            {
                string saveFolder = Server.MapPath(@"~/Upload/ImportData/"); //Pick a folder on your machine to store the uploaded files
                if (!Directory.Exists(saveFolder))
                    Directory.CreateDirectory(saveFolder);

                string filePath = Path.Combine(saveFolder, FileUpload.FileName);
                Session["filePath"] = filePath;
                Session["ImportedFileName"] = FileUpload.FileName;
                string fileExt = Path.GetExtension(filePath);
                FileUpload.SaveAs(filePath);
                var ValidateColumn = ToolsSetingBAL.ValidateExcelColumns(filePath);
                ExcelModel ex = new ExcelModel();
                ex.ExcelColumn = ValidateColumn;
                ex.StaticColumn = memberTableColumns.Select(x => new SelectListItem() { Text = x, Value = x }).ToList();
                partialHTML = ConvertViewToString("_UploadedExcelFileList", ex);
            }
            return Json(new { PartialView = partialHTML }, JsonRequestBehavior.AllowGet);
        }
        private string ConvertViewToString(string viewName, object model)
        {
            ViewData.Model = model;
            using (StringWriter writer = new StringWriter())
            {
                ViewEngineResult vResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                ViewContext vContext = new ViewContext(this.ControllerContext, vResult.View, ViewData, new TempDataDictionary(), writer);
                vResult.View.Render(vContext, writer);
                return writer.ToString();
            }
        }
        public ActionResult SaveExcelRecord(ExcelModel ex)
        {
            string filePath = Session["filePath"].ToString();
            MembersndDependents membersndDependents = new MembersndDependents();



            Dictionary<string, string> mappedColumn = new Dictionary<string, string>();
            foreach (var item in ex.DyanamicColumn) { mappedColumn.Add(item.StaticColumn, item.DynamicColumn); }

            if (filePath != null)
            {

                DataSet dataSet = ToolsSetingBAL.ReadExcel(filePath, ".xls");
                #region Members
                var Memebers = ReadExcelDataset(filePath, mappedColumn, dataSet, 0);
                membersndDependents.MembersList = Memebers.Item1;
                membersndDependents.MembersAvergeAge = Memebers.Item2;
                #endregion

                #region Dependent
                var Dependent = ReadExcelDataset(filePath, mappedColumn, dataSet, 1);
                membersndDependents.DependentList = Dependent.Item1;
                membersndDependents.DependenAvergeAge = Dependent.Item2;
                #endregion

                //if (dt != null && dt.Rows.Count > 0)
                //    WriteToDatabase(dataTable, newImportedId);
                //else
                //    TempData["message"] = "No record found to import please correct the sheet.";
            }
            return RedirectToAction("Index", "QuotationEngine");
        }
        private Tuple<List<ExcelActualTable>, double> ReadExcelDataset(string filePath, Dictionary<string, string> mappedColumn, DataSet dt, int tableOf)
        {

            DataTable dataTable = new DataTable();
            foreach (string item in memberTableColumns) { dataTable.Columns.Add(new DataColumn(item, typeof(string))); }
            if (dt.Tables.Count > tableOf)
            {
                foreach (DataRow item in dt.Tables[tableOf].Rows)
                {
                    DataRow newRow = dataTable.NewRow();
                    foreach (var keyValuePair in mappedColumn)
                    {
                        newRow[keyValuePair.Value] = item[keyValuePair.Key];
                    }
                    dataTable.Rows.Add(newRow);
                }
                var table = CommonBAL.ConvertDataTable<ExcelActualTable>(dataTable).ToList();
                var AvergeAge = table.Sum(x => Convert.ToDouble(x.Age)) / table.Count;
                return Tuple.Create(table, AvergeAge);
            }
            else
            {
                List<ExcelActualTable> excelActualTables = new List<ExcelActualTable>();
                return Tuple.Create(excelActualTables, 0.00);
            }
        }
    }
}