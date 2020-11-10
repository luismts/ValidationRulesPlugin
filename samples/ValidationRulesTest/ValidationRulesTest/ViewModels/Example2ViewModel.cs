using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;
using ValidationRulesTest.Models;

namespace ValidationRulesTest.ViewModels
{
    public class Example2ViewModel : ExtendedPropertyChanged
    {
        public Example2ViewModel()
        {
            _user = new ValidatableObject<User>();
            
            AddValidations();
        }

        private ValidatableObject<User> _user;
        public ValidatableObject<User> User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        private void AddValidations()
        {
            // User validations
            _user.Validations.Add(new UserRule());
        }

        public bool Validate()
        {
            return User.Validate();
        }

    }
}
