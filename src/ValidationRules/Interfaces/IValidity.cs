

using System.Collections.Generic;

namespace Plugin.ValidationRules.Interfaces
{
    public interface IValidity
    {
        bool IsValid { get; set; }

        List<string> Errors { get; set; }

        bool HasErrors { get; set; }

        string Error { get; set; }

        bool Validate();
    }
}
