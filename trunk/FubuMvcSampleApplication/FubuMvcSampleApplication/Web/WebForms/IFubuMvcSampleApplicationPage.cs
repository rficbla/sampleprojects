using FubuMVC.Core.View;

namespace FubuMvcSampleApplication.Web.WebForms
{
    public interface IFubuMvcSampleApplicationPage : IFubuViewWithModel
    {
        object Model { get; }
    }
}