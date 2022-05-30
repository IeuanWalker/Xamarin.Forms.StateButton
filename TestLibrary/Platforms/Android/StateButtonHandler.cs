using Android.Views;
using Android.Views.Accessibility;
using Microsoft.Maui.Handlers;
using Microsoft.Maui.Platform;
using static Android.Views.View;
using View = Android.Views.View;

namespace TestLibrary
{
    partial class StateButtonHandler : ViewHandler<StateButton, ContentViewGroup>
    {
        protected override ContentViewGroup CreatePlatformView()
        {
            return new(Context);
        }

        private Rect rect;
        protected override void ConnectHandler(ContentViewGroup platformView)
        {
            base.ConnectHandler(platformView);

            platformView.SetAccessibilityDelegate(new MyAccessibilityDelegate());

            platformView.Touch += (sender, te) =>
            {
                var v = (View)sender;
                switch (te.Event.Action)
                {
                    case MotionEventActions.Down:
                        rect = new Rect(v.Left, v.Top, v.Right, v.Bottom);
                        foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                touchGestureRecognizer.Pressed();
                            }
                        }
                        break;

                    case MotionEventActions.Up:

                        foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                if (rect.Contains(v.Left + (int)te.Event.GetX(), v.Top + (int)te.Event.GetY()))
                                {
                                    touchGestureRecognizer.Released();
                                    touchGestureRecognizer.Clicked();
                                }
                                else
                                {
                                    touchGestureRecognizer.Released();
                                }
                            }
                        }
                        break;

                    case MotionEventActions.Cancel:
                        foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                touchGestureRecognizer.Released();
                            }
                        }
                        break;
                    case MotionEventActions.Move:
                        foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                        {
                            if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                            {
                                if (!rect.Contains(v.Left + (int)te.Event.GetX(), v.Top + (int)te.Event.GetY()))
                                {
                                    touchGestureRecognizer.Released();
                                }
                            }
                        }
                        break;
                }
            };
        }


        private class MyAccessibilityDelegate : AccessibilityDelegate
        {
            public override void OnInitializeAccessibilityNodeInfo(View host, AccessibilityNodeInfo info)
            {
                base.OnInitializeAccessibilityNodeInfo(host, info);
                info.ClassName = "android.widget.Button";
                info.Clickable = true;
            }
        }
    }
}
