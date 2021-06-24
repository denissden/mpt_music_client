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
        public static string URL = "192.168.62.39:5000";
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
                List<Playlist> p = JsonSerializer.Deserialize<List<Playlist>>(message);
                Debug.Show(message);
                callback(true, p);
            }
            catch (Exception e)
            {
                Error.Show(e.StackTrace);
                callback(false, null);
            }
        }

        public static async Task AddPlaylist(string name, Action<bool, Playlist?> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("name", name),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/api/create_playlist", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                Playlist p = JsonSerializer.Deserialize<Playlist>(message);
                callback(true, p);
            }
            catch (Exception e)
            {
                Error.Show(e.StackTrace);
                callback(false, null);
            }
        }

        public static async Task LoadPlaylist(int id, Action<bool, Playlist?> callback)
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(URL + $"/api/playlist/{id}");
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                Playlist p = JsonSerializer.Deserialize<Playlist>(message);
                Debug.Show(message);
                callback(true, p);
            }
            catch (Exception e)
            {
                Error.Show(e.StackTrace);
                callback(false, null);
            }
        }

        public static async Task AddTrackToPlaylist(int track_id, int playlist_id, Action<bool, string> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {                                                                                             
                new KeyValuePair<string, string>("track_id", track_id.ToString()),
                new KeyValuePair<string, string>("playlist_id", playlist_id.ToString()),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/api/add_to_playlist", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
            }
            catch (Exception e)
            {
                Error.Show(e.StackTrace);
                callback(false, "error adding track to playlist");
            }
        }

        public static async Task SearchTrack(string search, Action<bool, List<Track>> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("string", search),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + $"/api/search_tracks", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                List<Track> p = JsonSerializer.Deserialize<List<Track>>(message);
                callback(true, p);
            }
            catch (Exception e)
            {
                Error.Show(e.StackTrace);
                callback(false, null);
            }
        }

        public static async Task PremiumStatus(Action<bool, string> callback)
        {
            try
            {
                HttpResponseMessage res = await client.GetAsync(URL + "/api/premium");
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
                return;
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }

        public static async Task TogglePremium(bool state, Action<bool, string> callback)
        {
            var content = new FormUrlEncodedContent(new[]
            {
                new KeyValuePair<string, string>("state", state.ToString()),
            });
            try
            {
                HttpResponseMessage res = await client.PostAsync(URL + "/api/premium", content);
                res.EnsureSuccessStatusCode();
                string message = await res.Content.ReadAsStringAsync();
                callback(message == success, message);
                return;
            }
            catch
            {
                callback(false, "status code not 200");
            }
        }


    }
}
