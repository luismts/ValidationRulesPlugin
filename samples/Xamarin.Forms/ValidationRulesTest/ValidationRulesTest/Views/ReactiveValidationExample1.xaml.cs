using System;
using Xamarin.Forms;
using ValidationRulesTest.ViewModels;

namespace ValidationRulesTest
{
    public partial class ReactiveValidationExample1 : ContentPage
    {
        ReactiveValidationExample1ViewModel _context;
        public ReactiveValidationExample1()
        {
            InitializeComponent();

            _context = new ReactiveValidationExample1ViewModel();
            BindingContext = _context;
        }

        protected override void OnDisappearing()
        {
            _context.Dispose();
            base.OnDisappearing();
        }
    }
}
