using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace AnimalSoundboard_Part0
{
    // Here is the class that represents your page. You can add C# code here to add functionality to your app
    // It inherits from a common Page class so that you can focus on the specifics of your page
    public sealed partial class MainPage : Page
    {

        // Constructor for your page, nothing to change here!
        public MainPage()
        {
            this.InitializeComponent();
        }

        // Event listener for you button click!
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.OurTextBlock.Text += " And more text?";
        }
    }
}
