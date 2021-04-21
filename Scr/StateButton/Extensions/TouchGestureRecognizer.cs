using System;
using Xamarin.Forms;

namespace StateButton.Extensions
{
    public class TouchGestureRecognizer : Element, IGestureRecognizer
    {
        public Action TouchDown;
        public Action TouchUp;
        public Action Clicked;
    }
}
