<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="CarTrack.aspx.cs" Inherits="Funeral.Web.Admin.WebForm5" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
    <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Car Tracking</h5>
                    <asp:HiddenField runat="server" ID="hdnId" />
                </div>
                <div class="ibox-content">
                    <div class="row">
                        <div class="panel blank-panel">
                            <div class="panel-heading">
                                <div class="panel-options" id="Tabs">
                                    <ul class="nav nav-tabs" id="myTab">
                                        <li class="active" id="tab1"><a data-toggle="tab" href="#tab-1" aria-expanded="true">Vehicle</a></li>
                                        <li class="" id="tab2"><a data-toggle="tab" href="#tab-2" aria-expanded="false">Vehicle Trip and Mileage</a></li>
                                        <li class="" id="tab3"><a data-toggle="tab" href="#tab-3" aria-expanded="false">Service Details</a></li>
                                        </ul>
                                        </div>
                                </div>
                            <div class="panel-body">
                                <div class="tab-content">
                                    <div id="tab-1" class="tab-pane active">
                                        <div class="row">
                                            <div class="col-lg-12">
                                            </div>
                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label>Make </label>
                                                    <asp:DropDownList runat="server" ID="ddlMake" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Toyota</asp:ListItem>
                                                <asp:ListItem Value="2">Benz</asp:ListItem>
                                                <asp:ListItem Value="3">VW</asp:ListItem>
                                                <asp:ListItem Value="4">Chev</asp:ListItem>
                                               
                                            </asp:DropDownList>
                                             </div>
                                                <div class="form-group">
                                                    <label>Model  </label>
                                                   <asp:DropDownList runat="server" ID="ddlModel" class="form-control m-b">
                                                <asp:ListItem Value="0">Select year</asp:ListItem>
                                                
                                            </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Year  </label>
                                                    <asp:DropDownList runat="server" ID="ddlYear" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                                <asp:ListItem Value="2">Dr</asp:ListItem>
                                                <asp:ListItem Value="3">Miss</asp:ListItem>
                                                <asp:ListItem Value="4">Mrs</asp:ListItem>
                                                <asp:ListItem Value="5">Prof</asp:ListItem>
                                            </asp:DropDownList>

                                                </div>
                                                </div>
                                            <div class="col-lg-6">
                                                <div class="form-group">
                                                    <label>Colour </label>
                                                    <asp:DropDownList runat="server" ID="ddlColour" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                                <asp:ListItem Value="2">Dr</asp:ListItem>
                                                <asp:ListItem Value="3">Miss</asp:ListItem>
                                                <asp:ListItem Value="4">Mrs</asp:ListItem>
                                                <asp:ListItem Value="5">Prof</asp:ListItem>
                                            </asp:DropDownList></div>
                                                <div class="form-group">
                                                    <label>Reg Number</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtReg" name="txtReg" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RegularExpressionValidator Display="None" ID="RegularExpressionValidator3" ValidationGroup="tab1" runat="server" ControlToValidate="txtTelePhone" ErrorMessage="Telephone Number Enter Only Number" ValidationExpression="^[0-9]*$" />--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Vin No.</label>
                                                    <asp:TextBox MaxLength="30" runat="server" ID="txtVin" name="txtVin" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RegularExpressionValidator Display="None" runat="server" ID="revEmailValidation" ValidationGroup="tab1" ControlToValidate="txtEmail" ErrorMessage="Please enter valid email address" ForeColor="Red" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>--%>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtEmail" ID="RequiredFieldValidator8" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                              </div>
                                            <div class="col-lg-12">

                                                <div class ="form-group">
                                                     <asp:GridView ID="GridView1" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" AllowSorting="True" DataSourceID="">
                                                         <PagerStyle CssClass="pagination-ys" />
                                                     </asp:GridView>
                                                         </div>
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary" Visible="true" ID="btnDelete" runat="server" Text="Delete"/>
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnPrint" CssClass="btn btn-sm btn-primary" Text="Print"  />
                                                    </div>
                                                     <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnSave" CssClass="btn btn-sm btn-primary" Text="Save"  />
                                                    </div>
                                                </div>
                                            </div>

                                        </div>
                                    </div>
                                   



                                     <div id="tab-2" class="tab-pane">
                                        <div class="row">
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <%--<div class="input-group">
                                                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" ValidationGroup="tab2" ForeColor="Red" />
                                                    </div>--%>
                                                </div>
                                            </div>
                                            <div class="col-lg-6">

                                                <div class="form-group">
                                                    <label>Departure Date </label>
                                                    <asp:TextBox MaxLength="50" runat="server" ID="txtStreetAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab2" ControlToValidate="txtStreetAddress" ID="RequiredFieldValidator5" ForeColor="red" runat="server" Display="None" ErrorMessage="Please enter street address"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Car</label>
                                                    <asp:DropDownList runat="server" ID="DropDownList1" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                                <asp:ListItem Value="2">Dr</asp:ListItem>
                                                <asp:ListItem Value="3">Miss</asp:ListItem>
                                                <asp:ListItem Value="4">Mrs</asp:ListItem>
                                                <asp:ListItem Value="5">Prof</asp:ListItem>
                                            </asp:DropDownList>
                                                    </div>
                                                <div class="form-group">
                                                    <label>Driver</label>
                                                    <asp:DropDownList runat="server" ID="DropDownList2" class="form-control m-b">
                                                <asp:ListItem Value="0">Select</asp:ListItem>
                                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                                <asp:ListItem Value="2">Dr</asp:ListItem>
                                                <asp:ListItem Value="3">Miss</asp:ListItem>
                                                <asp:ListItem Value="4">Mrs</asp:ListItem>
                                                <asp:ListItem Value="5">Prof</asp:ListItem>
                                            </asp:DropDownList>
                                                    </div>
                                                <div class="form-group">
                                                    <label>Departure Address</label>
                                                    <asp:DropDownList runat="server" ID="DropDownList3" class="form-control m-b">
                                                <asp:ListItem Value="0">Branch</asp:ListItem>
                                                <asp:ListItem Value="1">Mr</asp:ListItem>
                                                <asp:ListItem Value="2">Dr</asp:ListItem>
                                                <asp:ListItem Value="3">Miss</asp:ListItem>
                                                <asp:ListItem Value="4">Mrs</asp:ListItem>
                                                <asp:ListItem Value="5">Prof</asp:ListItem>
                                            </asp:DropDownList>
                                                    </div>
                                                <div class="form-group">
                                                    <label>Departure Mileage</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="txtStreetPostalAddress" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                </div>
                                                <div class="col-lg-6">
                                                    <div class="form-group">
                                                    <label>Arrival Address</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBox1" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Arrival Time</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBox2" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                            <div class="form-group">
                                                    <label>Arrival Mileage</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBox3" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                    <div class="form-group">
                                                    <label>KM</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBox4" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                    <div class="form-group">
                                                    <label>Trip Note</label>
                                                    <asp:TextBox MaxLength="10" runat="server" ID="TextBox5" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtStreetPostalAddress" ID="RequiredFieldValidator15" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                    </div>
                                            <div class="col-lg-12">
                                                     <div class ="form-group">
                                                     <asp:GridView ID="GridView3" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" AllowSorting="True">
                                                         <PagerStyle CssClass="pagination-ys" />
                                                         <Columns>
                                                             <asp:BoundField DataField="ContactFirstName" HeaderText="ContactFirstName" SortExpression="ContactFirstName" />
                                                             <asp:BoundField DataField="ContactLastName" HeaderText="ContactLastName" SortExpression="ContactLastName" />
                                                             <asp:BoundField DataField="CellNumber" HeaderText="CellNumber" SortExpression="CellNumber" />
                                                             <asp:BoundField DataField="DateOfQuotation" HeaderText="DateOfQuotation" SortExpression="DateOfQuotation" />
                                                             <asp:BoundField DataField="LastModified" HeaderText="LastModified" SortExpression="LastModified" />
                                                         </Columns>
                                                     </asp:GridView>
                                                         </div>

                                                <div class="form-group">
                                                   <div class="btn-group">
                                                        <asp:Button CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnDelte0" runat="server" Text="Delete"  />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnPrintTab2" runat="server" Text="Print"  />
                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button runat="server" ID="btnTab2"  CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Save" />
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
                                                    <label>Vehicle Details </label>
                                                    <asp:DropDownList ID="ddlDetails" class="form-control" runat="server">
                                                        <asp:ListItem Value="0">Select Policy</asp:ListItem>
                                                    </asp:DropDownList>
                                                </div>
                                                <div class="form-group">
                                                    <label>Date of Service </label>
                                                    <asp:TextBox MaxLength="10" CssClass="form-control" runat="server" ID="txtDOS" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtPolicyPremium" ID="RequiredFieldValidator17" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Mileage </label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="20" runat="server" ID="txtMileage"  type="text" class="form-control"></asp:TextBox>
                                                </div>
                                               
                                            </div>
                                            <div class="col-lg-6">
                                               <div class="form-group">
                                                    <label>Next Service Mileage </label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="20" runat="server" ID="txtNSM"  type="text" class="form-control"></asp:TextBox>
                                                </div>
                                                <div class="form-group">
                                                    <label>Dealership</label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtDealer" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtInception" ID="RequiredFieldValidator22" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class="form-group">
                                                    <label>Service Cost</label>
                                                    <asp:TextBox CssClass="form-control" MaxLength="30" runat="server" ID="txtServiceCost" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab4" ControlToValidate="txtInception" ID="RequiredFieldValidator22" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                
                                            </div>
                                            <div class="col-lg-12">
                                                <div class="form-group">
                                                    <label>Enter note description</label>
                                                    <asp:TextBox TextMode="MultiLine" Rows="5" ValidationGroup="tab7" MaxLength="75" runat="server" ID="txtArea" placeholder="Enter Note" name="name" type="text" class="form-control"></asp:TextBox>
                                                    <%--<asp:RequiredFieldValidator ValidationGroup="tab7" ControlToValidate="txtArea" ID="RequiredFieldValidator31" ForeColor="red" runat="server" ErrorMessage="*"></asp:RequiredFieldValidator>--%>
                                                </div>
                                                <div class ="form-group">
                                                     <asp:GridView ID="GridView2" runat="server" AutoGenerateColumns="False" CssClass="table table-striped table-bordered table-hover" AllowPaging="True" AllowSorting="True">
                                                         <PagerStyle CssClass="pagination-ys" />
                                                         <Columns>
                                                             <asp:BoundField DataField="ContactFirstName" HeaderText="ContactFirstName" SortExpression="ContactFirstName" />
                                                             <asp:BoundField DataField="ContactLastName" HeaderText="ContactLastName" SortExpression="ContactLastName" />
                                                             <asp:BoundField DataField="CellNumber" HeaderText="CellNumber" SortExpression="CellNumber" />
                                                             <asp:BoundField DataField="DateOfQuotation" HeaderText="DateOfQuotation" SortExpression="DateOfQuotation" />
                                                             <asp:BoundField DataField="LastModified" HeaderText="LastModified" SortExpression="LastModified" />
                                                         </Columns>
                                                     </asp:GridView>
                                                         </div>
                                                <div class="form-group">
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btnDelete1" runat="server" Text="Delete"  />

                                                    </div>
                                                    <div class="btn-group">
                                                        <asp:Button class="btn btn-sm btn-primary pull-right m-t-n-xs" Visible="true" ID="btPrintTab3" runat="server" Text="Print"  />

                                                    </div>
                                                    <div class="btn-group">
                                                        <%--<button class="btn btn-sm btn-primary pull-right m-t-n-xs" onclick="return goToTab(4);">Next</button>--%>
                                                        <asp:Button runat="server" ID="btnTab3" ValidationGroup="tab3" CssClass="btn btn-sm btn-primary pull-right m-t-n-xs" Text="Save"  />
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
        </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
