# Xamarin.Forms.StateButton

With this control you are able to create any style of button.
This is possible as it acts as a wrapper to you XAML and provides you the events/ commands to dind too and exposes a `StateEnum` that you can bind to, to change the button style.

## What can I do with it?
### Properties
| Property | What it does | Extra info |
|---|---|---- |
| State | This changes based on the button state. i.e. `Pressed`, `NotPressed` | Default state is `NotPressed` <br/>  <br/> The binding mode is `OneWayToSource` so it can only be controlled via this control. |

### Events
| Event | What it does |
|---|---|
| Clicked | Triggerd when the button is pressed and released |
| TouchedDown | Triggerd when the button is pressed |
| TouchedUp | Triggerd when the button is released |

### Commands
| Command | What it does |
|---|---|
| ClickedCommand | Triggerd when the button is pressed and released |
| TouchedDownCommand | Triggerd when the button is pressed |
| TouchedUpCommand | Triggerd when the button is released |


# How to style the button for different states
There are 2 ways to style the button -
- [DataTriggers](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/app-fundamentals/triggers#data-triggers)
- [VisualStateManager](https://docs.microsoft.com/en-us/xamarin/xamarin-forms/user-interface/visual-state-manager)

## DataTriggers
Simple add a DataTrigger to any element and bind it to the `State` property of the button - 
```xaml
<stateButton:StateButton HorizontalOptions="Center" TouchedUpCommand="{Binding TouchUpCommand}">
    <stateButton:StateButton.Content>
        <Frame Padding="20,10" BackgroundColor="Red">
            <Frame.Triggers>
                <DataTrigger Binding="{Binding Source={RelativeSource AncestorType={x:Type stateButton:StateButton}}, Path=State}"
                             TargetType="Frame"
                             Value="Pressed">
                    <Setter Property="BackgroundColor" Value="LightGray" />
                </DataTrigger>
            </Frame.Triggers>
            <Label Text="Test" TextColor="White">
                <Label.Triggers>
                    <DataTrigger Binding="{Binding Source={RelativeSource AncestorType={x:Type stateButton:StateButton}}, Path=State}"
                                 TargetType="Label"
                                 Value="Pressed">
                        <Setter Property="TextColor" Value="Blue" />
                    </DataTrigger>
                </Label.Triggers>
            </Label>
        </Frame>
    </stateButton:StateButton.Content>
</stateButton:StateButton>
```


## VisualStateManager
You can also use the VisualStateManager, but this is NOT RECOMENDED due to a bug in Xamarin.forms that causes a `NullReferenceException` when `TargetName` property is used, heres a link to the bug - https://github.com/xamarin/Xamarin.Forms/issues/10710.
If you still want to use it -
```xaml
<stateButton:StateButton BackgroundColor="Red" HorizontalOptions="Center">
    <stateButton:StateButton.Content>
        <Frame Padding="20,10" BackgroundColor="Transparent">
            <Label Text="Test" TextColor="White" />
        </Frame>
    </stateButton:StateButton.Content>
    <VisualStateManager.VisualStateGroups>
        <VisualStateGroup Name="ButtonStates">
            <VisualState Name="Pressed">
                <VisualState.Setters>
                    <Setter Property="BackgroundColor" Value="Blue" />
                </VisualState.Setters>
            </VisualState>
            <VisualState Name="NotPressed">
                <VisualState.Setters>
                    <Setter Property="BackgroundColor" Value="Red" />
                </VisualState.Setters>
            </VisualState>
        </VisualStateGroup>
    </VisualStateManager.VisualStateGroups>
</stateButton:StateButton>
```
