using System.Collections.Generic;
using Plugin.Reactive.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;
using Plugin.ValidationRules.Rules;
using ValidationRulesTest.Validations;
using EmailRule = Plugin.ValidationRules.Rules.EmailRule;

namespace ValidationRulesTest.ViewModels
{
    public class ReactiveValidationExample1ViewModel : ExtendedPropertyChanged
    {
        ReactiveValidatable<string> _name;
        public ReactiveValidatable<string> Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        public ReactiveValidatable<string> _email;
        public ReactiveValidatable<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }
        
        public ReactiveValidatable<string> _password;
        public ReactiveValidatable<string> Password
        {
            get => _password;
            set => SetProperty(ref _password, value);
        }

        public ReactiveValidatable<int> _age;
        public ReactiveValidatable<int> Age
        {
            get => _age;
            set => SetProperty(ref _age, value);
        }

        public ReactiveValidationExample1ViewModel()
        {
            Name = new ReactiveValidatable<string>(
                string.Empty, new NotEmptyRule<string>(string.Empty)
                {
                    ValidationMessage = "Name is required"
                });
            
            Email = new ReactiveValidatable<string>(string.Empty,
                new EmailRule()
                {
                    ValidationMessage = "Email wrongly formatted"
                }, 
                new NotEmptyRule<string>(string.Empty)
                {
                    ValidationMessage = "Email is required"
                });
            
            Password = new ReactiveValidatable<string>(string.Empty, new PasswordRule(), new NotEmptyRule<string>(string.Empty)
            {
                ValidationMessage = "Password is required"
            });
        }
    }
}