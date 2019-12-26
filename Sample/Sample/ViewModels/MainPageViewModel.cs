using System;
using System.IO;
using System.Linq;
using Prism.Mvvm;
using Reactive.Bindings;
using Xamarin.Forms;
using Xamarin.Forms.Svg;

namespace Sample.ViewModels
{
    public class MainPageViewModel : BindableBase
    {
        public ReactiveProperty<ImageSource> Image { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image2 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image3 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image4 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image5 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image6 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image7 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image8 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image9 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveProperty<ImageSource> Image10 { get; } = new ReactiveProperty<ImageSource>();
        public ReactiveCommand ChangeCommand { get; } = new ReactiveCommand();
        public ReactiveProperty<string> ImagePath { get; } = new ReactiveProperty<string>();

        int toggle = 0;

        public MainPageViewModel()
        {
            ImagePath.Value = "ios-star.svg";
            //var asm = this.GetType().Assembly;
            //var path = asm.GetManifestResourceNames()
            //              .FirstOrDefault(x => x.EndsWith("flower.jpg", StringComparison.CurrentCultureIgnoreCase));
            //Func<Stream> stream = () => asm.GetManifestResourceStream(path);

            //var svgPath = asm.GetManifestResourceNames()
            //              .FirstOrDefault(x => x.EndsWith("tiger.svg", StringComparison.CurrentCultureIgnoreCase));
            //Func<Stream> svgStream = () => asm.GetManifestResourceStream(svgPath);

            //Image = SvgImageSource.FromSvg("Resource.tiger.svg",50,50);
            //Image.Value = SvgImageSource.FromSvg("ios-star.svg",50,50);

            ChangeCommand.Subscribe(_ =>
            {
                if (toggle == 0)
                {
                    ImagePath.Value = "colours.svg";
                    toggle++;
                }
                else{
                    ImagePath.Value = "ios-star.svg";
                    toggle--;
                }

                //switch(toggle){
                //    case 0:
                //        Image.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image2.Value = SvgImageSource.FromSvg("tiger.svg");
                //        Image3.Value = SvgImageSource.FromSvg("colours.svg");
                //        Image4.Value = SvgImageSource.FromSvg("colours.svg",120,120,Color.Green);
                //        Image5.Value = SvgImageSource.FromSvg("tiger.svg",50,50);
                //        Image6.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image7.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image8.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image9.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image10.Value = SvgImageSource.FromSvg("ios-star.svg");

                //        //Image.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image2.Value = SvgImageSource.FromResource("ios-star.svg",50,50,Color.Red);
                //        //Image3.Value = SvgImageSource.FromStream(svgStream,100,100,Color.Blue);
                //        //Image4.Value = SvgImageSource.FromUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg",120,120,Color.Green);
                //        //Image5.Value = SvgImageSource.FromResource("tiger.svg",50,50);
                //        //Image6.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image7.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image8.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image9.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image10.Value = SvgImageSource.FromResource("ios-star.svg");
                //        //Image.Value = CachedImageSource.FromSvg("colours.svg",100,100,Color.Blue);
                //        //Image2.Value = CachedImageSource.FromSvg("colours.svg", 50, 50, Color.Red);
                //        break;
                //    case 1:
                //        Image.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image2.Value = SvgImageSource.FromSvg("tiger.svg");
                //        Image3.Value = SvgImageSource.FromSvg("colours.svg");
                //        Image4.Value = SvgImageSource.FromSvg("colours.svg", 120, 120, Color.Green);
                //        Image5.Value = SvgImageSource.FromSvg("tiger.svg", 50, 50);
                //        Image6.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image7.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image8.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image9.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        Image10.Value = SvgImageSource.FromSvg("ios-star.svg");
                //        toggle = -1;
                //        break;
                //    case 2:
                        
                //        break;
                //    case 3:
                //        //https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg
                //        //Image.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg",50,50, Color.FromHex("#80FFFF00"));
                //        //Image2.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 100, 100, Color.DarkOrange);
                //        //Image3.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg",50,50, Color.FromHex("#80FFFF00"));
                //        //Image4.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 100, 100, Color.DarkOrange);
                //        //Image5.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg",50,50, Color.FromHex("#80FFFF00"));
                //        //Image6.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 100, 100, Color.DarkOrange);
                //        //Image7.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg",50,50, Color.FromHex("#80FFFF00"));
                //        //Image8.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 100, 100, Color.DarkOrange);
                //        //Image9.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 50,50,Color.FromHex("#80FFFF00"));
                //        //Image10.Value = SvgImageSource.FromSvgUri("https://dl.dropbox.com/s/d2ijcomn07tfy56/adjust.svg", 100, 100, Color.DarkOrange);
                //        break;
                //    case 4:
                //        //Image.Value = SvgImageSource.FromStream(stream);
                //        //Image2.Value = SvgImageSource.FromStream(stream);
                //        //Image3.Value = SvgImageSource.FromStream(stream);
                //        //Image4.Value = SvgImageSource.FromStream(stream);
                //        //Image5.Value = SvgImageSource.FromStream(stream);
                //        //Image6.Value = SvgImageSource.FromStream(stream);
                //        //Image7.Value = SvgImageSource.FromStream(stream);
                //        //Image8.Value = SvgImageSource.FromStream(stream);
                //        //Image9.Value = SvgImageSource.FromStream(stream);
                //        //Image10.Value = SvgImageSource.FromStream(stream);
                //        break;
                //    case 5:
                //        //https://dl.dropbox.com/s/khdn0tpgjngdw4r/tiger.svg
                //        //Image.Value = SvgImageSource.FromSvgStream(svgStream,200,200,Color.Default);
                //        //Image2.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image3.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image4.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image5.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image6.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Default);
                //        //Image7.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image8.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image9.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Green);
                //        //Image10.Value = SvgImageSource.FromSvgStream(svgStream,50,50,Color.Red);
                //        break;
                //    case 6:
                //        //Image.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image2.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image3.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image4.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image5.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image6.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image7.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image8.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image9.Value = SvgImageSource.FromNativeFile("icon.png");
                //        //Image10.Value = SvgImageSource.FromNativeFile("icon.png");
                //        toggle = -1;
                //        break;
                //}

                //toggle++;

            });

            //ChangeCommand.Execute();
        }

    }
}