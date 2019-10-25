<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ManageMenu.aspx.cs" Inherits="Funeral.Web.Tools.ManageMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <%--<link href="https://maxcdn.bootstrapcdn.com/bootstrap/3.3.7/css/bootstrap.min.css" rel="stylesheet" type="text/css" />--%>
    <link href="https://cdn.datatables.net/1.10.15/css/dataTables.bootstrap.min.css" rel="stylesheet" type="text/css" />
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">

    <div class="col-lg-12">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="ibox">
            
                <div class="ibox-title">
                    <h5>Manage Menu</h5>
                    <a href="/Tools/AddEditMenu.aspx" class="btn btn-primary pull-right">Add Menu</a>
                </div>
           
        </div>
    </div>
    <div class="row">
        <div class="col-lg-12 ">
            <div class="ibox-content">
                <div class="table-responsive">
                    <table class="table table-striped table-bordered table-hover"  id="example">
                        <thead>
                            <tr class="bg-teal" role="row">
                                <th>#</th>
                                <th>Form Name</th>
                                <th>Form Key</th>
                                <th>Menu Name</th>
                                <th>Menu Url</th>
                                <th>Description</th>
                                <th>Active</th>
                                <th class="sorting_disabled" width="8%" data-orderable="false">Edit Right</th>
                            </tr>
                        </thead>
                         <tbody id="ibody" runat="server"></tbody>
                    </table>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script>
        $(document).ready(function () {
            var total = $('#example tr').length;
            // alert(total);

            var oTable = $('#example').dataTable({
                aaSorting: [[0, 'desc']],
                paging: true,
                "pageLength": 10,
                "fnRowCallback": function (nRow, aData, iDisplayIndex) {
                }
            });
        });

    </script>
</asp:Content>
