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
    public partial class ReactiveValidationExample1 : ContentPage
    {
        ReactiveValidationExample1ViewModel _viewModel;
        public ReactiveValidationExample1()
        {
            InitializeComponent();
            _viewModel = new ReactiveValidationExample1ViewModel();
            BindingContext = _viewModel;
        }
    }
}