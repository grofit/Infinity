namespace Infinity.Plugins
{
    public interface IMapper<T_To, T_From>
    {
        T_To Map(T_From mappingSource);
        T_From Map(T_To mappingSource);
    }
}
