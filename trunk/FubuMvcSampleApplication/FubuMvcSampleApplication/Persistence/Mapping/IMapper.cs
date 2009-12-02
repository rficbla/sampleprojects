namespace FubuMvcSampleApplication.Persistence.Mapping
{
    public interface IMapper<T1, T2>
    {
        T2 MapFrom(T1 objectToMapFrom);
    }
}