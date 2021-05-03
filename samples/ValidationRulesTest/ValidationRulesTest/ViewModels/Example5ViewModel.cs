using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;
using ValidationRulesTest.Validations;
using EmailRule = Plugin.ValidationRules.Rules.EmailRule;

namespace ValidationRulesTest.ViewModels
{
    public class Example5ViewModel 
    {
        ValidationUnit _unit1;

        public Example5ViewModel()
        {
           AddValidations();
        }

        public Validatable<string> LastName { get; set; }
        public Validatable<string> Name { get; set; }
        public Validatable<string> Email { get; set; }


        private void AddValidations()
        {
            Name = new Validatable<string>(new NotEmptyRule<string>("").WithMessage("A name is required."));
            LastName = new Validatable<string>(new IsNotNullOrEmptyRule<string>().WithMessage("A lastname is required."));

            //// You can add several Rules by this
            //Email = new Validatable<string>(
            //    new IsNotNullOrEmptyRule<string>().WithMessage("A email is required."), 
            //    new EmailRule()
            //);

            // Or this
            Email = Validator.Build<string>()
                    //.Add(new IsNotNullOrEmptyRule<string>(), "An email is required.")
                    .IsRequired("An email is required.")
                    .WithRule(new EmailRule());

            // Add to the unit
            _unit1 = new ValidationUnit(Name, LastName, Email);
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
