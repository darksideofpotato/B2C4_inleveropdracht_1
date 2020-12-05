using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace B2C4_inleveropdracht_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    
    public partial class ShowTip : ContentPage
    {
        public static FullTip Thetip;
        public ShowTip(FullTip tip)
        {
            InitializeComponent();

            titleHobby.Text = tip.hobby.hobbyName;
            titleTip.Text = tip.tip.title;
            levelImage.Source = tip.tip.level;
            tipInfo.Text = tip.tipInfo;
            Thetip = tip;

        }

        private void linkTip_Clicked(object sender, EventArgs e)
        {
            Console.WriteLine(Thetip.link);
            Uri newLink = new Uri(Thetip.link);
            Device.OpenUri(newLink);
        }
    }
}