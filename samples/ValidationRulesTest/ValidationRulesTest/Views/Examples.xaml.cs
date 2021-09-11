using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ValidationRulesTest.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class Examples : ContentPage
    {
        public Examples()
        {
            InitializeComponent();
        }

        private void example1_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example1());
        }

        private void example2_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example2());
        }

        private void example3_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example3());
        }

        private void example4_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example4());
        }

        private void example5_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example5());
        }

        private void example6_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example6());
        }

        private void example7_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new Example7());
        }
    }
}