<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Claims.aspx.cs" Inherits="Funeral.Web.Admin.Claims" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <link href="../Content/plugins/datapicker/bootstrap-timepicker.min.css" rel="stylesheet" />
    <style type="text/css">
        .rbl input[type="radio"] {
            margin-left: 10px;
            margin-right: 1px;
        }

        .modal-dialog {
            width: 1000px;
        }

        input[type=radio] {
            margin-left: 4px !important;
        }

        em {
            color: red;
        }


        .popup, #popup {
            position: fixed;
            width: 350px;
            height: 171px;
            top: 24%;
            left: 33%;
            margin: 0 auto;
            text-align: center;
            /*z-index: 2003;*/
            background-color: white;
            display: none;
        }

        #poptable {
            margin: 25px;
        }

            #poptable td, #poptable tr, #poptable td input[type=text] {
                margin: 10px;
            }

        .noteHead {
            width: 100%;
        }

            .noteHead h3 {
                float: left;
            }

        .noteDate {
            float: right;
            margin-right: 5px;
        }

        .noteContainer {
            margin-top: 44px;
            float: left;
        }

        #ddlDependencyTypeFormInput, #ddlDependencyRelationshipFormInput {
            max-width: 60% !important;
        }

        #NotePopUp {
            width: 50%;
            height: 50%;
            z-index: 5000;
            position: fixed;
            background-color: white;
            top: 22%;
            left: 25%;
            padding: 12px;
            display: none;
        }

        #NotePopUpWrap {
            height: 100%;
            width: 100%;
            position: fixed;
            z-index: 4000;
            background-color: black;
            opacity: .7;
            display: none;
            top: 0;
            left: 0;
        }

        .NoteCss {
            float: right;
            margin-right: 50Px;
        }

        .modalPopup {
            top: 20%;
            background-color: #FFFFFF;
            width: 500px;
            height: auto;
            border-radius: 5px;
            position: fixed;
            font-size: 12px;
            margin: 0 auto;
            /*z-index: 1003;*/
        }

        .modal {
            width: auto;
            /*top: 22% !important;*/
            /*left: 16% !important;*/
        }

        #TaskFollowUpModel {
            left: 12% !important;
        }

        /*#AgentInfoModel {
            left: 13% !important;
        }*/

        .PanelHeder {
            background-color: red;
            color: white;
            padding: 10px !important;
            margin: -10px !important;
        }

        .TabColor {
            background-color: #ED1C24;
            color: aliceblue;
        }

        td {
            cursor: pointer;
        }

        input[type="radio"] {
            margin: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="scriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:HiddenField runat="server" ID="hdnId" />
            <asp:HiddenField ID="TabName" runat="server" />
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Claim Capture</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="panel blank-panel">
                            <div class="panel-heading">
                                <div class="panel-options" id="Tabs">
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active" id="tab4"><a data-toggle="tab" href="#tab-4" aria-expanded="true">Claim Logged</a></li>
                                        <li class="" id="tab1"><a data-toggle="tab" href="#tab-1" aria-expanded="false">Claims Details</a></li>
                                        <li class="" id="tab2"><a data-toggle="tab" href="#tab-2" aria-expanded="false">Claimant Details</a></li>
                                        <li class="" id="tab3"><a data-toggle="tab" href="#tab-3" aria-expanded="false">Deceased Details</a></li>

                                    </ul>
                                </div>
                            </div>
                            <div class="panel-body">
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Member</h5>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-lg-4">
                                                            <div class="form-group">
                                                                <asp:CheckBox ID="chkMember" runat="server" Text="Claiming For Member" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    <asp:CheckBox ID="chkWait" runat="server" Text="Apply Waiting Period?" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-2">
                                                            <div class="form-group" style="text-align: right; vertical-align: middle;">
                                                                <label></label>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-4">
                                                            <div class="form-group">

                                                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                                                    <span class="input-group-btn">&nbsp;<asp:TextBox ID="txtKeyword" runat="server" CssClass="form-control" placeholder="Search by keyword" Width="200px"></asp:TextBox>

                                                                        <%--                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                            <ContentTemplate>--%>
                                                                        <asp:Button ID="btnSearch" class="pull-right" CssClass="btn btn-sm btn-primary" runat="server" Text="Search" OnClick="btnSearch_Click" />
                                                                        <%--<asp:Button ID="btnSearch" class="pull-right" CssClass="btn btn-sm btn-primary" runat="server" Text="Search" data-target="#GSCCModal" data-toggle="modal" OnClick="btnSearch_Click" />--%>
                                                                        <%-- </ContentTemplate>
                                                                        </asp:UpdatePanel>--%>
                                                                    </span>
                                                                    &nbsp;&nbsp;
                                                                </asp:Panel>
                                                            </div>
                                                        </div>

                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Claim Details</h5>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="col-lg-6">
                                                            <div class="form-group">
                                                                <label>Claim Date</label>
                                                                <asp:TextBox MaxLength="30" runat="server" ID="txtClaimDate" name="txtCauseOfDeath1" type="text" class="form-control" CssClass="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Cause of Death</label>
                                                                <asp:TextBox MaxLength="30" runat="server" ID="txtCauseOfDeath" name="txtVin" type="text" class="form-control" onchange="CODtextchange();"></asp:TextBox>
                                                                <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Claim Notes</label>
                                                                <asp:TextBox runat="server" ID="txtClaimNotes" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                                            </div>

                                                        </div>
                                                        <div class="col-lg-6">

                                                            <div class="form-group">
                                                                <label>Claim No.</label>
                                                                <asp:TextBox MaxLength="10" runat="server" ID="txtClaimNo" name="txtReg" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab1" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^[0-9]*$" />--%>
                                                            </div>
                                                            <div class="form-group">
                                                                <label>Cover Amount</label>
                                                                <asp:TextBox MaxLength="30" runat="server" ID="txtCoverAmt" name="txtVin" type="text" ReadOnly="true" class="form-control"></asp:TextBox>
                                                                <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group">
                                                                    <asp:CheckBox ID="ckbHostingFuneral" runat="server" Text="Hosting Funeral" />

                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group">
                                                                    <asp:CheckBox ID="chkAddpayout" runat="server" Text="Add Payout" />
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-3">
                                                                <div class="form-group">
                                                                    <div id="forhide">
                                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtAddpayout" name="txtVin" class="form-control"></asp:TextBox>
                                                                    </div>
                                                                    <asp:RegularExpressionValidator ID="rfeAddpayout" runat="server" ControlToValidate="txtAddpayout"
                                                                        ErrorMessage="Please Enter Only Numbers" Display="Dynamic" ValidationExpression="[0-9]*\.?[0-9]*"></asp:RegularExpressionValidator>
                                                                </div>
                                                            </div>
                                                        </div>
                                                        <div class="col-lg-6">

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
                                                                    <asp:HyperLink runat="server" ToolTip='Download document' ID="hrLink" NavigateUrl='<%# "~/Handler/DocumentHandler.ashx?DocClaimID="+Eval("pkiClaimPictureID")%>'><i class="glyphicon glyphicon-download"></i></asp:HyperLink>

                                                                                    &nbsp;
                                                                    <asp:LinkButton runat="server" ID="lbtnDeleteDocument" ToolTip="Click here To Delete Document" CommandArgument='<%#Eval("pkiClaimPictureID") %>' OnClientClick="return confirm('Are you sure you want to delete it?')" CommandName="DeleteClaimsdocumentById"><i class="fa fa-trash"></i></asp:LinkButton>
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

                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-content">

                                                        <div class="form-group">
                                                            <div class="btn-group">
                                                                <asp:Button runat="server" ID="btnPrintClaim" CssClass="btn btn-sm btn-primary" Text="Print Claim" OnClick="btnPrintClaim_click" Enabled="false" />
                                                                <%--  <asp:Button runat="server" ID="btnPrintClaim" CssClass="btn btn-sm btn-primary" Text="Print Claim" Enabled="false" />--%>
                                                            </div>
                                                            <div class="btn-group">
                                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>
                                                                        <asp:Button runat="server" ID="btnAddDocs" CssClass="btn btn-sm btn-primary" Text="Add Docs" data-target="#AddDocModal" data-toggle="modal" />
                                                                    </ContentTemplate>
                                                                </asp:UpdatePanel>
                                                            </div>
                                                            <div class="btn-group">
                                                                <%-- <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                    <ContentTemplate>--%>
                                                                <asp:Button runat="server" ID="btnPaymentHistory" CssClass="btn btn-sm btn-primary" Text="Payment History" Enabled="true" data-target="#PopPaymentHistory" data-toggle="modal" OnClick="btnPaymentHistory_click" />
                                                                <%-- </ContentTemplate>
                                                                </asp:UpdatePanel>--%>
                                                            </div>
                                                            <div class="btn-group">
                                                                <asp:Button runat="server" ID="btnClaimPayments" CssClass="btn btn-sm btn-primary" Text="Claim Payments" Enabled="false" />
                                                            </div>
                                                            <div class="btn-group">
                                                                <asp:Button runat="server" ID="btnSearchClaims" CssClass="btn btn-sm btn-primary" Text="Clear" OnClick="btnClear_Click" />
                                                            </div>
                                                            <%-- <div class="btn-group">
                                                                <asp:Button runat="server" ID="btnSearchClaims" CssClass="btn btn-sm btn-primary" Text="Search" OnClick="btnSearchClaims_Click" />
                                                            </div>--%>
                                                            <div class="btn-group">
                                                                <asp:Button runat="server" ID="btnNextStep" CssClass="btn btn-sm btn-primary" Text="Next" OnClick="btnNextTb1_Click" />
                                                            </div>

                                                        </div>
                                                    </div>

                                                    <%-- For Member GridView--%>
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvMembers" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvMembers_RowCommand">
                                                            <PagerStyle CssClass="pagination-ys" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Member name" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMainMemberName" runat="server" Text='<%#Eval("Surname")+ " " + Eval("FullNames")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Gender" HeaderText="Gender" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                                <asp:TemplateField HeaderText="ID Number" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblMainMemberIdnumber" runat="server" Text='<%#Eval("IDNumber")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Cellphone" HeaderText="Contact No" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lbtnSelect" CommandArgument='<%#Eval("pkiMemberID")%>' CommandName="SelectMainMember">Select</asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>
                                                    <%-- For Depedent GridView--%>
                                                    <div class="form-group">
                                                        <asp:GridView ID="gvDependent" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" OnRowCommand="gvDependent_RowCommand">
                                                            <PagerStyle CssClass="pagination-ys" />
                                                            <Columns>
                                                                <asp:TemplateField HeaderText="Dependent name" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepedentName" runat="server" Text='<%#Eval("Surname")+ " " + Eval("FullName")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Gender" HeaderText="Gender" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                                <asp:TemplateField HeaderText="ID Number" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDepedentIdnumber" runat="server" Text='<%#Eval("IDNumber")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                                <asp:BoundField DataField="Cellphone" HeaderText="Contact No" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                                <asp:TemplateField HeaderText="">
                                                                    <ItemTemplate>
                                                                        <asp:LinkButton runat="server" ID="lbtnSelect" CommandArgument='<%#Eval("pkiDependentID")%>' CommandName="SelectDependMember">Select</asp:LinkButton>

                                                                    </ItemTemplate>
                                                                </asp:TemplateField>
                                                            </Columns>
                                                        </asp:GridView>
                                                    </div>


                                                </div>
                                            </div>


                                        </div>
                                    </div>
                                    <%--<End Tab 1>--%>

                                    <div id="tab-2" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary3" ValidationGroup="tab2" ForeColor="Red" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Title </label>
                                                    <asp:DropDownList runat="server" ID="ddlClaimantTitle" class="form-control m-b">
                                                        <asp:ListItem Value="Select">Select</asp:ListItem>
                                                        <asp:ListItem Value="Mr">Mr</asp:ListItem>
                                                        <asp:ListItem Value="Dr">Dr</asp:ListItem>
                                                        <asp:ListItem Value="Miss">Miss</asp:ListItem>
                                                        <asp:ListItem Value="Mrs">Mrs</asp:ListItem>
                                                        <asp:ListItem Value="Prof">Prof</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Surname <em>*</em> </label>
                                                    <asp:TextBox MaxLength="25" runat="server" ID="txtClaimantLastName" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab2" ControlToValidate="txtClaimantLastName" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="tab2" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtClaimantLastName" ValidationExpression="[a-zA-Z ]*$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>full name  <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="25" runat="server" ID="txtClaimantFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab2" ControlToValidate="txtClaimantFirstname" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab2" runat="server" ControlToValidate="txtClaimantFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>ID Number <em>*</em>  </label>

                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtClaimantIdNumber" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFun();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab2" ControlToValidate="txtClaimantIdNumber" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter ID Number"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="cvIdvalidation" OnServerValidate="cvIdvalidation_ServerValidate" ControlToValidate="txtClaimantIdNumber" ValidationGroup="tab2" runat="server" />
                                                    <%-- <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="tab2" runat="server" ErrorMessage="Id Number Wrong" ControlToValidate="txtClaimantIdNumber" ValidationExpression="(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))" />--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date of Birth </label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtClaimantDateOfBirth" name="name" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>


                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Street Address <em>*</em> </label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtClaimantAddress1" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab2" ControlToValidate="txtClaimantAddress1" ID="RequiredFieldValidator8" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Town or City</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtClaimantAddress2" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtTown" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Province</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtClaimantAddress3" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtProvince" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Code</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtClaimantCode" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCode" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Contact Number</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtClaimantContactNumber" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Beneficiery Banking Detaails</h5>
                                                    </div>
                                                    <div class="col-lg-6">


                                                        <div class="form-group">
                                                            <label>Bank Name</label>
                                                            <%--<asp:TextBox MaxLength="255" runat="server" ID="txtbank" name="name" type="text" class="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList ID="ddlBank" runat="server" DataTextField="BankName" DataValueField="BranchCode" CssClass="form-control m-b"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtbank" ID="RequiredFieldValidator21" ForeColor="red" runat="server" ErrorMessage="Please enter bank name" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Bank Branch</label>
                                                            <asp:TextBox MaxLength="50" runat="server" ID="txtBranch" name="name" type="text" class="form-control"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranch" ID="RequiredFieldValidator24" ForeColor="red" runat="server" ErrorMessage="Please enter bank branch" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                        <div class="form-group">
                                                            <label>Branch Code</label>
                                                            <asp:TextBox MaxLength="50" runat="server" ID="txtBranchcode" name="name" type="text" class="form-control"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranchcode" ID="RequiredFieldValidator25" ForeColor="red" runat="server" ErrorMessage="Please enter branch code" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                    </div>
                                                    <div class="col-lg-6">
                                                        <div class="form-group">
                                                            <label>Account Holder</label>
                                                            <asp:TextBox MaxLength="50" runat="server" ID="txtAccountholder" name="name" type="text" class="form-control"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountholder" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter valid account number" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Account Number</label>
                                                            <asp:TextBox MaxLength="50" runat="server" ID="txtAccountno" name="name" type="text" class="form-control"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountno" ID="RequiredFieldValidator26" ForeColor="red" runat="server" ErrorMessage="Please enter account number" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Account Type</label>
                                                            <%--<asp:TextBox MaxLength="255" runat="server" ID="txtAccounttype" name="name" type="text" class="form-control"></asp:TextBox>--%>
                                                            <asp:DropDownList runat="server" ID="ddlAccountType" DataTextField="AccountType" DataValueField="AccountTypeID" class="form-control"></asp:DropDownList>
                                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccounttype" ID="RequiredFieldValidator27" ForeColor="red" runat="server" ErrorMessage="Please eneter account type" Display="None"></asp:RequiredFieldValidator>--%>
                                                        </div>

                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(1);">Back</button>

                                                        <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(3);">Next</button>--%>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnResetTab2" runat="server" Text="Reset" OnClick="btnResetTab_Click" />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnTab2" ValidationGroup="tab2" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Next" OnClick="btnTab2_Click" />
                                                    </div>

                                                </div>

                                            </div>
                                        </div>
                                    </div>


                                    <div id="tab-3" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary2" ValidationGroup="tab3" ForeColor="Red" />
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
                                                    <label>full name  <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="25" runat="server" ID="txtFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtFirstName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="tab1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>ID Number <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtIdNumber" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascript();"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtIdNumber" ID="RequiredFieldValidator91" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtIdNumber" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Id Number"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="CustomValidator1" OnServerValidate="cvIdvalidation_ServerValidate2" ControlToValidate="txtIdNumber" ValidationGroup="tab3" runat="server" />
                                                </div>

                                                <div class="form-group">
                                                    <label>Date of Birth </label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtBirthDay" name="name" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
                                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%><%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                <div class="form-group">
                                                    <label>Gender</label>
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
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDOD" name="txtDOD" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Collected From</label>
                                                    <asp:TextBox MaxLength="100" runat="server" ID="txtCollection" name="txtCollection" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>BI663 Serial No</label>
                                                    <asp:TextBox MaxLength="15" runat="server" ID="txtSerialNo" name="txtSerialNo" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                </div>

                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Street Address <em>*</em> </label>
                                                    <asp:TextBox MaxLength="100" runat="server" ID="txtStreetAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="txtStreetAddress" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>
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
                                                    <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtDateOfFuneral" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Enter date Of Funeral."></asp:RequiredFieldValidator>

                                                </div>
                                                <div class="form-group">
                                                    <label>Time of Funeral</label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtTime" name="txtTime" type="text" class="form-control form-field timepicker1"></asp:TextBox>

                                                </div>
                                                <div class="form-group">
                                                    <label>Driver and Car</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtDriver" name="txtDriver" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Cemetery / Crematorium</label>
                                                    <asp:TextBox MaxLength="500" runat="server" ID="txtCemetery" name="ttxtCemetery" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Grave No</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtGraveNo" name="txtGraveNo" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(2);">Back</button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnResetTab3" runat="server" Text="Reset" OnClick="btnResetTab_Click" />

                                                    </div>
                                                    <div class="btn-group">
                                                        <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(4);">Next</button>--%>
                                                        <asp:Button runat="server" ID="btnSave" ValidationGroup="tab3" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Save" OnClick="btnSave_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tab-4" class="tab-pane active">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="tab4" ForeColor="Red" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Date From: </label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtDateFrom" name="name" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Date to: </label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtDateTo" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Status </label>
                                                    <asp:DropDownList runat="server" ID="ddlStatusSearch" DataTextField="Status" DataValueField="Status" CssClass="form-control">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6" runat="server" id="dvAdministrator">
                                                <div class="form-group">
                                                    <label>Company </label>
                                                    <asp:DropDownList ID="ddlCompanyList" CssClass="form-control" runat="server" OnSelectedIndexChanged="ddlCompanyList_SelectedIndexChanged"></asp:DropDownList>
                                                </div>
                                            </div>
                                            <%--claim Details--%>
                                            <div class="ibox-title">
                                                <div class="col-lg-6">
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
                                                <div class="col-lg-4">
                                                    <div class="form-group">
                                                        <asp:Panel ID="Panel1" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                                            <span class="input-group-btn">&nbsp;<asp:TextBox ID="txtClaimKeyword" runat="server" CssClass="form-control" placeholder="Search by keyword" Width="200px"></asp:TextBox>
                                                                <asp:Button ID="btnClaimSearch" class="pull-right" CssClass="btn btn-sm btn-primary" runat="server" Text="Search" OnClick="btnClaimsSearch_Click" />
                                                            </span>
                                                            &nbsp;&nbsp;
                                                        </asp:Panel>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <asp:GridView ID="gvClaims" runat="server" AutoGenerateColumns="False" OnPageIndexChanging="gvClaims_PageIndexChanging" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" AllowSorting="True" DataKeyNames="pkiClaimID" OnRowCommand="gvClaims_RowCommand" OnSorting="gvClaims_Sorting">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="pkiClaimID" HeaderText="ClaimNo" SortExpression="pkiClaimID" />
                                                            <asp:BoundField DataField="MemberNumber" HeaderText="MemberNo" SortExpression="MemberNumber" />
                                                            <asp:BoundField DataField="ClaimDate" HeaderText="Claim Date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="ClaimDate" />
                                                            <asp:BoundField DataField="ClaimNotes" HeaderText="Claim Notes" SortExpression="ClaimNotes" />
                                                            <asp:BoundField DataField="CourseOfDearth" HeaderText="Cause Of Death" SortExpression="CourseOfDearth" />
                                                            <asp:TemplateField HeaderText="Claimant" SortExpression="ClaimantFullname">
                                                                <ItemTemplate>
                                                                    <asp:Label ID="lblClaimsName" runat="server" Text='<%#Eval("ClaimantFullname")+ " " + Eval("ClaimantSurname")%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="ClaimantContactNumber" HeaderText="Contact No" SortExpression="ClaimantContactNumber" />
                                                            <asp:TemplateField HeaderText="Status">
                                                                <ItemTemplate>
                                                                    <%if (this.HasEditRight)
                                                                        { %>
                                                                    <a href="#" id="aChangeStatus" class="cssStatus" onclick="OpenStatusPopup(this)" title="Click here to change status" data-id='<%#Eval("pkiClaimID")%>' data-status="<%#Eval("Status")%>"><%#Eval("Status")%></a>
                                                                    <%}
                                                                        else
                                                                        { %>
                                                                    <span id="aChangeStatus1" class="cssStatus" data-id='<%#Eval("pkiClaimID")%>' data-status="<%#Eval("Status")%>"><%#Eval("Status")%></span>
                                                                    <%}  %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:TemplateField HeaderText="Action">
                                                                <ItemTemplate>
                                                                    <%if (this.HasEditRight)
                                                                        { %>
                                                                    <asp:LinkButton runat="server" ID="lbtnSelect" CommandArgument='<%#Eval("pkiClaimID")%>' CommandName="SelectMember"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    &nbsp; &nbsp;
                                                                     <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="return confirm('Are you sure you want to Change Status?')" ToolTip="Click here To Change Status" CommandArgument='<%#Eval("pkiClaimID") %>' CommandName="ChangeStatus"><i class="glyphicon glyphicon-ok-sign"></i></asp:LinkButton>
                                                                    &nbsp;&nbsp;&nbsp;
                                                                    <a href="#" id="aSendEmail" onclick="OpenEmailPopup(this)" data-memberid='<%#Eval("fkiMemberID")%>' data-id='<%#Eval("pkiClaimID")%>' data-status="<%#Eval("Status")%>">Send Email</a>
                                                                    <%} %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                                <%--End Claim Details--%>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField runat="server" ID="hdnMemberNo" />
    <asp:HiddenField runat="server" ID="hdnMemberID" />
    <asp:HiddenField runat="server" ID="hdnClaimID" />
    <asp:HiddenField runat="server" ID="hdnOldReason" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">
        function CODtextchange() {
            var textchange = document.getElementById('<%= txtCauseOfDeath.ClientID %>').value;
            document.getElementById('<%= txtCOD.ClientID %>').value = textchange;
        }
        $(document).ready(function () {
            //set initial state.   

            if ($("[id$=chkAddpayout]").is(":checked")) {
                $('#forhide').show();
            }
            else {
                $('#forhide').hide();
            }
            $("[id$=chkAddpayout]").click(function () {
                if ($(this).is(":checked")) {
                    $('#forhide').show();
                }
                else {
                    $('#forhide').hide();
                }
            });

        });

        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy',autoclose: true });
            $(".datepicker").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
        }

        $(document).ready(function () {
            var tab = $("[id*=TabName]").val();
            $('#myTab a[href="#' + tab + '"]').tab('show');
            var liId = tab.replace("tab-", "");
            $('["#tab' + liId + '"]').addClass("active");
            $('["#tab' + liId + '"]').addClass("active");


            $("li").has('a[data-toggle="tab"]').removeClass("active");
            $("li").has('a[href="#tab-3"]').addClass("active");

        })
        function goToTab(id) {
            $("[id*=TabName]").val("tab-" + id + "");
        }
    </script>
    <script src="../Scripts/plugins/datapicker/bootstrap-timepicker.min.js"></script>
    <script src="~/Scripts/jquery-2.1.1.min.js"></script>
    <script type="text/javascript">
        $('.timepicker1').timepicker();
        $(".datepicker").datepicker({ format: 'dd MM yyyy',autoclose: true });
        $(".datepicker").on('changeDate', function (ev) {
            $(this).datepicker('hide');
        })

        $(document).ready(function () {
            var d = new Date,
                dformat = [d.getDate(),
                (d.getMonth() + 1),
                d.getFullYear()].join('/');

            $('#txtTime').val(dformat);
            //$('#ScheduleTime').val(dformat);
        })
        function SearchPopUp(id) {
            if (id == "SearchMember") {
                $('#PopPaymentHistory').modal('show');
            }
        }
        function selectFollowUpPopUp(id) {

            if (id == "Report") {
                $('#TaskFollowUpModel').modal('show');
                document.getElementById("PopFollowUpTitle").innerHTML = "Print Claim";
            }
        }
        function OpenStatusPopup(ctrl) {
            $("#<%=ddlStatus.ClientID%>").val($(ctrl).data("status"))
            $("#<%=hdnClaimID.ClientID%>").val($(ctrl).data("id"))
            $("#<%=hdnOldReason.ClientID%>").val($(ctrl).data("status"));
            $('#dvChangeStatusModel').modal('show');
        }
        function OpenEmailPopup(ctrl) {
            $("#<%=hdnClaimID.ClientID%>").val($(ctrl).data("id"))
            $("#<%=hdnMemberID.ClientID%>").val($(ctrl).data("memberid"))
            $('#dvSendEmail').modal('show');
        }
    </script>
    <div id="GSCCModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title" id="myModalLabel">Member Details</h4>
                </div>
                <div class="modal-body">
                    <div class="col-lg-12">
                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Claim Details</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label>Member No</label>
                                        <asp:TextBox MaxLength="30" runat="server" ID="txtSearchMemberNo" name="name" type="text" class="form-control" CssClass="form-control"></asp:TextBox>
                                    </div>
                                    <div class="form-group">
                                        <label>Society</label>
                                        <asp:DropDownList ID="ddlSearchSociety" runat="server" CssClass="form-control m-b"></asp:DropDownList>
                                        <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Full Name</label>
                                        <asp:TextBox runat="server" ID="txtSearchFullName" name="FullName" type="text" class="form-control"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                    </div>

                                </div>
                                <div class="col-lg-6">

                                    <div class="form-group">
                                        <label>Surname</label>
                                        <asp:TextBox runat="server" ID="txtSearchSurname" name="txtReg" type="text" class="form-control"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab1" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^[0-9]*$" />--%>
                                    </div>
                                    <div class="form-group">
                                        <label>ID Number</label>
                                        <asp:TextBox runat="server" ID="txtSearchIDNumber" name="txtVin" type="text" class="form-control"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Date of Birth</label>
                                        <asp:TextBox runat="server" ID="txtSearchDOB" name="txtVin" type="text" class="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                        <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                    <div class="col-lg-12">
                        <div class="form-group">
                            <asp:GridView ID="gvSearchMember" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" DataKeyNames="pkiMemberID" AllowPaging="True" AllowSorting="True" OnRowCommand="gvSearchMember_RowCommand">
                                <PagerStyle CssClass="pagination-ys" />
                                <Columns>
                                    <asp:BoundField DataField="Title" HeaderText="Title" ReadOnly="True" />
                                    <asp:BoundField DataField="Full Names" HeaderText="FullName" ReadOnly="True" />
                                    <asp:BoundField DataField="Surname" HeaderText="Surname" ReadOnly="True" />
                                    <asp:BoundField DataField="Gender" HeaderText="Gender" ReadOnly="True" />
                                    <asp:BoundField DataField="Date Of Birth" HeaderText="Date Of Birth" ReadOnly="True" />
                                    <asp:BoundField DataField="Cellphone" HeaderText="Cellphone" ReadOnly="True" />
                                    <asp:TemplateField HeaderText="">
                                        <ItemTemplate>

                                            <asp:LinkButton runat="server" ID="lbtnSelect" CommandArgument='<%#Eval("MemeberNumber")%>' CommandName="SelectMember">Select</asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">

                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>
                    <asp:Button ID="btnSearchMember" runat="server" type="button" class="btn btn-primary" Text="Submit" OnClick="btnSearchMember_Click" />

                </div>
            </div>
        </div>
    </div>
    <div id="AddDocModal" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title" id="DoCModalLabel">Claim Documnets</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <label>Select Supported Document File</label>
                        <div class="input-group">
                            <asp:FileUpload ID="fuSupportDocument" runat="server" />
                            <asp:RequiredFieldValidator ValidationGroup="file" ControlToValidate="fuSupportDocument" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="File is Empty"></asp:RequiredFieldValidator>
                            <span class="input-group-btn">&nbsp;
                            </span>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="radio">
                            <asp:RadioButtonList runat="server" ID="rdbdocument" RepeatDirection="Horizontal" CssClass="rbl">
                                <asp:ListItem Value="1" Text="BI663 Document" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="2" Text="Death Certificate"></asp:ListItem>
                                <asp:ListItem Value="3" Text="Other Document"></asp:ListItem>
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
    <div id="PopPaymentHistory" class="modal fade" tabindex="-1" role="dialog" aria-labelledby="myModalLabel" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title" id="headingclass">Payment History</h4>
                </div>
                <div class="modal-body">
                    <div class="form-group">
                        <div class="row">
                            <div class="col-lg-12 ">
                                <div class="table-responsive">
                                    <asp:GridView ID="gvMembersPaymentHistory" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                        AllowPaging="true" PageSize="10" AutoGenerateColumns="False" EmptyDataText="There are no Data to display." OnPageIndexChanging="gvMembersPaymentHistory_PageIndexChanging1">
                                        <PagerStyle CssClass="pagination-ys" />
                                        <Columns>
                                            <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="AmountPaid" HeaderText="Amount" ReadOnly="True" />
                                            <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                            <asp:BoundField DataField="Notes" HeaderText="Months Paid & Notes" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
    </div>
    <div class="modal inmodal" id="TaskFollowUpModel" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="ibox float-e-margins col-sm-6 center">
            <div class="ibox-title">
                <h5 id="PopFollowUpTitle">Branch</h5>
                <span data-dismiss="modal" target="_blank" class="closePopUp">x</span>
            </div>
            <div class="ibox-content">
                <div class="row">
                    <div class="col-sm-12">
                        <asp:Label runat="server" ID="Label3"></asp:Label>
                    </div>
                    <rsweb:ReportViewer ID="ReportViewerdata" ShowPrintButton="true" runat="server" Width="100%"></rsweb:ReportViewer>
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="dvChangeStatusModel" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title">Claim Status</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Status </label>
                                <asp:DropDownList runat="server" ID="ddlStatus" DataTextField="Status" DataValueField="Status" CssClass="form-control">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Reason </label>
                                <asp:TextBox MaxLength="500" TextMode="MultiLine" Columns="50" Rows="5" runat="server" ID="txtChangeReason" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Button Text="Submit" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" runat="server" ID="btnChangeSubmit" OnClick="btnChangeSubmit_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <div class="modal fade" id="dvSendEmail" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title">Send Email</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Enter Email ID </label>
                                <asp:TextBox MaxLength="150" runat="server" ID="txtEmailAddress" class="form-control"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <asp:Button Text="Send Email" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" runat="server" ID="btnSendEmail" OnClick="btnSendEmail_Click" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <script type="text/javascript">
        function DateComparisionJavascript() {
            var idNumber = $("#MainContent_txtIdNumber").val();
            var textLength = idNumber.length;
            if (textLength == 13) {
                ValidateDate();
            }
        }
        function DateComparisionJavascriptFun() {
            var idNumber = $("#MainContent_txtClaimantIdNumber").val();
            var textLength = idNumber.length;
            if (textLength == 13) {
                Validate();
            }
        }
        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

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

        function Validate() {
            var idNumber = $("#MainContent_txtClaimantIdNumber").val();
            // assume everything is correct and if it later turns out not to be, just set this to false
            var correct = true;

            //Ref: http://www.sadev.co.za/content/what-south-african-id-number-made
            // SA ID Number have to be 13 digits, so check the length
            if (idNumber.length != 13 || !isNumber(idNumber)) {
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
                $("#MainContent_txtClaimantDateOfBirth").val(fullDate);
                $("#MainContent_txtClaimantDateOfBirth").val(fullDate).datepicker('update');
            }
            return false;
        }
        function ValidateDate() {
            var idNumber = $("#MainContent_txtIdNumber").val();
            // assume everything is correct and if it later turns out not to be, just set this to false
            var correct = true;

            //Ref: http://www.sadev.co.za/content/what-south-african-id-number-made
            // SA ID Number have to be 13 digits, so check the length
            if (idNumber.length != 13 || !isNumber(idNumber)) {
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
                $("#MainContent_txtBirthDay").val(fullDate);
                $("#MainContent_txtBirthDay").val(fullDate).datepicker('update');

                var genderCode = idNumber.substring(6, 10);
                var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

                if (gender == "Female") {
                    $("#MainContent_rdbtnFemale").prop('checked', true)

                } else {
                    $("#MainContent_rdbtnMale").prop('checked', true)
                }
            }
            return false;
        }
    </script>
</asp:Content>
