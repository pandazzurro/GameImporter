using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using System.Xml;
using GameImporter.Dto;

namespace GameImporter.Importer
{
    public class ImportGameList
    {
        public const string Game = "Game";
        public const string Id = "id";
        public const string Title = "GameTitle";
        public const string Release = "ReleaseDate";
        public ImportGameList()
        { }

        public async Task<List<SimpleGame>> Execute(string url, int idPlatform, DateTime? dataInizio, DateTime? dataFine)
        {
            List<SimpleGame> GamesInPlatform = new List<SimpleGame>();
            try
            {
                WebClient wc = new WebClient();
                string xmlString = wc.DownloadString(url);                
                XmlTextReader reader = new XmlTextReader(new StringReader(xmlString));
                SimpleGame game = null;
                while (reader.Read())
                {
                    if (reader.NodeType == XmlNodeType.Element)
                    {
                        switch (reader.Name)
                        {

                            case Game:
                                {
                                    game = new SimpleGame();
                                    game.Platform = idPlatform;
                                    break;
                                }
                            case Id:
                                {
                                    game.ID = reader.ReadElementContentAsInt();
                                    break;
                                }
                            case Title:
                                {
                                    game.Title = reader.ReadElementContentAsString();
                                    break;
                                }
                            case Release:
                                {
                                    string date = reader.ReadElementContentAsString();
                                    game.Release = Convert.ToDateTime(date, new System.Globalization.CultureInfo("en-US", true));
                                    break;
                                }
                            default:
                                { break; }
                        }
                    }
                    if (reader.NodeType == XmlNodeType.EndElement && reader.Name == Game)
                    {
                        if( (dataInizio.HasValue && dataFine.HasValue && game.Release <= dataFine.Value && game.Release >= dataInizio.Value) 
                            || (!dataInizio.HasValue && !dataFine.HasValue))
                        {
                            if(game.Platform > 0 && !string.IsNullOrEmpty(game.Title))
                                GamesInPlatform.Add(game);
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            return GamesInPlatform;
        }
    }
}
