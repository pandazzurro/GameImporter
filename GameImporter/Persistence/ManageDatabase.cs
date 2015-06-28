using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameImporter.Persistence;

namespace GameImporter.Persistence
{
    public class ManageDatabase
    {
        public ManageDatabase()
        { }

        public List<Platform> GetPlatforms()
        {
            List<Platform> Platforms = new List<Platform>();
            try
            {
                using (ScambiContext db = ScambiContext.CreateContext())
                {
                    Platforms = db.Platforms.ToList();
                }
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            return Platforms;
        }

        public List<Genre> GetGenres()
        {
            List<Genre> Genres = new List<Genre>();
            try
            {
                using (ScambiContext db = ScambiContext.CreateContext())
                {
                    Genres = db.Genres.ToList();
                }
            }
            catch (Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            return Genres;
        }

        public bool SaveGame(List<Game>Games)
        {
            bool result = false;
            try
            {
                using (ScambiContext db = ScambiContext.CreateContext())
                {
                    Games.ForEach(game =>
                    {
                        // Se non era già stato importato il gioco allora lo inserisco
                        if (db.Games.Select(g => g.IdImport == game.IdImport).Count() == 0)
                        {
                            db.Games.Add(game);
                            db.SaveChanges();
                        }
                    });
                }
                result = true;
            }
            catch(Exception ex)
            {

            }
            return result;
        }
    }
}
