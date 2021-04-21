using System;
using System.Diagnostics;
using System.Windows.Input;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StateButton
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class StateButton : ContentView
    {
        #region Bindable Properties
        public static readonly BindableProperty StateProperty = BindableProperty.Create(nameof(State), typeof(ButtonStateEnum), typeof(StateButton), ButtonStateEnum.NotPressed, BindingMode.OneWayToSource);
        public ButtonStateEnum State
        {
            get => (ButtonStateEnum)GetValue(StateProperty);
            set => SetValue(StateProperty, value);
        }
#endregion

        #region Commands

        public static readonly BindableProperty ClickedCommandProperty = BindableProperty.Create(nameof(ClickedCommand), typeof(ICommand), typeof(StateButton));

        public ICommand ClickedCommand
        {
            get => (ICommand)GetValue(ClickedCommandProperty);
            set => SetValue(ClickedCommandProperty, value);
        }

        public static readonly BindableProperty ClickedCommandParameterProperty = BindableProperty.Create(nameof(ClickedCommandParameter), typeof(object), typeof(StateButton));

        public object ClickedCommandParameter
        {
            get => GetValue(ClickedCommandParameterProperty);
            set => SetValue(ClickedCommandParameterProperty, value);
        }

        public static readonly BindableProperty TouchedDownCommandProperty = BindableProperty.Create(nameof(TouchedDownCommand), typeof(ICommand), typeof(StateButton));

        public ICommand TouchedDownCommand
        {
            get => (ICommand)GetValue(TouchedDownCommandProperty);
            set => SetValue(TouchedDownCommandProperty, value);
        }

        public static readonly BindableProperty TouchedDownCommandParameterProperty = BindableProperty.Create(nameof(TouchedDownCommandParameter), typeof(object), typeof(StateButton));

        public object TouchedDownCommandParameter
        {
            get => GetValue(TouchedDownCommandParameterProperty);
            set => SetValue(TouchedDownCommandParameterProperty, value);
        }

        public static readonly BindableProperty TouchedUpCommandProperty = BindableProperty.Create(nameof(TouchedUpCommand), typeof(ICommand), typeof(StateButton));

        public ICommand TouchedUpCommand
        {
            get => (ICommand)GetValue(TouchedUpCommandProperty);
            set => SetValue(TouchedUpCommandProperty, value);
        }

        public static readonly BindableProperty TouchedUpCommandParameterProperty = BindableProperty.Create(nameof(TouchedUpCommandParameter), typeof(object), typeof(StateButton));

        public object TouchedUpCommandParameter
        {
            get => GetValue(TouchedUpCommandParameterProperty);
            set => SetValue(TouchedUpCommandParameterProperty, value);
        }

        #endregion Commands

        #region Events

        public event EventHandler<EventArgs> TouchedDown;
        public event EventHandler<EventArgs> TouchedUp;
        public event EventHandler<EventArgs> Clicked;

        #endregion

        public StateButton()
        {
            InitializeComponent();

            VisualStateManager.GoToState(this, nameof(ButtonStateEnum.NotPressed));

            TouchRecognizer.TouchDown += TouchDownGesture;
            TouchRecognizer.TouchUp += TouchUpGesture;
            TouchRecognizer.Clicked += ClickedGesture;
        }

        private void TouchDownGesture()
        {
            if (!IsEnabled) return;

            TouchedDown?.Invoke(this, EventArgs.Empty);
            TouchedDownCommand?.Execute(TouchedDownCommandParameter);

            try
            {
                VisualStateManager.GoToState(this, nameof(ButtonStateEnum.Pressed));
                State = ButtonStateEnum.Pressed;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void TouchUpGesture()
        {
            if (!IsEnabled) return;

            TouchedUp?.Invoke(this, EventArgs.Empty);
            TouchedUpCommand?.Execute(TouchedUpCommandParameter);

            try
            {
                VisualStateManager.GoToState(this, nameof(ButtonStateEnum.NotPressed));
                State = ButtonStateEnum.NotPressed;
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.Message);
            }
        }

        private void ClickedGesture()
        {
            if (!IsEnabled) return;

            Clicked?.Invoke(this, EventArgs.Empty);
            ClickedCommand?.Execute(ClickedCommandParameter);
        }

    }
}