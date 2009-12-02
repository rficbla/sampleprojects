using System;
using FubuMVC.Core.Controller.Config;
using FubuMvcSampleApplication.Web.WebForms;
using Microsoft.Practices.ServiceLocation;

namespace FubuMvcSampleApplication.Web
{
    public static class UrlExtensions
    {
        public static IUrlResolver UrlTo(this IFubuMvcSampleApplicationPage page)
        {
            return ServiceLocator.Current.GetInstance<IUrlResolver>();
        }
    }

    public static class UrlConverter
    {
        public static string EditUser(this IUrlResolver urlResolver, string userId)
        {
            return (String.Format("/user/edit?UserId={0}", userId));
        }

        public static string DeleteUser(this IUrlResolver urlResolver, string userId)
        {
            return (String.Format("/user/delete?UserId={0}", userId));
        }
    }
}