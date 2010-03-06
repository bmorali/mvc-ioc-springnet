<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace=" MVCAuthenticationSample.Utils" %>

<asp:Content ID="Content1" ContentPlaceHolderID="TitleContent" runat="server">
	Administration module
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="MainContent" runat="server">
    
    <h2>Administration module</h2>
    <table id="userslist" cellpadding="0" cellspacing="0">
    </table>
    <div id="pager" style="text-align: center;">
    </div>
   <button id="editUser">Edit</button>&nbsp;<button id="deleteUser">Delete</button>&nbsp;<button id="addUser">Add</button>
    <%= Html.ActionLink("Modify users", "ModifyUsers") %>
    <div id="dialogAmendUsers" style="display: none;">
        <% Html.RenderPartial("AmendUser"); %>
    </div>
</asp:Content>

<asp:Content ID="bottom" ContentPlaceHolderID="BottomScripts" runat="server">
    <%= "<script type=\"text/javascript\" src=\"" + UrlResorseHelper.GetApplicationPath("/Scripts/ViewsScripts/Admin.js") + "\"></script>"%>
</asp:Content>
