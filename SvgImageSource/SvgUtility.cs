using System;
using NGraphics;

namespace Xamarin.Forms.Svg
{
    /// <summary>
    /// Svg utility.
    /// </summary>
    public static class SvgUtility
    {
        /// <summary>
        /// Calculates the aspect.
        /// </summary>
        /// <returns>The aspect.</returns>
        /// <param name="size">Size.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        public static NGraphics.Size CalcAspect(NGraphics.Size size, double width, double height)
        {
            double w;
            double h;
            if (width <= 0 && height <= 0)
            {
                return size;
            }
            else if (width <= 0)
            {
                h = height;
                w = height * (size.Width / size.Height);
            }
            else if (height <= 0)
            {
                w = width;
                h = width * (size.Height / size.Width);
            }
            else
            {
                w = width;
                h = height;
            }

            return new NGraphics.Size(w, h);
        }

        /// <summary>
        /// Resize the specified g and size.
        /// </summary>
        /// <returns>The resize.</returns>
        /// <param name="g">The green component.</param>
        /// <param name="size">Size.</param>
        public static Graphic Resize(Graphic g, NGraphics.Size size)
        {
            var transform = Transform.AspectFillRect(g.ViewBox, new NGraphics.Rect(0, 0, size.Width, size.Height));
            return g.TransformGeometry(transform);
        }

        /// <summary>
        /// Applies the color.
        /// </summary>
        /// <param name="element">Element.</param>
        /// <param name="color">Color.</param>
        public static void ApplyColor(NGraphics.Element element, NGraphics.Color color)
        {
            var children = (element as Group)?.Children;
            if (children != null)
            {
                foreach (var child in children)
                {
                    ApplyColor(child, color);
                }
            }

            if (element?.Pen != null)
            {
                element.Pen = new Pen(color, element.Pen.Width);
            }

            if (element?.Brush != null)
            {
                element.Brush = new SolidBrush(color);
            }
        }
    }
}
