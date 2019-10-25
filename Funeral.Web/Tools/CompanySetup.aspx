<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CompanySetup.aspx.cs" Inherits="Funeral.Web.Tools.CompanySetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .chkChoice td {
            padding-left: 30px;
        }

        em {
            color: red;
        }
        .formfield * {
    vertical-align: middle;
}
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">

        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label><%--Showing cfirmation message--%>
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryCompanySetup" ValidationGroup="CompanySetup" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Company Setup</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Company Name <em>*</em> </label>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtCompanyName" name="CompanyName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtCompanyName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Company Name"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="CompanySetup" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                            <div class="form-group">
                                <label>Registration Number <%--<em>*</em>--%>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtRegistrationNumber" name="RegistrationNumber" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtRegistrationNumber" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Registration Number"></asp:RequiredFieldValidator>--%>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtRegistrationNumber" ErrorMessage="Registration Number Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                            <div class="form-group">
                                <label>Fsb Number </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtFsbNumber" name="FsbNumber" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtFsbNumber" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Fsb Number"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Company Slogan<%-- <em>*</em>--%> </label>
                                <asp:TextBox MaxLength="100" runat="server" ID="txtCompanySlogan" name="CompanySlogan" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtCompanySlogan" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter Company Slogan"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="row">
                                <div class="col-sm-11">
                                    <div class="form-group">
                                        <label>Company Rules </label>
                                        <asp:TextBox runat="server" ID="txtcompanyRules" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                    </div>
                                </div>
                            </div>
                            <div class="row">
                                <div class="col-sm-9">
                                    <div class="form-group">
                                        <label>Select Company Logo</label>
                                        <asp:FileUpload ID="fucompanyLogo" runat="server" class="form-control" />
                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                    </div>
                                    <div class="form-group">
                                        <label>Terms And Condition</label>
                                        <asp:TextBox runat="server" ID="txtTnC" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                       
                                    </div>
                                   <div class="form-group">
                                        <label>Allow Auto Generate Policy Number </label>
                                       <asp:CheckBox ID="cbAutoGeneratePolicy" runat="server" />
                                     </div>
                                    <div class="form-group">
                                        <label>VAT Number: </label>
                                     <asp:TextBox MaxLength="25" runat="server" ID="txtVatNo" name="txtVatNo" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                </div>
                                <asp:Button runat="server" ID="btnUpload" CssClass="btn btn-sm btn-primary pull-right" OnClick="btnUpload_Click" Text="Upload" Enabled="false" />
                            </div>

                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Email Address  <%--<em>*</em>--%> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtEmail" name="Email" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="CompanySetup" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtEmail" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Email Address"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Telephone Number <%--<em>*</em>--%> </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtTelePhone" name="TelePhone" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtTelePhone" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter cell Telephone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />--%>
                            </div>
                            <div class="form-group">
                                <label>Cellphone Number </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtCellphone" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtCellphone" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter cell phone number"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtCellphone" ErrorMessage="Cellphone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                            <div class="form-group">
                                <label>Fax Number <%--<em>*</em>--%>  </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtFaxNumber" name="FaxNumber" type="text" class="form-control"></asp:TextBox>
                               <%-- <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtFaxNumber" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter Fax Number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator7" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtFaxNumber" ErrorMessage="Fax Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />--%>
                            </div>
                            <p class="formfield">
                                <label for="textarea">Logo</label>
                                <asp:Image runat="server" ID="ImagePreview" Height="164px" Width="375px" />
                            </p>
                            <div class="form-group">
                                <label>Terms And Condition For Funeral</label>
                                <asp:TextBox runat="server" ID="txtTncFuneral" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>

                            </div>
                            <div class="form-group">
                                <label>Terms And Condition For TombStones</label>
                                <asp:TextBox runat="server" ID="txtTncTombstone" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Policy DECLARATION</label>
                                <asp:TextBox runat="server" ID="txtPolicyDeclaration" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Owners Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>First Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtownFirstName" name="ownFirstName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtownFirstName" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Owner First Name"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="CompanySetup" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                            <div class="form-group">
                                <label>Last Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtownLastName" name="ownLastName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtownLastName" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter Owner Last Name"></asp:RequiredFieldValidator>
                                <%--<asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="CompanySetup" runat="server" ErrorMessage="Company Name Enter Only characters" ControlToValidate="txtCompanyName" ValidationExpression="[a-zA-Z ]*$" />--%>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Telephone Number <em>*</em> </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtownTelephoneNumber" name="ownTelePhone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtownTelephoneNumber" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Owners Telephone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtownTelephoneNumber" ErrorMessage="Owners Telephone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                            <div class="form-group">
                                <label>Cellphone Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="15" runat="server" ID="txtOwnersCellphone" name="OwnersCellphone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtOwnersCellphone" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Owners cell phone number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator5" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtOwnersCellphone" ErrorMessage="Owners Cellphone Number Enter Only Number" ValidationExpression="^([0-9\(\)\/\+ \-]*)$" />
                            </div>
                            <div class="form-group">
                                <label>Email Address  <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtOwnersEmail" name="OwnersEmail" type="text" class="form-control"></asp:TextBox>
                                <asp:RegularExpressionValidator Display="None" runat="server" ID="RegularExpressionValidator1" ValidationGroup="CompanySetup" ControlToValidate="txtOwnersEmail" ErrorMessage="Please enter valid Owners email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtOwnersEmail" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter Owners Email Address"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Address</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12 form-horizontal">
                            <div class="form-group">
                                <label class="col-lg-2">line 1 <em>*</em> </label>
                                <div class="col-lg-10">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline1" name="line1" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtline1" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter line 1"></asp:RequiredFieldValidator>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">line 2 </label>
                                <div class="col-lg-10">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline2" name="line2" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtline2" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter line 2"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">line 3</label>
                                <div class="col-lg-10">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline3" name="line3" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtline3" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="Please enter line 3"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">line 4 </label>
                                <div class="col-lg-10">
                                    <asp:TextBox MaxLength="100" runat="server" ID="txtline4" name="line4" type="text" class="form-control"></asp:TextBox>
                                    <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtline4" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Please enter line 4"></asp:RequiredFieldValidator>--%>
                                </div>
                            </div>
                            <div class="form-group">
                                <label class="col-lg-2">postal code <em>*</em> </label>
                                <div class="col-lg-10">
                                    <asp:TextBox MaxLength="30" runat="server" ID="txtpostalcode" name="txtPassport" type="text" class="form-control"></asp:TextBox>
                                    <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtpostalcode" ID="RequiredFieldValidator16" ForeColor="red" runat="server" ErrorMessage="Please Enter Postal Code"></asp:RequiredFieldValidator>
                                    <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="CompanySetup" runat="server" ControlToValidate="txtpostalcode" ErrorMessage="Postal Code Enter Only Number" ValidationExpression="^[0-9]*$" />
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
                    <h5>SMS Sending Setting</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="form-group">
                            <asp:CheckBoxList ID="chksmsGroup" Width="100%" runat="server" DataTextField="Name" DataValueField="ID" RepeatColumns="3" RepeatLayout="Table"></asp:CheckBoxList>
                            <%-- MaxLength="30" runat="server" ID="TextBox1" name="UserName" type="text" class="form-control"></asp:CheckBoxList>--%>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Bank Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Account Holder <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaccountholder" name="CompanyName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtaccountholder" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter Account Holder"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Bank name<em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtbankname" name="RegistrationNumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtbankname" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="Please enter Bank name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Account Number <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtaccountnumber" name="FsbNumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtaccountnumber" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="Please enter Account Number"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Account type  <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtaccounttype" name="Email" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtaccounttype" ID="RequiredFieldValidator17" ForeColor="red" runat="server" ErrorMessage="Please enter Account type"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Branch <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtbranch" name="TelePhone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtbranch" ID="RequiredFieldValidator18" ForeColor="red" runat="server" ErrorMessage="Please enter Branch"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Branch code <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtbranchcode" name="Cellphone" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtbranchcode" ID="RequiredFieldValidator19" ForeColor="red" runat="server" ErrorMessage="Please enter Branch code"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="form-group">
                <div class="btn-group">
                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                </div>
                <div class="btn-group">
                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="CompanySetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div id="PaymentHistory" class="ui-tabs-panel ui-widget-content ui-corner-bottom" runat="server">
                <div class="dataTables_wrapper" id="tblMembersSearch_wrapper">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="ibox ">
                                <div class="ibox-title">
                                    <h5>Company List</h5>
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
                                                <label>Search Company :</label>
                                                <asp:Panel ID="pnlSearch" CssClass="input-group" DefaultButton="btnSearch" runat="server">
                                                    <asp:TextBox runat="server" ID="txtKeyword" MaxLength="50" CssClass="form-control" placeholder="Search by keyword"></asp:TextBox>
                                                    <span class="input-group-btn">&nbsp;<asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-w-m btn-primary" OnClick="btnSearch_Click" />
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
                                                <asp:GridView ID="gvCompany" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="true" OnSorting="gvCompany_Sorting"
                                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvCompany_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                                    OnRowCommand="gvCompany_RowCommand">
                                                    <PagerStyle CssClass="pagination-ys" />
                                                    <Columns>
                                                        <asp:BoundField DataField="ApplicationName" HeaderText="Company Name" ReadOnly="True" SortExpression="ApplicationName" />
                                                        <asp:BoundField DataField="OwnerFirstName" HeaderText="Owner First Name" ReadOnly="True" SortExpression="OwnerFirstName" />
                                                        <asp:BoundField DataField="RegistrationNumber" HeaderText="Registration Number" ReadOnly="True" SortExpression="RegistrationNumber" />
                                                        <asp:BoundField DataField="FSBNumber" HeaderText="Fsb Number" ReadOnly="True" SortExpression="FSBNumber" />
                                                        <asp:BoundField DataField="ManageEmail" HeaderText="Email" ReadOnly="True" SortExpression="ManageEmail" />
                                                        <asp:BoundField DataField="ManageTelNumber" HeaderText="Phone Number" ReadOnly="True" SortExpression="ManageTelNumber" />
                                                        <%--<asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="LastModified" />--%>
                                                        <%--<asp:HyperLinkField DataTextField="PolicyStatus" HeaderText="Policy Status" DataNavigateUrlFields="MemeberNumber,parlourid" DataNavigateUrlFormatString="~/Admin/ManageMembersPayment.aspx?Id={0}&ParlourId={1}" />--%>
                                                        <asp:TemplateField HeaderText="Actions">
                                                            <ItemTemplate>
                                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("pkiApplicationID")%>' CommandName="EditCompany"><i class="fa fa-edit"></i></asp:LinkButton>
                                                                <asp:HyperLink runat="server" ToolTip='Click here to Add Application User - ' ID="hrLink" NavigateUrl='<%#Eval("parlourid", "~/Tools/UserSetup.aspx?CompanyParlourID={0}") %>'><i class="fa fa-plus"></i></asp:HyperLink>
                                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiApplicationID")%>' CommandName="deleteCompany"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
            <!--SerachBox Enter CLick Event-->
            <script type="text/javascript">
                document.getElementById('<%=txtKeyword.ClientID%>').setAttribute("onkeyup", "runScript(event);return false;");
                function runScript(e) {
                    if (e.keyCode == 13) {
                        document.getElementById('<%=btnSearch.ClientID%>').click();
                        return false;
                    }
                }
            </script>
        </div>

    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
