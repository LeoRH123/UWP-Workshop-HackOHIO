// Here we have an updated Soundboard Project that builds on the last one
// Check out the comments for some of the key changes in the C# code-behind

using System;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using System.Collections.ObjectModel;


namespace AnimalSoundboard_Part2
{

    // This is our custom SoundItem class for storing and modeling the data required to display a sound
    // and fetch the associated audio.
    // Later in the code, an ObservableCollection of these objects is used to represent our SoundBoard.
    public class SoundItem
    {
        public string AudioFilename { get; set; }
        public string PreviewText { get; set; }
        public string TextToSpeech { get; set; }

        public SoundItem(string audio, string prev, string tts)
        {
            this.AudioFilename = audio;
            this.PreviewText = prev;
            this.TextToSpeech = tts;
        }
    }


    // Here is the class that represents your MainPage.
    // Like all Pages, it inherits from the Page class so you can focus on whats unique to your page, rather than UWP pages in general.

    public sealed partial class MainPage : Page
    {   


        // The addition of an ObservableCollection is probably the most important change we made.
        // An observable collection is a collection of objects that is "observable" by our UI.
        // In other words, controls can bind to this collection and listen for changes, updating in real time.
        // By storing our Sound Items in an ObservableCollection, we can create a dynamic set of objects that our UI can update to accomodate.
        public ObservableCollection<SoundItem> Sounds { get; set; }
        public bool IsTextToSpeech { get; set; } = false; // This is just a bool that tells us if we are using Text to Speech mode or not.

        // Constructor for your page.
        // Here we added a couple lines: initializing our ObservableCollection to an empty collection that so we can add to it
        // Calling InitSounds(), which adds some Sound Items to our Collection
        public MainPage()
        {
            this.Sounds = new ObservableCollection<SoundItem>();
            this.InitializeComponent();
            this.InitSounds();
        }

        // This is a new helper function that's pretty straightforward.
        // It creates and adds a starter set of SoundItems to our ObservableCollection
        private void InitSounds()
        {
            SoundItem Cow = new SoundItem("Cow.wav", "🐄", "moo moo");
            SoundItem Chicken = new SoundItem("Chicken.wav", "🐔", "bawk bawk");
            SoundItem Elephant = new SoundItem("Elephant.wav", "🐘", "elephant noise");
            SoundItem Owl = new SoundItem( "Owl.wav", "🦉", "who who");

            this.Sounds.Add(Cow);
            this.Sounds.Add(Chicken);
            this.Sounds.Add(Elephant);
            this.Sounds.Add(Owl);
        }

        // Here is our new main event listener, that checks for a click on one of our Sounds.
        // As you can see, we only have one event listener for all of our sound items, compared to a function for each in the last example.
        // We are abstracting all the info we need out to the SoundItem class, so that our program easily supports adding new sounds to the page.
        // This function checks what playback mode we are in (Text to Speech vs. Not) and calls the proper playback function with the SoundItem as a param.
        private void SoundItemClick(object sender, ItemClickEventArgs e)
        {
            SoundItem currentSoundItem = e.ClickedItem as SoundItem;
            if (this.IsTextToSpeech)
            {
                this.ReadTextToSpeech(currentSoundItem);
            }
            else
            {
                this.PlaySoundItem(currentSoundItem);
            }
        }

        // This is basically identical to our old playback functionality (in the cow and chicken buttons)
        // This function uses the SoundItem.AudioFilename property to retrieve the proper sound for whatever item was clicked. 
        private async void PlaySoundItem(SoundItem sound)
        {
            MediaElement mediaElement = new MediaElement();
            Windows.Storage.StorageFolder folder = await Windows.ApplicationModel.Package.Current.InstalledLocation.GetFolderAsync("Assets");
            Windows.Storage.StorageFile file = await folder.GetFileAsync(sound.AudioFilename);
            var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
            mediaElement.SetSource(stream, file.ContentType);
            mediaElement.Play();
            
        }

        // This uses a handy speech synthesis API to give a robot voice to our sounds!
        // Once again, this is fetching the needed playback data from our SoundItem, specifically from SoundItem.TextToSpeech
        private async void ReadTextToSpeech(SoundItem sound)
        {
            MediaElement mediaElement = new MediaElement();
            var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
            synth.Options.SpeakingRate = .7;
            Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(sound.TextToSpeech);
            mediaElement.SetSource(stream, stream.ContentType);
            mediaElement.Play();
        }

        // Here's the listener for our new toggle button.
        // This toggles a boolean value in our MainPage class that changes our playback behavior depending on its value.
        private void ToggleSwitch_Toggled(object sender, RoutedEventArgs e)
        {
            this.IsTextToSpeech = !this.IsTextToSpeech;
        }
        
        // Monkey button listener!
        // This listener creates a new SoundItem Monkey and adds it to the ObservableCollection.
        // This demonstrates how often times controls can dynamically update in response to a change in an ObservableCollection
        private void Monkey_Button_Click(object sender, RoutedEventArgs e)
        {
            SoundItem Monkey = new SoundItem("Monkey.wav", "🐒", "monke monke");
            this.Sounds.Add(Monkey);
        }
    }


    
 
}
