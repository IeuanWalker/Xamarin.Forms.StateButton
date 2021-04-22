using Foundation;
using StateButton.Extensions;
using StateButton.iOS;
using System.ComponentModel;
using System.Linq;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(StateButton.StateButton), typeof(StateButtonRenderer))]

namespace StateButton.iOS
{
    public class StateButtonRenderer : ViewRenderer
    {
        public static void Init() { }
        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);

            // Fix Xamarin.Forms Frame BackgroundColor Bug (https://github.com/xamarin/Xamarin.Forms/issues/2218)
            if (e.PropertyName == nameof(Element.BackgroundColor))
                Layer.BackgroundColor = Element.BackgroundColor.ToUIColor().CGColor;
        }

        protected override void OnElementChanged(ElementChangedEventArgs<View> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement != null) return;

            // Fix Xamarin.Forms Frame BackgroundColor Bug (https://github.com/xamarin/Xamarin.Forms/issues/2218)
            Layer.BackgroundColor = e.NewElement.BackgroundColor.ToUIColor().CGColor;

            if (!e.NewElement.GestureRecognizers.Any())
                return;

            if (e.NewElement.GestureRecognizers.All(x => x.GetType() != typeof(TouchGestureRecognizer)))
                return;

            // Clicked
            AddGestureRecognizer(new UITapGestureRecognizer(() =>
            {
                foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
                {
                    if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                    {
                        touchGestureRecognizer?.Clicked();
                    }
                }
            }));
        }

        public override void TouchesMoved(NSSet touches, UIEvent evt)
        {
            base.TouchesMoved(touches, evt);

            foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchUp();
                }
            }
        }

        public override void TouchesBegan(NSSet touches, UIEvent evt)
        {
            base.TouchesBegan(touches, evt);

            foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchDown();
                }
            }
        }

        public override void TouchesCancelled(NSSet touches, UIEvent evt)
        {
            base.TouchesCancelled(touches, evt);

            foreach (IGestureRecognizer recognizer in Element.GestureRecognizers.Where(x => x.GetType() == typeof(TouchGestureRecognizer)))
            {
                if (recognizer is TouchGestureRecognizer touchGestureRecognizer)
                {
                    touchGestureRecognizer?.TouchUp();
                }
            }
        }
    }
}