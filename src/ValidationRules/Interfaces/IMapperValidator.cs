

namespace Plugin.ValidationRules.Interfaces
{
    public interface IMapperValidator<out Model> where Model : class
    {
        Model Map();
    }
}
