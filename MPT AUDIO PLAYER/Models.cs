using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MPT_AUDIO_PLAYER
{
    public struct Track
    {
        public int id;
        public string name;
        public int artist_id;
    }

    public struct Artist
    {
        public int id;
        public string name;
    }

    public struct Playlist
    {
        public int id;
        public string name;
        public List<int> content;
        public int owner_id;
    }
}
