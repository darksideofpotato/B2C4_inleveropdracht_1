using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;

namespace B2C4_inleveropdracht_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class HorsePage : ContentPage
    {
        public IList<Tip> tipsList { get; set; }
        public IList<FullTip> fullTipsList { get; set; }
        public Hobby theHobby;
        public HorsePage(Hobby hobby ,IList<Tip> tip, IList<FullTip> fulltip)
        {
           
            InitializeComponent();
            theHobby = hobby;
            if (tip != null)
            {
                tipsList = tip;
                fullTipsList = fulltip;
                Console.WriteLine("hoi");
            }
            else
            {
                tipsList = new List<Tip>();
                fullTipsList = new List<FullTip>();
                if (hobby.hobbyName == "Horse riding") 
                { 
                    tipsList.Add(new Tip("steering", "A useful tip to get started on steering", "beginner.PNG"));
                    Tip firstTip = tipsList[0];
                    Console.WriteLine(tipsList[0]);
                    fullTipsList.Add(new FullTip(hobby, firstTip, "http://bokt.nl", "xx", "lorum ipsum"));
                }
                else
                {
                    
                }
                        
            }


                BindingContext = this;


            
        }

        private void addNewTip_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTip(theHobby, tipsList, fullTipsList));
        }

        private void listTips_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            Console.WriteLine(listTips.SelectedItem);

            foreach (FullTip x in fullTipsList)
            {
                Console.WriteLine("Deze: " + x.tip.title + " " + listTips.SelectedItem);
                if (listTips.SelectedItem.ToString() == x.tip.title)
                {
                    Console.WriteLine("yay");
                    FullTip tip = x;
                    Navigation.PushAsync(new ShowTip(tip));
                }
            }
            
        }
    }
}