using FubuMvcSampleApplication.Controllers;
using FubuMvcSampleApplication.Domain;
using FubuMvcSampleApplication.Persistence.Mapping;

using StructureMap.Configuration.DSL;

namespace FubuMvcSampleApplication
{
    public class FubuMvcSampleApplicationRegistry : Registry
    {
        public FubuMvcSampleApplicationRegistry()
        {
            ForRequestedType<IMapper<UserEditViewModel, User>>().TheDefaultIsConcreteType<UserMapper>();
        }
    }
}