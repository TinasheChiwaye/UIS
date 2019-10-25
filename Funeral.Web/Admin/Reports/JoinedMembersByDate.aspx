<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="JoinedMembersByDate.aspx.cs" Inherits="Funeral.Web.Admin.Reports.JoinedMembersByDate" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>


    <div class="row">
        <div class="col-sm-3 sideBar padding-top-20">
            <div class="row ">

                <div class="panel panel-primary">
                    <div class="panel-heading">
                        <i class="fa-2x fa fa-newspaper-o pull-left"></i>
                        <h3><span class="nav-label">Report Setting</span></h3>
                    </div>
                    <div class="panel-body">
                        <div class="col-sm-10">
                            <div class="form-group">
                                <label class="" for="email">Date From:</label>
                                <asp:TextBox CssClass="form-control" placeholder="DD/MM/YYYY" ID="txtDateFrom" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator3" ForeColor="Red" ControlToValidate="txtDateFrom" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label class="" for="email">Date From:</label>
                                <asp:TextBox CssClass="form-control" placeholder="DD/MM/YYYY" ID="txtDateTo" runat="server"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ForeColor="Red" ControlToValidate="txtDateTo" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>
                            </div>
                            <%-- <div class="form-group" id="data_1">
                                <label class="font-noraml">Simple data input format</label>
                                <div class="input-group date">
                                    <span class="input-group-addon"><i class="fa fa-calendar"></i></span><input type="text" class="form-control" value="03/04/2014">
                                </div>
                            </div>--%>
                            <button runat="server" id="btnRun" onserverclick="btnSubmit_Click" class="btn btn-outline btn-primary" title="Generate">
                                <i class="fa fa-check"></i> Generate
                            </button>
                            <%--   <i class='fa fa-check'></i><asp:Button ID="btnSubmit" CssClass="btn btn-outline btn-primary" runat="server" Text=" Generate" OnClick="btnSubmit_Click" />--%>
                        </div>
                    </div>
                </div>



            </div>

        </div>
        <div class="col-sm-9">
             <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
    <rsweb:ReportViewer ID="rvMembersByDateRange" runat="server" CssClass="ReportView" ShowPrintButton="true" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="613px">
        <LocalReport ReportPath="Reports\JoinedMembersByDate.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSourceJoinedMembersbyDate" Name="dsJoinedMembersByDate" />
                <rsweb:ReportDataSource DataSourceId="ObjectDataSourceJoinedMembersbyDate" Name="dsJoinedMembersByDate" />
            </DataSources>
        </LocalReport>
        <LocalReport ReportPath="Reports\JoinedMembersByDate.rdlc">
            <DataSources>
                <rsweb:ReportDataSource DataSourceId="ObjectDataSourceJoinedMembersbyDate" Name="dsJoinedMembersByDate" />
            </DataSources>
        </LocalReport>
    </rsweb:ReportViewer>
    <asp:ObjectDataSource ID="ObjectDataSourceJoinedMembersbyDate" runat="server" OldValuesParameterFormatString="original_{0}" SelectMethod="GetData" TypeName="Funeral.Web.Reports.MembersJoinedByDateTableAdapters.allmembersTableAdapter">
        <SelectParameters>
            <asp:Parameter DbType="Guid" Name="parlourid" />
            <asp:Parameter Name="branch" Type="String" />
            <asp:Parameter Name="StartDate" Type="DateTime" />
            <asp:Parameter Name="EndDate" Type="DateTime" />
        </SelectParameters>
    </asp:ObjectDataSource>
        </div>
    </div>



    
   
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
