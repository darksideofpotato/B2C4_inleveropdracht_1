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
        static IList<Hobby> currentHobbies;
        public AddHobby(IList<Hobby> hobbyList)
        {
            InitializeComponent();
            currentHobbies = hobbyList;
        }

        private void btnAddHobby_Clicked(object sender, EventArgs e)
        {
            Hobby newHobby = new Hobby(hobbyName.Text) { hobbyName = hobbyName.Text };

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
            currentHobbies.Add(newHobby);
            Navigation.PushAsync(new HobbyPage(currentHobbies));
        }
    }
}