using System;
using System.Collections.Generic;
using System.Linq;
using Android.Content;
using Android.Views;
using Google.Android.Material.BottomNavigation;
using NamingConvention.Droid.Renderer;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using Xamarin.Forms.Platform.Android.AppCompat;
using View = Android.Views.View;

//[assembly: ExportRenderer(typeof(TabbedPage), typeof(TabbedPageCustomRenderer))]
namespace NamingConvention.Droid.Renderer
{

    /// <summary>
    /// This Renderer is used for if App has more than 4 tabs. It will set propper height and width for all tabs.
    /// </summary>

    public class TabbedPageCustomRenderer : TabbedPageRenderer
    {
        public TabbedPageCustomRenderer(Context context) : base(context)
        {
            //Use this constructor for newest versions of XF saving the context parameter 
            // in a field so it can be used later replacing the Xamarin.Forms.Forms.Context which is deprecated.
        }

        private bool _isShiftModeSet;
        private BottomNavigationView _bottomBar;


        /// <summary>
        /// Override method of tabbed page layout
        /// </summary>
        /// <param name="changed"></param>
        /// <param name="l"></param>
        /// <param name="t"></param>
        /// <param name="r"></param>
        /// <param name="b"></param>
        protected override void OnLayout(bool changed, int l, int t, int r, int b)
        {
            base.OnLayout(changed, l, t, r, b);
            try
            {
                if (!_isShiftModeSet)
                {
                    var children = GetAllChildViews(ViewGroup);

                    if (children.SingleOrDefault(x => x is BottomNavigationView) is BottomNavigationView bottomNav)
                    {
                        bottomNav.SetShiftMode(false, false);
                        _isShiftModeSet = true;

                        int width = r - l;
                        int height = b - t;
                        bottomNav.Measure(MeasureSpecFactory.MakeMeasureSpec(width, MeasureSpecMode.Exactly), MeasureSpecFactory.MakeMeasureSpec(height, MeasureSpecMode.AtMost));
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error setting ShiftMode: {e}");
            }
        }


        /// <summary>
        /// Get All Child tabs views
        /// </summary>
        /// <param name="view"></param>
        /// <returns></returns>
        private List<View> GetAllChildViews(View view)
        {
            if (!(view is ViewGroup group))
            {
                return new List<View> { view };
            }
            var result = new List<View>();
            try
            {
                for (int i = 0; i < group.ChildCount; i++)
                {
                    var child = group.GetChildAt(i);

                    var childList = new List<View> { child };
                    childList.AddRange(GetAllChildViews(child));

                    result.AddRange(childList);
                }
                return result.Distinct().ToList();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return result.Distinct().ToList();
            }
        }
    }


    internal static class MeasureSpecFactory
    {
        public static int GetSize(int measureSpec)
        {
            const int modeMask = 0x3 << 30;
            return measureSpec & ~modeMask;
        }

        internal static int MakeMeasureSpec(int width, MeasureSpecMode exactly)
        {
            return (int)(width + exactly);
        }


    }
}