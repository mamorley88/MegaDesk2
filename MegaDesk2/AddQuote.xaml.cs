using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using MegaDesk2;
using Newtonsoft.Json;
using Windows.Storage;
using System.Text;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace MegaDesk2
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AddQuote : Page
    {
        object parent = null;
        List<Desk.DesktopMaterial> materials;
        List<DeskQuote.RUSH> rushOptions;
        List<DeskQuote> quotes;
        DeskQuote deskQuote;



        public AddQuote()
        {
            this.InitializeComponent();
            materials = new List<Desk.DesktopMaterial>()
            {
                Desk.DesktopMaterial.LAMINATE,
                Desk.DesktopMaterial.OAK,
                Desk.DesktopMaterial.PINE,
                Desk.DesktopMaterial.ROSEWOOD,
                Desk.DesktopMaterial.VENEER
            };
            


            rushOptions = new List<DeskQuote.RUSH>()
            {
                DeskQuote.RUSH.NO_RUSH,
                DeskQuote.RUSH.THREE,
                DeskQuote.RUSH.FIVE,
                DeskQuote.RUSH.SEVEN

            };
           
        }

        private void saveText_Click(object sender, RoutedEventArgs e)
        {
             this.DeserializeQuotes();

           this.SerializeQuotes(this.quotes.ToArray());
           // DisplayQuote dq = new DisplayQuote();
            //dq.Initialize(deskQuote);
            //dq.ShowDialog();
        }
        
        private void DeserializeQuotes()
        {
            var quotesList = new List<DeskQuote>();


            string path = ".\\quotes.json";

            if (!File.Exists(path))
            {
                 this.quotes = quotesList;
                return;

            }
            
            string[] lines = File.ReadAllLines(path);

            string text = "";

            foreach (string line in lines)
            {
                text += line;
            }
            quotesList = JsonConvert.DeserializeObject<List<DeskQuote>>(text) as List<DeskQuote>;

            this.quotes = quotesList;
            return;
        }

        private async void SerializeQuotes(DeskQuote[] quotes)

        {
            string path = ".\\quotes.json";
            int width =  Convert.ToInt32 (this.widthInput.Text );
            int depth = Convert.ToInt32 (this.depthInput.Text );
            int drawers = Convert.ToInt32(this.numDrawers.Text );
            Desk desk = new Desk(width, depth, drawers, (Desk.DesktopMaterial)materialChoice.SelectedValue);
            DeskQuote current = new DeskQuote(this.nameBox.Text, (int)this.rushOrderDays.SelectedValue, desk);

            //Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
            //Windows.Storage.StorageFile sampleFile = await storageFolder.GetFileAsync(path);

            // var stream = await sampleFile.OpenAsync(Windows.Storage.FileAccessMode.ReadWrite);
            List<DeskQuote> deskQuoteList = new List<DeskQuote>();
            foreach (DeskQuote quote in quotes)
            {
                deskQuoteList.Add(quote);
            }
            deskQuoteList.Add(current);
            var json = JsonConvert.SerializeObject(deskQuoteList);

            
                Windows.Storage.StorageFolder storageFolder = Windows.Storage.ApplicationData.Current.LocalFolder;
                Windows.Storage.StorageFile file = await storageFolder.CreateFileAsync(
                    path, Windows.Storage.CreationCollisionOption.ReplaceExisting);

            

            //StorageFile file = await StorageFile.GetFileFromApplicationUriAsync(new Uri("ms-appdata:///local/quotes.json"));
            using (Stream stream = await file.OpenStreamForWriteAsync())
            {
                byte[] toBytes = Encoding.UTF8.GetBytes(json.ToString());
                await stream.WriteAsync(toBytes, 0, toBytes.Length);
            }
             this.deskQuote = current;

        }




    }
}
