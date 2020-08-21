using Android.Content;
using NamingConvention.Droid.Renderer;
using NamingConvention.Views.CustomViews;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CustomEntry), typeof(CustomEntryRenderer))]
namespace NamingConvention.Droid.Renderer
{
    /// <summary>
    /// change xamarin forms Entry using Entry Renderer(Remove UnderLine)
    /// </summary>
    public class CustomEntryRenderer : EntryRenderer
    {
        #region Constructor
        public CustomEntryRenderer(Context context) : base(context)
        {
        }
        #endregion

        #region Methods
        /// <summary>
        /// Change entry properties here
        /// </summary>
        /// <param name="e"></param>
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (Control != null)
            {
                Control.SetBackgroundColor(global::Android.Graphics.Color.Transparent);
                var layoutParams= new MarginLayoutParams(Control.LayoutParameters);
                layoutParams.SetMargins(0, 0, 0, 0);
                LayoutParameters = layoutParams;
                Control.LayoutParameters = layoutParams;
                Control.TextAlignment = Android.Views.TextAlignment.Gravity;
            }
        }
        #endregion
    }
}
