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
            Hobby newHobby = new Hobby(hobbyName.Text);
            currentHobbies.Add(newHobby);
            Navigation.PushAsync(new HobbyPage(currentHobbies));
        }
    }
}