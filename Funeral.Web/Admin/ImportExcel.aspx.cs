using ExcelDataReader;
using Funeral.BAL;
using Funeral.Model;
using Funeral.Web.App_Start;
using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Web.UI.WebControls;
using System.Xml;

namespace Funeral.Web.Admin
{
    public partial class ImportExcel : AdminBasePage
    {
        private readonly ISiteSettings _siteConfig = new SiteSettings();
        private static readonly List<KeyValue> memberTableColumns = CommonBAL.ColumnList();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCompanyList(ddlSchemeList, dvAdministrator);
                if (!string.IsNullOrEmpty(Request.QueryString["ImportedId"]))
                {
                    GetImportedDetails(Convert.ToInt32(Request.QueryString["ImportedId"].ToString()));
                }
            }
        }
        protected void Submit_Click(object sender, EventArgs e)
        {
            try
            {
                hdnMemberName.Value = txtMainMemberName.Text.Trim();
                if (fn_excelFile.HasFile)
                {
                    string saveFolder = Server.MapPath(@"~/Upload/ImportData/"); //Pick a folder on your machine to store the uploaded files
                    if (!Directory.Exists(saveFolder))
                        Directory.CreateDirectory(saveFolder);

                    string systemDateTimeTicks = DateTime.Now.Ticks.ToString();
                    string filePath = Path.Combine(saveFolder, systemDateTimeTicks + fn_excelFile.FileName);

                    Session["filePath"] = filePath;
                    Session["ImportedFileName"] = systemDateTimeTicks + fn_excelFile.FileName;
                    string fileExt = Path.GetExtension(filePath);
                    fn_excelFile.SaveAs(filePath);
                    btnSubmit.Visible = true;
                    ValidateExcelColumns(filePath);

                    gridDependentMapping.DataSource = CommonBAL.GetUserTypes(false);
                    gridDependentMapping.DataBind();
                }
            }
            catch (Exception ex)
            {
                string msg = "Error occure in File Upload " + ex.Message;
                ShowMessage(ref lblMessage, MessageType.Danger, msg);
                GridviewData.Visible = false;
                btnSubmit.Visible = false;
                DivImportedDataList.Visible = false;
                btnConfirmAndSubmit.Visible = false;
                btnExceptionReport.Visible = false;
                btnPremiumReport.Visible = false;
                lblImportStatus.Visible = false;
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
        }
        protected void gvMapping_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                var List = memberTableColumns.OrderBy(q => q.NameText.ToString()).ToList();
                string gridValue = e.Row.Cells[1].Text.ToLower().Replace(" ", "");
                gridValue = gridValue == "effectivedate" ? "startdate" : gridValue;
                var SelectedValueList = List.FirstOrDefault(p => p.NameText.ToString().Replace(" ", "").ToLower().Equals(gridValue));
                DropDownList DropDownList1 = (e.Row.FindControl("ddMemberColumn") as DropDownList);
                DropDownList1.DataSource = List;
                DropDownList1.DataBind();
                DropDownList1.Items.Insert(0, new ListItem("--Select Column--", "0"));
                if (SelectedValueList != null)
                    DropDownList1.SelectedValue = SelectedValueList.Key.ToString();
            }
        }
        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                DataTable dataTable = new DataTable();
                foreach (KeyValue item in memberTableColumns.OrderBy(q => q.Key).ToList())
                {
                    dataTable.Columns.Add(new DataColumn(item.Key.ToString(), typeof(string)));
                }

                dataTable.Columns.Add(new DataColumn("ImportedId", typeof(Guid)));
                dataTable.Columns.Add(new DataColumn("ImportedBy", typeof(int)));
                dataTable.Columns.Add(new DataColumn("ImportedDate", typeof(DateTime)));
                dataTable.Columns.Add(new DataColumn("ImportedFileName", typeof(string)));
                dataTable.Columns.Add(new DataColumn("IsExecuted", typeof(bool)));

                MembersModel model = new MembersModel();
                // List<string> MappedColumns = new List<string>();
                int ColumnCount = Convert.ToInt32(hdnColumCount.Value);
                Dictionary<string, string> mappedColumn = new Dictionary<string, string>();

                string SelectedColumnValues = string.Empty;
                XmlDocument MappingColumnxml = new XmlDocument();
                XmlElement root = MappingColumnxml.CreateElement("MappingColumn");
                MappingColumnxml.AppendChild(root);
                XmlComment comment = MappingColumnxml.CreateComment("Mapped Column below...");
                root.AppendChild(comment);

                foreach (GridViewRow row in gvMapping.Rows)
                {
                    if (row.Cells[1].Text != string.Empty)
                    {
                        DropDownList ddl = (DropDownList)row.FindControl("ddMemberColumn");
                        if (ddl != null && ddl.SelectedValue != "0")
                        {
                            mappedColumn.Add(row.Cells[1].Text, ddl.SelectedItem.Value);
                            SelectedColumnValues += "[" + ddl.SelectedItem.Value + "],";

                            //mappedColumn Column add into XML
                            XmlElement child = MappingColumnxml.CreateElement("MappedColumns");
                            child.SetAttribute("DatabaseColumn", ddl.SelectedItem.Value.ToString());
                            child.SetAttribute("ExcelColumn", row.Cells[1].Text);
                            root.AppendChild(child);
                        }
                        else
                        {
                            //mappedColumn Column add into XML
                            XmlElement child = MappingColumnxml.CreateElement("MappedColumns");
                            child.SetAttribute("DatabaseColumn", "");
                            child.SetAttribute("ExcelColumn", row.Cells[1].Text);
                            root.AppendChild(child);
                        }
                    }
                }
                hdnSelectedGridColumn.Value = SelectedColumnValues.TrimEnd(',');
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
                        newRow["IsExecuted"] = false;
                        newRow["ImportedFileName"] = Session["ImportedFileName"].ToString();
                        if (ddlSchemeList != null && ddlSchemeList.SelectedValue != "0")
                            newRow["parlourId"] = ddlSchemeList.SelectedValue;
                        else
                            newRow["parlourId"] = ParlourId;
                        dataTable.Rows.Add(newRow);
                    }
                    if (dt != null && dt.Rows.Count > 0 && MappingColumnxml.OuterXml != null)
                    {
                        ToolsSetingBAL.SaveImportedHistory(Session["ImportedFileName"].ToString(), MappingColumnxml.OuterXml, Guid.Parse(ddlSchemeList.SelectedValue), false, UserName, newImportedId, txtMainMemberName.Text.Trim());
                        CopyMemberTableData(dataTable, newImportedId);
                        SaveMappedDependentTypes(newImportedId);
                        BindImportedMembers(newImportedId);
                        hdnnewImportedId.Value = newImportedId.ToString();
                        GridviewData.Visible = false;
                    }
                    else
                        ShowMessage(ref lblMessage, MessageType.Danger, "No record found to import please correct the sheet.");

                }
            }
            catch (Exception ex)
            {
                string msg = "Error occure in Staging " + ex.Message;
                ShowMessage(ref lblMessage, MessageType.Danger, msg);
                GridviewData.Visible = true;
                btnSubmit.Visible = true;
                DivImportedDataList.Visible = false;
                btnConfirmAndSubmit.Visible = false;
                btnExceptionReport.Visible = false;
                btnPremiumReport.Visible = false;
                lblImportStatus.Visible = false;
            }
        }
        private void CopyMemberTableData(DataTable dt, Guid newImportedId)
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
                    bulkCopy.ColumnMappings.Add("MemeberNumber", "MemeberNumber");
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
                    bulkCopy.ColumnMappings.Add("RefNumber", "RefNumber");
                    bulkCopy.ColumnMappings.Add("StartDate", "StartDate");
                    bulkCopy.ColumnMappings.Add("CustomId1", "CustomId1");
                    bulkCopy.ColumnMappings.Add("CustomId2", "CustomId2");
                    bulkCopy.ColumnMappings.Add("CustomId3", "CustomId3");
                    bulkCopy.ColumnMappings.Add("Premium", "Premium");
                    bulkCopy.ColumnMappings.Add("ImportedId", "ImportedId");
                    bulkCopy.ColumnMappings.Add("ImportedBy", "ImportedBy");
                    bulkCopy.ColumnMappings.Add("ImportedDate", "ImportedDate");
                    bulkCopy.ColumnMappings.Add("ImportedFileName", "ImportedFileName");
                    //Plan Staging
                    bulkCopy.ColumnMappings.Add("PlanDesc", "PlanDesc");
                    bulkCopy.ColumnMappings.Add("PlanName", "PlanName");
                    bulkCopy.ColumnMappings.Add("PlanSubscription", "PlanSubscription");
                    bulkCopy.ColumnMappings.Add("WaitingPeriod", "WaitingPeriod");
                    bulkCopy.ColumnMappings.Add("PolicyLaps", "PolicyLaps");
                    bulkCopy.ColumnMappings.Add("Cover", "Cover");
                    bulkCopy.ColumnMappings.Add("PlanUnderwriter", "PlanUnderwriter");
                    bulkCopy.ColumnMappings.Add("JoiningFee", "JoiningFee");
                    bulkCopy.ColumnMappings.Add("UnderwriterId", "UnderwriterId");
                    bulkCopy.ColumnMappings.Add("AgeBand", "AgeBand");
                    bulkCopy.ColumnMappings.Add("MemberType", "RelationType");
                    bulkCopy.ColumnMappings.Add("Age", "Age");
                    bulkCopy.ColumnMappings.Add("AgeFrom", "AgeFrom");
                    bulkCopy.ColumnMappings.Add("AgeTo", "AgeTo");
                    bulkCopy.ColumnMappings.Add("UnderwriterCover", "UnderwriterCover");
                    bulkCopy.ColumnMappings.Add("IsExecuted", "IsExecuted");
                    bulkCopy.ColumnMappings.Add("UnderwriterPremium", "UnderwriterPremium");
                    bulkCopy.WriteToServer(dt);
                    con.Close();
                }
            }
        }
        private void BindImportedMembers(Guid newImportedId)
        {
            var IsImported = ToolsSetingBAL.GetImportedHistory_ByNewImportedId(newImportedId);
            ImportedDataGriview.DataSource = ToolsSetingBAL.GetImportedMemberList(newImportedId, hdnSelectedGridColumn.Value);
            ImportedDataGriview.DataBind();
            if (ImportedDataGriview.Rows.Count > 0)
            {
                ImportedDataGriview.UseAccessibleHeader = true;
                ImportedDataGriview.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            GridviewData.Visible = false;
            btnSubmit.Visible = false;
            DivImportedDataList.Visible = true;
            //btnConfirmAndSubmit.Visible = IsImported == null ? true : (IsImported.IsImported == true ? false : true);
            btnConfirmAndSubmit.Visible = true;
            btnExceptionReport.Visible = true;
            btnPremiumReport.Visible = true;
            lblImportStatus.Visible = true;
            lblImportStatus.Text = IsImported.Status;

            if (IsImported.Status.StartsWith("Data Staging Imported"))
                btnConfirmAndSubmit.Enabled = true;
            else
                btnConfirmAndSubmit.Enabled = false;
        }
        protected void btnConfirmAndSubmit_Click(object sender, EventArgs e)
        {
            try
            {
                btnConfirmAndSubmit.Enabled = false;
                if (!string.IsNullOrEmpty(hdnnewImportedId.Value))
                {
                    MembersBAL.MakeReadyForImportMember(Guid.Parse(hdnnewImportedId.Value));
                    var ImportStatus = ToolsSetingBAL.GetImportedHistory_ByNewImportedId(Guid.Parse(hdnnewImportedId.Value));

                    //SavePlanCreatorStaging(Guid.Parse(hdnnewImportedId.Value));
                    //DataTable dt = GetDataTable_FromGridView(ImportedDataGriview);
                    //if (dt != null)
                    //{
                    //    if (dt.Rows.Count > 0)
                    //    {
                    //        ToolsSetingBAL.UpdateMemberStagingTable(dt);
                    //        //SavePlanCreatorStaging(Guid.Parse(hdnnewImportedId.Value));
                    //        MembersBAL.SaveMemberStaging(hdnMemberName.Value, Guid.Parse(hdnnewImportedId.Value));
                    //        ShowMessage(ref lblMessage, MessageType.Success, "Record imported successfully");
                    //        btnConfirmAndSubmit.Visible = false;
                    //        DivImportedDataList.Visible = false;
                    //        ToolsSetingBAL.SaveImportedHistory(null, null, Guid.Parse(ddlSchemeList.SelectedValue), true, UserName, Guid.Parse(hdnnewImportedId.Value), null);
                    //    }
                    //}
                    //else
                    //{
                    //    ShowMessage(ref lblMessage, MessageType.Danger, "Record could not imported");
                    //}
                    ShowMessage(ref lblMessage, MessageType.Success, "Data is ready for import");
                    
                    lblImportStatus.Text = ImportStatus.Status;
                }
                else
                {
                    ShowMessage(ref lblMessage, MessageType.Danger, "Record could not imported");
                }
            }
            catch (Exception ex)
            {
                string msg = "Error occure in Confirm " + ex.Message;
                ShowMessage(ref lblMessage, MessageType.Danger, msg);
                btnSubmit.Visible = false;
                GridviewData.Visible = false;
                DivImportedDataList.Visible = true;
                btnConfirmAndSubmit.Visible = false;
                btnExceptionReport.Visible = true;
                btnPremiumReport.Visible = true;
                lblImportStatus.Visible = true;
                
            }
        }
        public void SavePlanCreatorStaging(Guid hdnnewImportedId)
        {
            try
            {
                var UserTypeList = CommonBAL.GetUserTypes();
                var mappedDependent = ToolsSetingBAL.GetMappedDependent(hdnnewImportedId);
                var getPlanStagingList = ToolsSetingBAL.AddPlansAndGetPlanList_Staging(hdnnewImportedId);
                if (getPlanStagingList != null)
                {
                    if (getPlanStagingList.Count > 0)
                    {
                        foreach (var item in getPlanStagingList)
                        {
                            UserType SystemMemberType = new UserType();
                            MappedDependents getMemberType = mappedDependent.FirstOrDefault(x => x.ExcelTypeName.ToString().ToLower().Trim() == item.RelationType.ToLower().Trim());
                            if (getMemberType != null)
                            {
                                SystemMemberType = UserTypeList.FirstOrDefault(x => x.UserTypeName.ToString().ToLower().Trim() == getMemberType.SystemTypeName.ToString().ToLower().Trim());
                            }
                            var splitAge = item.AgeBand.Split('-').Select(x => Convert.ToInt32(Regex.Replace(x.ToString().Replace("+", ""), "[^0-9]", ""))).ToList();
                            if (splitAge.Count > 0)
                            {
                                item.RelationTypeId = SystemMemberType == null ? 1 : (SystemMemberType.UserTypeId == 0 ? 1 : SystemMemberType.UserTypeId);
                                item.CreatedBy = UserName;
                                item.FromAge = splitAge[0];
                                item.ToAge = splitAge.Count == 2 ? splitAge[1] : Int16.MaxValue;
                                ToolsSetingBAL.SavePlanCreatorStagingDetails(item);
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                string msg = "Error occure in Plan Creator " + e.Message;
                ShowMessage(ref lblMessage, MessageType.Danger, msg);
            }
        }
        protected void ImportedDataGriview_OnPageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            if (hdnnewImportedId.Value != "")
            {
                ImportedDataGriview.PageIndex = e.NewPageIndex;
                BindImportedMembers(Guid.Parse(hdnnewImportedId.Value));
            }
        }


        protected void ImportedDataGriview_OnRowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                int ColumnCount = 0;
                for (int i = 0; i < e.Row.Cells.Count; i++)
                {
                    DataRowView dr = (DataRowView)e.Row.DataItem;
                    string getCellValue = dr[i].ToString();
                    string HeaderName = ImportedDataGriview.HeaderRow.Cells[i].Text.Replace(" ", "").ToLower();
                    if (HeaderName.Contains("date"))
                    {
                        TextBox txtCountry = new TextBox();
                        txtCountry.ID = "txtMasterTexbox" + i + 1;
                        txtCountry.ReadOnly = true;
                        Label lblValidation = new Label();
                        if (getCellValue != "")
                        {
                            DateTime startDate;
                            try
                            {
                                if (!DateTime.TryParse(getCellValue, out startDate))
                                {
                                    txtCountry.Text = getCellValue;
                                    txtCountry.Attributes.Add("class", "txtGridViewdatetimepicker");
                                    lblValidation.Attributes.Add("class", "text text-danger");
                                    lblValidation.Text = "Please Validate Column!";
                                    txtCountry.Attributes.Add("onchange", "CheckEnterDateValidate(this)");
                                    //txtCountry.ReadOnly = true;

                                    txtCountry.Attributes.Add("runat", "server");
                                    e.Row.Cells[i].Text = getCellValue;
                                    e.Row.Cells[i].Controls.Add(txtCountry);
                                    e.Row.Cells[i].Controls.Add(lblValidation);
                                }
                                else
                                {
                                    txtCountry.Text = getCellValue;
                                    e.Row.Cells[i].Controls.Add(txtCountry);
                                }
                            }
                            catch (Exception ex)
                            {
                                txtCountry.Text = getCellValue;
                                e.Row.Cells[i].Controls.Add(txtCountry);
                            }
                        }
                        else
                        {
                            txtCountry.Text = getCellValue;
                            txtCountry.Attributes.Add("style", "display:none");
                            e.Row.Cells[i].Controls.Add(txtCountry);
                        }

                    }
                    else if (HeaderName.Equals("isexecuted"))
                    {
                        CheckBox chk = (CheckBox)e.Row.Cells[i].Controls[0];
                        chk.Visible = false;
                        Label lblValidation = new Label();
                        bool IsExecuted = getCellValue == "" ? false : Convert.ToBoolean(getCellValue);
                        string ErrorMessage = dr[i + 1].ToString();
                        if (!IsExecuted && !string.IsNullOrEmpty(ErrorMessage))
                        {
                            lblValidation.Text = "Record has an Error";
                            lblValidation.Attributes.Add("class", "label label-danger");
                        }
                        else if (!IsExecuted)
                        {
                            lblValidation.Text = "Execution Pending";
                            lblValidation.Attributes.Add("class", "label label-primary");
                        }
                        else
                        {
                            lblValidation.Text = "Yes";
                            lblValidation.Attributes.Add("class", "label label-primary");
                        }
                        e.Row.Cells[i].Text = IsExecuted.ToString();
                        e.Row.Cells[i].Controls.Add(lblValidation);

                        TextBox txt = new TextBox() { ID = "txtMasterTexbox", Text = getCellValue };
                        txt.Attributes.Add("style", "display:none");
                        e.Row.Cells[i].Controls.Add(txt);
                    }
                    else
                    {
                        Label lblValidation = new Label();
                        lblValidation.Text = getCellValue;
                        e.Row.Cells[i].Controls.Add(lblValidation);

                        TextBox txt = new TextBox() { ID = "txtMasterTexbox" + i + 1, Text = getCellValue };
                        txt.Attributes.Add("style", "display:none");
                        e.Row.Cells[i].Controls.Add(txt);

                    }
                    ColumnCount++;
                }
            }
        }
        private void GetImportedDetails(int ImportedId)
        {
            try
            {
                ImportedHistory getHistory = ToolsSetingBAL.GetImportedHistory_ByImportedId(ImportedId);
                if (getHistory != null)
                {
                    string dbColumnName = string.Empty;
                    var GetAttributesColumn = ToolsSetingBAL.GetXMLColumn(getHistory.MappingColumn);
                    if (GetAttributesColumn.Item1 != null)
                    {
                        dbColumnName = "[" + String.Join("],[", GetAttributesColumn.Item1.Where(s => !string.IsNullOrEmpty(s))).ToString() + "]";
                    }
                    hdnSelectedGridColumn.Value = dbColumnName;
                    hdnnewImportedId.Value = getHistory.NewImportedId.ToString();
                    hdnMemberName.Value = getHistory.MemberType;
                    txtMainMemberName.Text = getHistory.MemberType;
                    BindImportedMembers(getHistory.NewImportedId);
                    //btnConfirmAndSubmit.Enabled = getHistory.ReadyToImport = true ? true : false;
                    if (getHistory.Status.StartsWith("Data Staging Imported"))
                        btnConfirmAndSubmit.Enabled = true;
                    else
                        btnConfirmAndSubmit.Enabled = false;
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        private void SaveMappedDependentTypes(Guid ImportedId)
        {
            foreach (GridViewRow row in gridDependentMapping.Rows)
            {
                TextBox txtSystemType = (TextBox)row.FindControl("txtSystemType");
                TextBox txtExcelType = (TextBox)row.FindControl("txtDependentType");
                if (txtExcelType.Text != string.Empty)
                {
                    ToolsSetingBAL.SaveMappedDependents(txtSystemType.Text.Trim(), txtExcelType.Text.Trim(), Guid.Parse(ddlSchemeList.SelectedValue), ImportedId, UserName);
                }
            }
        }
        private DataTable GetDataTable_FromGridView(GridView dtg)
        {
            DataTable dt = new DataTable();
            if (dtg.HeaderRow != null)
            {
                for (int i = 0; i < dtg.HeaderRow.Cells.Count; i++)
                {
                    dt.Columns.Add(dtg.HeaderRow.Cells[i].Text);
                }
            }
            var Column1TextBoxes = Request.Form.AllKeys.Where(k => k.Contains("txtMasterTexbox")).ToList();
            int txt = 0;
            foreach (GridViewRow row in dtg.Rows)
            {
                DataRow dr;
                dr = dt.NewRow();
                for (int i = 0; i < row.Cells.Count; i++)
                {
                    dr[i] = Request.Form[Column1TextBoxes[txt]];
                    txt++;
                }
                dt.Rows.Add(dr);
            }

            DataRow[] dataRows = dt.Select("IsExecuted = " + false);
            DataTable dt1 = dataRows.CopyToDataTable();
            return dt1;
        }

        protected void btnExceptionReport_Click(object sender, EventArgs e)
        {
            
            string ExportTypeExtensions = "xls";
            string ReportName = "Copy of ARL Import Data Exception Report";//Copy of ARL Import Data Exception Report - ARL Import Data Exception Report
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            ReportViewer rpw = new ReportViewer();
            rpw.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new MyReportServerCredentials();
            rpw.ServerReport.ReportServerCredentials = irsc;


            rpw.ProcessingMode = ProcessingMode.Remote;
            rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ReportName;
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("Parlourid", ddlSchemeList.SelectedValue));
            reportParameters.Add(new ReportParameter("Importid", hdnnewImportedId.Value));
            rpw.ServerReport.SetParameters(reportParameters);
            byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            filename = string.Format("{0}.{1}", ReportName, ExportTypeExtensions);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }

        protected void btnPremiumReport_Click(object sender, EventArgs e)
        {

            string ExportTypeExtensions = "xls";
            string ReportName = "UIS Import Premium Report";//Copy of ARL Import Data Exception Report - ARL Import Data Exception Report
            Warning[] warnings;
            string[] streamids;
            string mimeType;
            string encoding;
            string extension;
            string filename;

            ReportViewer rpw = new ReportViewer();
            rpw.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new MyReportServerCredentials();
            rpw.ServerReport.ReportServerCredentials = irsc;


            rpw.ProcessingMode = ProcessingMode.Remote;
            rpw.ServerReport.ReportServerUrl = new Uri(_siteConfig.SSRSUrl);
            rpw.ServerReport.ReportPath = "/" + _siteConfig.SSRSFolderName + "/" + ReportName;
            ReportParameterCollection reportParameters = new ReportParameterCollection();
            reportParameters.Add(new ReportParameter("DateFrom", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("DateTo", DateTime.Now.ToString("yyyy/MM/dd")));
            reportParameters.Add(new ReportParameter("Parlourid", ddlSchemeList.SelectedValue));
            reportParameters.Add(new ReportParameter("Importid", hdnnewImportedId.Value));
            rpw.ServerReport.SetParameters(reportParameters);
            byte[] bytes = rpw.ServerReport.Render("Excel", null, out mimeType, out encoding, out extension, out streamids, out warnings);
            filename = string.Format("{0}.{1}", ReportName, ExportTypeExtensions);

            Response.ClearHeaders();
            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment;filename=" + filename);
            Response.ContentType = mimeType;
            Response.BinaryWrite(bytes);
            Response.Flush();
            Response.End();

        }
    }
}