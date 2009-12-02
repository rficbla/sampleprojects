<%@ Page Title="" Language="C#" MasterPageFile="~/Views/Shared/Site.Master" Inherits="System.Web.Mvc.ViewPage<SampleMvc.Models.UserModel>" %>

<asp:Content ID="Head" ContentPlaceHolderID="head" runat="server">
	<title>User</title>
</asp:Content>

<asp:Content ID="Content" ContentPlaceHolderID="MainContent" runat="server">
    <h2>User Details</h2>        
    <% using(Html.BeginForm()) { %>        
        <div>
            <fieldset>
                <legend>User Details</legend>
                <p>
                    <label for="UserId">UserId:</label>
                    <%=Html.TextBox("UserId")%>
					<input type="button" id="GetUser" value="Populate User Details" onclick='populateUserDetails()'/>
                </p>
                <p>
                    <label for="LastName">LastName:</label>
                    <%=Html.TextBox("LastName")%>
                </p>                    
                <p>
                    <label for="FirstName">FirstName:</label>
                    <%=Html.TextBox("FirstName")%>
               </p>               
            </fieldset>
        </div>
    <% } %>
</asp:Content>
