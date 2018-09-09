using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules;
using ValidationRulesTest.Validations;

namespace ValidationRulesTest.ViewModels
{
    public class Example1ViewModel : ExtendedPropertyChanged
    {

        ValidationUnit _unit1;

        public Example1ViewModel()
        {
            _name = new ValidatableObject<string>();
            _lastname = new ValidatableObject<string>();
            _email = new ValidatableObject<string>();

            _unit1 = new ValidationUnit(_name, _lastname, _email);

            AddValidations();
        }

        private ValidatableObject<string> _name;
        public ValidatableObject<string> Name
        {
            get => _name;
            set => SetProperty(ref _name, value);
        }

        private ValidatableObject<string> _lastname;
        public ValidatableObject<string> LastName
        {
            get => _lastname;
            set => SetProperty(ref _lastname, value);
        }

        private ValidatableObject<string> _email;
        public ValidatableObject<string> Email
        {
            get => _email;
            set => SetProperty(ref _email, value);
        }


        private void AddValidations()
        {
            // Name validations
            _name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            //Lastname validations
            _lastname.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A lastname is required." });

            //Email validations
            _email.Validations.Add(new IsNotNullOrEmptyRule<string>{ ValidationMessage = "A email is required." });
            _email.Validations.Add(new EmailRule<string> { ValidationMessage = "Email is not valid." });
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
