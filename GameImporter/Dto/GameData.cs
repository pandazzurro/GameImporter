using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameImporter.Persistence;

namespace GameImporter.Dto
{
    public class GameData
    {
        public ObservableCollection<SimpleGame> GameList { get; set; }
        public ObservableCollection<Data> GameDetails { get; set; }
        public ObservableCollection<string> ErrorImport { get; set; }
        public ObservableCollection<Genre> Genres { get; set; }
        public ObservableCollection<Platform> Platforms { get; set; }
        public ObservableCollection<Game> Games { get; set; }

        public GameData()
        {
            GameList = new ObservableCollection<SimpleGame>();
            GameDetails = new ObservableCollection<Data>();
            Games = new ObservableCollection<Game>();
            Genres = new ObservableCollection<Genre>();
            Platforms = new ObservableCollection<Platform>();
        }
    }
}
