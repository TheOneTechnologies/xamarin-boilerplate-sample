using System;
using System.Diagnostics;
using System.Linq;
using Android.Graphics.Drawables;
using NamingConvention.Droid.Effects;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ResolutionGroupName("NamingConvention")]
[assembly: ExportEffect(typeof(FrameEffect), "FrameShadowEffect")]
namespace NamingConvention.Droid.Effects
{
    public class FrameEffect : PlatformEffect
    {
        protected override void OnAttached()
        {
            try
            {
                var control = Control ?? Container as Android.Views.View;

                var effect = (FrameEffect)Element.Effects.FirstOrDefault(e => e is FrameEffect);

                if (effect != null)
                {
                    control.SetBackgroundResource(Resource.Drawable.FrameRenderValue);
                    GradientDrawable drawable = (GradientDrawable)control.Background;
                    drawable.SetColor(Android.Graphics.Color.ParseColor("#F0F0F0"));
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
