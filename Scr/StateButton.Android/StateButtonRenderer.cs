﻿using Android.Content;
using Android.Views;
using StateButton.Android;
using System.Linq;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using View = Xamarin.Forms.View;

[assembly: ExportRenderer(typeof(StateButton.StateButton), typeof(StateButtonRenderer))]

namespace StateButton.Android
{
    public class StateButtonRenderer : FrameRenderer
    {
        public StateButtonRenderer(Context context) : base(context)
        {
        }

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
                switch (te.Event.Action)
                {
                    case MotionEventActions.Down:
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
                                touchGestureRecognizer.Released();
                                touchGestureRecognizer.Clicked();
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
                }
            };
        }
    }
}