<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="403Error.aspx.cs" Inherits="Funeral.Web.Admin._403Error" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeaderContent" runat="server">
    <style>
        @media (min-width: 979px) {
    body {
		/*padding-top: 60px;*/ /* 60px to make the container go all the way to the bottom of the topbar */
	}
	.container {
  		width: 540px;
  	}
}

.vtexIdUI-page {
	box-shadow: 0 0 3px #ccc;
}

/* 
já está no main.less 
#############################
*/
.vtexIdUI-email-field input {
    font-size: 18px;
    height: 40px;
    line-height: 120%;
}

.info-code {margin-bottom: 4px;}

.vtexIdUI-auth-code input {
    font-size: 40px;
    height: 54px;
    padding-top: 3px;
    line-height: 120%;
    text-align: center;
	margin-bottom: 2px;
}

.vtexIdUI-code-field {
	margin: 0 auto;
	width: 30%; /* VALOR ALTERADO */
	margin-bottom: 10px;
}


/* 
estilo novo 
#############################
*/
.alert-general {
    border-radius: 0;
    border-width: 1px 0 1px 0;
    margin-bottom: 0;
}
.alert-modal-body {padding-top: 15px;}
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    <div class="container">
  
    <div class="vtexIdUI-403 vtexIdUI-page" id="vtexIdUI-no-permission">
      <div class="modal-header">
        <h4><span class="vtexIdUI-heading">403 - ACCESS DENIED</span> <small class="pull-right">Error 403</small></h4>
      </div>
      
      <div class="alert alert-info alert-general alert-modal-body clearfix">
        <i class="icon-frown icon-4x pull-left"></i>
        <p><strong>access denied, You are not authorised to use this.</strong><br>
          <small></small>
        </p>
      </div>
      
        <div class="modal-footer">
          <a class="vtexIdUI-back-link pull-left dead-link" href="/Admin/Dashboard.aspx" data-i18n="vtexid.backLink" "><i class="icon-arrow-left">Go back</i></a>
          <%--<button  class="btn pull-right btn-large" type="submit" data-i18n="vtexid.send">Sair</button>--%>
        </div>

    </div>
  
</div>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="ScriptControl" runat="server">
</asp:Content>
