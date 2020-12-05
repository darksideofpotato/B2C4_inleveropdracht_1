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
    public partial class AddTip : ContentPage
    {
        static Hobby whichHobby2;
        static IList<Tip> currentTips;
        static IList<FullTip> currentFullTips;
        public AddTip(Hobby whichHobby, IList<Tip> _currentTips, IList<FullTip> _currentFullTips)
        {
            InitializeComponent();
            lblWelcome.Text = "Add a tip, info or a link to " + whichHobby.hobbyName + ".";
            currentTips = _currentTips;
            whichHobby2 = whichHobby;
            currentFullTips = _currentFullTips;

        }

        private void btnAddTip_Clicked(object sender, EventArgs e)
        {
            string levelToLink;

            switch (level.SelectedItem)
            {
                case "Beginner":
                    levelToLink = "beginner.PNG";
                    break;
                case "Intermediate":
                    levelToLink = "intermediate.PNG";
                    break;
                case "Expert":
                    levelToLink = "expert.PNG";
                    break;
                default:
                    levelToLink = "error.PNG";
                    break;

            }
            Tip smallTip = new Tip(title.Text, description.Text, levelToLink);
            FullTip fullTip = new FullTip(whichHobby2 ,smallTip, tipLink.Text, "beginner.PNG", tipInfo.Text);
            lblWelcome.Text = fullTip.tip.title.ToString();
            currentTips.Add(smallTip);
            currentFullTips.Add(fullTip);
            Navigation.PushAsync(new HorsePage(whichHobby2 ,currentTips, currentFullTips));
            
        }
    }
}