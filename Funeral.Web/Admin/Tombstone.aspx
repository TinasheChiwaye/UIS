<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="Tombstone.aspx.cs" Inherits="Funeral.Web.Admin.TombStone" %>

<%@ Register Src="~/UserControl/ctrlTombstonePaymentHistory.ascx" TagName="ucTombstonePaymentHistory" TagPrefix="uc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <div class="col-lg-12">
        <asp:Label ID="lblMessage" runat="server"></asp:Label>
        <div class="ibox ">
            <div class="ibox float-e-margins">
                <div class="ibox-title">
                    <h5>TombStone</h5>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <div class="col-lg-12">
            <div class="form-group">
                <div class="input-group">
                    <asp:ValidationSummary runat="server" ID="vSummaryQuotation" ValidationGroup="tomb" ForeColor="Red" />
                </div>
            </div>
        </div>

        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox float-e-margins">
                    <div class="ibox-title">
                        <h5>Member</h5>
                    </div>
                    <div class="ibox-content">

                        <div class="col-lg-6">
                            <div class="form-group">


                                <label>Last Name <em>*</em> </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtLastName" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtLastName" ID="RequiredFieldValidator4" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator2" ValidationGroup="tab1" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtLastName" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>First Name <em>*</em>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtFirstName" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtFirstName" ID="RequiredFieldValidator6" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab1" runat="server" ControlToValidate="txtFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                            </div>
                            <div class="form-group">
                                <label>ID Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtIdNumber1" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFun();"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtIdNumber1" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="Please enter Id number"></asp:RequiredFieldValidator>
                                <asp:CustomValidator ErrorMessage="Invalid Id Number" ID="cvIdvalidation" OnServerValidate="cvIdvalidation_ServerValidate" ControlToValidate="txtIdNumber1" ValidationGroup="tomb" runat="server" />
                                <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="tab1" ControlToValidate="txtIdNumber1" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter id number"></asp:RequiredFieldValidator>--%>
                            </div>
             
                            <div class="form-group">
                                <label>Policy Number <em>*</em>  </label>
                                <asp:TextBox MaxLength="25" runat="server" ID="txtPolicyNumber" name="PolicyNumber" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtPolicyNumber" ID="RequiredFieldValidator11" ForeColor="red" runat="server" ErrorMessage="Please enter Policy Number"></asp:RequiredFieldValidator>
                                <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator4" ValidationGroup="tomb" runat="server" ControlToValidate="txtPolicyNumber" ErrorMessage="Enter Valid Policy Number" ValidationExpression="[a-zA-Z ]*$" />
                            </div>

                            <div class="form-group">
                                <label>Date of Application </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtAppDate" name="name" type="text" class="form-control" CssClass="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                            </div>
                            <div class="form-group">
                                <label>Tell Number <em>*</em> </label>
                                <asp:TextBox MaxLength="30" runat="server" ID="txtTel" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtTel" ID="RequiredFieldValidator5" ForeColor="red" runat="server" ErrorMessage="Please enter Tell number"></asp:RequiredFieldValidator>
                            </div>
                        </div>
                        <div class="col-lg-6">
                            <div class="form-group">
                                <label>Street Address <em>*</em> </label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtAdd1" name="name" type="text" class="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator ValidationGroup="tomb" ControlToValidate="txtAdd1" ID="RequiredFieldValidator8" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>
                            </div>
                            <div class="form-group">
                                <label>Town or City</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtadd2" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Province</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtadd3" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                            </div>
                            <div class="form-group">
                                <label>Code</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtCode" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            </div>
                            <div class="form-group">
                                <label>Cell Number</label>
                                <asp:TextBox MaxLength="50" runat="server" ID="txtCell" name="name" type="text" class="form-control"></asp:TextBox>
                                <%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                            </div>
                        </div>



                        <div class="ibox float-e-margins">
                            <div class="ibox-title">
                                <h5>Deceased Info</h5>
                            </div>
                            <div class="ibox-content">
                                <div class="row">
                                    <%-- <div class="col-lg-12">
                                                
                                            </div>--%>
                                    <div class="col-lg-6">
                                        <div class="form-group">


                                            <label>Last Name <em>*</em> </label>
                                            <asp:TextBox MaxLength="25" runat="server" ID="txtDLastName" name="name" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtDLastName" ID="RequiredFieldValidator1" ForeColor="red" runat="server" ErrorMessage="Please enter Surname"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="None" ID="RegExp1" ValidationGroup="tomb" runat="server" ErrorMessage="Surname Enter Only characters" ControlToValidate="txtDLastName" ValidationExpression="[a-zA-Z ]*$" />
                                        </div>
                                        <div class="form-group">
                                            <label>First Name <em>*</em>  </label>
                                            <asp:TextBox MaxLength="25" runat="server" ID="txtDFirstname" name="name" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtDFirstName" ID="RequiredFieldValidator2" ForeColor="red" runat="server" ErrorMessage="Please enter full name"></asp:RequiredFieldValidator>
                                            <asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator1" ValidationGroup="tomb" runat="server" ControlToValidate="txtDFirstname" ErrorMessage="full name Enter Only characters" ValidationExpression="[a-zA-Z ]*$" />
                                        </div>
                                        <div class="form-group">
                                            <label>ID Number <em>*</em>  </label>
                                            <asp:TextBox MaxLength="50" runat="server" ID="txtDIdNumber" name="name" type="text" class="form-control" onkeyup="DateComparisionJavascriptFun();"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="None" ValidationGroup="tomb" ControlToValidate="txtDIdNumber" ID="RequiredFieldValidator3" ForeColor="red" runat="server" ErrorMessage="Please enter id number of Deceased ..."></asp:RequiredFieldValidator>
                                            <asp:CustomValidator ErrorMessage="Invalid Id Number of Deceased" ID="CustomValidator1" OnServerValidate="cvIdvalidation_ServerValidate2" ControlToValidate="txtDIdNumber" ValidationGroup="tomb" runat="server" />
                                        </div>

                                        <%--<asp:RequiredFieldValidator ValidationGroup="tab1" ControlToValidate="txtPassport" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%><%--<asp:DropDownList ID="ddlCemetery" runat="server" DataTextField="Name" DataValueField="CountryCode" CssClass="form-control m-b"></asp:DropDownList>--%>
                                        <div class="form-group">
                                            <label>Deceased</label><br />
                                            &nbsp &nbsp 
                                            <asp:RadioButton ID="rdbBuried" GroupName="Deceased" runat="server" Text="Buried" Checked="true" />&nbsp &nbsp  &nbsp &nbsp
                                    <asp:RadioButton ID="rdbCremated" GroupName="Deceased" runat="server" Text="Cremated" />

                                        </div>
                                        <div class="form-group">
                                            <label>Contact Person</label>
                                            <asp:TextBox MaxLength="50" runat="server" ID="txtContactPerson" name="txtContactPerson" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="tomb" Display="None" ControlToValidate="txtContactPerson" ID="RequiredFieldValidator7" ForeColor="red" runat="server" ErrorMessage="Please enter contact person."></asp:RequiredFieldValidator>
                                        </div>
                                        <div class="form-group">
                                            <label>Contact Number</label>
                                            <asp:TextBox MaxLength="10" runat="server" ID="txtContactNumber" name="txtContactNumber" type="text" class="form-control"></asp:TextBox>
                                            <asp:RequiredFieldValidator ValidationGroup="tomb" Display="None" ControlToValidate="txtContactNumber" ID="RequiredFieldValidator10" ForeColor="red" runat="server" ErrorMessage="Please enter contact number"></asp:RequiredFieldValidator>
                                        </div>
                                    </div>
                                    <div class="col-lg-6">
                                        <div class="form-group">
                                            <div class="form-group">
                                                <label>Date of Memorial</label>
                                                <asp:TextBox MaxLength="30" runat="server" ID="txtDateOfMemorial" name="name" type="text" class="form-control" CssClass="form-control datepicker" placeholder="DD/MM/YYYY"></asp:TextBox>
                                            </div>
                                            <label>Cemetery / Crematorium</label>
                                            <asp:TextBox MaxLength="50" runat="server" ID="txtCemetery" name="name" type="text" class="form-control"></asp:TextBox>
                                        </div>
                                        <div class="form-group">
                                            <label>Grave No</label>
                                            <asp:TextBox MaxLength="50" runat="server" ID="txtGraveNo" name="name" type="text" class="form-control"></asp:TextBox>
                                            <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtCitizenship" ID="RequiredFieldValidator9" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                        </div>
                                        <div class="form-group">
                                            <label>Erect</label><br />
                                            &nbsp &nbsp 
                                            <asp:RadioButton ID="rdbYes" GroupName="Erect" runat="server" Text="Yes" Checked="true" />&nbsp &nbsp  &nbsp &nbsp
                                    <asp:RadioButton ID="rdbNo" GroupName="Erect" runat="server" Text="No" />
                                        </div>

                                    </div>
                                    <div class="ibox float-e-margins">
                                        <div class="ibox-title">
                                            <h5>Tombstone</h5>
                                        </div>
                                        <div class="ibox-content">
                                            <div class="row">
                                                <div class="col-lg-12">

                                                    <div class="col-lg-6">


                                                        <div class="form-group">
                                                            <label>Select Tombstone</label>
                                                            <asp:FileUpload ID="flTombStone" runat="server" class="form-control" />
                                                            <label>Only image formats (jpg, png, gif) are accepted</label>
                                                            <asp:RequiredFieldValidator ID="lblExtension1" ControlToValidate="flTombStone" ValidationGroup="btnup" ForeColor="red" runat="server" ErrorMessage="Please Select Image"></asp:RequiredFieldValidator>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Button runat="server" ID="btnUpload" ValidationGroup="btnup" CssClass="btn btn-sm btn-primary pull-right" Text="Upload" Visible="false" OnClick="btnUpload_Click" />
                                                        </div>
                                                        <div class="form-group">
                                                            <label for="textarea">Tombstone</label>
                                                        </div>
                                                        <div class="form-group">
                                                            <asp:Image runat="server" ID="ImagePreview" Height="164px" Width="375px" />
                                                        </div>
                                                    </div>




                                                    <div class="col-lg-6">
                                                        <div class="form-group">
                                                            <label>Notes </label>
                                                            <asp:TextBox runat="server" ID="txtNotes" name="txtNotes" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>
                                                            <%--<asp:RequiredFieldValidator Display="None" ValidationGroup="CompanySetup" ControlToValidate="txtcompanyRules" ID="RequiredFieldValidator20" ForeColor="red" runat="server" ErrorMessage="Please enter Company Rules"></asp:RequiredFieldValidator>--%>
                                                        </div>
                                                        <div class="form-group">
                                                            <label>Message </label>
                                                            <asp:TextBox runat="server" ID="txtMessage" name="CompanyRules" type="text" class="form-control" TextMode="MultiLine" Rows="4"></asp:TextBox>

                                                        </div>

                                                    </div>

                                                </div>
                                            </div>
                                        </div>

                                        <div class="col-lg-12">
                                            <div class="form-group">
                                                <div class="btn-group">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnCreateInvoice" CssClass="btn btn-sm btn-primary" runat="server" Text="Process Payment" Enabled="false" OnClick="btnCreateInvoice_Click" />
                                                    </div>
                                                </div>
                                                <div class="btn-group">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnViewPayments" CssClass="btn btn-sm btn-primary" runat="server" Text="View Payments" Enabled="false" OnClick="btnViewPayments_Click" />
                                                    </div>
                                                </div>

                                                <div class="btn-group">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnSave" ValidationGroup="tomb" CssClass="btn btn-sm btn-primary" runat="server" Text="Save" OnClick="btnSubmitClick" />
                                                    </div>
                                                </div>
                                                <div class="btn-group">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnCancel" CssClass="btn btn-sm btn-primary" runat="server" Text="Clear" OnClick="btnClear_Click" />
                                                    </div>
                                                </div>
                                                <div class="btn-group">
                                                    <div class="form-group">
                                                        <asp:Button ID="btnService" CssClass="btn btn-sm btn-primary" runat="server" Text="Services" OnClick="btnService_Click" />
                                                    </div>
                                                </div>
                                            </div>
                                        </div>


                                    </div>

                                </div>
                            </div>
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
                                                <br />
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
                                        <asp:GridView ID="gvTombStonelList" OnRowDataBound="gvTombStoneList_RowDataBound" OnPageIndexChanging="gvTombStone_PageIndexChanging" OnRowCommand="gvTombStone_RowCommand" OnSorting="gvTombStone_Sorting" AllowSorting="true" runat="server" Width="100%" CssClass="table table-striped table-bordered table-hover" AutoGenerateColumns="False" AllowPaging="True" PageSize="25" EmptyDataText="There are no TombStone  added.">

                                            <PagerStyle CssClass="pagination-ys" />
                                            <Columns>

                                                <asp:BoundField DataField="LastName" HeaderText="Surname" SortExpression="LastName" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="FirstName" HeaderText="full name" SortExpression="FirstName" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="PolicyNumber" HeaderText="Policy Number" SortExpression="PolicyNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="TelNumber" HeaderText="Tel Number" SortExpression="TelNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="DeceasedIDNumber" HeaderText="Deceased ID Number" SortExpression="DeceasedIDNumber" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" />
                                                <asp:BoundField DataField="DateOfApplication" HeaderText="Date Of Application" SortExpression="DateOfApplication" ReadOnly="True" ItemStyle-CssClass="visible-lg" HeaderStyle-CssClass="visible-lg" DataFormatString="{0:dd/M/yyy}" />


                                                <asp:TemplateField HeaderText="Actions">
                                                    <ItemTemplate>
                                                        <% if (this.HasEditRight)
                                                           {%>
                                                        <asp:LinkButton runat="server" ID="lbtnEditDependant" ToolTip="Click here To Edite Funeral" CommandArgument='<%#Eval("pkiTombstoneID") %>' CommandName="EditTombStone"><i class="fa fa-edit"></i></asp:LinkButton>
                                                        &nbsp;
                                                         <%} if (this.HasDeleteRight)
                                                           { %>
                                                        <asp:LinkButton runat="server" ID="lbtnDeleteDependatn" ToolTip="Click here To Delete TombStone" OnClientClick="return confirm('Are you sure you want to delete it?')" CommandArgument='<%#Eval("pkiTombstoneID") %>' CommandName="DeleteTombStone"><i class="fa fa-trash"></i></asp:LinkButton>
                                                        &nbsp;
                                                <%} if (this.HasReadRight)
                                                           { %>
                                                        <asp:LinkButton runat="server" ID="LinkButton1" ToolTip="Click here Print " CommandArgument='<%#Eval("pkiTombstoneID") %>' CommandName="PrintTombstone"><i class="fa fa-search"></i></asp:LinkButton>
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
        </div>
    </div>

</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
    <script type="text/javascript">
        function pageLoad(sender, args) {
            $(".datepicker").datepicker({ format: 'dd MM yyyy' });
            $(".datepicker").on('changeDate',
                function(ev) {
                    $(this).datepicker('hide');
                });
        }
        function openModal() {
            $('#ucPaymentHistory').modal('show');
        }
    </script>
    <div class="modal fade" id="ucPaymentHistory" tabindex="-1" role="dialog" aria-hidden="true">
        <div class="modal-dialog">
            <div class="modal-content">
                <div class="modal-header">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">&times;  </button>
                    <h4 class="modal-title">Payment history</h4>
                </div>
                <div class="modal-body">
                    <div class="row">
                        <uc:ucTombstonePaymentHistory runat="server" ID="ucPaymentHistory1" />
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-default" data-dismiss="modal">Close</button>

                </div>
            </div>
        </div>
        <!-- /.modal-dialog -->
    </div>
    <!-- /.modal -->
</asp:Content>
