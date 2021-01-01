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
            _user = new Validatable<User>();
            _user.Value = new User();
            
            AddValidations();
        }

        private Validatable<User> _user;
        public Validatable<User> User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }

        private void AddValidations()
        {
            // Your validations goes here
            _user.Validations.Add(new UserRule());
        }

        public bool Validate()
        {
            // Your logic goes here
            return User.Validate();
        }

    }
}
