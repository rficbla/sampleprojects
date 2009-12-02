<%@ Page Language="C#" Inherits="FubuMvcSampleApplication.Views.UserEditView" MasterPageFile="~/Views/Shared/Site.Master"%>
    <asp:Content ID="UserDetails" ContentPlaceHolderID="MainContent" runat="server">    
    <%= this.FormFor((UserController controller) => controller.Display(null))%>
        <div>
            <fieldset>
                <legend>User Details</legend>
                <p>Enter john_doe or user2</p>
                <p>
                    <%= this.TextBoxFor(user => user.UserId).ElementId("UserId").Required().WithLabel("UserId")%>
                    <input type="button" id="DisplayUsingJson" name="DisplayUsingJson" value = "Display Details Using Json" class="button" />                    
                    <%= this.SubmitButton("Display", "Display").Class("button") %>
                </p>                
                <p>
                    <%= this.TextBoxFor(user => user.LastName).ElementId("LastName").ReadOnly().WithLabel("Last Name") %>
                </p>
                <p>    
                    <%= this.TextBoxFor(user => user.FirstName).ElementId("FirstName").ReadOnly().WithLabel("First Name") %>                    
                </p>
                
            </fieldset>
        </div>
    </form>
</asp:Content>
