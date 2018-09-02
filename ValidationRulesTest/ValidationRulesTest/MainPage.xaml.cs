using System;
using Xamarin.Forms;
using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class MainPage : ContentPage
    {
        MainPageViewModel _context;
        public MainPage()
        {
            InitializeComponent();

            _context = new ViewModels.MainPageViewModel();
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
            _context.Name.Validate();
        }

        private void lastnameEntry_Unfocused(object sender, FocusEventArgs e)
        {
            _context.LastName.Validate();
        }

        private void emailEntry_Unfocused(object sender, FocusEventArgs e)
        {
            _context.Email.Validate();
        }
    }
}
