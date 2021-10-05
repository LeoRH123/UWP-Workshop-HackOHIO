// This is the C# code file asssociated with your XAML file.
// Simple logic can be added to this file and bound to your XAML file to add functionality to your app.
// Together, these two files are the core of any page you create for a simple UWP app.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimalSoundboard_Part1
{
    
    // Here is the class that represents your MainPage.
    // Like all Pages, it inherits from the Page class so you can focus on whats unique to your page, rather than UWP pages in general.

    public sealed partial class MainPage : Page
    {   

        // Constructor for your page.
        public MainPage()
        {
            this.InitializeComponent();
        }

        // Here is the function that your Cow Button binds to.
        // It reads a .wav file into a stream and sets that stream as a MediaElement source to be played.
        private async void Cow_Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mysong = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Cow.wav");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mysong.SetSource(stream, file.ContentType);
            mysong.Play();
        }

        // Same functionality as above but for Chicken Button
        private async void Chicken_Button_Click(object sender, RoutedEventArgs e)
        {
            MediaElement mysong = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync("Chicken.wav");
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mysong.SetSource(stream, file.ContentType);
            mysong.Play();
        }
    }


    
}
