using System.Threading;
using System.Threading.Tasks;
using Foundation;
using UIKit;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;
using System.Collections.Generic;
using System.Collections.Concurrent;
using System;

namespace Xamarin.Forms.Svg.iOS
{
    /// <summary>
    /// Svg image source handler.
    /// </summary>
    public class SvgImageSourceHandler : IImageSourceHandler
    {
        /// <summary>
        /// Loads the image async.
        /// </summary>
        /// <returns>The image async.</returns>
        /// <param name="imagesource">Imagesource.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        /// <param name="scale">Scale.</param>
        public async Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1)
        {
            var svgImageSource = imagesource as SvgImageSource;

            using (var stream = await svgImageSource.GetImageStreamAsync(cancelationToken).ConfigureAwait(false))
            {
                if (stream == null)
                {
                    return null;
                }
                return UIImage.LoadFromData(NSData.FromStream(stream), SvgImageSource.ScreenScale);
            }
        }

    }
}
