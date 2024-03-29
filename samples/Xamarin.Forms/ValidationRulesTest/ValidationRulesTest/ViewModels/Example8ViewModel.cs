﻿using Plugin.ValidationRules;
using Plugin.ValidationRules.Extensions;
using System.Collections.Generic;

namespace ValidationRulesTest.ViewModels
{
    public class Example8ViewModel
    {
        public Example8ViewModel()
        {
            Quantity = Validator.Build<int>()
                .Must(value => value > 0, "Need to specify the quantity");

            var monkeyList = new List<string>();
            monkeyList.Add("Baboon");
            monkeyList.Add("Capuchin Monkey");
            monkeyList.Add("Blue Monkey");
            monkeyList.Add("Squirrel Monkey");
            monkeyList.Add("Golden Lion Tamarin");
            monkeyList.Add("Howler Monkey");
            monkeyList.Add("Japanese Macaque");

            MonkeyList = ValidatorList.Build<string>()
                .AddItemsSource(monkeyList)
                .IsRequired("An item is required.")
                .Must(value => 
                {
                    if(MonkeyList.SelectedIndex == 2 && Quantity.Value < 5) // == blue monkey
                        return false;

                    return true;
                }, "Minimum quantity of this monkey is 5");

            UnitValidation = new ValidationUnit(Quantity, MonkeyList);
        }

        public ValidationUnit UnitValidation;

        public Validatable<int> Quantity { get; set; }
        public ValidatableList<string> MonkeyList { get; set; }

        public bool Validate()
        {
            return Quantity.Validate() && MonkeyList.Validate();
        }
    }
}
