using Foundation;
using StateButton.iOS;
using System;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StateButton.StateButton), typeof(StateButtonRenderer))]

namespace StateButton.iOS
{
    public class StateButtonRenderer : FrameRenderer
    {
        public new static void Init()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Frame> e)
        {
            try
            {
                base.OnElementChanged(e);

                AccessibilityTraits = UIAccessibilityTrait.Button;

                if (e.OldElement != null) return;

                if (!e.NewElement.GestureRecognizers.Any())
                    return;

                if (e.NewElement.GestureRecognizers.All(x => !(x is TouchGestureRecognizer)))
                    return;

                AddGestureRecognizer(new UITapGestureRecognizer(() =>
                {
                    foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                    {
                        if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                        {
                            touchGestureRecognizer.Clicked();
                        }
                    }
                }));
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            try
            {
                base.TouchesMoved(touches, evt);

                foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                {
                    if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                    {
                        touchGestureRecognizer.Released();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            try
            {
                base.TouchesBegan(touches, evt);

                foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                {
                    if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                    {
                        touchGestureRecognizer.Pressed();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            try
            {
                base.TouchesCancelled(touches, evt);

                foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x is TouchGestureRecognizer))
                {
                    if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                    {
                        touchGestureRecognizer.Released();
                    }
                }
            }
            catch (Exception)
            {
                // ignored
            }
        }
    }
}