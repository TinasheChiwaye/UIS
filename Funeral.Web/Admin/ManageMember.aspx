<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageMember.aspx.cs" Inherits="Funeral.Web.Admin.ManageMember" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">

    <style type="text/css">
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

        #TaskFollowUpModel {
            left: 12% !important;
        }

        #TaskCopyPopupModel {
            left: 12% !important;
        }
        /*#AgentInfoModel {
            left: 13% !important;
        }*/

        .closePopUp {
            float: right;
            color: #676a6c;
            margin-right: 3px;
            cursor: pointer;
        }

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

        .datepicker {
            z-index: 2060 !important;
            position: relative;
        }
        /*.ui-datepicker-div { z-index: 2060 !important; }*/
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
            <asp:HiddenField ID="TabName" runat="server" />
            <asp:HiddenField ID="hdnNoteId" runat="server" />
            <asp:HiddenField ID="hdnProductId" runat="server" />
            <asp:HiddenField ID="hdnDepedencyCoverDate" runat="server" />
            <asp:HiddenField ID="hdnParlourid" runat="server" />
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Members Details</h5>

                    <%--  <asp:Label ID="lblResult" Style="padding-left: 375px" ForeColor="Red" runat="server" Text=""></asp:Label>--%>

                    <asp:HiddenField runat="server" ID="hdnId" />
                    <div class="pull-right">
                        <div class="btn-group">
                            <asp:Button ID="btnCopy" Style="margin-right: 12px; margin-top: -5px" runat="server" CssClass="btn btn-sm btn-primary" Text="CopyPolicy" OnClick="btnCopy_Click" />
                            <asp:Button runat="server" ID="PolicyDoc" ValidationGroup="tab3" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Policy Doc" OnClick="PolicyDoc_Click" />
                            <!--<asp:Button runat="server" ID="PolicyDocPrint" ValidationGroup="tab3" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Policy Doc Print" OnClick="PolicyDocPrint_Click" />-->
                        </div>
                    </div>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="panel blank-panel">
                            <div class="panel-heading">
                                <div class="panel-options" id="Tabs">
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active" id="tab1"><a data-toggle="tab" href="#tab-1" aria-expanded="true">Personal Details</a></li>
                                        <li class="" id="tab2"><a data-toggle="tab" href="#tab-2" aria-expanded="false">Physical Address</a></li>
                                        <li class="" id="tab3"><a data-toggle="tab" href="#tab-3" aria-expanded="false">Policy Details</a></li>
                                        <li class="" id="tab9"><a data-toggle="tab" href="#tab-9" aria-expanded="false">Product</a></li>
                                        <li class="" id="tab4"><a data-toggle="tab" href="#tab-4" aria-expanded="false">Debit Order Details</a></li>
                                        <li class="" id="tab5"><a data-toggle="tab" href="#tab-5" aria-expanded="false">Dependent & Extended Family </a></li>

                                        <li class="" id="tab6"><a data-toggle="tab" href="#tab-6" aria-expanded="false">Payment History</a></li>
                                        <li class="" id="tab7"><a data-toggle="tab" href="#tab-7" aria-expanded="false">Note</a></li>
                                        <li class="" id="tab8"><a data-toggle="tab" href="#tab-8" aria-expanded="false">Supporting Documents</a></li>

                                    </ul>
                                </div>
                            </div>

                            <div class="panel-body">
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="vSummaryTab1" ValidationGroup="tab1" ForeColor="Red" />
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
                                                    <label>full name<em>*</em>  </label>
                                                    <asp:TextBox MaxLength="25" runat="server" ID="txtFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtFirstName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="tab1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>ID Number <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtIdNumber" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFun();"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab2" ControlToValidate="txtStreetAddress" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>--%>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtIdNumber" ID="rfvIdnumber" ForeColor="red" runat="server" ErrorMessage="Please enter id number"></asp:RequiredFieldValidator>
                                                    <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="cvIdvalidation" OnServerValidate="cvIdvalidation_ServerValidate" ControlToValidate="txtIdNumber" ValidationGroup="tab1" runat="server" />
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtTown" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date of Birth </label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtBirthDay" name="name" type="text" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Age</label>
                                                    <asp:TextBox MaxLength="4" runat="server" ID="txtAge" ReadOnly="true" Text="Will be Calculated From Date Of Birth" name="txtAge" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtProvince" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Gender</label>
                                                    <asp:RadioButtonList CssClass="radio radiogroup " RepeatDirection="Horizontal" ID="rbtnlGender" runat="server">
                                                        <asp:ListItem Selected="True" Value="0">Male</asp:ListItem>
                                                        <asp:ListItem Value="1">Female</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Cellphone Number <em>*</em>  </label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtCellphone" name="txtCellphone" TextMode="Number" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtCellphone" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="Enter Cellphone Number"></asp:RequiredFieldValidator>
                                                    <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(3);">Next</button>--%>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="tab1" runat="server" ControlToValidate="txtCellphone" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^[0-9]+$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Telephone Number </label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtTelePhone" name="txtTelePhone" TextMode="Number" class="form-control"></asp:TextBox>
                                                    <%--  <asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtTelePhone" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Enter Telephone Number"></asp:RequiredFieldValidator>--%>
                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab1" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^[0-9]*$" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Email Address</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtEmail" name="txtEmail" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtPolicyPremium" ID="RequiredFieldValidator17" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Citizenship</label>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtInception" ID="RequiredFieldValidator22" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList ID="ddlCitizenship" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtInception" ID="RequiredFieldValidator22" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Passport</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtPassport" name="txtPassport" type="text" class="form-control" ReadOnly="true"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="rfvPassport" ForeColor="red" runat="server" ErrorMessage="Enter Passport Number"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:CheckBox ID="chkIdORPass" runat="server" Text="Either Allow Passport Number Or ID Number" AutoPostBack="true" OnCheckedChanged="IdorPass_chkEvent" />
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnResetTab1" runat="server" Text="Reset" OnClick="btnResetTab1_Click" />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnNextStep" ValidationGroup="tab1" CssClass="btn btn-sm btn-primary" Text="Next" OnClick="btnNextTb1_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 ">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvMembers" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                        AllowPaging="true" PageSize="25" OnPageIndexChanging="gvMembers_PageIndexChanging" OnRowCommand="gvMembers_RowCommand" AutoGenerateColumns="False" EmptyDataText="There are no member to display.">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="pkiMemberID" HeaderText="MemberId" ReadOnly="True" />
                                                            <asp:BoundField DataField="MemeberNumber" HeaderText="Policy No" ReadOnly="True" />
                                                            <asp:BoundField DataField="IDNumber" HeaderText="ID No" ReadOnly="True" />
                                                            <asp:BoundField DataField="MemberBranch" HeaderText="Branch" ReadOnly="True" />
                                                            <asp:BoundField DataField="StartDate" HeaderText="Start date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="InceptionDate" HeaderText="Inception date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="CoverDate" HeaderText="Cover date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />


                                                            <asp:TemplateField HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <%if (this.HasEditRight)
                                                                        { %>
                                                                    <%-- <asp:HyperLink runat="server"  ID="hrLink" NavigateUrl='<%#Eval("pkiMemberID", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>
                                                                    --%>
                                                 &nbsp;
                                                  <asp:LinkButton runat="server" ID="LinkButton3" CommandArgument='<%#Eval("pkiMemberID")%>' CommandName="EditPolicy"><i class="fa fa-edit"></i></asp:LinkButton>

                                                                    <% }
                                                                        if (this.HasDeleteRight)
                                                                        { %>
                                                         &nbsp;
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiMemberID")%>' CommandName="DeletePolicy"><i class="fa fa-trash-o"></i></asp:LinkButton>
                                                                    <%--<asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Member" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiMemberID") %>' CommandName="deleteMemeber"><i class="fa fa-trash"></i></asp:LinkButton>--%>
                                                                    <%} %>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tab-2" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="tab2" ForeColor="Red" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Postal Address</label>
                                                    <asp:TextBox MaxLength="100" runat="server" ID="txtStreetPostalAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtUnderwriter" ID="RequiredFieldValidator23" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Street Address <%--<asp:TextBox MaxLength="50" runat="server" ID="txtAgent" name="name" type="text" class="form-control"></asp:TextBox>--%> </label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtStreetAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAgent" ID="RequiredFieldValidator16" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Town or City</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtTown" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="txtTotalPremium" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter Total premium amount" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Province</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtProvince" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<div class="btn-group">
                                                       
                                                        <asp:Button runat="server" ID="PolicyDoc" ValidationGroup="tab3"  CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Policy Doc"  OnClick="PolicyDoc_Click" />

                                                    </div>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Code</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtCode" name="name" type="text" onkeypress="return isDecimalNumber(event,this);" class="form-control"></asp:TextBox>
                                                    <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(4);">Next</button>--%>
                                                </div>
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(1);">Back</button>

                                                        <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(4);">Next</button>--%>
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
                                                    <label>Policy Name<em>*</em> </label>
                                                    <asp:DropDownList ID="ddlPolicy" class="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Policy</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="ddlPolicy" Display="None" InitialValue="0" ID="RequiredFieldValidator17" ForeColor="red" runat="server" ErrorMessage="Select Policy"></asp:RequiredFieldValidator>
                                                    <asp:HiddenField ID="hdCoverDate" runat="server" />
                                                </div>
                                                <div class="form-group">
                                                    <label>Policy Premium </label>
                                                    <asp:TextBox MaxLength="10" CssClass="form-control" Enabled="false" runat="server" ID="txtPolicyPremium" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:HyperLink runat="server" ToolTip='Edit ' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkAddonProductID="+Eval("pkiMemberProductID")%>'></asp:HyperLink>
                                                                    <asp:HyperLink OnClientClick="return confirm('Are you sure want to delete?');" runat="server" ToolTip='Delete' ID="HyperLink2" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkInoteIDRemove="+Eval("pkiMemberProductID")%>'><i class="fa fa-trash"></i></asp:HyperLink>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>
                                                        Policy No / Membership No<em>
                                                            <asp:Literal runat="server" ID="ltrPolicyNumber" Text="*"></asp:Literal></em>
                                                    </label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="20" runat="server" ID="txtPolicyNo" name="txtPolicyNo" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="txtPolicyNo" ID="rfvPolicyNo" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter policy no"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Easy Pay</label>
                                                    <asp:TextBox MaxLength="50" CssClass="form-control" runat="server" ID="txtEasyToPay" name="txtEasyToPay" type="text" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Select Branch</label>
                                                    <label>
                                                        <em>
                                                            <asp:Literal runat="server" ID="ltrPolicyNumber0" Text="*"></asp:Literal></em>
                                                    </label>
                                                    <asp:DropDownList ID="ddlBankBranch" class="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Branch</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="ddlBankBranch" Display="None" InitialValue="0" ID="RequiredFieldValidator36" ForeColor="red" runat="server" ErrorMessage="Select a Branch"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Custom Payment Method</label>
                                                    <asp:DropDownList ID="ddlCustom1" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Custom Grouping 2</label>
                                                    <asp:DropDownList ID="ddlCustom2" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Custom Grouping 3</label>
                                                    <asp:DropDownList ID="ddlCustom3" class="form-control" runat="server">
                                                    </asp:DropDownList>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Society</label>
                                                    <asp:DropDownList ID="ddlMemberSociety" class="form-control m-b" runat="server">
                                                        <asp:ListItem Value="0">Select Society</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>

                                                <div class="form-group">
                                                    <label>Policy Start Date</label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtPolicyStartDate" name="name" type="text" placeholder="Enter Start  Date" class="form-control"></asp:TextBox>

                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="txtPolicyStartDate" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter Start date" Display="None"></asp:RequiredFieldValidator>

                                                </div>

                                                <div class="form-group">
                                                    <label>Policy Inception Date</label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtInception" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab3" ControlToValidate="txtInception" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Inception date" Display="None"></asp:RequiredFieldValidator>
                                                    <%--<asp:ValidationSummary runat="server" ID="ValidationSummary3" ValidationGroup="tab4" ForeColor="Red" />--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Policy Cover Date</label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="30" Enabled="false" runat="server" ID="txtCoverDate" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:HiddenField ID="hdEditCoverDate" runat="server" />
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountholder" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter valid account number" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Policy Underwritten By</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtUnderwriter" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:TextBox MaxLength="255" runat="server" ID="txtbank" name="name" type="text" class="form-control"></asp:TextBox>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Policy Agent or Broker</label>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtbank" ID="RequiredFieldValidator21" ForeColor="red" runat="server" ErrorMessage="Please enter bank name" Display="None"></asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList ID="ddlAgent" runat="server" DataTextField="Agent" DataValueField="AgentID" CssClass="form-control m-b"></asp:DropDownList>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranch" ID="RequiredFieldValidator24" ForeColor="red" runat="server" ErrorMessage="Please enter bank branch" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Total Premium</label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtTotalPremium" name="name" type="text" ReadOnly="true" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranchcode" ID="RequiredFieldValidator25" ForeColor="red" runat="server" ErrorMessage="Please enter branch code" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(2);">Back</button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnResetTab3" runat="server" Text="Reset" OnClick="btnResetTab3_Click" />

                                                    </div>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountno" ID="RequiredFieldValidator26" ForeColor="red" runat="server" ErrorMessage="Please enter account number" Display="None"></asp:RequiredFieldValidator>--%>
                                                    <div class="btn-group">
                                                        <%--<asp:TextBox MaxLength="255" runat="server" ID="txtAccounttype" name="name" type="text" class="form-control"></asp:TextBox>--%>
                                                        <asp:Button runat="server" ID="btnTab3" ValidationGroup="tab3" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Next" OnClick="btnTab3_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                    <div id="tab-9" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary5" ValidationGroup="tabs-9" ForeColor="Red" />
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <label>Product Name <em>*</em></label>
                                                    <asp:DropDownList ID="drpProductName" class="form-control m-b" runat="server">
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="drpProductName" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please select product name" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Premium<em>*</em></label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtPremium" placeholder="Premium " name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="txtPremium" ID="RequiredFieldValidator30" ForeColor="red" runat="server" ErrorMessage="Please enter premium amount" Display="None"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <label>Inception Date<em>*</em></label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtProductInceptionDate" placeholder="InceptionDate" name="name" type="text" class="form-control"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="txtPremium" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter premium amount" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Cover Date<em>*</em></label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtProductCoverDate" placeholder="CoverDate" name="name" type="text" class="form-control"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="txtPremium" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter premium amount" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Start Date<em>*</em></label>
                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtProductStartDate" placeholder="StartDate" name="name" type="text" class="form-control"></asp:TextBox>
<%--                                                    <asp:RequiredFieldValidator ValidationGroup="tabs-9" ControlToValidate="txtPremium" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter premium amount" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnAdd" CssClass="btn btn-sm btn-primary" ValidationGroup="tabs-9" runat="server" Text="Save AddonProducts" OnClick="btnAdd_Click" />
                                                    <asp:Button ID="btnUpdateProduct" class="btn btn-sm btn-primary" ValidationGroup="tabs-9" Visible="false" runat="server" Text="Update AddonProducts" OnClick="btnUpdateProduct_Click" />
                                                    <br />
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(3);">Back</button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="Button1" runat="server" Text="Reset" OnClick="Button1_Click" />
                                                    </div>
                                                    <div class="btn-group">
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccounttype" ID="RequiredFieldValidator27" ForeColor="red" runat="server" ErrorMessage="Please eneter account type" Display="None"></asp:RequiredFieldValidator>--%>
                                                        <asp:Button runat="server" ID="Button2" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Next" OnClick="btnTab4_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvProduct" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                        AllowPaging="false" AutoGenerateColumns="False" EmptyDataText="There are no product available." OnRowCommand="gvProduct_RowCommand">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ProductName" HeaderText="Product Name" ReadOnly="True" />
                                                            <asp:BoundField DataField="DateCreated" HeaderText="Date Created" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="ProductCost" HeaderText="Product Cost" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:n}" />
                                                            <%--<asp:BoundField DataField="InceptionDate" HeaderText="InceptionDate"  DataFormatString="{0:d}" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="Cover Date" HeaderText="CoverDate"  DataFormatString="{0:d}" ItemStyle-CssClass  ="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="Start Date" HeaderText="StartDate "  DataFormatString="{0:d}" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
--%>


                                                            <asp:TemplateField HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <asp:LinkButton runat="server" ID="btnEditProduct" CommandName="EditProduct" ToolTip="Click here to edit product" CommandArgument='<%#Eval("pkiMemberProductID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="LinkButton1" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandName="DeleteProduct" ToolTip="Click here to delete product" CommandArgument='<%#Eval("pkiMemberProductID") %>'><i class="fa fa-trash"></i></asp:LinkButton>
                                                                    <%--<asp:HyperLink runat="server" ToolTip='Edit ' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkAddonProductID="+Eval("pkiMemberProductID")%>'></asp:HyperLink>
                                                                    <asp:HyperLink OnClientClick="return confirm('Are you sure want to delete?');" runat="server" ToolTip='Delete' ID="HyperLink2" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkInoteIDRemove="+Eval("pkiMemberProductID")%>'><i class="fa fa-trash"></i></asp:HyperLink>--%>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                    <div id="tab-4" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="input-group">
                                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtDebitdate" ID="RequiredFieldValidator28" ForeColor="red" runat="server" ErrorMessage="Please enter debit date" Display="None"></asp:RequiredFieldValidator>--%>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label>Account Holder</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtAccountholder" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%#Eval("Surname") %>
                                                </div>
                                                <div class="form-group">
                                                    <label>Bank Name</label>
                                                    <%#Eval("FullName") %>
                                                    <asp:DropDownList ID="ddlBank" runat="server" DataTextField="BankName" DataValueField="BranchCode" CssClass="form-control m-b"></asp:DropDownList>
                                                    <%--<asp:BoundField DataField="RelationshipType" HeaderText="RelationShip" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Bank Branch</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtBranch" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyIdNumber" ID="RequiredFieldValidator36" ForeColor="red" runat="server" ErrorMessage="Please eneter id number" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>

                                                <div class="form-group">
                                                    <label>Branch Code</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtBranchcode" name="txtBranchcode" type="text" ReadOnly="true" class="form-control"></asp:TextBox>
                                                    <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator7" ValidationGroup="tab5" runat="server" ErrorMessage="Id Number is Wrong" ControlToValidate="txtDependencyIdNumber" ValidationExpression="(((\d{2}((0[13578]|1[02])(0[1-9]|[12]\d|3[01])|(0[13456789]|1[012])(0[1-9]|[12]\d|30)|02(0[1-9]|1\d|2[0-8])))|([02468][048]|[13579][26])0229))(( |-)(\d{4})( |-)(\d{3})|(\d{7}))" />--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Account Number</label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtAccountno" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyInceptionDate" ID="RequiredFieldValidator34" ForeColor="red" runat="server" ErrorMessage="Please enter inception date" Display="None"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Account Type</label>
                                                    <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyCovertDate" ID="RequiredFieldValidator33" ForeColor="red" runat="server" ErrorMessage="Please enter cover date" Display="None"></asp:RequiredFieldValidator>--%>
                                                    <asp:DropDownList runat="server" ID="ddlAccountType" DataTextField="AccountType" DataValueField="AccountTypeID" class="form-control"></asp:DropDownList>
                                                    <%--<div class="form-group">
                                                                    <label>Select RelationShip</label>
                                                                    <asp:DropDownList ID="ddlDependencyRelationship" class="form-control m-b" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Debit Date</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDebitdate" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(9);">Back</button>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnResetTab4" runat="server" Text="Reset" OnClick="btnResetTab4_Click" />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" OnClick="btnSave_Click" ID="btnSave" ValidationGroup="tab4" runat="server" Text="Save" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tab-5" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12 ">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Dependent & Extended Family</h5>
                                                        <div class="ibox-tools">
                                                            <a class="collapse-link">
                                                                <i class="fa fa-chevron-up"></i>
                                                            </a>
                                                            <a class="close-link">
                                                                <i class="fa fa-times"></i>
                                                            </a>
                                                        </div>
                                                    </div>
                                                    <div class="ibox-content">
                                                        <div class="row">
                                                            <div class="col-sm-12 ">
                                                                <div class="table-responsive">
                                                                    <asp:GridView ID="gvFamilyDependency" OnRowDataBound="gvFamilyDependency_RowDataBound" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                                        AllowPaging="false" AutoGenerateColumns="False" EmptyDataText="There are no Dependents added." OnRowCommand="gvFamilyDependency_RowCommand">
                                                                        <PagerStyle CssClass="pagination-ys" />
                                                                        <Columns>
                                                                            <asp:TemplateField HeaderText="Name">
                                                                                <ItemTemplate>
                                                                                    <%#Eval("Surname") %>&nbsp<%#Eval("FullName") %>
                                                                                </ItemTemplate>
                                                                            </asp:TemplateField>
                                                                            <asp:BoundField DataField="IdNumber" HeaderText="ID Number" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                                            <%--<asp:BoundField DataField="RelationshipType" HeaderText="RelationShip" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />--%>
                                                                            <asp:BoundField DataField="InceptionDate" HeaderText="InceptionDate" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                            <asp:BoundField DataField="CoverDate" HeaderText="CoverDate" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                            <asp:BoundField DataField="Premium" HeaderText="Premium" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                            <asp:BoundField DataField="DependencyType" HeaderText="Dependency" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                            <asp:BoundField DataField="Gender" HeaderText="Gender" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                                            <asp:TemplateField HeaderText="Actions">
                                                                                <ItemTemplate>
                                                                                    <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edite Department" CommandArgument='<%#Eval("pkiDependentID") %>' CommandName="EditDependant"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                                    &nbsp;
                                                                                    <asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete Department" CommandArgument='<%#Eval("pkiDependentID") %>' OnClientClick="return confirm('Are you sure you want to delete it?')" CommandName="DeleteDependant"><i class="fa fa-trash"></i></asp:LinkButton>
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
                                            <div class="col-lg-12 ">
                                                <div class="ibox float-e-margins">
                                                    <div class="ibox-title">
                                                        <h5>Add/Update Dependent</h5>
                                                        <div class="ibox-tools">
                                                            <a class="collapse-link">
                                                                <i class="fa fa-chevron-up"></i>
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
                                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary4" ValidationGroup="tab5" ForeColor="Red" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label>Surname <em>*</em></label>
                                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyLastName" placeholder="Enter Surname" name="name" type="text" class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyLastName" ID="RequiredFieldValidator19" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter dependency Surname"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="tab5" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtDependencyLastName" ValidationExpression="[a-zA-Z ]*$" />
                                                                </div>

                                                                <div class="form-group">
                                                                    <label>full name <em>*</em></label>
                                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyFirstName" placeholder="Enter full name" name="name" type="text" class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyFirstName" ID="RequiredFieldValidator29" ForeColor="red" runat="server" ErrorMessage="Please enter dependency full name" Display="None"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator5" ValidationGroup="tab5" runat="server" ErrorMessage="full name Enter Only characters" ControlToValidate="txtDependencyFirstName" ValidationExpression="[a-zA-Z ]*$" />
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>ID Number <em></em></label>
                                                                    <asp:TextBox MaxLength="20" runat="server" ID="txtDependencyIdNumber" placeholder="Enter ID Number" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFundep();"></asp:TextBox>
                                                                    <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="CustomValidator1" OnServerValidate="cvIdvalidation_ServerValidate2" ControlToValidate="txtDependencyIdNumber" ValidationGroup="tab5" runat="server" />
                                                                    <%#Eval("Notes") %>                                                                    <%--<asp:HyperLink runat="server" ToolTip='Edit' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?Id="+MemberId.ToString()+"&PkInoteIDList="+Eval("pkiNoteID")%>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Date Of Birth <em>*</em></label>
                                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtDependencyDOB" placeholder="Enter Date Of Birth" name="name" type="text" class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyDOB" ID="RequiredFieldValidator35" ForeColor="red" runat="server" ErrorMessage="Please enter date of birth" Display="None"></asp:RequiredFieldValidator>

                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Age</label>
                                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtDependencyAge" ReadOnly="true" Text="Will be Calculated From Date Of Birth" name="name" class="form-control"></asp:TextBox>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Gender</label>


                                                                    <br />
                                                                    &nbsp &nbsp
                                                                    <asp:RadioButton ID="rdbtnMale" GroupName="Gender" runat="server" Text="Male" />&nbsp &nbsp                       &nbsp &nbsp
                                                                      <asp:RadioButton ID="rdbtnFemale" GroupName="Gender" runat="server" Text="Female" />

                                                                </div>

                                                            </div>

                                                            <div class="col-lg-6">
                                                                <div class="form-group">
                                                                    <label>Start Date <em>*</em></label>
                                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyStartDate" placeholder="Enter Start  Date" name="name" class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyStartDate" ID="RequiredFieldValidator33" ForeColor="red" runat="server" ErrorMessage="Please enter Start date" Display="None"></asp:RequiredFieldValidator>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Inception Date <em>*</em></label>
                                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyInceptionDate" placeholder="Enter Inception Date" name="name" class="form-control"></asp:TextBox>
                                                                    <%--<asp:Label ID="lbldoctype" runat="server" Text='<%#Eval("DocType")%>'></asp:Label>--%>
                                                                </div>

                                                                <div class="form-group">
                                                                    <label>Cover Date <em>*</em></label>
                                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyCovertDate" ReadOnly="true" placeholder="Enter Cover Date" name="name" type="text" class="form-control"></asp:TextBox>
                                                                    <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyCovertDate" ID="RequiredFieldValidator33" ForeColor="red" runat="server" ErrorMessage="Please enter cover date" Display="None"></asp:RequiredFieldValidator>--%>
                                                                </div>
                                                                <div class="form-group">
                                                                    <label>Select Dependency Type</label>
                                                                    <asp:DropDownList ID="ddlDependencyType" class="form-control m-b" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>
                                                                <%--<div class="form-group">
                                                                    <label>Select RelationShip</label>
                                                                    <asp:DropDownList ID="ddlDependencyRelationship" class="form-control m-b" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>--%>

                                                                <div class="form-group">
                                                                    <label>Premium <em>*</em></label>
                                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyPremium" placeholder="Enter Preminum" name="name" type="text" class="form-control"></asp:TextBox>
                                                                    <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyPremium" ID="RequiredFieldValidator32" ForeColor="red" runat="server" ErrorMessage="Please enter premium" Display="None"></asp:RequiredFieldValidator>
                                                                </div>

                                                            </div>
                                                            <div class="col-lg-12">
                                                                <div class="form-group">
                                                                    <div class="btn-group">
                                                                        <asp:HiddenField ID="hfDependentId" runat="server" />
                                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnDependecyReset" runat="server" Text="Reset" OnClick="btnDependecyReset_Click" />
                                                                    </div>
                                                                    <div class="btn-group">
                                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" ValidationGroup="tab5" Visible="true" ID="btnDependecySubmit" runat="server" Text="Save" OnClick="btnDependecySubmit_Click" />

                                                                    </div>
                                                                    <div class="btn-group">
                                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="false" ValidationGroup="tab5" ID="btnDependecyUpdate" runat="server" Text="Update" OnClick="btnDependecyUpdate_Click" />
                                                                    </div>
                                                                </div>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tab-6" class="tab-pane">
                                        <br />
                                        <br />
                                        <div class="col-lg-12 ">
                                            <div class="table-responsive">
                                                <asp:GridView ID="gvInvoices" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                    AllowPaging="true" PageSize="25" AutoGenerateColumns="False" EmptyDataText="There are no payment history.">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:BoundField DataField="AmountPaid" HeaderText="Amount" ReadOnly="True" />
                                                        <asp:BoundField DataField="DatePaid" HeaderText="Date paid" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="RecievedBy" HeaderText="Recieved by" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="InvNumber" HeaderText="Invoice Number" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                        <asp:BoundField DataField="PaymentBranch" HeaderText="Payment Branch" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" />
                                                    </Columns>
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </div>
                                    <div id="tab-7" class="tab-pane">
                                        <br />
                                        <br />
                                        <div class="row">
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Enter note description</label>
                                                    <asp:TextBox TextMode="MultiLine" Rows="12" ValidationGroup="tab7" MaxLength="255" runat="server" ID="txtArea" placeholder="Enter Note" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <asp:RequiredFieldValidator ValidationGroup="tab7" ControlToValidate="txtArea" ID="RequiredFieldValidator31" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" ID="BtnAddNote" ValidationGroup="tab7" runat="server" Text="Add Note" OnClick="BtnAddNote_Click" />
                                                    <asp:Button ID="btnUpdateNotes" ValidationGroup="tab7" class="btn btn-sm btn-primary pull-right m-t-n-x" Visible="false" runat="server" Text="Update Note" OnClick="btnUpdateNotes_Click" />
                                                    <br />
                                                </div>
                                            </div>

                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 ">
                                                <div class="table-responsive">
                                                    <asp:GridView OnRowDataBound="gvNotes_RowDataBound" ID="gvNotes" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                        AllowPaging="true" PageSize="25" AutoGenerateColumns="False" EmptyDataText="There are no notes to diplay." OnRowCommand="gridNoteList_RowCommand">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="Notes" HeaderText="Note" ReadOnly="True" />
                                                            <asp:BoundField DataField="NoteDate" HeaderText="Note Date" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:BoundField DataField="LastModified" HeaderText="Last Modified" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:TemplateField HeaderText="Actions">
                                                                <ItemTemplate>
                                                                    <div class="modal inmodal fade" id="myModal<%#Eval("pkiNoteID") %>>" role="dialog" hidden="true">
                                                                        <div class="modal-dialog modal-lg">
                                                                            <div class="modal-content">
                                                                                <div class="modal-header">
                                                                                    <%--<button type="button" class="close" data-dismiss="modal"><span aria-hidden="true">&times;</span><span class="sr-only">Close</span></button>--%>
                                                                                    <h4 class="modal-title">Modal title</h4>
                                                                                    <small class="font-bold"></small>
                                                                                </div>
                                                                                <div class="modal-body">
                                                                                    <%#Eval("Notes") %>
                                                                                </div>
                                                                                <div class="modal-footer">
                                                                                    <button type="button" class="btn btn-sm btn-primary close" data-dismiss="modal">Close</button>
                                                                                </div>
                                                                            </div>
                                                                        </div>
                                                                    </div>
                                                                    <asp:LinkButton runat="server" ID="LinkButton2" CausesValidation="false" ToolTip="Click here to edit Note" CommandName="EditNote" CommandArgument='<%#Eval("pkiNoteID") %>'><i class="fa fa-edit"></i></asp:LinkButton>
                                                                    <asp:LinkButton runat="server" ID="LinkButton4" CausesValidation="false" ToolTip="Click here to Delete Note" CommandName="DeleteNote" CommandArgument='<%#Eval("pkiNoteID") %>'><i class="fa fa-trash"></i></asp:LinkButton>

                                                                    <%--<asp:HyperLink runat="server" ToolTip='Edit' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?Id="+MemberId.ToString()+"&PkInoteIDList="+Eval("pkiNoteID")%>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                                                    &nbsp;
                                                                    <button type="button" title="Click here to view Note" onclick="ViewNote('<%#Eval("pkiNoteID") %>'); return false;">
                                                                        <i class="fa fa-search"></i>
                                                                    </button>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                        </Columns>
                                                    </asp:GridView>
                                                </div>
                                            </div>
                                            <asp:Panel ID="Panel2" runat="server" CssClass="modalPopup" Visible="false">
                                                <asp:LinkButton ID="lnkClose" Font-Bold="true" ForeColor="Blue" CssClass="lnkClose" runat="server" Text="[X]" OnClick="lnkClose_Click"></asp:LinkButton>
                                                <br />
                                                <div class="spaceMargin">
                                                    <div class="noteHead">
                                                        <h3 style="float: left;">Note</h3>
                                                        <span class="noteDate" style="margin-top: 5px; float: right;">
                                                            <asp:Literal ID="ltrNoteDate" runat="server"></asp:Literal>
                                                        </span>
                                                    </div>
                                                    <div class="noteContainer">
                                                        <asp:Literal ID="ltrNotes" runat="server"></asp:Literal>
                                                    </div>

                                                    <br />
                                                    <asp:Literal Visible="false" ID="ltrLastDate" runat="server"></asp:Literal>
                                                    <br />


                                                </div>

                                            </asp:Panel>
                                            <asp:Panel Visible="false" ID="pnlWrapper" runat="server">
                                                <div class="WrapLayout"></div>
                                            </asp:Panel>
                                        </div>
                                    </div>
                                    <div id="tab-8" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <label>Select Supported Document File</label>
                                                    <div class="input-group">
                                                        <asp:FileUpload ID="fuSupportDocument" runat="server" />
                                                        <span class="input-group-btn">&nbsp;
                                                        </span>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <div class="radio">
                                                        <asp:RadioButtonList runat="server" ID="rdbdocument" RepeatDirection="Horizontal">
                                                            <asp:ListItem Value="1" Text="Id Documents/ passports – main member – spouse" Selected="True"></asp:ListItem>
                                                            <asp:ListItem Value="2" Text="Marriage certificate"></asp:ListItem>
                                                            <asp:ListItem Value="3" Text="Dependants – birth certificate"></asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </div>
                                                </div>
                                                <div class="form-group">
                                                    <asp:Button ID="btnDocumentSubmit" class="btn btn-sm btn-primary pull-right m-t-n-xs" OnClick="BtnDocumentSubmit_Click" runat="server" Text="Save Document" />
                                                    <br />
                                                </div>
                                            </div>
                                            <div class="col-lg-6">
                                            </div>
                                        </div>
                                        <div class="row">
                                            <div class="col-lg-12 ">
                                                <div class="table-responsive">
                                                    <asp:GridView ID="gvSupportedDocument" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                                        AllowPaging="false" AutoGenerateColumns="False" EmptyDataText="There are no document upload." OnRowCommand="gvSupportedDocument_RowCommand">
                                                        <PagerStyle CssClass="pagination-ys" />
                                                        <Columns>
                                                            <asp:BoundField DataField="ImageName" HeaderText="Documet" ReadOnly="True" />
                                                            <asp:TemplateField HeaderText="Documet Type">
                                                                <ItemTemplate>
                                                                    <%--<asp:Label ID="lbldoctype" runat="server" Text='<%#Eval("DocType")%>'></asp:Label>--%>
                                                                    <asp:Label ID="lbldoctype" runat="server" Text='<%# ( Convert.ToInt32(Eval("DocType"))==1?"Id Documents/ passports":(Convert.ToInt32(Eval("DocType"))==2?"Marriage certificate":"Dependants – birth certificate Save Document")) %>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateField>
                                                            <asp:BoundField DataField="CreateDate" HeaderText="Created date" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                            <asp:TemplateField HeaderText="">
                                                                <ItemTemplate>
                                                                    &nbsp;
                                                                    <asp:HyperLink runat="server" ToolTip='Download document' ID="hrLink" NavigateUrl='<%# "~/Handler/DocumentHandler.ashx?DocID="+Eval("pkiPictureID")%>'><i class="glyphicon glyphicon-download"></i></asp:HyperLink>
                                                                    &nbsp; <a href='<%#ResolveUrl("~/Handler/DocumentHandler.ashx?DocID="+Eval("pkiPictureID"))%>' title="View Document" data-gallery=""><i class="fa fa-search"></i></a>
                                                                    &nbsp;
                                                                    <asp:LinkButton runat="server" ID="lbtnDeleteDocument" ToolTip="Click here To Delete Document" CommandArgument='<%#Eval("pkiPictureID") %>' OnClientClick="return confirm('Are you sure you want to delete it?')" CommandName="DeleteDocument"><i class="fa fa-trash"></i></asp:LinkButton>
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
                    </div>

                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">

        $(document).ready(function () {



            //$("#tab2").click(function (e) {
            //    e.preventDefault();
            //    if ($('#MainContent_txtLastName').val() == '' || $('#MainContent_txtFirstname').val() == '' || $('#MainContent_txtCellphone').val() == '') { return false; }
            //    else { return true; }

            //});
            //$("#tab3").click(function (e) {
            //    e.preventDefault();
            //    if ($('#MainContent_txtStreetAddress').val() == '' || $('#MainContent_txtLastName').val() == '' || $('#MainContent_txtFirstname').val() == '' || $('#MainContent_txtCellphone').val() == '') { return false; }
            //    else { return true; }

            //});
            //$("#tab4").click(function (e) {
            //    e.preventDefault();
            //    if ($('#MainContent_ddlPolicy').val() == '0' || $('#MainContent_txtPolicyStartDate').val() == '' || $('#MainContent_txtStreetAddress').val() == '' || $('#MainContent_txtLastName').val() == '' || $('#MainContent_txtFirstname').val() == '' || $('#MainContent_txtCellphone').val() == '') { return false; }
            //    else { return true; }

            //});
        });
    </script>
    <script type="text/javascript">


      <%--  $(function () {

            $("#<%=txte_sdate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

        });--%>

        $(document).ready(function () {
            var currentTime = new Date();
            var startDateTo = new Date(currentTime.getFullYear(), currentTime.getMonth() + 1, 0);
            //set InceptionDates
            $("#<%=txte_sdate.ClientID %>").datepicker();
            $("#<%=txtDependencyInceptionDate.ClientID %>").val('<%=System.DateTime.Now.ToString("dd MMM yyyy")%>');
            //$("#<%=txtInception.ClientID %>").val('<%=System.DateTime.Now.ToString("dd MMM yyyy")%>');


            enabledisabletab();
            $("#<%=txtDependencyDOB.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyInceptionDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyCovertDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyStartDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });


            $("#<%=txtDependencyCopiedPolicyDOB.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyCopiedPolicyStartDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyCopiedPolicyInceptionDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });
            $("#<%=txtDependencyCopiedPolicyCoverDate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

            $("#<%=txtPolicyStartDate.ClientID %>").datepicker(
                {

                    dateFormat: 'dd MM yyyy',
                    endDate: new Date()
                }
            );


            $("#<%=txtBirthDay.ClientID %>").datepicker({ format: 'dd MM yyyy' });

            $("#<%=txte_idate.ClientID %>").datepicker({ format: 'dd MM yyyy', endDate: new Date() });
            $("#<%=txte_cdate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

            <%--$("#<%=txtBirthDay.ClientID %>").datepicker({ 'format': 'dd MM yyyy' }).on('show', function () {
                var dp = $(this);
                if (dp.val() == '') {
                    dp.val('02-05-1980').datepicker('update');
                }
            });--%>

            $("#<%=txtInception.ClientID %>").datepicker({ format: 'dd MM yyyy', endDate: new Date() });
            $("#<%=txtDebitdate.ClientID %>").datepicker({ format: 'dd MM yyyy' });

            //Hide Calender on date selected
            $("#<%=txtDependencyDOB.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
                getDependencyAge($("#<%=txtDependencyDOB.ClientID %>").val())
            })
            $("#<%=txtDependencyInceptionDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
            $("#<%=txtDependencyInceptionDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
            $("#<%=txtDependencyStartDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');

                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/admin/ManageMember.aspx/DependencyStartdateChange")%>',


                    data: JSON.stringify({ id: $('#<%=ddlPolicy.ClientID%>').val(), date: $('#<%=txtDependencyStartDate.ClientID%>').val() }),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {

                        $('#<%=txtDependencyCovertDate.ClientID%>').val(data.d[0]);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });
            })

            $("#<%=txtDependencyCopiedPolicyDOB.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
                getDependencyAge($("#<%=txtDependencyCopiedPolicyDOB.ClientID %>").val())
            })
            $("#<%=txtDependencyCopiedPolicyInceptionDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })

            $("#<%=txtDependencyCopiedPolicyStartDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');

                $.ajax({
                    type: "POST",
                    url: '<%=ResolveUrl("~/admin/ManageMember.aspx/DependencyStartdateChange")%>',


                    data: JSON.stringify({ id: $('#<%=ddlPolicy.ClientID%>').val(), date: $('#<%=txtDependencyCopiedPolicyStartDate.ClientID%>').val() }),
                    contentType: "application/json; charset=utf-8",
                    success: function (data) {

                        $('#<%=txtDependencyCopiedPolicyCoverDate.ClientID%>').val(data.d[0]);
                    },
                    failure: function (response) {
                        alert(response.d);
                    }
                });

            })

            $("#<%=txtBirthDay.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
                getAge($("#<%=txtBirthDay.ClientID %>").val())
            })
            $("#<%=txtInception.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
            $("#<%=txtPolicyStartDate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
            $("#<%=txtDebitdate.ClientID %>").on('changeDate', function (ev) {
                $(this).datepicker('hide');
            })
            $("#<%=ddlBank.ClientID%>").change(function () {
                $("#<%=txtBranchcode.ClientID%>").val($("#<%=ddlBank.ClientID%>").val());
            });

            //#txtDependencyPremium, #txtPremium'txtPolicyPremium
            $("#<%=txtPremium.ClientID%>").on('input', function () {
                var DependPremium = parseInt($("#<%=txtTotalPremium.ClientID%>").val());
                var Premium = parseInt($("#<%=txtPremium.ClientID%>").val());

                $('#<%=txtTotalPremium.ClientID%>').val(Premium.toString());
                //$('#txtTotalPremium').val((Premium * DependPremium ? Premium * DependPremium : 0).toFixed(2));
            });


            function getDependencyAge(DOB1) {

                var DOB = new Date(DOB1);
                var today = new Date();
                var nowyear = today.getFullYear();
                var nowmonth = today.getMonth();
                var nowday = today.getDate();

                var birthyear = DOB.getFullYear();
                var birthmonth = DOB.getMonth();
                var birthday = DOB.getDate();

                var age = nowyear - birthyear;
                var age_month = nowmonth - birthmonth;
                var age_day = nowday - birthday;
                if (age_month < 0 || (age_month == 0 && age_day < 0)) {
                    age = parseInt(age) - 1;
                }
                $('#<%=txtDependencyAge.ClientID%>').val(age.toString());
                $('#<%=txtDependencyCopiedPolicyAge.ClientID%>').val(age.toString());

            }

            validate();
            $('#MainContent_txtLastName, #MainContent_txtFirstname').change(validate);

            var tab = $("[id*=TabName]").val();
            $('#myTab a[href="#' + tab + '"]').tab('show');
            var liId = tab.replace("tab-", "");
            $('["#tab' + liId + '"]').addClass("active");
            $('["#tab' + liId + '"]').addClass("active");


            $("li").has('a[data-toggle="tab"]').removeClass("active");
            $("li").has('a[href="#tab-3"]').addClass("active");


        });

        function getAge(DOB1) {
            var DOB = new Date(DOB1);
            var today = new Date();
            var nowyear = today.getFullYear();
            var nowmonth = today.getMonth();
            var nowday = today.getDate();

            var birthyear = DOB.getFullYear();
            var birthmonth = DOB.getMonth();
            var birthday = DOB.getDate();

            var age = nowyear - birthyear;
            var age_month = nowmonth - birthmonth;
            var age_day = nowday - birthday;
            if (age_month < 0 || (age_month == 0 && age_day < 0)) {
                age = parseInt(age) - 1;
            }
            $('#<%=txtAge.ClientID%>').val(age.toString());

        }

        function validate() {
            var var1 = $("#<%=txtLastName.ClientID%>").val();
            var var2 = $("#<%=txtFirstname.ClientID%>").val();
            // var var3 = $("#<%=txtIdNumber.ClientID%>").val();
            if (var1 != '' && var2 != '') {
                $('[id$=btnNextStep]').prop("disabled", false);
            }
            else {
                $('[id$=btnNextStep]').prop("disabled", true);
            }
        }

        $(function () {
            $('#invoices').DataTable();
            $('#policy').DataTable();
            $('#SupportedDocuments').DataTable();
            $('#Notes').DataTable();
            $('#Product').DataTable();

        });
        function makeReset(target) {
            $('#tabs-' + target + ' input').val('');
            $('#tabs-' + target + ' select').val('0');
            return false;
        }

        $("#<%=ddlPolicy.ClientID%>").change(function () {
            FillAjaxdata();

        });
        $("#<%=txtPolicyStartDate.ClientID%>").change(function () {
            FillAjaxdata();
        });
        function FillAjaxdata() {

            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/admin/ManageMember.aspx/ddlPolicy_SelectedIndexChanged1")%>',
                //data: '{"id":' + $('#<%=ddlPolicy.ClientID%>').val() + '}',
                // data: JSON.stringify({id:  $('#<%=ddlPolicy.ClientID%>').val() }),
                data: JSON.stringify({ id: $('#<%=ddlPolicy.ClientID%>').val(), date: $('#<%=txtPolicyStartDate.ClientID%>').val() }),
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#<%=txtPolicyPremium.ClientID%>').val(data.d[0]);
                    $('#<%=txtUnderwriter.ClientID%>').val(data.d[1]);
                    $('#<%=hdCoverDate.ClientID%>').val(data.d[2]);
                    $('#<%=txtDependencyCovertDate.ClientID%>').val(data.d[2]);
                    $('#<%=txtCoverDate.ClientID%>').val(data.d[2]);
                    $('#<%=txtTotalPremium.ClientID%>').val(data.d[0]);

                   <%-- $('#<%=txtTotalPremium.ClientID%>').val(data.d[3]);--%>


                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

        $("#<%=drpProductName.ClientID%>").change(function () {
            FillProductAjaxdata();
        });
        function FillProductAjaxdata() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/admin/ManageMember.aspx/ddlProductNameChanged")%>',
                data: '{"id":"' + $('#<%=drpProductName.ClientID%>').val() + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $('#<%=txtPremium.ClientID%>').val(data.d);

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }



        function ViewNote(NoteId) {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/admin/ManageMember.aspx/ViewNoteDetails")%>',
                data: '{"id":"' + NoteId + '"}',
                contentType: "application/json; charset=utf-8",
                success: function (data) {
                    $("#NoteDesription").html(data.d);
                    document.getElementById("NotePopUpWrap").style.display = "block";
                    document.getElementById("NotePopUp").style.display = "block";

                },
                failure: function (response) {
                    alert(response.d);
                }
            });
            return false;

        }



        function hideNotePopUp() {
            document.getElementById("NotePopUpWrap").style.display = "none";
            document.getElementById("NotePopUp").style.display = "none";
        }

        function enabledisabletab() {
            var IsNew = $("#<%=hdnId.ClientID%>").val();
            if (eval(IsNew) == 0) {
                DisableTab();
            }
            else
                EnableTab();

        }

        function goToTab(id) {

            $("[id*=TabName]").val("tab-" + id + "");
        }
        function DisableTab() {
            $("#tab5").hide();
            $("#tab6").hide();
            $("#tab7").hide();
            $("#tab8").hide();
        }
        function EnableTab() {
            $("#tab5").show();
            $("#tab6").show();
            $("#tab7").show();
            $("#tab8").show();
            //$("#tab9").show()
            FillAjaxdata();
        }
    </script>

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

                    // clear the result div
                    //$('#result').empty();
                    // and put together a result message
                    // $('#result').append('<p>South African ID Number:   ' + idNumber + '</p><p>Birth Date:   ' + fullDate + '</p><p>Gender:  ' + gender + '</p><p>SA Citizen:  ' + citzenship + '</p>');
                    $("#<%=txtBirthDay.ClientID %>").val(fullDate);
                $("#<%=txtBirthDay.ClientID %>").val(fullDate).datepicker('update');

                var genderCode = idNumber.substring(6, 10);
                var gender = parseInt(genderCode) < 5000 ? "Female" : "Male";

                var elementRef = document.getElementById('<%= rbtnlGender.ClientID %>');
                var inputElementArray = elementRef.getElementsByTagName('input');

                for (var i = 0; i < inputElementArray.length; i++) {
                    var inputElement = inputElementArray[i];
                    inputElement.checked = false;
                    if (gender == "Male") {
                        var inputElement = inputElementArray[0];
                        inputElement.checked = true;
                    }
                    else {
                        var inputElement = inputElementArray[1];
                        inputElement.checked = true;
                    }
                }
            }
            // otherwise, show the error
            //else {
            //    error.css('display', 'block');
            //}

            return false;
        }

        function isNumber(n) {
            return !isNaN(parseFloat(n)) && isFinite(n);
        }

        //$('#idCheck').submit(Validate);
        function DateComparisionJavascriptFun() {
            var idNumber = $("#<%=txtIdNumber.ClientID %>").val();
            //alert(idNumber);
            var textLength = idNumber.length;
            if (textLength == 13) {
                // //alert(textLength);
                Validate();
            }
        }
        function DateComparisionJavascriptFundep() {
            var idNumberdep = $("#<%=txtDependencyIdNumber.ClientID %>").val();
            //alert(idNumberdep);
            var textLengthdep = idNumberdep.length;
            if (textLengthdep == 13) {
                // //alert(textLength);
                Validatedep();
            }
        }
        function Validatedep() {

            var idNumberd = $("#<%=txtDependencyIdNumber.ClientID %>").val();
            var correctd = true;
            if (idNumberd.length != 13 || !isNumber(idNumberd)) {
                correctd = false;
            }

            var tempDated = new Date(idNumberd.substring(0, 2), idNumberd.substring(2, 4) - 1, idNumberd.substring(4, 6));
            var id_dated = tempDated.getDate();
            var id_monthd = tempDated.getMonth();
            var dMonthd = id_monthd + 1;
            var dMonthNamed = tempDated.getMonthName();
            var id_yeard = tempDated.getFullYear();

            var fullDated = id_dated + " " + dMonthNamed + " " + id_yeard;
            if (!((tempDated.getYear() == idNumberd.substring(0, 2)) && (id_monthd == idNumberd.substring(2, 4) - 1) && (id_dated == idNumberd.substring(4, 6)))) {
                correctd = false;
            }
            var citzenshipd = parseInt(idNumberd.substring(10, 11)) == 0 ? "Yes" : "No";
            var tempTotald = 0;
            var checkSumd = 0;
            var multiplierd = 1;
            for (var i = 0; i < 13; ++i) {
                tempTotald = parseInt(idNumberd.charAt(i)) * multiplierd;
                if (tempTotald > 9) {
                    tempTotald = parseInt(tempTotald.toString().charAt(0)) + parseInt(tempTotald.toString().charAt(1));
                }
                checkSumd = checkSumd + tempTotald;
                multiplierd = (multiplierd % 2 == 0) ? 1 : 2;
            }
            if ((checkSumd % 10) != 0) {
                correctd = false;
            };

            if (correctd) {

                $("#<%=txtDependencyDOB.ClientID %>").val(fullDated);
                $("#<%=txtDependencyDOB.ClientID %>").val(fullDated).datepicker('update');

            }
            // otherwise, show the error
            //else {
            //    error.css('display', 'block');
            //}

            return false;
        }

    </script>
    <script type="text/javascript">

        var count = 0;
        function isDecimalNumber(evt, c) {
            count = count + 1;
            var charCode = (evt.which) ? evt.which : event.keyCode;
            var dot1 = c.value.indexOf('.');
            var dot2 = c.value.lastIndexOf('.');
            if (count > 25) {
                c.value = "";
                count = 0;
            }
            if (dot1 > 2) {
                c.value = "";
            }
            if (charCode != 46 && charCode > 31 && (charCode < 48 || charCode > 57))
                return false;
            else if (charCode == 46 && (dot1 == dot2) && dot1 != -1 && dot2 != -1)
                return false;

            return true;
        }
    </script>

    <div id="NotePopUp">
        <div class="row">
            <div class="col-lg-6">
                <h2>Note</h2>
            </div>
            <div class="col-lg-6">
                <a onclick="hideNotePopUp();return false;" class="pull-right">[x]</a>
            </div>

        </div>
        <div class="row">
            <div class="col-lg-8">
                <p id="NoteDesription"></p>
            </div>

        </div>
    </div>
    <div id="NotePopUpWrap" onclick="hideNotePopUp();"></div>
    <div id="blueimp-gallery" class="blueimp-gallery">
        <div class="slides"></div>
        <h3 class="title"></h3>
        <a class="prev">‹</a>
        <a class="next">›</a>
        <a class="close">×</a>
        <a class="play-pause"></a>
        <ol class="indicator"></ol>
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
                    <rsweb:ReportViewer ID="ReportViewer1" ShowPrintButton="true" runat="server" Width="100%"></rsweb:ReportViewer>
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>

    </div>
    <div class="modal fade" id="TaskCopyPopupModel" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <span data-dismiss="modal" target="_blank" class="closePopUp">x</span>
                    <h4 id="PopForCopyTitle" class="modal-title"></h4>

                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <label>Policy Premium<em>*</em> </label>
                                <asp:TextBox ID="txtPremiumPolicy" runat="server"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="pull-right">
                                    <div class="btn-group">

                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                                    </div>
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <%--<div class="ibox float-e-margins col-sm-6 center">
            <div class="ibox-title">
                <h5 id="PopForCopyTitle"></h5>
                <span data-dismiss="modal" target="_blank" class="closePopUp">x</span>
            </div>
            <div class="ibox-content">
                <div class="row">
                    <div class="col-sm-12">
                        <div>
                            <b>Policy Premium</b>
                            <asp:TextBox ID="txtPremiumPolicy" runat="server"></asp:TextBox>
                        </div>
                        <br />
                        <div style="margin-left: 103px;">
                            <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
                        </div>
                    </div>
                    <div class="col-sm-12">
                    </div>
                </div>
            </div>
        </div>--%>
    </div>
    <div class="modal fade" id="PolicyUpdatePopupModel" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <span data-dismiss="modal" target="_blank" class="closePopUp">x</span>
                    <h4 id="PopForPolicyTitle" class="modal-title">Edit Policy</h4>

                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary6" ValidationGroup="editPolicy" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">

                            <div class="form-group">
                                <label>Policy Name<em>*</em> </label>
                                <asp:DropDownList ID="DDe_Pname" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Select Policy</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="editPolicy" ControlToValidate="DDe_Pname" Display="None" InitialValue="0" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Select Policy"></asp:RequiredFieldValidator>
                                <asp:HiddenField ID="HiddenField1" runat="server" />
                            </div>
                            <div class="form-group">
                                <label>Policy Premium </label>
                                <asp:TextBox MaxLength="10" CssClass="form-control" runat="server" ID="txte_ppremium" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:HyperLink runat="server" ToolTip='Edit ' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkAddonProductID="+Eval("pkiMemberProductID")%>'></asp:HyperLink>
                                                                    <asp:HyperLink OnClientClick="return confirm('Are you sure want to delete?');" runat="server" ToolTip='Delete' ID="HyperLink2" NavigateUrl='<%# "~/Admin/ManageMember.aspx?ID="+MemberId.ToString()+"&PkInoteIDRemove="+Eval("pkiMemberProductID")%>'><i class="fa fa-trash"></i></asp:HyperLink>--%>
                            </div>
                            <div class="form-group">
                                <label>
                                    Policy No / Membership No<em>
                                        <asp:Literal runat="server" ID="Literal1" Text="*"></asp:Literal></em>
                                </label>
                                <asp:TextBox CssClass="form-control" MaxLength="20" runat="server" ID="txte_pnumber" name="txtPolicyNo" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="editPolicy" ControlToValidate="txte_pnumber" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter policy no"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Easy Pay</label>
                                <asp:TextBox MaxLength="50" CssClass="form-control" runat="server" ID="txte_easypay" name="txtEasyToPay" type="text" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Select Branch</label>
                                <label>
                                    <em>
                                        <asp:Literal runat="server" ID="Literal2" Text="*"></asp:Literal></em>
                                </label>
                                <asp:DropDownList ID="DDe_branch" class="form-control" runat="server">
                                    <asp:ListItem Value="0">Select Branch</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ValidationGroup="editPolicy" ControlToValidate="DDe_branch" Display="None" InitialValue="0" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Select a Branch"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Custom Grouping 1</label>
                                <asp:DropDownList ID="DDe_cg1" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Custom Grouping 2</label>
                                <asp:DropDownList ID="DDe_cg2" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                            <div class="form-group">
                                <label>Custom Grouping 3</label>
                                <asp:DropDownList ID="DDe_cg3" class="form-control" runat="server">
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Society</label>
                                <asp:DropDownList ID="DDe_society" class="form-control m-b" runat="server">
                                    <asp:ListItem Value="0">Select Society</asp:ListItem>
                                </asp:DropDownList>
                            </div>

                            <div class="form-group">
                                <label>Policy Start Date</label>
                                <asp:TextBox CssClass="form-control datepicker" MaxLength="30" runat="server" ID="txte_sdate" name="name" type="text" placeholder="Enter Start  Date" class="form-control date1  "></asp:TextBox>

                                <asp:RequiredFieldValidator ValidationGroup="editPolicy" ControlToValidate="txte_sdate" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter Start date" Display="None"></asp:RequiredFieldValidator>

                            </div>

                            <div class="form-group">
                                <label>Policy Inception Date</label>
                                <asp:TextBox CssClass="form-control datepicker" MaxLength="30" runat="server" ID="txte_idate" name="name" type="text" class="form-control date2 "></asp:TextBox>
                                <%--<asp:ValidationSummary runat="server" ID="ValidationSummary3" ValidationGroup="tab4" ForeColor="Red" />--%>
                            </div>
                            <div class="form-group">
                                <label>Policy Cover Date</label>
                                <asp:TextBox CssClass="form-control datepicker" MaxLength="30" runat="server" ID="txte_cdate" name="name" type="text" class="form-control date3 "></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountholder" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter valid account number" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Policy Underwritten By</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txte_puw" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:TextBox MaxLength="255" runat="server" ID="txtbank" name="name" type="text" class="form-control"></asp:TextBox>--%>
                            </div>
                            <div class="form-group">
                                <label>Policy Agent or Broker</label>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtbank" ID="RequiredFieldValidator21" ForeColor="red" runat="server" ErrorMessage="Please enter bank name" Display="None"></asp:RequiredFieldValidator>--%>
                                <asp:DropDownList ID="DDe_pagent" runat="server" DataTextField="Agent" DataValueField="AgentID" CssClass="form-control m-b"></asp:DropDownList>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranch" ID="RequiredFieldValidator24" ForeColor="red" runat="server" ErrorMessage="Please enter bank branch" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Total Premium</label>
                                <asp:TextBox MaxLength="20" runat="server" ID="txte_tpremium" name="name" class="form-control"></asp:TextBox>
                                <asp:HiddenField ID="hdnSelectedMemberId" runat="server" />
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtBranchcode" ID="RequiredFieldValidator25" ForeColor="red" runat="server" ErrorMessage="Please enter branch code" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="pull-right">
                                    <div class="btn-group">

                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnEditPolicy" runat="server" Text="Update Policy" OnClick="btnEditPolicy_Click" />
                                    </div>
                                </div>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountno" ID="RequiredFieldValidator26" ForeColor="red" runat="server" ErrorMessage="Please enter account number" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>

    <div class="modal fade" id="DependencyCopiedPolicyPopupModel" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <span data-dismiss="modal" target="_blank" class="closePopUp">x</span>
                    <h4 id="PopForDependencyTitle" class="modal-title">Add Dependents for a Copiedpolicy</h4>

                </div>
                <div class="modal-body">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="input-group">
                                    <asp:ValidationSummary runat="server" ID="ValidationSummary3" ValidationGroup="addDependentsforCopiedpolicy" ForeColor="Red" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Surname <em>*</em></label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyCopiedPolicyLastName" placeholder="Enter Surname" name="name" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyLastName" ID="RequiredFieldValidator9" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter dependency Surname"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="tab5" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtDependencyLastName" ValidationExpression="[a-zA-Z ]*$" />
                                --%>
                            </div>

                            <div class="form-group">
                                <label>full name<em>*</em></label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyCopiedPolicyFirstName" placeholder="Enter full name" name="name" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyFirstName" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter dependency full name" Display="None"></asp:RequiredFieldValidator>
                                                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator7" ValidationGroup="tab5" runat="server" ErrorMessage="full name Enter Only characters" ControlToValidate="txtDependencyFirstName" ValidationExpression="[a-zA-Z ]*$" />
                                --%>
                            </div>
                            <div class="form-group">
                                <label>ID Number <em></em></label>
                                <asp:TextBox MaxLength="20" runat="server" ID="txtDependencyCopiedPolicyIdNumber" placeholder="Enter ID Number" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFundep();"></asp:TextBox>
                                <%-- <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="CustomValidator2" OnServerValidate="cvIdvalidation_ServerValidate2" ControlToValidate="txtDependencyIdNumber" ValidationGroup="tab5" runat="server" />
                                                                    <%#Eval("Notes") %>   --%>                                                                 <%--<asp:HyperLink runat="server" ToolTip='Edit' ID="hrLink" NavigateUrl='<%# "~/Admin/ManageMember.aspx?Id="+MemberId.ToString()+"&PkInoteIDList="+Eval("pkiNoteID")%>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                            </div>
                            <div class="form-group">
                                <label>Date Of Birth <em>*</em></label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtDependencyCopiedPolicyDOB" placeholder="Enter Date Of Birth" name="name" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyDOB" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter date of birth" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Age</label>
                                <asp:TextBox MaxLength="10" runat="server" ID="txtDependencyCopiedPolicyAge" ReadOnly="true" Text="Will be Calculated From Date Of Birth" name="name" class="form-control"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Gender</label>


                                <br />
                                &nbsp &nbsp
                                                                    <asp:RadioButton ID="nbtnDependencyCopiedPolicyMale" GroupName="Gender" runat="server" Text="Male" />&nbsp &nbsp                       &nbsp &nbsp
                                                                      <asp:RadioButton ID="nbtnDependencyCopiedPolicyFemale" GroupName="Gender" runat="server" Text="Female" />

                            </div>

                        </div>

                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Start Date <em>*</em></label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyCopiedPolicyStartDate" placeholder="Enter Start  Date" name="name" class="form-control"></asp:TextBox>
                                <%--  <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyStartDate" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter Start date" Display="None"></asp:RequiredFieldValidator>
                                --%>
                            </div>
                            <div class="form-group">
                                <label>Inception Date <em>*</em></label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyCopiedPolicyInceptionDate" placeholder="Enter Inception Date" name="name" class="form-control"></asp:TextBox>
                                <%--<asp:Label ID="lbldoctype" runat="server" Text='<%#Eval("DocType")%>'></asp:Label>--%>
                            </div>

                            <div class="form-group">
                                <label>Cover Date <em>*</em></label>
                                <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDependencyCopiedPolicyCoverDate" ReadOnly="true" placeholder="Enter Cover Date" name="name" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyCovertDate" ID="RequiredFieldValidator33" ForeColor="red" runat="server" ErrorMessage="Please enter cover date" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Select Dependency Type</label>
                                <asp:DropDownList ID="ddlCopiedPolicyDependencyType" class="form-control m-b" runat="server">
                                </asp:DropDownList>
                            </div>
                            <%--<div class="form-group">
                                                                    <label>Select RelationShip</label>
                                                                    <asp:DropDownList ID="ddlDependencyRelationship" class="form-control m-b" runat="server">
                                                                    </asp:DropDownList>
                                                                </div>--%>

                            <div class="form-group">
                                <label>Premium <em>*</em></label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtDependencyCopiedPolicyPremium" placeholder="Enter Preminum" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab5" ControlToValidate="txtDependencyPremium" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Please enter premium" Display="None"></asp:RequiredFieldValidator>
                                --%>
                            </div>

                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="pull-right">
                                    <div class="btn-group">

                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnDependencyCopiedPolicy" runat="server" Text="Add Dependent to Copied Policy" OnClick="btnDependencyCopiedPolicy_Click" />
                                    </div>
                                </div>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtAccountno" ID="RequiredFieldValidator26" ForeColor="red" runat="server" ErrorMessage="Please enter account number" Display="None"></asp:RequiredFieldValidator>--%>
                            </div>
                        </div>
                    </div>
                </div>

            </div>
        </div>
    </div>



    <script>
        function selectFollowUpPopUp(id) {

            if (id == "Report") {
                // alert("3");
                $('#TaskFollowUpModel').modal('show');
                document.getElementById("PopFollowUpTitle").innerHTML = "Policy Doc";
            }
        }
    </script>

    <script>
        function GetForCopyDataPopUp(id) {

            if (id == "Report") {
                $('#TaskCopyPopupModel').modal('show');
                document.getElementById("PopForCopyTitle").innerHTML = "Edit Copy Policy";
            }
        }
    </script>
    <script>
        function GetForPolicyDataPopUp(id) {
            // alert(id);
            if (id == "Report") {
                $('#PolicyUpdatePopupModel').modal('show');
                document.getElementById("PopForPolicyTitle").innerHTML = "Edit Policy";
            }
        }
    </script>
    <script>
        function GetForDependencyCopiedPolicyDataPopUp(id) {
            // alert(id);
            if (id == "Report") {
                $('#DependencyCopiedPolicyPopupModel').modal('show');
                document.getElementById("PopForDependencyTitle").innerHTML = "Add Dependents for a Copiedpolicy";
            }
        }
    </script>

    <script>

        $("#<%=DDe_Pname.ClientID%>").change(function () {
            FillAjaxdataForUpdatePremium();
            getTotalPremium();
        });
        $("#<%=txte_sdate.ClientID%>").change(function () {
            FillAjaxdataForUpdatePremium();
        });
        function FillAjaxdataForUpdatePremium() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/admin/ManageMember.aspx/ddlPolicy_SelectedIndexChanged")%>',
                        data: JSON.stringify({ id: $('#<%=DDe_Pname.ClientID%>').val(), date: $('#<%=txte_sdate.ClientID%>').val(), parlorId: $('#<%=hdnParlourid.ClientID%>').val() }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $('#<%=txte_ppremium.ClientID%>').val(data.d[0]);
                            $('#<%=txte_puw.ClientID%>').val(data.d[1]);

                   <%-- $('#<%=hdEditCoverDate.ClientID%>').val(data.d[2]);--%>

                            $('#<%=txte_cdate.ClientID%>').val(data.d[2]);


                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }
        function getTotalPremium() {
            $.ajax({
                type: "POST",
                url: '<%=ResolveUrl("~/admin/ManageMember.aspx/getTotalPremiumForMember")%>',
                        data: JSON.stringify({ planId: $('#<%=DDe_Pname.ClientID%>').val(), id: $('#<%=hdnSelectedMemberId.ClientID%>').val(), parlorId: $('#<%=hdnParlourid.ClientID%>').val() }),
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            $('#<%=txte_tpremium.ClientID%>').val(data.d);
                },
                failure: function (response) {
                    alert(response.d);
                }
            });
        }

    </script>
    <script>

        //$(function () {
        //    $('#txte_sdate').datetimepicker({
        //        language: 'pt-BR'
        //    });
        //});


        $('.date1').datepicker({
            dateFormat: 'dd-mm-yyyy',
            //minDate: '+5d',
            //changeMonth: true,
            //changeYear: true,
            //altField: "#idTourDateDetailsHidden",
            //altFormat: "yy-mm-dd"
        });
        $('.date2').datepicker({
            dateFormat: 'dd-mm-yyyy',
            //minDate: '+5d',
            //changeMonth: true,
            //changeYear: true,
            //altField: "#idTourDateDetailsHidden",
            //altFormat: "yy-mm-dd"
        });
        $('.date3').datepicker({
            dateFormat: 'dd-mm-yyyy',
            //minDate: '+5d',
            //changeMonth: true,
            //changeYear: true,
            //altField: "#idTourDateDetailsHidden",
            //altFormat: "yy-mm-dd"
        });


    </script>
</asp:Content>
