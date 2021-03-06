using Android.Content;
using Android.Views;
using StateButton.Android;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Android.Views.Accessibility;
using AndroidView = Android.Views;
using Android.Runtime;

[assembly: ExportRenderer(typeof(StateButton.StateButton), typeof(StateButtonRenderer))]

namespace StateButton.Android
{
    public class StateButtonRenderer : FrameRenderer
    {
        public StateButtonRenderer(Context context) : base(context)
        {
            SetAccessibilityDelegate(new MyAccessibilityDelegate());
        }

        private Rect rect;
        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;

            if (!e.NewElement.GestureRecognizers.Any())
                return;

            if (e.NewElement.GestureRecognizers.All(x => x.GetType() != typeof(TouchGestureRecognizer)))
                return;

            Touch += (sender, te) =>
            {
                var v = (AndroidView.View)sender;
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

        public override bool OnKeyUp([GeneratedEnum] Keycode keyCode, KeyEvent e)
        {
            if(keyCode == Keycode.Space || keyCode == Keycode.Enter)
            {
                foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                {
                    if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                    {
                        touchGestureRecognizer.Clicked();
                    }
                }
            }

            return base.OnKeyUp(keyCode, e);
        }

        private class MyAccessibilityDelegate : AccessibilityDelegate
        {
            public override void OnInitializeAccessibilityNodeInfo(AndroidView.View host, AccessibilityNodeInfo info)
            {
                base.OnInitializeAccessibilityNodeInfo(host, info);
                info.ClassName = "android.widget.Button";
                info.Clickable = true;
            }
        }

    }
}