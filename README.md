# SvgImageSource for Xamarin.Forms

This is a package to add SvgImageSource to Xamarin.Forms as new ImageSource. Thereby Xamarin.Forms.Image will become able to display a SVG image without particular modified.

**Now version is pre release.**  
Using this library have to install more than or equal Xamarin.Forms 2.4.0.266 -pre1.

## Nuget Installation

https://www.nuget.org/packages/Xamarin.Forms.Svg/

```bash
Install-Package Xamarin.Forms.Svg -pre
```

You need to install this nuget package to PCL project and each platform project.

## Preparing to use

### iOS AppDelegate.cs.

```csharp
public override bool FinishedLaunching(UIApplication app, NSDictionary options) 
{    
	global::Xamarin.Forms.Forms.Init();

    Xamarin.Forms.Svg.iOS.SvgImage.Init();  //need to write here

    LoadApplication(new App(new iOSInitializer()));

    return base.FinishedLaunching(app, options);
}
```

### Android MainActivity.cs

```csharp
protected override void OnCreate(Bundle bundle)
{
    ...
    global::Xamarin.Forms.Forms.Init(this, bundle);

    Xamarin.Forms.Svg.Droid.SvgImage.Init(); //need to write here

    LoadApplication(new App(new AndroidInitializer()));
}
```

### PCL 

In case specifying svg's resource with only Xaml, following code has to be written in App.xaml.cs.

```csharp
public App()
{
	InitializeComponent();

	SvgImageSource.RegisterAssembly();
	//SvgImageSource.RegisterAssembly(typeof(typehavingresource)); in case other assembly
}
```

## How to use

```csharp
public ImageSource Image { get; set; } 

public SomeViewModel(){
	//specify resource's path 
	Image = SvgImageSource.FromSvg("Resource.some.svg");
	//specify color
	Image = SvgImageSource.FromSvg("Resource.some.svg", Color.Red);
	//specify width height
	Image = SvgImageSource.FromSvg("Resourece.some.svg", 150, 50)
}
```

```xml
<Image Source="{Binding Image}" />
```
Resource path is found by backward match.


### only Xaml

```xml
<ContentPage xmlns="http://xamarin.com/schemas/2014/forms" 
	xmlns:x="http://schemas.microsoft.com/winfx/2009/xaml" 
	xmlns:svg="clr-namespace:Xamarin.Forms.Svg;assembly=SvgImageSource"
	x:Class="Sample.Views.MainPage" Title="MainPage">
	<StackLayout>
        <Image>
            <Image.Source>
                <svg:SvgImageSource Resource="some.svg" Width=150 Height=50 Color="Red" />
            </Image.Source>
        </Image>
	</StackLayout> 
</ContentPage>
```

## License

MIT Licensed.
