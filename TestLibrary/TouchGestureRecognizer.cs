namespace TestLibrary
{
    public sealed class TouchGestureRecognizer : Element, IGestureRecognizer
    {
        public Action Pressed;
        public Action Released;
        public Action Clicked;
    }
}