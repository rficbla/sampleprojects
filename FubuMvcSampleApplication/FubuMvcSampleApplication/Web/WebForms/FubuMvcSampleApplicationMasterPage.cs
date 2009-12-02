using System;
using System.Web.UI;

namespace FubuMvcSampleApplication.Web.WebForms
{
    public class FubuMvcSampleApplicationMasterPage : MasterPage, IFubuMvcSampleApplicationPage
    {
        public ViewModel Model
        {
            get { return ((IFubuMvcSampleApplicationPage)Page).Model as ViewModel; }
        }
        object IFubuMvcSampleApplicationPage.Model
        {
            get { return ((IFubuMvcSampleApplicationPage)Page).Model; }
        }

        public void SetModel(object model)
        {
            throw new NotImplementedException();
        }
    }
}