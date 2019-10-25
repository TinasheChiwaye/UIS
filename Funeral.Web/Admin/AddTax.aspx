<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="AddTax.aspx.cs" Inherits="Funeral.Web.Admin.AddTax" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="col-lg-12">
        <div class="ibox ">
            <div class="ibox-title">
                <h5>Add Tax</h5>
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
                    <div class="col-lg-12" style="text-align: center">
                        <asp:Label ID="lblMessage" ForeColor="Red" Text="" runat="server"></asp:Label>
                    </div>
                    <div class="panel-body">
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Tax Name<em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtTax" name="" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtTax" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Tax name"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Tax Value<em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtTaxValue" name="" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtTaxValue" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter Tax Value"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-12">
                            <div class="form-group">
                                <div class="btn-group">
                                    <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" ID="btnSave" ValidationGroup="tab4" runat="server" Text="Save" OnClick="btnSave_Click" />
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
                    <h5>Tax List</h5>
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
                         
                            <div class="table-responsive">
                                <asp:GridView ID="gvTaxSetting" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="False" EmptyDataText="There are no member to display."
                                    OnRowCommand="gvTaxSetting_RowCommand">
                                    <Columns>
                                        <asp:BoundField DataField="TaxText" HeaderText="Tax Text" ReadOnly="True" />
                                        <asp:BoundField DataField="TaxValue" HeaderText="Tax Value" ReadOnly="True" />
                                        <asp:TemplateField HeaderText="Edit">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnEdit" runat="server" CommandName="updateTxt" CommandArgument='<%#Bind("Id")%>'>Edit</asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Delete">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="lnkbtnDelete" runat="server" CommandName="deleteTxt" CommandArgument='<%#Bind("Id")%>'>Delete</asp:LinkButton>
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
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
