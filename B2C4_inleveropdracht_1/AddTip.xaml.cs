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
        // Static variabelen die de huidige hobby en de imagebytes bijhouden.
        // images worden omgezet in bytes om in de database op te slaan.
        // Dit was makkelijker dan een string gebruiken.
        static byte[] imageInBytes;
        static Hobby whichHobby2;
        public AddTip(Hobby whichHobby)
        {
            InitializeComponent();
            whichHobby2 = whichHobby;
            // Past de welkomsttekst aan op basis van de hobby waar de tip bij hoort
            lblWelcome.Text = "Add a tip, info or a link to " + whichHobby2.hobbyName + ".";
            
        }

        private void btnAddTip_Clicked(object sender, EventArgs e)
        {
            string levelToLink;
            // De afbeeldingen worden opgeslagen op basis van het niveau dat gekozen is
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
            // Een object van FullTip wordt aangemaakt met de ingevoerde gegevens
            FullTip fullTip = new FullTip() 
            { 
                hobbyName = whichHobby2.hobbyName, title = title.Text, 
                description = description.Text, level = levelToLink, 
                tipInfo = tipInfo.Text, link = tipLink.Text, imageLink = imageInBytes
            };

            using (SQLiteConnection connection = new SQLiteConnection(App.DatabaseLocation))
            {
                // Het object van fulltip wordt ingevoerd in de database, met een melding 
                // en redirection.
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

        // Een functie die het uploaden van een image afhandeld
        private async void imageUploader_Clicked(object sender, EventArgs e)
        {
            // Kijkt of er een juiste file is geselecteerd en geeft een foutmelding
            // als dit niet zo is
            if (!CrossMedia.Current.IsPickPhotoSupported)
            {
                await DisplayAlert("No upload", "Picking a photo is not supported", "ok");
                return;
            }

            // Als er geen foto is gekozen, stopt de functie
            var file = await CrossMedia.Current.PickPhotoAsync();
            if(file == null)
            {
                return;
            }

            // Deimage wordt omgezet in een array van bytes, om op te slaan in de 
            // database. 
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