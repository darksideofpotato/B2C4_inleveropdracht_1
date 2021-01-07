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
                connection.CreateTable<Hobby>();
                var hobbies = connection.Table<Hobby>().ToList();
                listHobbies.ItemsSource = hobbies;


            }
        }

        private void ListView_ItemSelected(object sender, SelectedItemChangedEventArgs e)
        {
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
            Navigation.PushAsync(new AddHobby());
        }
    }
}