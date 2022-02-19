using System;
using ValidationRulesTest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValidationRulesTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Example8 : ContentPage
    {
        Example8ViewModel _context;
        public Example8()
        {
            try
            {
                InitializeComponent();

                _context = new Example8ViewModel();
                BindingContext = _context;
            }
            catch (Exception e)
            {

                throw;
            }
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            //var isValid = _context.Validate();
            var isValid = _context.UnitValidation.Validate();

            if (isValid)
            {
                DisplayAlert(":)", "This form is valid", "OK");
            }
            else
            {
                var error = _context.UnitValidation.Error;
                DisplayAlert(":(", error, "OK");
            }
        }
    }
}