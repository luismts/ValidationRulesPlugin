using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Rules;
using System.Collections.Generic;
using ValidationRulesTest.Models;
using ValidationRulesTest.Validations;
using EmailRule = Plugin.ValidationRules.Rules.EmailRule;

namespace ValidationRulesTest.ViewModels
{
    public class Example5ViewModel 
    {
        ValidationUnit _validationUnit;
        UserValidator2 _testModelValidator;

        public Example5ViewModel()
        {
           AddValidations();
        }

        public Validatable<string> LastName { get; set; }
        public Validatable<string> Name { get; set; }
        public Validatable<string> Email { get; set; }


        private void AddValidations()
        {
            Name = new Validatable<string>(
                new NotEmptyRule<string>("").WithMessage("A name is required."),
                new IsNotNullOrEmptyRule<string>().WithMessage(() => "Hi!")
            );

            LastName = Validator.Build<string>()
                        .IsRequired("A last name is required.")
                        .Must(CustomValidation, "Last name need to be longer.")
                        .When(x => Name.Validate());

            //// You can add several Rules by this
            ///
            //Email = new Validatable<string>(
            //    new IsNotNullOrEmptyRule<string>().WithMessage("A email is required."), 
            //    new EmailRule()
            //);

            // Or this
            Email = Validator.Build<string>()
                    //.Add(new IsNotNullOrEmptyRule<string>(), "An email is required.")
                    .IsRequired("An email is required.")
                    .WithRule(new EmailRule())
                    .When(x => Name.Validate() && LastName.Validate());
            
            // Add to the unit
            _validationUnit = new ValidationUnit(Name, LastName, Email);

            //var listValidatables = new List<Validatable<string>> { Name, LastName, Email };
            //_validationUnit = new ValidationUnit(listValidatables);

            // Validator Model
            _testModelValidator = new UserValidator2();
        }

        public bool Validate()
        {
            // Test model
            //var isValidModel = _testModelValidator.Validate();

            return _validationUnit.Validate();
        }

        private bool CustomValidation(string parameter)
        {
            return parameter?.Length > 3;
        }

    }
}
