<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="ImportExcelBackup.aspx.cs" Inherits="Funeral.Web.Admin.ImportExcel" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <asp:Label ID="lblMessage" runat="server"></asp:Label>
        </div>
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryQuotation" ValidationGroup="Qoutation" ForeColor="Red" />
                </div>
            </div>
        </div>
        <div class="col-lg-12">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>Import Excel File</h5>
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
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>FileName</label>
                                <asp:FileUpload ID="FileUpload1" runat="server" />

                            </div>

                            <div class="col-lg-6">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="Submit" Width="95%" ValidationGroup="ImportDetail" CssClass="btn btn-md btn-primary" OnClick="Submit_Click" Text="Upload File" />
                                </div>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Main Member Name</label>
                                <asp:TextBox runat="server" ID="txtMainMemberName" name="txtEmail" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="ImportDetail" ControlToValidate="txtMainMemberName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please Enter Name"></asp:RequiredFieldValidator>
                            </div>


                        </div>
                    </div>
                    <div class="row">
                        <div class="col-sm-12">
                            <asp:HiddenField ID="hdnnewImportedId" runat="server" />
                            <asp:Button runat="server" ID="btnConfirmAndSubmit" Width="95%" CssClass="btn btn-md btn-primary" OnClick="btnConfirmAndSubmit_Click" Visible="false" Text="Confirm and Save" />
                        </div>
                    </div>
                    <div runat="server" class="row" id="GridviewData">
                        <div class="col-lg-12">
                            <div class="table-responsive">
                                <asp:GridView ID="gvMapping" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover"
                                    AutoGenerateColumns="True" EmptyDataText="There are no member to display." OnRowDataBound="gvMapping_RowDataBound">
                                    <PagerStyle CssClass="pagination-ys" />
                                    <Columns>

                                        <asp:TemplateField HeaderText="Member Column Name">
                                            <ItemTemplate>
                                                <asp:DropDownList ID="ddMemberColumn" Style="width: 300px;" runat="server"></asp:DropDownList>
                                            </ItemTemplate>
                                        </asp:TemplateField>

                                    </Columns>
                                </asp:GridView>
                            </div>
                            <div class="col-lg-2">
                                <div class="form-group">
                                    <asp:Button runat="server" ID="btnSubmit" Width="95%" CssClass="btn btn-md btn-primary" OnClick="btnSubmit_Click" Visible="false" Text="Submit" />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </div>
        </div>

    </div>
    <asp:HiddenField runat="server" ID="hdnColumCount" />
    <asp:HiddenField runat="server" ID="hdnMemberName" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
