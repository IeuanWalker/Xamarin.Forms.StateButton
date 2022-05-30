namespace TestLibrary
{
    partial class StateButtonHandler
    {
        public static PropertyMapper<IStateButton, StateButtonHandler> StateButtonMapper =>
            new()
            {
                [nameof(StateButton.Pressed)] = MapProgress,
                [nameof(StateButton.Released)] = MapReleased,
                [nameof(StateButton.Clicked)] = MapClicked
            };
    }
}
