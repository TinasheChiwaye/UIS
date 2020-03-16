<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Funeral.aspx.cs" Inherits="Funeral.Web.Admin.Funeral" %>

<%@ Register Src="~/UserControl/ctrlFuneralPaymentHistory.ascx" TagName="ucFuneralPaymentHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../Content/plugins/datapicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Deceased Info</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:ValidationSummary runat="server" ID="vSummaryFuneral" ValidationGroup="tab1" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Surname <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtLastName" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtLastName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="tab1" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtLastName" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>full name <em>*</em>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtFirstName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="tab1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>ID Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtIdNumber" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFun();"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtIdNumber" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter id number"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="cvIdvalidation" OnServerValidate="cvIdvalidation_ServerValidate" ControlToValidate="txtIdNumber" ValidationGroup="tab1" runat="server" />
                            </div>
                            <div class="form-group">
                                <label>Date of Birth </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtBirthDay" name="name" type="text" class="form-control" CssClass="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                            </div>
                            <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%><%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            <div class="form-group">
                                <label>Gender</label>

                                <%--<asp:RadioButtonList CssClass="radio radiogroup " RepeatDirection="Horizontal" ID="rbtnlGender" runat="server" Width="156px">
									<asp:ListItem Selected="True" Value="0" Text="Male"></asp:ListItem>
									<asp:ListItem Value="1" Text="Female"></asp:ListItem>
								</asp:RadioButtonList>--%>
                                <br />
                                &nbsp &nbsp
								<asp:RadioButton ID="rdbtnMale" GroupName="Gender" runat="server" Text="Male" Checked="true" />&nbsp &nbsp  &nbsp &nbsp
									<asp:RadioButton ID="rdbtnFemale" GroupName="Gender" runat="server" Text="Female" />

                            </div>
                            <div class="form-group">
                                <label>Cause of Death</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtCOD" name="txtCOD" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Date of Death </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtDOD" name="txtDOD" type="text" class="form-control" CssClass="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Collected From</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtCollection" name="txtCollection" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>BI663 Serial No</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtSerialNo" name="txtSerialNo" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            </div>
                            <div class="form-group">
                                <label>Contact Person</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtContactPerson" name="txtContactPerson" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtContactPerson" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Enter contact person"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Contact Number</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtContactNumber" name="txtContactNumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtContactNumber" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Enter contact number"></asp:RequiredFieldValidator>
                            </div>

                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Street Address <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtStreetAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="tab2" ControlToValidate="txtStreetAddress" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Town or City</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtTown" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Province</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtProvince" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Code</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtCode" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            </div>
                            <div class="form-group">
                                <label>Date of Funeral <em>*</em>  </label>
                                <asp:TextBox CssClass="form-control datepicker" MaxLength="10" runat="server" ID="txtDateOfFuneral" type="text" placeholder="DD/MM/YYYY"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="tab1" Display="None" ControlToValidate="txtDateOfFuneral" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Enter date Of Funeral."></asp:RequiredFieldValidator>

                            </div>
                            <div class="form-group">
                                <label>Time of Funeral</label>

                                <asp:TextBox MaxLength="10" runat="server" ID="txtTIme" name="txtTIme" type="text" class="form-control form-field timepicker1"></asp:TextBox>


                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            </div>
                            <div class="form-group">
                                <label>Driver and Car</label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtDriver" name="txtDriver" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Cemetery / Crematorium</label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtCemetery" name="ttxtCemetery" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Grave No</label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtGraveNo" name="txtGraveNo" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Supported Document</label>
                                <div class="table-responsive">
                                    <asp:GridView ID="gvSupportedDocument" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                        AllowPaging="false" AutoGenerateColumns="False" EmptyDataText="There are no document upload." OnRowCommand="gvSupportedDocument_RowCommand">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField DataField="ImageName" HeaderText="Document" ReadOnly="True" />
                                            <asp:TemplateField HeaderText="Document Type">
                                                <ItemTemplate>
                                                    <%--<asp:Label ID="lbldoctype" runat="server" Text='<%#Eval("DocType")%>'></asp:Label>--%>
                                                    <asp:Label ID="lbldoctype" runat="server" Text='<%# ( Convert.ToInt32(Eval("DocType"))==1?"BI663 Document":(Convert.ToInt32(Eval("DocType"))==2?"Death Certificate":"Other Document")) %>'></asp:Label>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CreateDate" HeaderText="Created date" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                            <asp:TemplateField HeaderText="">
                                                <ItemTemplate>
                                                    &nbsp;
																	<asp:HyperLink runat="server" ToolTip='Download document' ID="hrLink" NavigateUrl='<%# "~/Handler/DocumentHandler.ashx?DocFID="+Eval("pkiFuneralPictureID")%>'><i class="glyphicon glyphicon-download"></i></asp:HyperLink>

                                                    &nbsp;
																	<asp:LinkButton runat="server" ID="lbtnDeleteDocument" ToolTip="Click here To Delete Document" CommandArgument='<%#Eval("pkiFuneralPictureID") %>' OnClientClick="return confirm('Are you sure you want to delete it?')" CommandName="DeleteDocument"><i class="fa fa-trash"></i></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="col-lg-12">
                            <div class="form-group">

                                <div class="btn-group">
                                    <asp:Button ID="btnProcessPayment" OnClick="btnProcessPayment_Click" CssClass="btn btn-sm btn-primary" Enabled="false" runat="server" Text="Process Payment" />
                                </div>

                                <div class="btn-group">
                                    <asp:Button ID="btnViewPayments" CssClass="btn btn-sm btn-primary" OnClick="btnViewPayments_Click" Enabled="false" runat="server" Text="View Payments" />
                                    <button type="button" style="display: none;" id="btnShowPopup" class="btn btn-primary btn-lg"
                                        data-toggle="modal" data-target="#ucPaymentHistory">
                                        View Payment
                                    </button>
                                </div>
                                <div class="btn-group">
                                    <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                        <ContentTemplate>
                                            <asp:Button ID="BtnAddDocs" CssClass="btn btn-sm btn-primary" Enabled="false" runat="server" Text="Add Docs" data-target="#GSCCModal" data-toggle="modal" />
                                        </ContentTemplate>
                                    </asp:UpdatePanel>
                                </div>

                                <div class="btn-group">
                                    <asp:Button ID="btnService" CssClass="btn btn-sm btn-primary" runat="server" Text="Services" OnClick="btnServiceClick" />
                                </div>
                                <div class="btn-group">
                                    <asp:Button ID="btnClear" CssClass="btn btn-sm btn-primary" Enabled="false" runat="server" Text="Clear Section" OnClick="btn_ClearControl" />
                                </div>
                                <div class="btn-group">
                                    <asp:Button ID="btnSave" CssClass="btn btn-sm btn-primary" runat="server" Text="Save" OnClick="btnSaveFuneral_Click" ValidationGroup="tab1" />
                                </div>
                            </div>
                        </div>


                    </div>

                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Funeral List</h5>
                    <div class="ibox-tools">
                        <a class="collapse-link">
                            <i class="fa fa-chevron-up"></i>
                        </a>
                        <a class="dropdown-toggle">
                            <i class="fa fa-wrench"></i>
                        </a>
                        <a class="close-link">
                            <i class="fa fa-times"></i>
                        </a>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-sm-6">
                            <div class="form-group">
                                <div class="input-group">
                                    <label>Show</label>
                                    <asp:DropDownList AutoPostBack="true" ID="ddlPageSize" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlPageSize_SelectedIndexChanged">
                                        <asp:ListItem>10</asp:ListItem>
                                        <asp:ListItem>20</asp:ListItem>
                                        <asp:ListItem>25</asp:ListItem>
                                        <asp:ListItem>50</asp:ListItem>
                                        <asp:ListItem>100</asp:ListItem>
                                        <asp:ListItem>200</asp:ListItem>
                                        <asp:ListItem>250</asp:ListItem>
                                        <asp:ListItem>500</asp:ListItem>
                                    </asp:DropDownList>
                                    entries
                                </div>
                            </div>
                        </div>
                        <div class="col-sm-6">
                            <div class="form-group">
                                <label>Search Plan :</label>

                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
                                        <br />
                                    </span>
                                </asp:Panel>
                            </div>
                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:Label runat="server" ID="Label1"></asp:Label>
                        </div>
                        <asp:Label ID="lblRecords" runat="server" Text=""></asp:Label>
                    </div>
                    <div class="row">
                        <div class="col-lg-12 ">
                            <div class="table-responsive">
                                <asp:GridView ID="gvFuneralList" OnRowDataBound="gvFuneralList_RowDataBound" OnPageIndexChanging="gvFuneral_PageIndexChanging" OnRowCommand="gvFuneral_RowCommand" OnSorting="gvFuneral_Sorting" AllowSorting="true" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" EmptyDataText="There are no Funeral  added.">

                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:BoundField DataField="FullNames" HeaderText="Full Name" SortExpression="FullNames" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="Surname" HeaderText="Surname" SortExpression="Surname" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="IDNumber" HeaderText="ID Number" SortExpression="IDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        <asp:BoundField DataField="DateOfDeath" HeaderText="Date Of Death" SortExpression="DateOfDeath" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}" />
                                        <asp:BoundField DataField="DateOfFuneral" HeaderText="Date Of Funeral" SortExpression="DateOfFuneral" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}" />
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <% if(this.HasEditRight) {%>
                                                <asp:LinkButton runat="server" ID="lbtnEditFunral" ToolTip="Click here To Edite Funeral" CommandArgument='<%#Eval("pkiFuneralID") %>' CommandName="EditFuneral"><i class="fa fa-edit"></i></asp:LinkButton>
                                                &nbsp;
                                                <%}if(this.HasDeleteRight){ %>
											   <asp:LinkButton runat="server" ID="lbtnDeleteFunral" ToolTip="Click here To Delete Funeral" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiFuneralID") %>' CommandName="DeleteFuneral"><i class="fa fa-trash"></i></asp:LinkButton>
                                                &nbsp;
                                                <%}if(this.HasReadRight){ %>
											   <asp:LinkButton runat="server" ID="lbPrintFunral" ToolTip="Click here Print " CommandArgument='<%#Eval("pkiFuneralID") %>' CommandName="PrintFuneral"><i class="fa fa-search"></i></asp:LinkButton>
                                            <%} %>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy',autoclose: true });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        }
    </script>
    <script src="../Scripts/plugins/datapicker/bootstrap-timepicker.min.js"></script>

    <script type="text/javascript">
        $('.timepicker1').timepicker();
    </script>
    <script type="text/jscript">
        $(document).ready(function () {
            var d = new Date,
			dformat = [d.getDate(),
						(d.getMonth() + 1),
						d.getFullYear()].join('/');

            $('#txtTIme').val(dformat);
            //$('#ScheduleTime').val(dformat);           
        })
        function openModal() {
            $('#ucPaymentHistory').modal('show');
        }
    </script>

    <div id="GSCCModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title" id="myModalLabel">Funeral Documnets</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Select Supported Document File</label>
                        <div class="input-group">
                            <asp:FileUpload ID="fuSupportDocument" runat="server" />
                            <asp:RequiredFieldValidator ValidationGroup="file" ControlToValidate="fuSupportDocument" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="File is Empty"></asp:RequiredFieldValidator>
                            <span class="input-group-btn">&nbsp;
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="radio">
                            <asp:RadioButtonList runat="server" ID="rdbdocument" RepeatDirection="Horizontal" CssClass="rbl">
                                <asp:ListItem Value="1" Text="BI663 Document" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Death Certificate"></asp:ListItem>

                            </asp:RadioButtonList>
                        </div>
                    </div>
                    <div class="form-group">
                        <asp:Button ID="btnDocumentSubmit" class="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="file" runat="server" Text="Save Document" OnClick="BtnDocumentSubmit_Click" />
                        <br />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="ucPaymentHistory" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title">Payment history</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <uc:ucFuneralPaymentHistory runat="server" ID="UcFuneralPaymentHistory1" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
    <script>
        Date.prototype.monthNames = [
                  "January", "February", "March",
                  "April", "May", "June",
                  "July", "August", "September",
                  "October", "November", "December"
        ];

        Date.prototype.getMonthName = function () {
            return this.monthNames[this.getMonth()];
        };
        Date.prototype.getShortMonthName = function () {
            return this.getMonthName().substr(0, 3);
        };
        function DateComparisionJavascriptFun() {
            var idNumber = $("#<%=txtIdNumber.ClientID%>").val();
            //alert(idNumber);
            var textLength = idNumber.length;
            if (textLength == 13) {
                // //alert(textLength);
                Validate();
            }
        }

        function Validate() {
            // first clear any left over error messages
            // $('#error p').remove();

            // store the error div, to save typing
            // var error = $('#error');

            var idNumber = $("#<%=txtIdNumber.ClientID %>").val();


            // assume everything is correct and if it later turns out not to be, just set this to false
            var correct = true;

            //Ref: http://www.sadev.co.za/content/what-south-african-id-number-made
            // SA ID Number have to be 13 digits, so check the length
            if (idNumber.length != 13 || !isNumber(idNumber)) {
                //    error.append('<p>ID number does not appear to be authentic - input not a valid number</p>');
                correct = false;
            }

            // get first 6 digits as a valid date

            var tempDate = new Date(idNumber.substring(0, 2), idNumber.substring(2, 4) - 1, idNumber.substring(4, 6));
            var id_date = tempDate.getDate();
            var id_month = tempDate.getMonth();
            var dMonth = id_month + 1;
            var dMonthName = tempDate.getMonthName();
            var id_year = tempDate.getFullYear();

            var fullDate = id_date + " " + dMonthName + " " + id_year;
            if (!((tempDate.getYear() == idNumber.substring(0, 2)) && (id_month == idNumber.substring(2, 4) - 1) && (id_date == idNumber.substring(4, 6)))) {
                //    error.append('<p>ID number does not appear to be authentic - date part not valid</p>');
                correct = false;
            }

            // get country ID for citzenship
            var citzenship = parseInt(idNumber.substring(10, 11)) == 0 ? "Yes" : "No";

            // apply Luhn formula for check-digits
            var tempTotal = 0;
            var checkSum = 0;
            var multiplier = 1;
            for (var i = 0; i < 13; ++i) {
                tempTotal = parseInt(idNumber.charAt(i)) * multiplier;
                if (tempTotal > 9) {
                    tempTotal = parseInt(tempTotal.toString().charAt(0)) + parseInt(tempTotal.toString().charAt(1));
                }
                checkSum = checkSum + tempTotal;
                multiplier = (multiplier % 2 == 0) ? 1 : 2;
            }
            if ((checkSum % 10) != 0) {
                //    error.append('<p>ID number does not appear to be authentic - check digit is not valid</p>');
                correct = false;
            };


            // if no error found, hide the error message
            if (correct) {
                //error.css('display', 'none');

                $("#<%=txtBirthDay.ClientID %>").val(fullDate);
                $("#<%=txtBirthDay.ClientID %>").val(fullDate).datepicker('update');

                var genderCode = idNumber.substring(6, 10);
                var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

                if (gender == "Male") {
                    $("#<%=rdbtnMale.ClientID %>").prop("checked", true)
	         }
	         else {
	             $("#<%=rdbtnFemale.ClientID %>").prop("checked", true)
	         }
         }

         return false;
     }
     function isNumber(n) {
         return !isNaN(parseFloat(n)) && isFinite(n);
     }
    </script>
</asp:Content>
