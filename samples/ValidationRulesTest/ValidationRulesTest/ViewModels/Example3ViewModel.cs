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
            return _unit1.Validate();
        }

    }
}
