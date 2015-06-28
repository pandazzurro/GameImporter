using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Json;
using Newtonsoft.Json;
using System.Net.Http.Formatting;
using System.Text;

namespace GameImporter.Persistence
{
    public class ManageApi
    {
        private const string gameImportApi = "gameImport/{0}";
        private const string gameApi = "games/{0}";
        private const string genreApi = "genres/{0}";
        private const string platformsApi = "platforms/{0}";
        private HttpClient client;
        private HttpResponseMessage response;
        public ManageApi(string baseUrl)
        {
            client = new HttpClient();
            client.BaseAddress = new Uri(baseUrl);
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public async Task<List<Genre>> GetGenres()
        {
            List<Genre> genres = null;
            response = await client.GetAsync(string.Format(genreApi, ""));
            if (response.IsSuccessStatusCode)
            {
                using (Stream stream = await response.Content.ReadAsStreamAsync())
                {
                    DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Genre>));
                    genres = (List<Genre>)serializer.ReadObject(stream);
                }
            }
            return genres;
        }

        public async Task<List<Platform>> GetPlatforms()
        {
            List<Platform> platforms = null;
            try
            {
                response = await client.GetAsync(string.Format(platformsApi, ""));
                if (response.IsSuccessStatusCode)
                {
                    using (Stream stream = await response.Content.ReadAsStreamAsync())
                    {
                        DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(List<Platform>));
                        platforms = (List<Platform>)serializer.ReadObject(stream);
                    }
                }
            }
            catch(Exception ex)
            {
                Utitlity.LogError.AddError(ex);
            }
            return platforms;
        }

        public async Task<List<Game>> SaveGame(List<Game>Games)
        {
            Game gameAlreadyExist = null;
            foreach (Game g in Games)
            {
                gameAlreadyExist = null;
                try
                {
                    response = await client.GetAsync(string.Format(gameImportApi, g.ImportId));
                    if (response.IsSuccessStatusCode)
                    {
                        using (Stream stream = await response.Content.ReadAsStreamAsync())
                        {
                            if(stream.Length > 0)
                            {
                                DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(Game));
                                gameAlreadyExist = (Game)serializer.ReadObject(stream);
                                g.GameId = gameAlreadyExist.GameId;
                            }
                        }
                    }
                    if(gameAlreadyExist != null)
                    {
                        response = await client.PutAsJsonAsync(string.Format(gameApi, ""), g);
                        if (response.IsSuccessStatusCode)
                        {
                            continue;
                        }
                    }
                    else
                    {
                        //string gSerialize = JsonConvert.SerializeObject(g, Formatting.Indented, new JsonSerializerSettings { StringEscapeHandling = StringEscapeHandling.EscapeHtml });
                        //response = await client.PostAsync(string.Format(gameApi, ""), new StringContent(gSerialize, Encoding.UTF8, "application/json"));
                        response = await client.PostAsJsonAsync(string.Format(gameApi, ""), g);
                        if (response.IsSuccessStatusCode)
                        {
                            DataContractJsonSerializer serializer = new DataContractJsonSerializer(typeof(GameIdentity));
                            GameIdentity game = (GameIdentity)serializer.ReadObject(await response.Content.ReadAsStreamAsync());
                            g.GameId = game.GameId;
                        }
                    }
                }
                catch (Exception ex)
                {
                    Utitlity.LogError.AddError(ex);
                }
            }
            return Games;
        }
    }
}
