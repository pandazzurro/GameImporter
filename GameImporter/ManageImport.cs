using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using GameImporter.Dto;
using GameImporter.Importer;
using GameImporter.Persistence;
using System.Windows.Controls;
using System.Threading;

namespace GameImporter
{
    public class ManageImport
    {
        private ImportGameList ImportGameList;
        public GameData Data;
        private List<Platform> Platforms;
        private List<Genre> Genres;
        private ImportGameDetails ImportGameDetails;
        private ManageApi api;
        private ProgressBar progress;

        public ManageImport()
        {
            ImportGameList = new ImportGameList();
            ImportGameDetails = new ImportGameDetails();
            Data = new GameData();
            Platforms = new List<Platform>();
            Genres = new List<Genre>();
        }

        public void ConnectToApi(string baseUrl)
        {
            api = new ManageApi(baseUrl);
        }

        public async Task<List<Genre>> LoadGenre()
        {
            Genres = await api.GetGenres();
            return Genres;
        }

        public async Task<List<Platform>> LoadPlatform()
        {
            Platforms = await api.GetPlatforms();
            return Platforms;
        }

        public async Task<bool> Execute(ProgressBar progress, DateTime? dataInizio, DateTime? dataFine, bool? mustSave, string baseUrl)
        {
            bool result = false;            
            try
            {
                progress.Value = 0;
                List<SimpleGame> gameList = new List<SimpleGame>();
                List<Data> gameDetails = new List<Data>();
                double percentage = (double)(100/3) / (double)Platforms.Count;
                foreach (Platform p in Platforms)
                {
                    string urlGamePlatform = string.Format("{0}{1}", "http://thegamesdb.net/api/GetPlatformGames.php?platform=", p.ImportId);
                    gameList.AddRange(await ImportGameList.Execute(urlGamePlatform, p.ImportId, dataInizio, dataFine));
                    Data.GameList = new ObservableCollection<SimpleGame>(gameList);
                    
                    progress.Value = progress.Value + percentage;
                }                
                
                percentage = (double)(100/3) / (double)Data.GameList.Count;
                gameList.ForEach(game =>
                {
                    try
                    {
                        string urlGameDetails = string.Format("{0}{1}", "http://thegamesdb.net/api/GetGame.php?id=", game.ID);
                        Data gameDetail = ImportGameDetails.Execute(urlGameDetails);
                        Game gameResult = GameDetailsToGameDb.Convert(gameDetail, Genres, Platforms);

                        Monitor.Enter(Data);
                        Data.GameDetails.Add(gameDetail);
                        Data.Games.Add(gameResult);
                        Monitor.Exit(Data);
                        progress.Value = progress.Value + percentage;
                    }
                    catch(Exception ex)
                    {
                        Utitlity.LogError.AddError(ex);
                    }
                });

                List<string> generi = Data.GameDetails.Select(x => x.Game.Genres.genre).Distinct().ToList();
                // salvo nel DB
                if (mustSave.HasValue && mustSave.Value)
                    await api.SaveGame(Data.Games.ToList());

                gameDetails = Data.GameDetails.ToList();
                Data.GameDetails.Clear();
                Data.Games.Clear();
                int i = 0;
                gameDetails.ForEach(game =>
                {
                    try
                    {
                        Data gameDetail = ImportGameDetails.DownloadImage(game);
                        Game gameResult = GameDetailsToGameDb.Convert(gameDetail, Genres, Platforms);
                        gameResult.index = i;
                        i++;
                        Data.GameDetails.Add(gameDetail);
                        Data.Games.Add(gameResult);
                        progress.Value = progress.Value + percentage;
                    }
                    catch(Exception ex)
                    {
                        Utitlity.LogError.AddError(ex);
                    }
                });
                // aggiorno nel DB
                if (mustSave.HasValue && mustSave.Value)
                    await api.SaveGame(Data.Games.ToList());

                progress.Value = 100;
                result = true;
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            return result;
        }

        //public void dbAccess()
        //{
        //    using(ScambiContext db = ScambiContext.CreateContext())
        //    {
        //        List<Game> titoli = db.Games.ToList();
        //    }
            
        //}

        

    }
}
