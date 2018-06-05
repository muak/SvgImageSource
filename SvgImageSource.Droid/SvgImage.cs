using System;
using Android.Content;

namespace Xamarin.Forms.Svg.Droid
{
    /// <summary>
    /// Svg image.
    /// </summary>
    public static class SvgImage
    {
        /// <summary>
        /// Init this instance.
        /// </summary>
        public static void Init(Context context)
        {
            Internals.Registrar.Registered.Register(typeof(SvgImageSource), typeof(SvgImageSourceHandler));

            using (var display = context.Resources.DisplayMetrics)
            {
                SvgImageSource.ScreenScale = display.Density;
            }
        }
    }
}
