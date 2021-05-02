using System;
using Xamarin.Forms;
using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class Example5 : ContentPage
    {
        Example5ViewModel _context;
        public Example5()
        {
            InitializeComponent();

            _context = new Example5ViewModel();
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

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            // Test clear all 1
            _context.Name.Value = null;
            _context.LastName.Value = null;
            _context.Email.Value = null;
        }
    }
}
