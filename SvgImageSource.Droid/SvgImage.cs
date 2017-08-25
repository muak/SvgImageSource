using System;

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
        public static void Init()
        {
            Internals.Registrar.Registered.Register(typeof(SvgImageSource), typeof(SvgImageSourceHandler));
        }
    }
}
