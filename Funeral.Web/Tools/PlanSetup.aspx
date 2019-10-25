<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="PlanSetup.aspx.cs" Inherits="Funeral.Web.Tools.PlanSetup" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        .chkChoice td {
            padding-left: 30px;
        }

        em {
            color: red;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryPlanSetup" ValidationGroup="PlanSetup" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Plan Details</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-6">
                            <div class="form-group" runat="server" id="dvAdministrator">
                                <div class="form-group">
                                    <label>Company </label>
                                    <asp:DropDownList ID="ddlCompanyList" CssClass="form-control" runat="server"></asp:DropDownList>
                                </div>
                            </div>
                            <div class="form-group">
                                <label>Plan Name <em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtPlanName" name="PlanName" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtPlanName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Plan Name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Description of plan/Package <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtDescription" name="Description" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtDescription" ID="RequiredFieldValidator12" ForeColor="red" runat="server" ErrorMessage="Please enter Description of plan/Package"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Premium <em>*</em> </label>
                                <%=Currency.Trim() %><asp:TextBox MaxLength="20" runat="server" ID="txtPremium" name="Premium" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtPremium" ID="RequiredFieldValidator13" ForeColor="red" runat="server" ErrorMessage="Please enter Premium"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator7" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtPremium" ErrorMessage="Premium Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Joining fee </label>
                                <%=Currency.Trim() %><asp:TextBox MaxLength="20" runat="server" ID="txtJoiningfee" name="Joiningfee" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtJoiningfee" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Joining fee"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator8" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtJoiningfee" ErrorMessage="Joining fee Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>cover amount <em>*</em> </label>
                                <%=Currency.Trim() %><asp:TextBox MaxLength="25" runat="server" ID="txtMianMember" name="MianMember" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtMianMember" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter Main Member/cover amount"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator9" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtMianMember" ErrorMessage=">Main Member/cover amount Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Admin Split </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtAdminSplit" name="AdminSplit" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtAdminSplit" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Admin Split"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator6" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAdminSplit" ErrorMessage="Admin Split Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <%--<div class="form-group">
                                <label>Cash Payout  </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtCashPayout" name="CashPayout" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtAdminSplit" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Admin Split"></asp:RequiredFieldValidator>--%>
                            <%-- <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator14" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtCashPayout" ErrorMessage="Cash Payout Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>--%>
                        </div>
                        <div class="col-lg-6">
                            <%-- <div class="form-group">
                                <label>Underwriter Name&nbsp; </label>
                                <%--<asp:TextBox runat="server" ID="txtUnderwriterName" name="UnderwriterName" type="text" class="form-control"></asp:TextBox>--%>
                            <%--<asp:DropDownList runat="server" ID="ddlUnderwriter" name="ddlUnderwriter" class="form-control"></asp:DropDownList>
                            </div>--%>
                            <%--<div class="form-group">
                                <label>Underwriter split </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtUnderwriterSpit" name="UnderwriterSplit" type="text" class="form-control"></asp:TextBox>
                                <%-- <asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtUnderwriterSpit" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter Underwriter split"></asp:RequiredFieldValidator>--%>
                            <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtUnderwriterSpit" ErrorMessage="Underwriter split Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>--%>


                            <div class="form-group">
                                <label>Cash Payout  </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtCashPayout" name="CashPayout" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtAdminSplit" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Admin Split"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator14" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtCashPayout" ErrorMessage="Cash Payout Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Agent split </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtAgentSpit" name="AgentSpit" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtAgentSpit" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Agent Spit"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAgentSpit" ErrorMessage="Agent split Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Office Split </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtOfficeSplit" name="OfficeSplit" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtOfficeSplit" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter Office Split"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtOfficeSplit" ErrorMessage="Office Split Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Company Split </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtCompanySplit" name="CompanySplit" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtCompanySplit" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter Company Split"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtCompanySplit" ErrorMessage="Company Split Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                            <div class="form-group">
                                <label>Head Manager </label>
                                <%=Currency.Trim() %><asp:TextBox runat="server" ID="txtHeadManager" name="HeadManager" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtHeadManager" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter Head Manager"></asp:RequiredFieldValidator>--%>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator5" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtHeadManager" ErrorMessage="Head Manager Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Dependants</h5>
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <label for="exampleInputEmail2">Spouse</label>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlSpouse" class="form-control" runat="server">
                                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6" id="div2" runat="server">
                                                    <div class="form-group form-inline">
                                                        <label><%=Currency.Trim() %></label><asp:TextBox runat="server" ID="txtSpouseCover" name="SpouseCover" type="text" class="form-control" placeholder="Enter Spouse Cover"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtSpouseCover" ID="RequiredFieldValidator14" ForeColor="red" runat="server" ErrorMessage="Please enter Spouse Cover"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator18" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtSpouseCover" ErrorMessage="Spouse Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="form-group">
                                        <label for="ddlChildren">children</label>
                                        <div class="col-sm-12">
                                            <div class="row">
                                                <div class="col-sm-6">
                                                    <asp:DropDownList ID="ddlChildren" class="form-control" runat="server">
                                                        <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                        <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                        <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                        <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                        <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                        <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                        <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                        <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                        <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                        <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                        <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                        <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                        <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                        <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                        <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                        <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                        <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                        <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                        <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                        <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                        <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                        <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                        <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                        <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                        <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                        <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                        <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                        <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                        <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                        <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                        <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="col-sm-6" id="divage" runat="server">
                                                    <div class="form-group form-inline">
                                                        <%--<label>0-5 years </label>--%>
                                                        <label><%=Currency.Trim() %></label><asp:TextBox MaxLength="10" runat="server" ID="txtFirstYear" name="FirstYear" type="text" class="form-control" placeholder="Enter 0-5 years Cover"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtFirstYear" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter 0-5 years"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator11" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtFirstYear" ErrorMessage="0-5 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                    </div>
                                                    <div class="form-group form-inline">
                                                        <%--<label>6-13 years </label>--%>
                                                        <label><%=Currency.Trim() %></label>
                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtSecondYear" name="SecondYear" type="text" class="form-control" placeholder="Enter 6-13 years Cover"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtSecondYear" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter 6-13 years"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator13" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtSecondYear" ErrorMessage="6-13 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                    </div>
                                                    <div class="form-group form-inline">
                                                        <label><%=Currency.Trim() %></label>
                                                        <asp:TextBox MaxLength="10" runat="server" ID="txtThirdYear" name="ThirdYear" type="text" class="form-control" placeholder="Enter 14-21 years Cover"></asp:TextBox>
                                                        <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtThirdYear" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter 14-21 years"></asp:RequiredFieldValidator>--%>
                                                        <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator12" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtThirdYear" ErrorMessage="14-21 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>
                                    </div>
                                </div>
                                <div class="col-lg-6">
                                    <div class="form-group">
                                        <div class="form-group">
                                            <label for="ddlAdults">Adults</label>
                                            <div class="col-sm-12">
                                                <div class="row">
                                                    <div class="col-sm-6">
                                                        <asp:DropDownList ID="ddlAdults" class="form-control" runat="server">
                                                            <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                            <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                            <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                            <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                            <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                            <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                            <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                            <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                            <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                            <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                            <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                            <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                            <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                            <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                            <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                            <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                            <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                            <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                            <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            <asp:ListItem Text="19" Value="19"></asp:ListItem>
                                                            <asp:ListItem Text="20" Value="20"></asp:ListItem>
                                                            <asp:ListItem Text="21" Value="21"></asp:ListItem>
                                                            <asp:ListItem Text="22" Value="22"></asp:ListItem>
                                                            <asp:ListItem Text="23" Value="23"></asp:ListItem>
                                                            <asp:ListItem Text="24" Value="24"></asp:ListItem>
                                                            <asp:ListItem Text="25" Value="25"></asp:ListItem>
                                                            <asp:ListItem Text="26" Value="26"></asp:ListItem>
                                                            <asp:ListItem Text="27" Value="27"></asp:ListItem>
                                                            <asp:ListItem Text="28" Value="28"></asp:ListItem>
                                                            <asp:ListItem Text="29" Value="29"></asp:ListItem>
                                                            <asp:ListItem Text="30" Value="30"></asp:ListItem>
                                                        </asp:DropDownList>
                                                    </div>
                                                    <div class="col-sm-6" id="div1" runat="server">
                                                        <div class="form-group form-inline">
                                                            <label><%=Currency.Trim() %> </label>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtAdultCover1" name="FirstYear" type="text" class="form-control" placeholder="Enter 22-40 years Cover"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtFirstYear" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter 0-5 years"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator10" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAdultCover1" ErrorMessage="0-5 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                        </div>
                                                        <div class="form-group form-inline">
                                                            <%--<label>6-13 years </label>--%>
                                                            <label><%=Currency.Trim() %> </label>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtAdultCover2" name="SecondYear" type="text" class="form-control" placeholder="Enter 41-59 years Cover"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtSecondYear" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="Please enter 6-13 years"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator15" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAdultCover2" ErrorMessage="6-13 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                        </div>
                                                        <div class="form-group form-inline">
                                                            <%--<label>14-21 years </label>--%>
                                                            <label><%=Currency.Trim() %> </label>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtAdultCover3" name="ThirdYear" type="text" class="form-control" placeholder="Enter 60-65 years Cover"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtThirdYear" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter 14-21 years"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator16" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAdultCover3" ErrorMessage="14-21 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                        </div>
                                                        <div class="form-group form-inline">
                                                            <label><%=Currency.Trim() %> </label>
                                                            <asp:TextBox MaxLength="10" runat="server" ID="txtAdultCover4" name="ThirdYear" type="text" class="form-control" placeholder="Enter 66-75 years Cover"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="PlanSetup" ControlToValidate="txtThirdYear" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter 14-21 years"></asp:RequiredFieldValidator>--%>
                                                            <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator17" ValidationGroup="PlanSetup" runat="server" ControlToValidate="txtAdultCover4" ErrorMessage="14-21 years Cover Enter Only Number With 2 Desimal" ValidationExpression="((\d+)((\.\d{1,2})?))$" />
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                            <div class="form-group">
                                                <label for="ddlWaiting">Waiting Period</label>
                                                <div class="col-sm-12">
                                                    <div class="row">
                                                        <div class="col-sm-6">
                                                            <asp:DropDownList ID="ddlWaitingPeriod" class="form-control" runat="server">
                                                                <asp:ListItem Text="0" Value="0"></asp:ListItem>
                                                                <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                <asp:ListItem Text="13" Value="13"></asp:ListItem>
                                                                <asp:ListItem Text="14" Value="14"></asp:ListItem>
                                                                <asp:ListItem Text="15" Value="15"></asp:ListItem>
                                                                <asp:ListItem Text="16" Value="16"></asp:ListItem>
                                                                <asp:ListItem Text="17" Value="17"></asp:ListItem>
                                                                <asp:ListItem Text="18" Value="18"></asp:ListItem>
                                                            </asp:DropDownList>
                                                        </div>
                                                        <div class="col-sm-6" id="div4" runat="server">
                                                            <div class="form-group">

                                                                <asp:DropDownList ID="ddlLaspsedperiod" class="form-control" placeholder="Choose Lapsed Period" runat="server">

                                                                    <asp:ListItem Text="Choose Lapsed Period" Value="0"></asp:ListItem>
                                                                    <asp:ListItem Text="1" Value="1"></asp:ListItem>
                                                                    <asp:ListItem Text="2" Value="2"></asp:ListItem>
                                                                    <asp:ListItem Text="3" Value="3"></asp:ListItem>
                                                                    <asp:ListItem Text="4" Value="4"></asp:ListItem>
                                                                    <asp:ListItem Text="5" Value="5"></asp:ListItem>
                                                                    <asp:ListItem Text="6" Value="6"></asp:ListItem>
                                                                    <asp:ListItem Text="7" Value="7"></asp:ListItem>
                                                                    <asp:ListItem Text="8" Value="8"></asp:ListItem>
                                                                    <asp:ListItem Text="9" Value="9"></asp:ListItem>
                                                                    <asp:ListItem Text="10" Value="10"></asp:ListItem>
                                                                    <asp:ListItem Text="11" Value="11"></asp:ListItem>
                                                                    <asp:ListItem Text="12" Value="12"></asp:ListItem>
                                                                </asp:DropDownList>
                                                            </div>
                                                        </div>
                                                    </div>
                                                </div>
                                            </div>
                                        </div>

                                    </div>
                                </div>
                            </div>
                            <div class="form-group">
                                <div class="btn-group">
                                    <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" />
                                </div>
                                <div class="btn-group">
                                    <asp:Button runat="server" ID="btnSubmite" ValidationGroup="PlanSetup" CssClass="btn btn-sm btn-primary" Text="Save" OnClick="btnSubmite_Click" />
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
                    <h5>Plan List</h5>
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
                                <asp:GridView ID="gvPlan" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AllowSorting="true" OnSorting="gvPlan_Sorting"
                                    AllowPaging="true" PageSize="25" OnPageIndexChanging="gvPlan_PageIndexChanging" AutoGenerateColumns="False" EmptyDataText="There are no jobs to display."
                                    OnRowDataBound="gvPlan_OnRowDataBound" OnRowCommand="gvPlan_RowCommand">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>
                                        <asp:BoundField DataField="PlanName" HeaderText="Plan Name" ReadOnly="True" SortExpression="PlanName" />
                                        <asp:BoundField DataField="PlanDesc" HeaderText="Plan Description" ReadOnly="True" SortExpression="PlanDesc" />
                                        <asp:BoundField DataField="PlanSubscription" HeaderText="Premium" ReadOnly="True" SortExpression="PlanSubscription" DataFormatString="{0:n}" />
                                        <asp:BoundField DataField="PlanUnderwriter" HeaderText="Underwriter Name" ReadOnly="True" SortExpression="PlanUnderwriter" />
                                        <asp:BoundField DataField="LastModified" HeaderText="Modified date" DataFormatString="{0:d}" HeaderStyle-CssClass="visible-lg" ItemStyle-CssClass="visible-lg" SortExpression="LastModified" />
                                        <%--<asp:HyperLinkField DataTextField="PolicyStatus" HeaderText="Policy Status" DataNavigateUrlFields="MemeberNumber,parlourid" DataNavigateUrlFormatString="~/Admin/ManageMembersPayment.aspx?Id={0}&ParlourId={1}" />--%>
                                        <asp:TemplateField HeaderText="Actions">
                                            <ItemTemplate>
                                                <%--<asp:HyperLink runat="server" ToolTip='Click here to Edit - ' ID="hrLink" NavigateUrl='<%#Eval("pkiMemberID", "~/Admin/ManageMember.aspx?Id={0}") %>'><i class="fa fa-edit"></i></asp:HyperLink>--%>
                                                <% if (this.HasEditRight)
                                                    {%>
                                                <asp:LinkButton runat="server" ToolTip='Click here to Edit - ' ID="lbtnEdit" CommandArgument='<%#Eval("pkiPlanID")%>' CommandName="EditPlan"><i class="fa fa-edit"></i></asp:LinkButton>
                                                <%}
                                                    if (this.HasDeleteRight)
                                                    { %>
                                                <asp:LinkButton runat="server" ID="lbtnDelete" OnClientClick="return confirm('Are you sure you want to delete?')" CommandArgument='<%#Eval("pkiPlanID")%>' CommandName="deletePlan"><i class="fa fa-trash-o"></i></asp:LinkButton>
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
</asp:Content>
