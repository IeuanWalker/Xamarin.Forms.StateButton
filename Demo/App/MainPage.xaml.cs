using System;
using Xamarin.Forms;

namespace App
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private async void StateButton_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert ("Clicked event", "Button clicked", "OK");
        }

        private async void StateButton_OnPressed(object sender, EventArgs e)
        {
            await DisplayAlert ("Pressed event", "Button pressed", "OK");
        }

        private async void StateButton_OnReleased(object sender, EventArgs e)
        {
            await DisplayAlert ("Released event", "Button released", "OK");
        }

        private void TapGestureRecognizer_OnTapped(object sender, EventArgs e)
        {
            Label textExtender = sender as Label;

            if (Description.MaxLines == int.MaxValue)
            {
                Description.MaxLines = 3;
                textExtender.Text = "See more..";
            }
            else
            {
                Description.MaxLines = int.MaxValue;
                textExtender.Text = "See less...";
            }

        }

        private async void Card_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert ("Card click", string.Empty, "OK");
        }

        private async void Button_OnClicked(object sender, EventArgs e)
        {
            await DisplayAlert ("XF button clicked", string.Empty, "OK");
        }

        private async void NestButton_OnClicked(object sender, EventArgs e)
        {
             await DisplayAlert ("Nested state button clicked", string.Empty, "OK");
        }
    }
}
