using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using ValidationRulesTest.Validations;

namespace ValidationRulesTest.Models
{
    public class UserValidator2 : Validator<User>
    {
        public UserValidator2()
        {
            //Name validations
            Name = Build<string>()
                    .WithRule(new IsNotNullOrEmptyRule<string>(), "A name is required.");

            //Lastname validations
            LastName = Build<string>()
                        .WithRule(new IsNotNullOrEmptyRule<string>(), "A lastname is required.");

            //Email validations
            Email = Build<string>()
                        .IsRequired("A email is required.")
                        .WithRule(new EmailRule());

            InitUnit();
        }

        public Validatable<string> LastName { get; set; }
        public Validatable<string> Name { get; set; }
        public Validatable<string> Email { get; set; }

        //public override bool Validate()
        //{
        //    Your logic goes here
        //    return _unit1.Validate();
        //}

        public override User Map()
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
