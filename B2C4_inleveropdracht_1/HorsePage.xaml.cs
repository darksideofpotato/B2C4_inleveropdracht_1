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
    public partial class HorsePage : ContentPage
    {
        static List<FullTip> tips;

        public Hobby theHobby;
        public HorsePage(Hobby hobby)
        {       
            InitializeComponent();
            theHobby = hobby;       
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                tips = new List<FullTip>();
                connection.CreateTable<FullTip>();
                var fullTip = connection.Table<FullTip>().ToList();
                var sqlQueryEnumerable = connection.CreateCommand("select * from FullTip where hobbyName = '" + theHobby.hobbyName + "'").ExecuteDeferredQuery<FullTip>();


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
            Console.WriteLine(listTips.SelectedItem);
            FullTip selectedTip = (FullTip)listTips.SelectedItem;

            foreach (FullTip tip in tips)
            {
                Console.WriteLine("Deze: " + tip.tipId + " " + selectedTip.tipId);
                if (selectedTip.tipId == tip.tipId)
                {
                    Navigation.PushAsync(new ShowTip(tip));
                }
            }
            
        }
    }
}