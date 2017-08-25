using System;

namespace Xamarin.Forms.Svg
{
    /// <summary>
    /// Svg image source.
    /// </summary>
    public interface ISvgImageSource
    {
        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        double Width { get; set; }
        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        double Height { get; set; }
        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        Color Color { get; set; }
    }
}
