using System;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;

namespace Xamarin.Forms.Svg
{
    public class SvgImageSource : ImageSource
    {
        internal static float ScreenScale;

        public static BindableProperty StreamFuncProperty =
            BindableProperty.Create(
                nameof(StreamFunc),
                typeof(Func<CancellationToken, Task<Stream>>),
                typeof(SvgImageSource),
                default(Func<CancellationToken, Task<Stream>>),
                defaultBindingMode: BindingMode.OneWay
            );

        public Func<CancellationToken, Task<Stream>> StreamFunc
        {
            get { return (Func<CancellationToken, Task<Stream>>)GetValue(StreamFuncProperty); }
            set { SetValue(StreamFuncProperty, value); }
        }

        public static BindableProperty SourceProperty =
            BindableProperty.Create(
                nameof(Source),
                typeof(string),
                typeof(SvgImageSource),
                default(string),
                defaultBindingMode: BindingMode.OneWay
            );

        public string Source
        {
            get { return (string)GetValue(SourceProperty); }
            set { SetValue(SourceProperty, value); }
        }

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
        public double Width
        {
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
        public double Height
        {
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
        public Color Color
        {
            get { return (Color)GetValue(ColorProperty); }
            set { SetValue(ColorProperty, value); }
        }

        static Assembly AssemblyCache;

        static Lazy<HttpClient> _lazyClient = new Lazy<HttpClient>();
        static HttpClient _httpClient => _lazyClient.Value;

        /// <summary>
        /// Registers the assembly.
        /// </summary>
        /// <param name="typeHavingResource">Type having resource.</param>
        public static void RegisterAssembly(Type typeHavingResource = null)
        {
            if (typeHavingResource == null)
            {
#if NETSTANDARD2_0
                AssemblyCache = Assembly.GetCallingAssembly();
#else
                MethodInfo callingAssemblyMethod = typeof(Assembly).GetTypeInfo().GetDeclaredMethod("GetCallingAssembly");
                if (callingAssemblyMethod != null)
                {
                    AssemblyCache = (Assembly)callingAssemblyMethod.Invoke(null, new object[0]);
                }
#endif
            }
            else
            {
                AssemblyCache = typeHavingResource.GetTypeInfo().Assembly;
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
        public static ImageSource FromResource(string resource, double width, double height, Color color = default(Color))
        {
#if NETSTANDARD2_0
            AssemblyCache = AssemblyCache ?? Assembly.GetCallingAssembly();
#endif
            if (AssemblyCache == null)
            {
                return null;
            }

            return new SvgImageSource { StreamFunc = GetResourceStreamFunc(resource), Source = resource, Width = width, Height = height, Color = color };
        }

        /// <summary>
        /// Froms the svg.
        /// </summary>
        /// <returns>The svg.</returns>
        /// <param name="resource">Resource.</param>
        /// <param name="color">Color.</param>
        public static ImageSource FromResource(string resource, Color color = default(Color))
        {
#if NETSTANDARD2_0
            AssemblyCache = AssemblyCache ?? Assembly.GetCallingAssembly();
#endif
            if (AssemblyCache == null)
            {
                return null;
            }

            return new SvgImageSource { StreamFunc = GetResourceStreamFunc(resource), Source = resource, Color = color };

        }

        public static ImageSource FromUri(string uri, double width, double height, Color color)
        {
            return new SvgImageSource { StreamFunc = GethttpStreamFunc(uri), Source = uri, Width = width, Height = height, Color = color };
        }

        public static ImageSource FromStream(Func<Stream> streamFunc, double width, double height, Color color, string key = null)
        {
            key = key ?? streamFunc.GetHashCode().ToString();
            return new SvgImageSource { StreamFunc = token => Task.Run(streamFunc), Width = width, Height = height, Color = color };
        }

        static Func<CancellationToken, Task<Stream>> GetResourceStreamFunc(string resource)
        {
            var realResource = GetRealResource(resource);
            if (realResource == null)
            {
                return null;
            }
            return token => Task.Run(() => AssemblyCache.GetManifestResourceStream(realResource), token);

        }

        static Func<CancellationToken, Task<Stream>> GethttpStreamFunc(string uri)
        {
            return token => Task.Run(async () =>
            {
                using (var response = await _httpClient.GetAsync(uri, token))
                {
                    if (!response.IsSuccessStatusCode)
                    {
                        Log.Warning("HTTP Request", $"Could not retrieve {uri}, status code {response.StatusCode}");
                        return null;
                    }
                    return await response.Content.ReadAsStreamAsync();
                }
            }, token);

        }

        static string GetRealResource(string resource)
        {
            return AssemblyCache.GetManifestResourceNames()
                              .FirstOrDefault(x => x.EndsWith(resource, StringComparison.CurrentCultureIgnoreCase));
        }

        protected override void OnPropertyChanged(string propertyName)
        {
            if (propertyName == StreamFuncProperty.PropertyName)
                OnSourceChanged();
            base.OnPropertyChanged(propertyName);
        }

        internal virtual async Task<Stream> GetImageStreamAsync(CancellationToken userToken)
        {

            OnLoadingStarted();
            userToken.Register(CancellationTokenSource.Cancel);

            Stream imageStream = null;
            try
            {
                using (var stream = await StreamFunc(CancellationTokenSource.Token).ConfigureAwait(false))
                {
                    if (stream == null)
                    {
                        OnLoadingCompleted(false);
                        return null;
                    }
                    imageStream = await SvgUtility.CreateImage(stream, Width, Height, Color);
                }

                OnLoadingCompleted(false);
            }
            catch (OperationCanceledException oex)
            {
                OnLoadingCompleted(true);
                System.Diagnostics.Debug.WriteLine($"cancel exception {oex.Message}");
                throw;
            }

            return imageStream;
        }
    }
}
