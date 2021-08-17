using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ValidationRulesTest.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValidationRulesTest.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class Example6 : ContentPage
	{
        Example6ViewModel _context;
        public Example6 ()
		{
			InitializeComponent ();

            _context = new Example6ViewModel();
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