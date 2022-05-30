using System.Windows.Input;

namespace TestLibrary
{
    public interface IStateButton : IElement
    {
        ButtonStateEnum State { get; }
        ICommand ClickedCommand { get; }
        object ClickedCommandParameter { get; }
        ICommand PressedCommand { get; }
        object PressedCommandParameter { get; }
        ICommand ReleasedCommand { get; }
        object ReleasedCommandParameter { get; }
    }
}
