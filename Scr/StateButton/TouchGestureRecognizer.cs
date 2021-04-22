using System;
using Xamarin.Forms;

namespace StateButton
{
    public sealed class TouchGestureRecognizer : Element, IGestureRecognizer
    {
        public Action Pressed;
        public Action Released;
        public Action Clicked;
    }
}