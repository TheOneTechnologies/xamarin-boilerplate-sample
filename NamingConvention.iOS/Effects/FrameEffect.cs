using System;
using System.Diagnostics;
using System.Linq;
using CoreGraphics;
using NamingConvention.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ResolutionGroupName("NamingConvention")]
[assembly: ExportEffect(typeof(FrameEffect), "FrameShadowEffect")]
namespace NamingConvention.iOS.Effects
{
    public class FrameEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var effect = (FrameEffect)Element.Effects.FirstOrDefault(e => e is FrameEffect);

                if (effect != null)
                {
                    Container.Layer.CornerRadius = 10;
                    Container.Layer.MasksToBounds = false;
                    Container.Layer.ShadowOffset = new CGSize(-2, 2);
                    Container.Layer.ShadowRadius = 5;
                    Container.Layer.ShadowOpacity = 0.4f;
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine("Cannot set property on attached control. Error: {0}", ex.Message);
            }
        }

        protected override void OnDetached()
        {
        }
    }

}