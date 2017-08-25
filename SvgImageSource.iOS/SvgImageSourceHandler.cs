using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using NGraphics;
using UIKit;
using Xamarin.Forms.Internals;
using Xamarin.Forms.Platform.iOS;

namespace Xamarin.Forms.Svg.iOS
{
    /// <summary>
    /// Svg image source handler.
    /// </summary>
    public class SvgImageSourceHandler : IImageSourceHandler
    {
        internal static float ScreenScale;

        /// <summary>
        /// Loads the image async.
        /// </summary>
        /// <returns>The image async.</returns>
        /// <param name="imagesource">Imagesource.</param>
        /// <param name="cancelationToken">Cancelation token.</param>
        /// <param name="scale">Scale.</param>
        public async Task<UIImage> LoadImageAsync(ImageSource imagesource, CancellationToken cancelationToken = default(CancellationToken), float scale = 1)
        {
            UIImage image = null;
            var svgsource = imagesource as SvgImageSource;
            if (svgsource?.Stream != null)
            {
                using (var streamImage = await ((IStreamImageSource)svgsource).GetStreamAsync(cancelationToken).ConfigureAwait(false))
                {
                    if (streamImage != null)
                        image = GetUIImage(streamImage, svgsource.Width, svgsource.Height, svgsource.Color, scale);
                }
            }

            if (image == null)
            {
                Log.Warning(nameof(SvgImageSourceHandler), "Could not load image: {0}", svgsource);
            }

            return image;
        }

        UIImage GetUIImage(Stream stream, double width, double height, Color color, float scale)
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

            if (scale <= 1)
            {
                scale = ScreenScale;
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

            return canvas.GetImage().GetUIImage();
        }

    }
}
