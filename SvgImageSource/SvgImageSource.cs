using System;
using System.Reflection;
using System.Threading.Tasks;
using System.IO;
using System.Linq;

namespace Xamarin.Forms.Svg
{
    /// <summary>
    /// Svg image source.
    /// </summary>
    public class SvgImageSource : StreamImageSource, ISvgImageSource
    {
        /// <summary>
        /// The width property.
        /// </summary>
        public static BindableProperty WidthProperty =
            BindableProperty.Create(
                nameof(Width),
                typeof(double),
                typeof(SvgImageSource),
                default(double),
                defaultBindingMode: BindingMode.OneWay
            );

        /// <summary>
        /// Gets or sets the width.
        /// </summary>
        /// <value>The width.</value>
        public double Width {
            get { return (double)GetValue(WidthProperty); }
            set { SetValue(WidthProperty, value); }
        }

        /// <summary>
        /// The height property.
        /// </summary>
        public static BindableProperty HeightProperty =
            BindableProperty.Create(
                nameof(Height),
                typeof(double),
                typeof(SvgImageSource),
                default(double),
                defaultBindingMode: BindingMode.OneWay
            );

        /// <summary>
        /// Gets or sets the height.
        /// </summary>
        /// <value>The height.</value>
        public double Height {
            get { return (double)GetValue(HeightProperty); }
            set { SetValue(HeightProperty, value); }
        }

        /// <summary>
        /// The color property.
        /// </summary>
        public static BindableProperty ColorProperty =
            BindableProperty.Create(
                nameof(Color),
                typeof(Color),
                typeof(SvgImageSource),
                default(Color),
                defaultBindingMode: BindingMode.OneWay
            );

        /// <summary>
        /// Gets or sets the color.
        /// </summary>
        /// <value>The color.</value>
        public Color Color {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        /// <summary>
        /// The resource property.
        /// </summary>
        public static BindableProperty ResourceProperty =
            BindableProperty.Create(
                nameof(Resource),
                typeof(string),
                typeof(SvgImageSource),
                default(string),
                defaultBindingMode: BindingMode.OneWay,
                propertyChanged: OnResourceChanged
            );

        /// <summary>
        /// Gets or sets the resource.
        /// </summary>
        /// <value>The resource.</value>
        public string Resource {
            get { return (string)GetValue(ResourceProperty); }
            set { SetValue(ResourceProperty, value); }
        }

        static void OnResourceChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (oldValue != newValue)
            {
                if (AssemblyCache == null) return;

                var svgImageSource = bindable as SvgImageSource;
                var resource = GetRealResource((string)newValue);
                Func<Stream> streamFunc = () => AssemblyCache.GetManifestResourceStream(resource);

                svgImageSource.Stream = token => Task.Run(streamFunc, token);
            }
        }

        static Assembly AssemblyCache;

        /// <summary>
        /// Registers the assembly.
        /// </summary>
        /// <param name="typeHavingResource">Type having resource.</param>
        public static void RegisterAssembly(Type typeHavingResource = null)
        {
            if (typeHavingResource == null)
            {
                MethodInfo callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
                if (callingAssemblyMethod != null)
                {
                    AssemblyCache = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                }
            }
        }

        /// <summary>
        /// Froms the svg.
        /// </summary>
        /// <returns>The svg.</returns>
        /// <param name="resource">Resource.</param>
        /// <param name="width">Width.</param>
        /// <param name="height">Height.</param>
        /// <param name="color">Color.</param>
        public static ImageSource FromSvg(string resource, double width, double height, Color color = default(Color))
        {
            if (AssemblyCache == null)
            {
                MethodInfo callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
                if (callingAssemblyMethod != null)
                {
                    AssemblyCache = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                }
                else
                {
                    return null;
                }
            }
            var source = (SvgImageSource)FromSvg(resource, color);

            source.Width = width;
            source.Height = height;

            return source;
        }

        /// <summary>
        /// Froms the svg.
        /// </summary>
        /// <returns>The svg.</returns>
        /// <param name="resource">Resource.</param>
        /// <param name="color">Color.</param>
        public static ImageSource FromSvg(string resource, Color color = default(Color))
        {
            if (AssemblyCache == null)
            {
                MethodInfo callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
                if (callingAssemblyMethod != null)
                {
                    AssemblyCache = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                }
                else
                {
                    return null;
                }
            }

            var realResource = GetRealResource(resource);
            if (realResource == null)
            {
                return null;
            }

            Func<Stream> streamFunc = () => AssemblyCache.GetManifestResourceStream(realResource);

            return new SvgImageSource { Stream = token => Task.Run(streamFunc, token), Color = color };

        }

        static string GetRealResource(string resource)
        {
            return AssemblyCache.GetManifestResourceNames()
                              .FirstOrDefault(x => x.EndsWith(resource, StringComparison.CurrentCultureIgnoreCase));

        }
    }
}
