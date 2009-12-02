using System.Web.UI;

using FubuMVC.Core.View;

namespace FubuMvcSampleApplication.Web.WebForms
{
    public class FubuMvcSampleApplicationPage<MODEL> : Page, IFubuMvcSampleApplicationPage, IFubuView<MODEL>
        where MODEL : ViewModel
    {
        public void SetModel(object model)
        {
            Model = (MODEL)model;
        }

        object IFubuMvcSampleApplicationPage.Model
        {
            get { return Model; }
        }

        public MODEL Model { get; set; }

        public ViewModel GetModel()
        {
            return Model;
        }
    }
}