<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddEditMenu.aspx.cs" Inherits="Funeral.Web.Tools.AddEditMenu" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-md-12">
            <div class="panel panel-flat">
                <div class="panel-heading">
                    <h5 class="panel-title">Edit/Update Right</h5>
                    <h5 class="panel-title">ADD New Right</h5>
                    <asp:ValidationSummary ID="tab1" runat="server" ValidationGroup="tab1" ForeColor="Red" />
                </div>
                <div class="col-lg-12">
                    <asp:Label ID="lblMessage" runat="server"></asp:Label>
                </div>
                <asp:HiddenField ID="hdfID" runat="server" />
                <div class="panel-body">
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Application </label>
                                <br />
                                <asp:DropDownList ID="ddlapplication" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="UIS" Value="1" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <asp:HiddenField ID="MenuId" runat="server" />
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkInmenu" runat="server" CssClass="styled" />
                                    In Menu
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Menu Name</label><br />
                                <asp:TextBox ID="txtMenuName" runat="server" CssClass="form-control controlwidth" placeholder="Enter Menu Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rqf" runat="server" ControlToValidate="txtMenuName" ValidationGroup="tab1" ErrorMessage="Please Enter Menu Name" Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Menu URL</label><br />
                                <asp:TextBox ID="txtMenuURL" runat="server" CssClass="form-control controlwidth" placeholder="Enter Menu URL"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtMenuURL" ValidationGroup="tab1" ErrorMessage="Please Enter Menu URL" Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Menu Level</label><br />
                                <asp:DropDownList ID="ddlmenulevel" runat="server" CssClass="form-control">
                                    <asp:ListItem Text="Level 0" Value="0" />
                                    <asp:ListItem Text="Level 1" Value="1" />
                                    <asp:ListItem Text="Level 2" Value="2" />
                                </asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Parent Role Form</label><br />
                                <asp:DropDownList ID="ddlParentRole" runat="server" CssClass="form-control"></asp:DropDownList>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsMenu" runat="server" CssClass="styled" />
                                    Is Menu
                                </label>
                            </div>
                        </div>

                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsOthers" runat="server" CssClass="styled" />
                                    Is Other
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Menu Order</label><br />
                                <asp:TextBox ID="txtMenuOrder" runat="server"  FilterType="Numbers" CssClass="form-control controlwidth" placeholder="Enter Menu Order"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="col-md-6">
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsForm" runat="server" CssClass="styled" />
                                    Is Form
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Form Name</label><br />
                                <asp:TextBox ID="txtFormName" runat="server" CssClass="form-control controlwidth" placeholder="Enter Form Name"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="txtFormName" ValidationGroup="tab1" ErrorMessage="Please enter Form Name" Display="None"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Form Key</label><br />
                                <asp:TextBox ID="txtFormkey" runat="server" CssClass="form-control controlwidth" placeholder="Enter Form Key"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Icon ClassName</label><br />
                                <asp:TextBox ID="txtIconClassName" runat="server" CssClass="form-control controlwidth" placeholder="Enter Icon ClassName"></asp:TextBox>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsRead" runat="server" CssClass="styled" />
                                    Is Read
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsCreate" runat="server" CssClass="styled" />
                                    Is Create
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsUpdate" runat="server" CssClass="styled" />
                                    Is Update
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label class="checkbox-inline">
                                    <asp:CheckBox ID="chkIsDelete" runat="server" CssClass="styled" />
                                    Is Delete
                                </label>
                            </div>
                        </div>
                        <div class="col-md-12">
                            <div class="form-group">
                                <label>Description</label><br />
                                <asp:TextBox ID="txtDescription" runat="server" TextMode="multiline" CssClass="form-control" placeholder="Enter Description"></asp:TextBox>
                            </div>
                        </div>
                    </div>
                    <div class="form-group">
                        <div class="col-lg-9">
                            <a href="ManageMenu.aspx" class="btn btn-primary">Cancel</a>
                            <asp:Button ID="btnSaveMenu" runat="server" Text="Save" ValidationGroup="tab1" OnClick="btnSaveMenu_click" class="btn btn-primary" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script>
        function isNumberKey(evt) {
            var val1;
            if (!(evt.keyCode == 46 || (evt.keyCode >= 48 && evt.keyCode <= 57)))
                return false;
            var parts = evt.srcElement.value.split('.');
            if (parts.length > 2)
                return false;
            if (evt.keyCode == 46)
                return (parts.length == 1);
            if (evt.keyCode != 46) {
                var currVal = String.fromCharCode(evt.keyCode);
                val1 = parseFloat(String(parts[0]) + String(currVal));
                if (parts.length == 2)
                    val1 = parseFloat(String(parts[0]) + "." + String(currVal));
            }
            if (val1 > 9999999999.99)
                return false;
            if (parts.length == 2 && parts[1].length >= 2) return false;
            return true;
        }
    </script>
</asp:Content>
