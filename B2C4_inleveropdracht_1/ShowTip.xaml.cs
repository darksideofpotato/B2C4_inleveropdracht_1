using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B2C4_inleveropdracht_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ShowTip : ContentPage
    {
        public static FullTip theTip;
        public ShowTip(FullTip tip)
        {
            InitializeComponent();
            // De informatie van de tip wordt ingevoerd in verschillende labels
            // en andere sources (zoals de image). Bij de image worden de bytes
            // terug vertaald naar de image. 
            titleHobby.Text = tip.hobbyName;
            titleTip.Text = tip.title;
            levelImage.Source = tip.level;
            uploadedImage.Source = ImageSource.FromStream(() =>
            {
                var ms = new MemoryStream(tip.imageLink);
                return ms;
            });
            tipInfo.Text = tip.tipInfo;
            theTip = tip;

        }

        private void linkTip_Clicked(object sender, EventArgs e)
        {
            // Zorgt dat de ingevoerde link een webbrowser opent met de link
            Uri newLink = new Uri(theTip.link);
            Device.OpenUri(newLink);
        }

        private async void ToolbarItem_Clicked(object sender, EventArgs e)
        {
            // Zorgt dat een tip gedeeld kan worden. De tip wordt gedeeld als tekst,
            // met intro, titel en de inhoud van de tip. 
            await Share.RequestAsync(new ShareTextRequest
            {
                Title = "Share a tip",
                Text = "I just found this cool tip on tip sharing!: " + theTip.title + ": " + theTip.tipInfo
            }) ;
        }
       
    }
}