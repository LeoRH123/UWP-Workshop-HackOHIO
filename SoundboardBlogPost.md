# Building Your First UWP Application

## What's the deal with UWP?  

Universal Windows Platform is all about developing apps that are not only for Windows, but look, feel, and interact like they *should* be on Windows. Any device that runs Windows can run a UWP application, and UWP allows for integration and styling that really makes your application feel like it belongs on Windows.

One of my favorite UWP applications is [Ambie](https://github.com/jenius-apps/ambie), an open source, white-noise sound application built by one of my colleagues, [Daniel Paulino](https://twitter.com/kid_jenius):  


![](MarkdownImages/Ambie.png)

Ambie is an awesome app, so lets try to imitate it!

## UWP Animal Soundboard

What if instead of soothing atmospheric noises and a level of iterative polish, Ambie had obnoxious animal noises and was built in 30 minutes?

![](MarkdownImages/AnimalSoundboard.png)

As simple as this demo app is, it shows some cool things you can do right away with UWP:
- Working with different types of media (in our case, audio, but its just as easy to get started with video, photo, etc.)
- Speech Synthesis! Just one example of a neat API included with UWP
- Simple, easy-to-use, and consistent UI controls, especially if you're using [WinUI](https://docs.microsoft.com/en-us/windows/apps/winui/winui2/getting-started)
- Templated/dynamic UI that changes through user interaction. You can add as many monkey buttons as you want to this UI, just keep clicking!

### Resources

You can clone the soundboard source code [here](https://aka.ms/win-dev/student/osu/uwp/sample).

If you're brand new to UWP, this repo has a [Getting Started With UWP Guide](https://aka.ms/https://aka.ms/win-dev/student/osu/uwp/sample/getting-started-uwp) and in depth comments on all the source code. Go check it out and make some changes!


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
GridView controls have an `ItemsSource` property that allows us to send it a whole collection of items to display. Our GridView's `ItemSource` is bound to an `ObservableCollection` that's declared in our code-behind:
```csharp
public ObservableCollection<SoundItem> Sounds { get; set; }
```
`ObservableCollections` are "observable" by the UI, that is, if the collection changes, so will the UI. Awesome! Our `ObservableCollection` is a collection of `SoundItems`:
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
These `SoundItem` objects store the data that will be consumed by our GridView to display our sound items. But wait, how does it know *how* to display them? That's where we provide a `DataTemplate` to our GridViews `ItemTemplate` property:
```xml
<Page.Resources>
    <DataTemplate x:Key="SoundTemplate" x:DataType="local:SoundItem">
        <Border  Background="White" Height="200" Width="200" Margin="10,10,10,10" CornerRadius="30">
            <TextBlock FontSize="50" VerticalAlignment="Center" HorizontalAlignment="Center" Text="{x:Bind PreviewText}"/>
        </Border>
    </DataTemplate>
</Page.Resources>        
```
Great, we have the look of our soundboard, but now we need some functionality. Let's add a click listener function to our GridView's `ItemClick` property:
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

Our click listener calls two different functions depending on whether or not Text-To-Speech mode is enabled. The first plays audio from a file:
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
While the second uses the `SpeechSynthesizer` API to play some text-to-speech audio:
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
Sweet! That's the core functionality of our soundboard. You can check out the rest of the code that makes it all come together on the github repository.

Check out the documentation for MediaElement [here](https://docs.microsoft.com/en-us/uwp/api/Windows.UI.Xaml.Controls.MediaElement?view=winrt-22000) and the documentation for SpeechSynthesizer [here](https://docs.microsoft.com/en-us/uwp/api/Windows.Media.SpeechSynthesis.SpeechSynthesizer?view=winrt-22000) if you want to play with these APIs a bit more.

## Next Steps

Get out there and start building your own Windows apps!

Here are some links to check out with resources on building for UWP:
- [Official UWP Getting Started Documentation](https://docs.microsoft.com/en-us/windows/uwp/get-started/)
- [Getting Started with UWP From Scratch](https://docs.microsoft.com/en-us/windows/apps/winui/winui2/getting-started)
- [Windows Development for Absolute Beginners](https://channel9.msdn.com/Series/Windows-10-development-for-absolute-beginners)
- [Ambie Source Code](https://github.com/jenius-apps/ambie)

Thanks for tuning in! Feel free to reach out on my socials linked above if you have any questions.
