<%@ Page Language="C#" Inherits="FubuMvcSampleApplication.Views.UserEditView" MasterPageFile="~/Views/Shared/Site.Master"%>
    <asp:Content ContentPlaceHolderID="MainContent" runat="server">    
    <%= this.FormFor((UserController controller) => controller.Cancel(null))%>
        <div>            
            <fieldset>
                <legend>Add/Edit User</legend>
                <p>
                    <%= this.TextBoxFor(user => user.UserId).ElementId("UserId").Required().WithLabel("UserId")%>
                </p>
                <p>
                    <%= this.TextBoxFor(user => user.LastName).ElementId("LastName").WithLabel("Last Name") %>
                </p>
                <p>
                    <%= this.TextBoxFor(user => user.FirstName).ElementId("FirstName").WithLabel("First Name")%>
                </p>
                <p>
                    <%= this.TextBoxFor(user => user.DateOfBirth).ElementId("DateOfBirth").WithLabel("Date of Birth") %>
                </p>
                <p>
                    <input type="button" id="Save" name="Save" value = "Save" class="button" />
                    <%= this.SubmitButton("Cancel", "Cancel").Class("button") %>
                </p>
            </fieldset>
        </div>
    </form>
</asp:Content>
