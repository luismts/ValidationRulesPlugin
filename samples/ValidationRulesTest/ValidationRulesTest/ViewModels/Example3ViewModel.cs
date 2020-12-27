using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;
using ValidationRulesTest.Models;

namespace ValidationRulesTest.ViewModels
{
    public class Example3ViewModel : ExtendedPropertyChanged
    {

        public Example3ViewModel()
        {
            _user = new UserValidator();
        }

        private UserValidator _user;
        public UserValidator User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        public bool Validate()
        {
            User modelUser = User.Map();

            // Your logic goes here
            return User.Validate();
        }

    }
}
