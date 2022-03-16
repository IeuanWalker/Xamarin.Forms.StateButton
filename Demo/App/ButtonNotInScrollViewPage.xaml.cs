using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace App
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ButtonNotInScrollViewPage : ContentPage
    {
        public ButtonNotInScrollViewPage()
        {
            InitializeComponent();
        }

        private async void StateButton_Clicked(object sender, EventArgs e)
        {
            await DisplayAlert("Clicked", string.Empty, "OK");
        }

        private async void StateButton_Released(object sender, EventArgs e)
        {
            await DisplayAlert("Release", string.Empty, "OK");
        }
    }
}