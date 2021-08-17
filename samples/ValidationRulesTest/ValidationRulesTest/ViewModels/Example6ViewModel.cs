using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Formatters;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationRulesTest.ViewModels
{
    public class Example6ViewModel
    {
        public Example6ViewModel()
        {
            Name = Validator.Build<string>().IsRequired("The name is required.");
            Phone = Validator.Build<string>()
                .When(x => !string.IsNullOrEmpty(x))
                .Must(x => x.Length == 12, "Minimum lenght is 12.");

            Phone.ValueFormatter = new MaskFormatter("XXX-XXX-XXXX");
        }

        public Validatable<string> Name { get; set; }
        public Validatable<string> Phone { get; set; }

        public bool Validate()
        {
            return Name.Validate() && Phone.Validate();
        }
    }
}
