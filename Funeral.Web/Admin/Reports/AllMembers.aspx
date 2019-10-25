<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AllMembers.aspx.cs" Inherits="Funeral.Web.Admin.Reports.AllMembers" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=11.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
     <asp:Label ID="lblMessage" runat="server" Text=""></asp:Label>
        <rsweb:ReportViewer ID="rvMembersByDateRange" runat="server" ShowPrintButton="true" CssClass="ReportView" Font-Names="Verdana" Font-Size="8pt" WaitMessageFont-Names="Verdana" WaitMessageFont-Size="14pt" Width="100%" Height="613px" >
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
