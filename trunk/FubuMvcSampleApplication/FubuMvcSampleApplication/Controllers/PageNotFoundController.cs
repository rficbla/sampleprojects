using FubuMvcSampleApplication.Web;

namespace FubuMvcSampleApplication.Controllers
{
    public class PageNotFoundController
    {
        public PageNotFoundViewModel Index(PageNotFoundViewModel pageNotFoundViewModel)
        {
            return new PageNotFoundViewModel
                       {
                           Description = "Requested Url not found"
                       };
        }
    }

    public class PageNotFoundViewModel : ViewModel
    {
        public string Description { get; set; }
    }
}