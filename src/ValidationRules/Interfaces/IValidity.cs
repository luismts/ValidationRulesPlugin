

namespace Plugin.ValidationRules.Interfaces
{
    public interface IValidity
    {
        bool IsValid { get; set; }

        bool Validate();
    }
}
