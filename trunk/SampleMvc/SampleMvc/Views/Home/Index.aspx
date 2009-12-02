<%@ Page Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage" %>
<%@ Import Namespace="SampleMvc.Controllers"%>

<asp:Content ID="indexHead" ContentPlaceHolderID="head" runat="server">
    <title>Home Page</title>
</asp:Content>

<asp:Content ID="indexContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2>Home</h2>
    <p>
        <%=Html.ActionLink("Click here to go to User Details", "UserDetails", "User")%>
    </p>
</asp:Content>
