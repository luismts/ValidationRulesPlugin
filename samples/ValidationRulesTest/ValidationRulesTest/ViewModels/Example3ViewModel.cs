using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;
using ValidationRulesTest.Models;

namespace ValidationRulesTest.ViewModels
{
    public class Example3ViewModel : ExtendedPropertyChanged
    {
        ValidationUnit _unit1;

        public Example3ViewModel()
        {
            _user = new UserValidator();
            _unit1 = new ValidationUnit(_user.Name, _user.LastName, _user.Email);
        }

        private UserValidator _user;
        public UserValidator User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool Validate()
        {
            //var isValidName = _name.Validate();
            //var isValidLastname = _lastname.Validate();
            //var isValidEmail = _email.Validate();

            //return isValidName && isValidLastname && isValidEmail;
            return _unit1.Validate();
        }

    }
}
