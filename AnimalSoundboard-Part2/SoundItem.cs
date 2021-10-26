using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
}
