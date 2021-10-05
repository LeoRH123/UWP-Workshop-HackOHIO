// This is the C# code file asssociated with your XAML file.
// Simple logic can be added to this file and bound to your XAML file to add functionality to your app.
// Together, these two files are the core of any page you create for a simple UWP app.

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimalSoundboard_Part2
{
    
    // Here is the class that represents your MainPage.
    // Like all Pages, it inherits from the Page class so you can focus on whats unique to your page, rather than UWP pages in general.

    public sealed partial class MainPage : Page
    {   

        public ObservableCollection<SoundItem> Sounds { get; set; }

        // Constructor for your page.
        public MainPage()
        {
            this.Sounds = new ObservableCollection<SoundItem>();
            this.InitializeComponent();
            this.InitSounds();
        }

        private void InitSounds()
        {
            SoundItem Cow = new SoundItem("Assets/Cow.jpg", "Cow.wav", "🐄");
            SoundItem Chicken = new SoundItem("Assets/Chicken.jpg", "Chicken.wav", "🐔");
            SoundItem Elephant = new SoundItem("Assets/Elephant.jpg", "Elephant.wav", "🐘");
            SoundItem Owl = new SoundItem("Assets/Owl.jpg", "Owl.wav", "🦉");

            this.Sounds.Add(Cow);
            this.Sounds.Add(Chicken);
            this.Sounds.Add(Elephant);
            this.Sounds.Add(Owl);
        }

        private async void SoundItemClick(object sender, ItemClickEventArgs e)
        {
            SoundItem clickedItem = (e.ClickedItem as SoundItem);
            MediaElement mysong = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(clickedItem.AudioFilename);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mysong.SetSource(stream, file.ContentType);
            mysong.Play();
        }

    }


    public class SoundItem
    {
        public string ImageFilename { get; set; }
        public string AudioFilename { get; set; }
        public string PreviewText { get; set; }

        public SoundItem(string img, string audio, string prev)
        {
            this.ImageFilename = img;
            this.AudioFilename = audio;
            this.PreviewText = prev;
        }
    }


    
}
