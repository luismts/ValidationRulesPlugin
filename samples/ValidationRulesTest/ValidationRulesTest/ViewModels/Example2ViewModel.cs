using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;
using ValidationRulesTest.Models;

namespace ValidationRulesTest.ViewModels
{
    public class Example2ViewModel : ExtendedPropertyChanged
    {
        ValidationUnit _unit1;

        public Example2ViewModel()
        {
            _user = new User();
            _unit1 = new ValidationUnit(_user.Name, _user.LastName, _user.Email);
            
            AddValidations();
        }

        private User _user;
        public User User
        {
            get => _user;
            set => SetProperty(ref _user, value);
        }


        private void AddValidations()
        {
            // Name validations
            _user.Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            //Lastname validations
            _user.LastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A lastname is required." });

            //Email validations
            _user.Email.Validations.Add(new IsNotNullOrEmptyRule<string>{ ValidationMessage = "A email is required." });
            _user.Email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not valid." });
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
