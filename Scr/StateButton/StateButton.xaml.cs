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

        public static readonly BindableProperty PressedCommandProperty = BindableProperty.Create(nameof(PressedCommand), typeof(ICommand), typeof(StateButton));

        public ICommand PressedCommand
        {
            get => (ICommand)GetValue(PressedCommandProperty);
            set => SetValue(PressedCommandProperty, value);
        }

        public static readonly BindableProperty PressedCommandParameterProperty = BindableProperty.Create(nameof(PressedCommandParameter), typeof(object), typeof(StateButton));

        public object PressedCommandParameter
        {
            get => GetValue(PressedCommandParameterProperty);
            set => SetValue(PressedCommandParameterProperty, value);
        }

        public static readonly BindableProperty ReleasedCommandProperty = BindableProperty.Create(nameof(ReleasedCommand), typeof(ICommand), typeof(StateButton));

        public ICommand ReleasedCommand
        {
            get => (ICommand)GetValue(ReleasedCommandProperty);
            set => SetValue(ReleasedCommandProperty, value);
        }

        public static readonly BindableProperty ReleasedCommandParameterProperty = BindableProperty.Create(nameof(ReleasedCommandParameter), typeof(object), typeof(StateButton));

        public object ReleasedCommandParameter
        {
            get => GetValue(ReleasedCommandParameterProperty);
            set => SetValue(ReleasedCommandParameterProperty, value);
        }

        #endregion Commands

        #region Events

        public event EventHandler<EventArgs> Pressed;
        public event EventHandler<EventArgs> Released;
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

            Pressed?.Invoke(this, EventArgs.Empty);
            PressedCommand?.Execute(PressedCommandParameter);

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

            Released?.Invoke(this, EventArgs.Empty);
            ReleasedCommand?.Execute(ReleasedCommandParameter);

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