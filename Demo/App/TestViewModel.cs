using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace App
{
    public class TestViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand ClickedCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("Clicked command", "Button clicked", "Ok"));
        public ICommand TouchDownCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("TouchDown command", "Button pressed", "Ok"));
        public ICommand TouchUpCommand => new Command(async _ => await Application.Current.MainPage.DisplayAlert("TouchUp command", "Button released", "Ok"));
    }
}
