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
            Navigation.PushAsync(new HobbyPage());
        }
    }
}