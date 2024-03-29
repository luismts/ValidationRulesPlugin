﻿using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class Example3 : ContentPage
    {
        Example3ViewModel _context;
        public Example3()
        {
            InitializeComponent();

            _context = new Example3ViewModel();
            BindingContext = _context;
        }
        
        private void Button_Clicked(object sender, EventArgs e)
        {
            var isValid = _context.Validate();

            if (isValid)
            {
                DisplayAlert(":)", "This form is valid", "OK");
            }
            else
            {
                DisplayAlert(":(", "This form is not valid", "OK");
            }
        }

        private void nameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            _context.User.Name.Validate();
        }

        private void lastnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            _context.User.LastName.Validate();
        }

        private void emailEntry_Unfocused(object sender, FocusEventArgs e)
        {
            _context.User.Email.Validate();
        }
    }
}
