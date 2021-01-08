using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using System.Data;
using SQLite;
using System.Diagnostics;


namespace B2C4_inleveropdracht_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    // De naam horsepage is een foutje, omdat ik dacht dat ik voor elke 
    // hobby een nieuwe pagina aan moest maken (Deze zou dan voor paardrijden zijn).
    // Het proberen te wijzigen van deze naam zorgde voor veel gezeur van visual studio,
    // dus ik heb besloten dat het te insignificant was om te wijzigen.
    public partial class HorsePage : ContentPage
    {
        static List<FullTip> tips;
        public Hobby theHobby;
        public HorsePage(Hobby hobby)
        {
            InitializeComponent();
            theHobby = hobby;
            changeHobbyTitle.Text = theHobby.hobbyName;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            // vult de listview met de tips van de gekozen hobby uit de database
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                tips = new List<FullTip>();
                connection.CreateTable<FullTip>();
                var fullTip = connection.Table<FullTip>().ToList();
                var sqlQueryEnumerable = connection.CreateCommand("select * from FullTip where hobbyName = '" + theHobby.hobbyName + "'").ExecuteDeferredQuery<FullTip>();

                // vult de bovenaan gedeclarede lijst tips. Deze stap was nodig 
                // omdat er gewerkt moest worden met een custom SQL statement,
                // wat bij de lijst van hobbies niet het geval was.
                // De itemsource wordt vervolgens naar deze lijst gezet.
                foreach (var row in sqlQueryEnumerable)
                {
                    Debug.WriteLine(row.title);
                    tips.Add(row);
                }

                listTips.ItemsSource = tips;

            }
        }

        private void addNewTip_Clicked(object sender, EventArgs e)
        {
            Navigation.PushAsync(new AddTip(theHobby));
        }

        private void listTips_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
            // De gekozen tip wordt gezocht middels een object van Fulltip,
            // en geopend
            FullTip selectedTip = (FullTip)listTips.SelectedItem;

            foreach (FullTip tip in tips)
            {
                if (selectedTip.tipId == tip.tipId)
                {
                    Navigation.PushAsync(new ShowTip(tip));
                }
            }

        }

        private void deleteHobby_Clicked(object sender, EventArgs e)
        {
            // een aangeklikte hobby wordt verwijderd
            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Hobby>();
                int rows = conn.Delete(theHobby);

                if (rows > 0)
                {
                    App.Current.MainPage.DisplayAlert("Succes", "deleted", "Ok");
                    Navigation.PushAsync(new HobbyPage());
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Failure", "failed", "Ok");
                }
            }
        }

        private void updateHobby_Clicked(object sender, EventArgs e)
        {
            // De hobby wordt geupdate met de tekst dat op dat moment in het veld staat
            theHobby.hobbyName = changeHobbyTitle.Text;

            using (SQLiteConnection conn = new SQLiteConnection(App.DatabaseLocation))
            {
                conn.CreateTable<Hobby>();
                int rows = conn.Update(theHobby);

                if (rows > 0)
                {
                    App.Current.MainPage.DisplayAlert("Succes", "updated", "Ok");
                    Navigation.PushAsync(new HobbyPage());
                }
                else
                {
                    App.Current.MainPage.DisplayAlert("Failure", "failed", "Ok");
                }
            }
        }

        private void searchForTips_TextChanged(object sender, TextChangedEventArgs e)
        { // De functie die de zoekbalk afhandeld. 
            // Wanneer de zoekbalk leeg is, worden alle tips weergeven.
            // Is er een of meerdere letters ingevoerd? Kan wordt gezocht welke
            // tips in het systeem staan die die reeks aan letters bevatten,
            // en weergeeft een lijst met de resultaten.
            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                tips = new List<FullTip>();
                connection.CreateTable<FullTip>();
                var fullTip = connection.Table<FullTip>().ToList();
                var sqlQueryEnumerable = connection.CreateCommand("select * from FullTip where hobbyName = '" + theHobby.hobbyName + "'").ExecuteDeferredQuery<FullTip>();


                foreach (var row in sqlQueryEnumerable)
                {
                    tips.Add(row);
                }

                listTips.ItemsSource = tips;

                listTips.BeginRefresh();

                if (string.IsNullOrWhiteSpace(e.NewTextValue))
                    listTips.ItemsSource = tips;
                else
                    listTips.ItemsSource = tips.Where(name => name.title.ToLower().Contains(e.NewTextValue.ToLower()));

                listTips.EndRefresh();
            }
        }
    }
}