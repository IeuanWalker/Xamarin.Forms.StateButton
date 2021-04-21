using System.Windows.Input;
using Xamarin.Forms;

namespace App
{
    public class TestViewModel
    {
        public ICommand ClickedCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("Clicked command", "Button clicked", "Ok"));
        public ICommand PressedCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("Pressed command", "Button pressed", "Ok"));
        public ICommand ReleasedCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("Released command", "Released released", "Ok"));
    }
}
