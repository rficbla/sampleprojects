using NHibernateSampleApplication.Domain;
using StructureMap;

namespace NHibernateSampleApplication
{
    public class BootStrapper : IBootstrapper
    {
        private static bool _hasStarted;

        public void BootstrapStructureMap()
        {
            ObjectFactory.Initialize(x => x.Scan(s =>
                                                     {
                                                         s.AssemblyContainingType<Book>();
                                                         s.WithDefaultConventions();
                                                         s.LookForRegistries();
                                                     }));
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