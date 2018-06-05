using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using Xamarin.Forms.Platform.Android;

namespace Xamarin.Forms.Svg.Droid
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
        /// <param name="context">Context.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        public async Task<Bitmap> LoadImageAsync(ImageSource imagesource, Context context, CancellationToken cancelationToken = default(CancellationToken))
        {
            var svgImageSource = imagesource as SvgImageSource;

            using (var stream = await svgImageSource.GetImageStreamAsync(cancelationToken).ConfigureAwait(false))
            {
                if (stream == null)
                {
                    return null;
                }
                return await BitmapFactory.DecodeStreamAsync(stream);
            }
        }
    }
}
