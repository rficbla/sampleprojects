using System.Web.Routing;
using FubuMVC.Container.StructureMap.Config;
using FubuMVC.Core.Controller.Config;
using FubuMvcSampleApplication.Domain;
using Microsoft.Practices.ServiceLocation;
using StructureMap;

namespace FubuMvcSampleApplication
{
    public class BootStrapper : IBootstrapper
    {
        private static bool _hasStarted;

        public void BootstrapStructureMap()
        {
            ControllerConfiguration.Configure();

            ObjectFactory.Initialize(x =>
                                         {
                                             x.Scan(s =>
                                                        {
                                                            s.AssemblyContainingType<User>();
                                                            s.WithDefaultConventions();
                                                        });
                                             x.AddRegistry(new FrameworkServicesRegistry());
                                             x.AddRegistry(new FubuMvcSampleApplicationRegistry());
                                             x.AddRegistry(new ControllerConfig());
                                         });

            ObjectFactory.AssertConfigurationIsValid();

            setup_service_locator();

            initializeRoutes();
        }


        private static void setup_service_locator()
        {
            ServiceLocator.SetLocatorProvider(() => new StructureMapServiceLocator());
        }


        private static void initializeRoutes()
        {
            ObjectFactory.GetInstance<IRouteConfigurer>().LoadRoutes(RouteTable.Routes);
        }

        public static void BootStrap()
        {
            new BootStrapper().BootstrapStructureMap();
        }

        public static void Reset()
        {
            if (_hasStarted)
            {
                ObjectFactory.ResetDefaults();
            }
            else
            {
                BootStrap();
                _hasStarted = true;
            }
        }
    }
}