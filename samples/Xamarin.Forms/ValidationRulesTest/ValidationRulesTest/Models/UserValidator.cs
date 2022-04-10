using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using Plugin.ValidationRules.Interfaces;
using ValidationRulesTest.Validations;

namespace ValidationRulesTest.Models
{
    public class UserValidator : IMapperValidator<User>
    {
        ValidationUnit _unit1;

        public UserValidator()
        {
            LastName = new Validatable<string>();
            Name = new Validatable<string>();
            Email = new Validatable<string>();

            _unit1 = new ValidationUnit(Name, LastName, Email);

            // Name validations
            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            //Lastname validations
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A lastname is required." });

            //Email validations
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new EmailRule());
        }

        public Validatable<string> LastName { get; set; }
        public Validatable<string> Name { get; set; }
        public Validatable<string> Email { get; set; }

        public bool Validate() 
        { 
            // Your logic goes here
            return _unit1.Validate(); 
        }

        public User Map()
        {
            // Simple Manual Mapper
            var manualMapperUser = new User
            {
                Name = this.Name.Value,
                LastName = this.LastName.Value,
                Email = this.Email.Value
            };

            return manualMapperUser;
        }
    }
}
