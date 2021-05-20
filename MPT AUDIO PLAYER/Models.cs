using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT_AUDIO_PLAYER
{
    public struct Track
    {
        public int id { get; set; }
        public string name { get; set; }
        public int artist_id { get; set; }
        public string artist_name { get; set; }
        public DateTime duration { get; set; }
    }

    public struct Artist
    {
        public int id { get; set; }
        public string name { get; set; }
    }

    public struct Playlist
    {
        public int id { get; set; }
        public string name { get; set; }
        public List<int> content { get; set; }
        public int owner_id { get; set; }
        public List<Track> tracks { get; set; }
    }
}
