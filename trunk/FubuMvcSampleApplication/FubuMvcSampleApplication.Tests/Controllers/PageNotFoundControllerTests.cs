using FubuMvcSampleApplication.Controllers;
using NUnit.Framework;
using StructureMap.AutoMocking;

namespace FubuMvcSampleApplication.Tests.Controllers
{
    public class PageNotFoundControllerTests
    {
        [TestFixture]
        public class When_you_enter_a_url_that_is_not_valid
        {
            private RhinoAutoMocker<PageNotFoundController> _mocker;
            private PageNotFoundController _pageNotFoundController;

            [SetUp]
            public void SetUp()
            {
                _mocker = new RhinoAutoMocker<PageNotFoundController>();
                _pageNotFoundController = _mocker.ClassUnderTest;
            }

            [Test]
            public void Should_render_a_PageNotFound_view_and_append_standard_error_message_to_description()
            {
                PageNotFoundViewModel pageNotFoundViewModel = new PageNotFoundViewModel();
                Assert.AreEqual("Requested Url not found",
                                _pageNotFoundController.Index(pageNotFoundViewModel).Description);
            }
        }
    }
}