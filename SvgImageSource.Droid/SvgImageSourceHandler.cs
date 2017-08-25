using System.IO;
using System.Threading;
using System.Threading.Tasks;
using Android.Content;
using Android.Graphics;
using NGraphics;
using Xamarin.Forms.Internals;
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
            var svgsource = imagesource as SvgImageSource;
            Bitmap bitmap = null;
            if (svgsource?.Stream != null)
            {
                using (Stream stream = await ((IStreamImageSource)svgsource).GetStreamAsync(cancelationToken).ConfigureAwait(false))
                    bitmap = GetBitmap(stream, svgsource.Width, svgsource.Height, svgsource.Color, context.Resources.DisplayMetrics.Density);
            }

            if (bitmap == null)
            {
                Log.Warning(nameof(SvgImageSourceHandler), "Image data was invalid: {0}", svgsource);
            }

            return bitmap;
        }

        Bitmap GetBitmap(Stream stream, double width, double height, Color color, float scale)
        {
            Graphic g = null;
            using (var sr = new StreamReader(stream))
            {
                g = Graphic.LoadSvg(sr);
            }

            var newSize = SvgUtility.CalcAspect(g.Size, width, height);

            if (width > 0 || height > 0)
            {
                g = SvgUtility.Resize(g, newSize);
            }


            var canvas = Platforms.Current.CreateImageCanvas(newSize, scale);

            if (color != Xamarin.Forms.Color.Default)
            {
                var nColor = new NGraphics.Color(color.R, color.G, color.B, color.A);

                foreach (var element in g.Children)
                {
                    SvgUtility.ApplyColor(element, nColor);
                    element.Draw(canvas);
                }
            }
            else
            {
                g.Draw(canvas);
            }

            return (canvas.GetImage() as BitmapImage)?.Bitmap;
        }

    }
}
