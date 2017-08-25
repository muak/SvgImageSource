using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Xamarin.Forms;
using Xamarin.Forms.Svg;

namespace Sample.ViewModels
{
    public class MainPageViewModel : BindableBase, INavigationAware
    {

        private ImageSource _Image;
        public ImageSource Image {
            get { return _Image; }
            set { SetProperty(ref _Image, value); }
        }

        bool toggle = true;
        private DelegateCommand _ChangeCommand;
        public DelegateCommand ChangeCommand {
            get {
                return _ChangeCommand = _ChangeCommand ?? new DelegateCommand(() => {
                    if (toggle)
                    {
                        Image = SvgImageSource.FromSvg("Resource.colours.svg", 50, 50);
                    }
                    else
                    {
                        Image = SvgImageSource.FromSvg("Resource.tiger.svg");
                    }

                    toggle = !toggle;
                });
            }
        }

        public MainPageViewModel()
        {

            //Image = SvgImageSource.FromSvg("Resource.tiger.svg",50,50);
            Image = SvgImageSource.FromSvg("Resource.tiger.svg");
        }

        public void OnNavigatedFrom(NavigationParameters parameters)
        {

        }

        public void OnNavigatedTo(NavigationParameters parameters)
        {

        }

        public void OnNavigatingTo(NavigationParameters parameters)
        {

        }
    }
}

