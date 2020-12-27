

namespace Plugin.ValidationRules.Interfaces
{
    public interface IMapperValidator<in Validator, out Model> 
        where Validator : class
        where Model : class
    {
        Model Map(Validator input);
    }
}
