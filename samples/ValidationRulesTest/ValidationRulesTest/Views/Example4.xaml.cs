using System;
using Xamarin.Forms;
using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class Example4 : ContentPage
    {
        Example4ViewModel _context;
        public Example4()
        {
            InitializeComponent();

            _context = new Example4ViewModel();
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
    }
}
