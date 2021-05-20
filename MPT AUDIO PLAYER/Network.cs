using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace MPT_AUDIO_PLAYER
{
    class Network
    {
        static readonly HttpClient client = new HttpClient();
        static string URL = "http://192.168.19.202:5000/";
        static readonly string success = "success";

        public static async Task Register(string login, string password, string email, Action<bool, string> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("password", password),
                new KeyValuePair<string, string>("email", email),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/register", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }

        public static async Task Login(string login, string password, Action<bool, string> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("login", login),
                new KeyValuePair<string, string>("password", password),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/login", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }

        public static async Task UploadTrack(string track_name, string artist_id, Stream file_stream, Action<bool, string> callback)
        {
            var content = new MultipartFormDataContent();
            HttpContent file_content = new StreamContent(file_stream);
            content.Add(new StringContent(track_name), "name");
            content.Add(new StringContent(artist_id), "artist_id");
            content.Add(file_content, "track", "track.mp3");
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/api/upload_track", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }

        public static async Task LoadPlaylists(Action<bool, List<Playlist>> callback)
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(URL + "/api/all_playlists");
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                List<Playlist> p = JsonConvert.DeserializeObject<List<Playlist>>(message);
                callback(message == success, message);
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }
    }
}
