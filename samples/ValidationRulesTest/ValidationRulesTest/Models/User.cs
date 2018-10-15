using Plugin.ValidationRules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ValidationRulesTest.Models
{
    public class User
    {
        public User()
        {
            LastName = new ValidatableObject<string>();
            Name = new ValidatableObject<string>();
            Email = new ValidatableObject<string>();
        }

        public ValidatableObject<string> LastName { get; set; }
        public ValidatableObject<string> Name { get; set; }
        public ValidatableObject<string> Email { get; set; }
    }
}
