using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using GameImporter.Dto;

namespace GameImporter.Importer
{
    public class ImportGameDetails
    {
        private WebClient client;
        private const string frontAttribute = "front";
        public ImportGameDetails()
        {
            client = new WebClient();
        }

        public Data Execute(string url)
        {
            StringReader reader = null;
            Data DetailGame = null;
            try
            {
                WebClient wc = new WebClient();
                string xmlString = wc.DownloadString(url);
                DetailGame = new Data();
                XmlSerializer serializer = new XmlSerializer(typeof(Data));
                reader = new StringReader(xmlString);
                DetailGame = (Data)serializer.Deserialize(reader);
                return DetailGame;
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            finally
            {
                if(reader != null)
                    reader.Close();
            }
            return DetailGame;
        }
         
        public Data DownloadImage(Data DetailGame)
        {
            try
            {
            
                string frontImage = DetailGame.Game.Images.boxart
                                                    .Where(x => x.side.Equals(frontAttribute))
                                                    .Select(y => y.Value)
                                                    .FirstOrDefault();
                if (!string.IsNullOrEmpty(frontImage))
                {
                    string url = string.Format("{0}{1}", DetailGame.baseImgUrl, frontImage);
                    DetailGame.DowloadedFrontImage = client.DownloadData(new Uri(url));
                }
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(string.Format("Per il gioco {0} - {1} non è disponibile nessuna immagine", DetailGame.Game.GameTitle, DetailGame.Game.Platform));
            }
            return DetailGame;
        }        
    }
}
