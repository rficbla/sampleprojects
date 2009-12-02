using NHibernateSampleApplication.Behavior;
using StructureMap.Configuration.DSL;

namespace NHibernateSampleApplication
{
    public class NHibernateSampleApplicationRegistry : Registry
    {
        public NHibernateSampleApplicationRegistry()
        {
            ForRequestedType<IRequestHandler>()
                .TheDefaultIsConcreteType<AtomicRequestHandler>();
        }
    }
}