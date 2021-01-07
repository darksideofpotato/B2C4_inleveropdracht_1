using System;
using System.Collections.Generic;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Plugin.Media;
using System.IO;
using SQLite;

namespace B2C4_inleveropdracht_1
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddTip : ContentPage
    {
        static byte[] imageInBytes;
        static Hobby whichHobby2;
        static IList<Tip> currentTips;
        static IList<FullTip> currentFullTips;
        public AddTip(Hobby whichHobby)
        {
            InitializeComponent();
            lblWelcome.Text = "Add a tip, info or a link to " + whichHobby.hobbyName + ".";
            whichHobby2 = whichHobby;
        }

        private void btnAddTip_Clicked(object sender, EventArgs e)
        {
            string levelToLink;

            switch (level.SelectedItem)
            {
                case "Beginner":
                    levelToLink = "beginner.PNG";
                    break;
                case "Intermediate":
                    levelToLink = "intermediate.PNG";
                    break;
                case "Expert":
                    levelToLink = "expert.PNG";
                    break;
                default:
                    levelToLink = "error.PNG";
                    break;

            }
            FullTip fullTip = new FullTip() 
            { 
                hobbyName = whichHobby2.hobbyName, title = title.Text, 
                description = description.Text, level = levelToLink, 
                tipInfo = tipInfo.Text, link = tipLink.Text, imageLink = imageInBytes
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                connection.CreateTable<FullTip>();
                int rows = connection.Insert(fullTip);

                if (rows > 0)
                {
                    DisplayAlert("Succes", "Tip succesfully inserted", "Ok");
                }
                else
                {
                    DisplayAlert("Failure", "Tip failed to insert", "Ok");
                }
            }

            Navigation.PushAsync(new HorsePage(whichHobby2));
            
        }

        private async void imageUploader_Clicked(object sender, EventArgs e)
        {
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No upload", "Picking a photo is not supported", "ok");
                return;
            }

            var file = await CrossMedia.Current.PickPhotoAsync();
            if(file == null)
            {
                return;
            }

            using(var memoryStream = new MemoryStream())
            {
                file.GetStream().CopyTo(memoryStream);
                file.Dispose();
                imageInBytes = memoryStream.ToArray();
            }
            imageToUpload.Text = imageInBytes.ToString();
        }

    }
}