using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using GameImporter.Persistence;
using Microsoft.Win32;

namespace GameImporter
{
    /// <summary>
    /// Logica di interazione per MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ManageImport manage;
        public Game SelectedGame;
        public byte[] CurrentImage;
        public MainWindow()
        {
            InitializeComponent();
            this.DataContext = this;
            Init();
        }

        private void Init()
        {
            manage = new ManageImport();
            DataInizio.SelectedDate = DateTime.Today.AddMonths(-2);
            DataFine.SelectedDate = DateTime.Today;
            GameDetails.ItemsSource = manage.Data.Games;
            GameList.ItemsSource = manage.Data.GameList;
            SelectedGame = new Game();
            GameDetails.SelectionChanged += GameDetails_SelectionChanged;
        }

        private void GameDetails_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (GameDetails.SelectedItem != null)
            {
                Game gameSelected = (Game)GameDetails.SelectedItem;
                ImageFront.Source = CreateImage(gameSelected.Image);
            }
        }

        private async void Importa_Click(object sender, RoutedEventArgs e)
        {
            await manage.Execute(Progress, DataInizio.SelectedDate, DataFine.SelectedDate, MustSave.IsChecked, UrlApiTarget.Text);
            StringBuilder builder = new StringBuilder();
            foreach(string error in Utitlity.LogError.GetError())
            {
                if(!string.IsNullOrEmpty(error))
                    builder.Append(string.Format("{0}{1}", error, Environment.NewLine));
            }
            Log.Text = builder.ToString();
        }

        public BitmapImage CreateImage(byte[] byteVal)
        {
            try
            {
                if (byteVal != null)
                {
                    MemoryStream strmImg = new MemoryStream(byteVal);
                    BitmapImage myBitmapImage = new BitmapImage();
                    myBitmapImage.BeginInit();
                    myBitmapImage.StreamSource = strmImg;
                    myBitmapImage.EndInit();
                    return myBitmapImage;
                }
            }
            catch (Exception ex)
            {
                string message = ex.Message;
            }
            return null;
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await new ManageApi(UrlApiTarget.Text).SaveGame(manage.Data.Games.ToList());
            StringBuilder builder = new StringBuilder();
            foreach (string error in Utitlity.LogError.GetError())
            {
                if (!string.IsNullOrEmpty(error))
                    builder.Append(string.Format("{0}{1}", error, Environment.NewLine));
            }
            Log.Text = builder.ToString();
        }

        private async void Connect_Click(object sender, RoutedEventArgs e)
        {
            manage.ConnectToApi(UrlApiTarget.Text);
            List<Genre>genres = await manage.LoadGenre();
            foreach(Genre gn in await manage.LoadGenre())
            {
                manage.Data.Genres.Add(gn);
            }
            foreach(Platform pl in await manage.LoadPlatform())
            {
                manage.Data.Platforms.Add(pl);
            }
            chbGenere.ItemsSource = null;
            chbPiattaforma.ItemsSource = null;
            chbGenere.ItemsSource = manage.Data.Genres.ToList();
            chbPiattaforma.ItemsSource = manage.Data.Platforms;
        }

        private void AddGame_Click(object sender, RoutedEventArgs e)
        {
            Game g = AddGame();
            manage.Data.Games.Add(g);
            manage.Data.GameList.Add(new Dto.SimpleGame() { Title = g.Title });
            ClearGame();
        }

        private void UploadImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Multiselect = false;
            dlg.Filter = "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
            //Open the Pop-Up Window to select the file 
            if (dlg.ShowDialog() == true)
            {
                CurrentImage = GetImageByte(dlg.OpenFile());
                BitmapImage i = CreateImage(CurrentImage);
                CurrentImage = ResizeImage(CurrentImage, i.PixelWidth, i.PixelHeight, 450, 450);
                ImageFront.Source = CreateImage(CurrentImage);
            }
        }

        private Game AddGame()
        {
            Game g = new Game();
            g.Description = txtDescrizione.Text;
            g.Genre = (Genre)chbGenere.SelectedItem;
            g.Platform = (Platform)chbPiattaforma.SelectedItem;
            g.shortName = txtShortName.Text;
            g.Title = txtTitolo.Text;
            g.index = manage.Data.Games.Count + 1;
            if (CurrentImage != null)
                g.Image = CurrentImage;
            return g;
        }

        public static byte[] GetImageByte(Stream input)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                input.CopyTo(ms);
                return ms.ToArray();
            }
        }

        private void ClearGame()
        {
            CurrentImage = null;
            ImageFront.Source = null;
            txtDescrizione.Text = string.Empty;
            txtShortName.Text = string.Empty;
            txtTitolo.Text = string.Empty;
            chbGenere.SelectedIndex = -1;
            chbPiattaforma.SelectedIndex = -1;
        }

        private void DeleteGame_Click(object sender, RoutedEventArgs e)
        {
            if (GameDetails.SelectedItem != null)
            {
                Game gameSelected = (Game)GameDetails.SelectedItem;
                manage.Data.Games.Remove( manage.Data.Games.Where(x => x.index == gameSelected.index).FirstOrDefault() );
                ImageFront.Source = null;
            }
        }

        private void ClearForm_Click(object sender, RoutedEventArgs e)
        {
            ClearGame();
        }

        private byte[] ResizeImage(byte[] myBytes, int startWidth, int startHeight, int aspectedWidth, int aspectedHeight)
        {
            float nPercent;
            float destWidth = startHeight, destHeight = startWidth;
            if (startWidth > aspectedWidth)
            {
                nPercent = (float)aspectedWidth / startWidth;   // get ratio for scaling image
                destWidth = aspectedWidth;
                destHeight = startHeight * nPercent;
            }

            // Check if current height is larger than max
            if (startHeight > aspectedHeight)
            {
                nPercent = (float)aspectedHeight / startHeight;   // get ratio for scaling image
                destHeight = aspectedHeight;
                destWidth = startWidth * nPercent;
            }

            System.IO.MemoryStream myMemStream = new System.IO.MemoryStream(myBytes);
            System.Drawing.Image fullsizeImage = System.Drawing.Image.FromStream(myMemStream);
            System.Drawing.Image newImage = fullsizeImage.GetThumbnailImage((int)destWidth, (int)destHeight, null, IntPtr.Zero);
            System.IO.MemoryStream myResult = new System.IO.MemoryStream();
            newImage.Save(myResult, System.Drawing.Imaging.ImageFormat.Gif);  //Or whatever format you want.
            return myResult.ToArray();  //Returns a new byte array.
        }
    }
}
