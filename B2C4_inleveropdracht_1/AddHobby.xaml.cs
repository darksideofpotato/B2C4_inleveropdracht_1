using SQLite;
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
    public partial class AddHobby : ContentPage
    {
        public AddHobby()
        {
            InitializeComponent();
        }

        private void btnAddHobby_Clicked(object sender, EventArgs e)
        {
            // Wanneer op de button geclicked wordt, maakt het programma 
            // een object aan van de class Hobby, die hij vervolgens
            // in de database insert. De gebruiker krijgt een melding op basis 
            // van of dit goed gegaan is of niet.

            string insertedHobbyName = hobbyName.Text;
            Hobby newHobby = new Hobby() { hobbyName = insertedHobbyName};

            using(SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation)){
                connection.CreateTable<Hobby>();
                int rows = connection.Insert(newHobby);

                if (rows > 0)
                {
                    DisplayAlert("Succes", "Hobby succesfully inserted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Hobby failed to insert", "Ok");
                }
            }

            // Na het invoegen wordt de gebruiker teruggeleid naar de hobbypagina.
            Navigation.PushAsync(new HobbyPage());
        }
    }
}