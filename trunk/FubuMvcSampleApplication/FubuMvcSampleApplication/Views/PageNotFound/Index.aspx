<%@ Page Language="C#" Inherits="FubuMvcSampleApplication.Views.PageNotFoundView" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="IndexUser" ContentPlaceHolderID="MainContent" runat="server">
    <%= Model.Description %>. Please check your url.
</asp:Content>