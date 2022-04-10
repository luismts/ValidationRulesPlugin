using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class Example2 : ContentPage
    {
        Example2ViewModel _context;
        public Example2()
        {
            InitializeComponent();

            _context = new Example2ViewModel();
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
                DisplayAlert(":(", $"This form is not valid. {_context.User.Error}", "OK");
            }
        }
    }
}
