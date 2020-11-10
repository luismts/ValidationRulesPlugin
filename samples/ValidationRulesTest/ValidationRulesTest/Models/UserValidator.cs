using Plugin.ValidationRules;
using System;
using System.Collections.Generic;
using System.Text;
using ValidationRulesTest.Validations;

namespace ValidationRulesTest.Models
{
    public class UserValidator
    {
        public UserValidator()
        {
            LastName = new ValidatableObject<string>();
            Name = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();

            // Name validations
            Name.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A name is required." });

            //Lastname validations
            LastName.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A lastname is required." });

            //Email validations
            Email.Validations.Add(new IsNotNullOrEmptyRule<string> { ValidationMessage = "A email is required." });
            Email.Validations.Add(new EmailRule());
        }

        public ValidatableObject<string> LastName { get; set; }
        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<string> Email { get; set; }

        public User Cast()
        {
            return new User
            {
                Name = this.Name.Value,
                LastName = this.LastName.Value,
                Email = this.Email.Value
            };
        }
    }
}
