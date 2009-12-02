using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Web.WebForms;

namespace FubuMvcSampleApplication.Views
{
    //Master Page
    public class MasterPageView : FubuMvcSampleApplicationMasterPage
    {
    }

    //Views
    public class UserIndexView : FubuMvcSampleApplicationPage<UserListViewModel>
    {
    }

    public class UserEditView : FubuMvcSampleApplicationPage<UserEditViewModel>
    {
    }

    public class UserGreetingView : FubuMvcSampleApplicationPage<UserEditViewModel>
    {
    }

    public class PageNotFoundView : FubuMvcSampleApplicationPage<PageNotFoundViewModel>
    {
    }
}