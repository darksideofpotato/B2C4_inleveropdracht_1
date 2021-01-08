using SQLite;
using System;
using System.Collections.Generic;
using System.Diagnostics;
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
        public HobbyPage()
        {
            InitializeComponent();
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                // De lijst met hobby's wordt gevuld met de data uit de database
                connection.CreateTable<Hobby>();
                var hobbies = connection.Table<Hobby>().ToList();
                listHobbies.ItemsSource = hobbies;
            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            //Wanneer een hobby wordt geselecteerd, wordt de gebruiker geleid 
            // naar de tips van deze hobby. De hobby wordt meegenomen als parameter
            // zodat bijgehouden kan worden op welke pagina de gebruiker zich bevind. 
            var chosenItem = e.SelectedItem;
            Hobby theItem = (Hobby)chosenItem;

            Console.WriteLine(theItem.hobbyName);

            Navigation.PushAsync(new HorsePage(theItem));
            
        }

        private void ListView_ItemTapped(object sender, ItemTappedEventArgs e)
        {

        }

        private void addNewHobby_Clicked(object sender, EventArgs e)
        {
            // De pagina waarin een nieuwe hobby kan worden toegevoegd.
            Navigation.PushAsync(new AddHobby());
        }

        private void deleteHobby_Clicked(object sender, EventArgs e)
        {

        }

        private void searchHobby_TextChanged(object sender, TextChangedEventArgs e)
        {
            // De functie die de zoekbalk afhandeld. 
            // Wanneer de zoekbalk leeg is, worden alle hobby's weergeven.
            // Is er een of meerdere letters ingevoerd? Kan wordt gezocht welke
            // hobby's in het systeem staan die die reeks aan letters bevatten,
            // en weergeeft een lijst met de resultaten.

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<Hobby>();
                var hobbies = connection.Table<Hobby>().ToList();
                listHobbies.ItemsSource = hobbies;

                listHobbies.BeginRefresh();

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    listHobbies.ItemsSource = hobbies;
                else
                    listHobbies.ItemsSource = hobbies.Where(name => name.hobbyName.ToLower().Contains(e.NewTextValue.ToLower()));
                Debug.WriteLine(hobbies[0].hobbyName);

                listHobbies.EndRefresh();
            }
            
        }


    }
}