using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ReactiveValidationExample1 : ContentPage
    {
        ReactiveValidationExample1ViewModel _viewModel;
        
        public ReactiveValidationExample1()
        {
            InitializeComponent();
            _viewModel = new ReactiveValidationExample1ViewModel();
            BindingContext = _viewModel;
        }

        protected override void OnDisappearing()
        {
            _viewModel.Dispose();
            base.OnDisappearing();
        }
    }
}