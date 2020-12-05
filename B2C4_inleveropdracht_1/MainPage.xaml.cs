using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace B2C4_inleveropdracht_1
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
        }

        private void beginButton_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new HobbyPage(null));
        }
    }
}
