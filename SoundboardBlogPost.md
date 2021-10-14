# Building Your First UWP Demo

## What's the deal with UWP?  

Universal Windows Platform is all about developing apps that are not only for Windows, but look, feel, and interact like Windows. Any device that runs Windows can run a UWP application, and UWP allows for integration and styling that really makes your application feel like it belongs on Windows.

One of my favorite UWP applications is [Ambie](https://github.com/jenius-apps/ambie), an open source, white-noise sound application built by one of my colleagues:  


![](MarkdownImages/Ambie.png)

Ambie is an awesome app, so lets try to imitate it!

## UWP Animal Soundboard

What if instead of soothing atmospheric noises and a level of iterative polish, Ambie had obnoxious animal noises and was built in 30 minutes?

![](MarkdownImages/AnimalSoundboard.png)

You can clone the soundboard source code [here](https://github.com/zateutsch/UWP-Workshop-HackOHIO).

If you're brand new to UWP, this repo has a [Getting Started With UWP Guide](https://github.com/zateutsch/UWP-Workshop-HackOHIO/blob/master/GettingStartedWithUWP.md) and in depth comments on all the source code. Go check it out and make some changes!


## What makes this soundboard tick?



A GridView control that will dynamically display our sounds:
```xml
<GridView x:Name="SoundView"
    ItemsSource="{x:Bind Sounds}"
    ItemTemplate="{StaticResource SoundTemplate}"
    IsItemClickEnabled="True"
    ItemClick="SoundItemClick"
    SelectionMode="Single"/>
```

A DataTemplate that will format our sounds (see `ItemTemplate` property on our GridView):
```xml
<Page.Resources>
    <DataTemplate x:Key="SoundTemplate" x:DataType="local:SoundItem">
        <Border  Background="White" Height="200" Width="200" Margin="10,10,10,10" CornerRadius="30">
            <TextBlock FontSize="50" VerticalAlignment="Center" HorizontalAlignment="Center" Text="{x:Bind PreviewText}"/>
        </Border>
    </DataTemplate>
</Page.Resources>        
```

A SoundItem class to represent each sound (see `x:DataType` property on our DataTemplate):
```csharp
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
```
An ObservableCollection of SoundItems to data bind to (see `ItemsSource` property on our GridView):
```csharp
public ObservableCollection<SoundItem> Sounds { get; set; }
```

A SoundItemClick function for when one of our sounds is selected (see `ItemClick` property on our GridView):
```csharp
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
```

And then two functions called by our click listener (see `SoundItemClick`) to either play an audio file, or synthesize some text to speech:
```csharp
private async void PlaySoundItem(SoundItem sound)
{
    MediaElement mediaElement = new MediaElement();
    Windows.Storage.StorageFolder folder = await Package.Current.InstalledLocation.GetFolderAsync("Assets");
    Windows.Storage.StorageFile file = await folder.GetFileAsync(sound.AudioFilename);
    var stream = await file.OpenAsync(Windows.Storage.FileAccessMode.Read);
    mediaElement.SetSource(stream, file.ContentType);
    mediaElement.Play();
    
}
```
```csharp
private async void ReadTextToSpeech(SoundItem sound)
{
    MediaElement mediaElement = new MediaElement();
    var synth = new Windows.Media.SpeechSynthesis.SpeechSynthesizer();
    synth.Options.SpeakingRate = .7;
    Windows.Media.SpeechSynthesis.SpeechSynthesisStream stream = await synth.SynthesizeTextToStreamAsync(sound.TextToSpeech);
    mediaElement.SetSource(stream, stream.ContentType);
    mediaElement.Play();
}
```

Check out the documentation for MediaElement [here](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.MediaElement?view=winrt-22000) and the documentation for SpeechSynthesizer [here](https://docs.microsoft.com/en-us/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer?view=winrt-22000) if you want to play with these APIs a bit more.
