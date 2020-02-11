<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="UserRight.aspx.cs" Inherits="Funeral.Web.Admin.UserRight" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="row">
        <div class="col-lg-12">
            <div class="ibox ">
                <div class="ibox-title">
                    <h5>Setup Details</h5>
                </div>
                 <div class="ibox-content">
                    
                    <br />
                    <asp:Label  ID="lblRegistration" runat="server" class="col-lg-2">Registration Done</asp:Label>
                    <asp:Image ID="imgRegistration1" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="true" />
                    
                    <br />
                    <br />
                    <asp:Label ID="lblCompany" runat="server" class="col-lg-2">Scheme Setup</asp:Label>
                    <asp:Image ID="imgCompany1" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgCompany2" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br />
                    <br />
                    <asp:Label ID="lblBranch" runat="server" class="col-lg-2">Branch Setup</asp:Label>
                    <asp:Image ID="imgBranch1" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgBranch2" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br />
                    <br />
                    <asp:Label ID="lblUsers" runat="server" class="col-lg-2">Users Setup</asp:Label>
                    <asp:Image ID="imgUsers1" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgUsers2" runat="server" Width="4%" class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblPlans" runat="server"  class="col-lg-2">Plans Setup</asp:Label>
                    <asp:Image ID="imgPlans1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgPlans2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblUnderwriterPlanSetup" runat="server"  class="col-lg-2">Underwriter Plan Setup</asp:Label>
                    <asp:Image ID="imgUnderwriterPlanSetup1" runat="server"  class="col-lg-2" Width="4%" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgUnderwriterPlanSetup2" runat="server"  class="col-lg-2" Width="4%" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblSocieties" runat="server"  class="col-lg-2">Societies Setup</asp:Label>
                    <asp:Image ID="imgSocieties1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgSocieties2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblSMS" runat="server"  class="col-lg-2">SMS Messages</asp:Label>
                    <asp:Image ID="imgSMS1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgSMS2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="true" />
                    <br /><br />
                    <asp:Label ID="lblServices" runat="server"  class="col-lg-2">Funeral Services Pricing Setup</asp:Label>
                    <asp:Image ID="imgServices1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgServices2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblProduct" runat="server"  class="col-lg-2">Addon Product Setup</asp:Label>
                    <asp:Image ID="imgProduct1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgProduct2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblLifes" runat="server"  class="col-lg-2">main Lifes Imported</asp:Label>
                    <asp:Image ID="imgLifes1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgLifes2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                    <asp:Label ID="lblDependents" runat="server"  class="col-lg-2">Dependents Imported</asp:Label>
                    <asp:Image ID="imgDependents1" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/True.png" Visible="false" />
                    <asp:Image ID="imgDependents2" runat="server" Width="4%"  class="col-lg-2" ImageUrl="~/Admin/Images/False.png" Visible="false" />
                    <br /><br />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
