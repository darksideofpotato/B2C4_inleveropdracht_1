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
    public partial class HobbyPage : ContentPage
    {
        public IList<Hobby> hobbyList { get; set; }

        public HobbyPage(IList<Hobby> hobbys)
        {
            InitializeComponent();

            if (hobbys != null)
            {
                hobbyList = hobbys;
            }
            else
            {
                hobbyList = new List<Hobby>();
                hobbyList.Add(new Hobby("Horse riding"));
             
            }


            BindingContext = this;
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            var chosenItem = e.SelectedItem;
            Hobby theItem = (Hobby)chosenItem;

            Console.WriteLine(theItem.hobbyName);

            Navigation.PushAsync(new HorsePage(theItem , null, null));
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void addNewHobby_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddHobby(hobbyList));
        }
    }
}