using ExcelDataReader;
using Funeral.BAL;
using Funeral.DAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Funeral.Web.Admin
{
    public partial class ImportExcel : AdminBasePage
    {
        private static readonly List<string> memberTableColumns = new List<string>() {
                                                                            "CreateDate",
                                                                            "MemberType",
                                                                            "Title",
                                                                            "Full Names",
                                                                            "Surname",
                                                                            "Gender",
                                                                            "Active",
                                                                            "ID Number" ,
                                                                            "Date Of Birth",
                                                                            "Telephone",
                                                                            "Cellphone",
                                                                            "Address1",
                                                                            "Address2",
                                                                            "Address3",
                                                                            "Address4",
                                                                            "Code",
                                                                            "PolicyNumber",
                                                                            "MemberSociety",
                                                                            "InceptionDate",
                                                                            "Claimnumber",
                                                                            "PolicyStatus",
                                                                            "parlourid",
                                                                            "Agent",
                                                                            "AccountHolder",
                                                                            "Bank",
                                                                            "BranchCode",
                                                                            "Branch",
                                                                            "AccountNumber",
                                                                            "AccountType",
                                                                            "DebitDate",
                                                                            "MemberBranch",
                                                                            "CoverDate",
                                                                            "Email",
                                                                            "Citizenship",
                                                                            "Passport",
                                                                            "StartDate",
                                                                            "CustomId1",
                                                                            "CustomId2",
                                                                            "CustomId3",
                                                                            "PlanName",
                                                                            "Premium"
                                                                        };

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Submit_Click(object sender, EventArgs e)
        {
            hdnMemberName.Value = txtMainMemberName.Text;

            if (FileUpload1.HasFile)
            {
                string saveFolder = Server.MapPath(@"~/Upload/ImportData/"); //Pick a folder on your machine to store the uploaded files
                if (!Directory.Exists(saveFolder))
                    Directory.CreateDirectory(saveFolder);

                string filePath = Path.Combine(saveFolder, FileUpload1.FileName);
                Session["filePath"] = filePath;
                Session["ImportedFileName"] = FileUpload1.FileName;
                string fileExt = Path.GetExtension(filePath);
                FileUpload1.SaveAs(filePath);
                btnSubmit.Visible = true;
                ValidateExcelColumns(filePath);

                // var ReadExcelData = ReadExcel(filePath, fileExt);
            }
        }

        public void ValidateExcelColumns(string filePath)
        {
            using (var stream = File.Open(filePath, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    List<string> ColumnNameList = new List<string>();
                    var column = result.Tables[0].Columns;
                    hdnColumCount.Value = result.Tables[0].Columns.Count.ToString();
                    int cnt = column.Count;
                    for (int i = 0; i < cnt; i++)
                    {
                        ColumnNameList.Add(column[i].ColumnName);
                    }
                    gvMapping.DataSource = ColumnNameList;
                    gvMapping.DataBind();
                }
            }
        }

        public DataTable ReadExcel(string fileName, string fileExt)
        {
            if (Session["filePath"] != null)
                fileExt = Path.GetExtension(Session["filePath"].ToString());

            using (var stream = File.Open(fileName, FileMode.Open, FileAccess.Read))
            {
                using (var reader = ExcelReaderFactory.CreateReader(stream))
                {
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (tableReader) => new ExcelDataTableConfiguration()
                        {
                            UseHeaderRow = true,
                        }
                    });
                    return result.Tables[0];
                }
            }

            //string conn = string.Empty;
            //DataTable dtexcel = new DataTable();
            //if (fileExt.ToLower().Equals(".xls"))
            //    conn = @"provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + fileName + ";Extended Properties='Excel 8.0;HRD=Yes;IMEX=1';"; //for below excel 2007  
            //else
            //    conn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + fileName + ";Extended Properties=\"Excel 12.0;IMEX=1;HDR=Yes;TypeGuessRows=0;ImportMixedTypes=Text\"";

            //OleDbConnection schemaConnection = new OleDbConnection(conn);
            //schemaConnection.Open();
            //DataTable dtSchema = schemaConnection.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);
            //if (dtSchema == null)
            //{
            //    lblMessage.Text = "Please upload proper file.";
            //    return null;
            //}

            //using (OleDbConnection con = new OleDbConnection(conn))
            //{
            //    try
            //    {
            //        using (var cmd = con.CreateCommand())
            //        {
            //            cmd.CommandText = "SELECT * FROM [" + dtSchema.Rows[0]["TABLE_NAME"].ToString() + "] ";
            //            var adapter = new OleDbDataAdapter(cmd);
            //            adapter.Fill(dtexcel);
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        lblMessage.Text = ex.Message;
            //    }
            //}
            //return dtexcel;
        }

        protected void gvMapping_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                MembersModel model = new MembersModel();

                DropDownList DropDownList1 = (e.Row.FindControl("ddMemberColumn") as DropDownList);
                DropDownList1.DataSource = memberTableColumns;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Column--", "0"));

            }
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            DataTable dataTable = new DataTable();
            foreach (string item in memberTableColumns)
            {
                dataTable.Columns.Add(new DataColumn(item, typeof(string)));
            }

            dataTable.Columns.Add(new DataColumn("ImportedId", typeof(Guid)));
            dataTable.Columns.Add(new DataColumn("ImportedBy", typeof(int)));
            dataTable.Columns.Add(new DataColumn("ImportedDate", typeof(DateTime)));
            dataTable.Columns.Add(new DataColumn("ImportedFileName", typeof(string)));

            MembersModel model = new MembersModel();
            // List<string> MappedColumns = new List<string>();
            int ColumnCount = Convert.ToInt32(hdnColumCount.Value);
            Dictionary<string, string> mappedColumn = new Dictionary<string, string>();


            foreach (GridViewRow row in gvMapping.Rows)
            {
                if (row.Cells[1].Text != string.Empty)
                {
                    DropDownList ddl = (DropDownList)row.FindControl("ddMemberColumn");
                    if (ddl != null && ddl.SelectedValue != "0")
                        mappedColumn.Add(row.Cells[1].Text, ddl.SelectedItem.Value);
                }
            }
            Guid newImportedId = Guid.NewGuid();
            DateTime importedDate = DateTime.Now;
            if (Session["filePath"] != null)
            {
                DataTable dt = ReadExcel(Session["filePath"].ToString(), ".xls");
                foreach (DataRow item in dt.Rows)
                {
                    DataRow newRow = dataTable.NewRow();
                    foreach (var keyValuePair in mappedColumn)
                    {
                        newRow[keyValuePair.Value] = item[keyValuePair.Key];
                    }
                    newRow["ImportedId"] = newImportedId;
                    newRow["ImportedBy"] = UserID;
                    newRow["ImportedDate"] = importedDate;
                    newRow["ImportedFileName"] = Session["ImportedFileName"].ToString();
                    newRow["parlourId"] = ParlourId;
                    dataTable.Rows.Add(newRow);

                }
                if (dt != null && dt.Rows.Count > 0)
                    WriteToDatabase(dataTable, newImportedId);
                else
                    lblMessage.Text = "No record found to import please correct the sheet.";
            }

        }
        private void WriteToDatabase(DataTable dt, Guid newImportedId)
        {
            using (SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["FuneralConnection"].ConnectionString))
            {
                using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                {
                    con.Open();
                    bulkCopy.DestinationTableName = "dbo.MembersRowImport";
                    bulkCopy.ColumnMappings.Add("CreateDate", "CreateDate");
                    bulkCopy.ColumnMappings.Add("MemberType", "MemberType");
                    bulkCopy.ColumnMappings.Add("Title", "Title");
                    bulkCopy.ColumnMappings.Add("Full Names", "Full Names");
                    bulkCopy.ColumnMappings.Add("Surname", "Surname");
                    bulkCopy.ColumnMappings.Add("Gender", "Gender");
                    bulkCopy.ColumnMappings.Add("ID Number", "ID Number");
                    bulkCopy.ColumnMappings.Add("Date Of Birth", "Date Of Birth");
                    bulkCopy.ColumnMappings.Add("Telephone", "Telephone");
                    bulkCopy.ColumnMappings.Add("Cellphone", "Cellphone");
                    bulkCopy.ColumnMappings.Add("Address1", "Address1");
                    bulkCopy.ColumnMappings.Add("Address2", "Address2");
                    bulkCopy.ColumnMappings.Add("Address3", "Address3");
                    bulkCopy.ColumnMappings.Add("Address4", "Address4");
                    bulkCopy.ColumnMappings.Add("Code", "Code");
                    bulkCopy.ColumnMappings.Add("MemeberNumber", "PolicyNumber");
                    bulkCopy.ColumnMappings.Add("MemberSociety", "MemberSociety");
                    bulkCopy.ColumnMappings.Add("Active", "Active");
                    bulkCopy.ColumnMappings.Add("InceptionDate", "InceptionDate");
                    bulkCopy.ColumnMappings.Add("Claimnumber", "Claimnumber");
                    bulkCopy.ColumnMappings.Add("PolicyStatus", "PolicyStatus");
                    bulkCopy.ColumnMappings.Add("parlourid", "parlourid");
                    bulkCopy.ColumnMappings.Add("Agent", "Agent");
                    bulkCopy.ColumnMappings.Add("AccountHolder", "AccountHolder");
                    bulkCopy.ColumnMappings.Add("Bank", "Bank");
                    bulkCopy.ColumnMappings.Add("BranchCode", "BranchCode");
                    bulkCopy.ColumnMappings.Add("Branch", "Branch");
                    bulkCopy.ColumnMappings.Add("AccountNumber", "AccountNumber");
                    bulkCopy.ColumnMappings.Add("AccountType", "AccountType");
                    bulkCopy.ColumnMappings.Add("DebitDate", "DebitDate");
                    bulkCopy.ColumnMappings.Add("MemberBranch", "MemberBranch");
                    bulkCopy.ColumnMappings.Add("CoverDate", "CoverDate");
                    bulkCopy.ColumnMappings.Add("Email", "Email");
                    bulkCopy.ColumnMappings.Add("Citizenship", "Citizenship");
                    bulkCopy.ColumnMappings.Add("Passport", "Passport");
                    bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
                    bulkCopy.ColumnMappings.Add("CustomId1", "CustomId1");
                    bulkCopy.ColumnMappings.Add("CustomId2", "CustomId2");
                    bulkCopy.ColumnMappings.Add("CustomId3", "CustomId3");
                    bulkCopy.ColumnMappings.Add("PlanName", "PlanName");
                    bulkCopy.ColumnMappings.Add("Premium", "Premium");

                    bulkCopy.ColumnMappings.Add("ImportedId", "ImportedId");
                    bulkCopy.ColumnMappings.Add("ImportedBy", "ImportedBy");
                    bulkCopy.ColumnMappings.Add("ImportedDate", "ImportedDate");
                    bulkCopy.ColumnMappings.Add("ImportedFileName", "ImportedFileName");

                    bulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
            try
            {
                MembersBAL.MemberRowImportToMember(hdnMemberName.Value, newImportedId);
                //client.MemberRowImportToMember(hdnMemberName.Value, ImportedId);
                ShowMessage(ref lblMessage, MessageType.Success, "Record imported successfully");
                GridviewData.Visible = false;


            }
            catch (Exception ex)
            {
                lblMessage.Text = ex.Message;
                // Response.Write("<script>alert('Please Mapped The Column Correct');</script>");
            }


        }

        //protected void gvDependentMapping_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    if (e.Row.RowType == DataControlRowType.DataRow)
        //    {

        //        DropDownList DropDownList2 = (e.Row.FindControl("ddDependentColumn") as DropDownList);
        //        DropDownList2.DataSource = dependentTableColumns;
        //        DropDownList2.DataBind();
        //        DropDownList2.Items.Insert(0, new ListItem("--Select Column--", "0"));
        //    }
        //}
    }
}