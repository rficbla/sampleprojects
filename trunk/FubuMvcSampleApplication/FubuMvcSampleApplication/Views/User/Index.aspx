<%@ Page Language="C#" Inherits="FubuMvcSampleApplication.Views.UserIndexView" MasterPageFile="~/Views/Shared/Site.Master" %>

<asp:Content ID="IndexUser" ContentPlaceHolderID="MainContent" runat="server">
    <%= this.FormFor<UserController>(controller => controller.Index(null)) %>
    <div>
        <fieldset>
            <legend>List Users</legend>
            <div id="Navigate"><a href="/user/new">Add New</a></div>
            <div id="Div1"><a href="/user/display">Display User</a></div>
            <table id = "listTable">
                <thead>
                    <tr>
                        <th>UserId</th>
                        <th>LastName</th>
                        <th>FirstName</th>
                        <th></th>
                        <th></th>
                    </tr>
                </thead>
                <tbody>
                <% foreach(var model in Model.Users) {%>
                    <tr>
                        <td><%= model.UserId%></td>
                        <td><%= model.LastName %></td>
                        <td><%= model.FirstName %></td>
                        <td><a href="<%= this.UrlTo().EditUser(model.UserId) %>">Edit</a></td>
                        <td><a href="<%= this.UrlTo().DeleteUser(model.UserId) %>">Delete</a></td>
                    </tr>
                <% } %>
                </tbody>
            </table>
        </fieldset>
    </div>
    </form>
</asp:Content>