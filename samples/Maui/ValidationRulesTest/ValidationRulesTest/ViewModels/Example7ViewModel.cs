using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using ValidationRulesTest.Validations;

namespace ValidationRulesTest.ViewModels
{
    public class Example7ViewModel
    {
        public Example7ViewModel()
        {
            Password = Validator.Build<string>()
                .IsRequired("A password is required.")
                .WithRule(new PasswordRule());

            ConfirmPassword = Validator.Build<string>()
                .When(_ => !string.IsNullOrEmpty(Password.Value))
                .Must(x => x == Password.Value, "Password is not matching.");
        }

        public Validatable<string> Password { get; set; }
        public Validatable<string> ConfirmPassword { get; set; }

        public bool Validate()
        {
            return Password.Validate() && ConfirmPassword.Validate();
        }
    }
}
