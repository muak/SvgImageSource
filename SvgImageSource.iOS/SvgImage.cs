using System;
using UIKit;

namespace Xamarin.Forms.Svg.iOS
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

            // gets screen's scale here. can't use MainScreen.Scale in LoadImageAsync because of not being main thread.
            SvgImageSourceHandler.ScreenScale = (float)UIScreen.MainScreen.Scale;
        }
    }
}
