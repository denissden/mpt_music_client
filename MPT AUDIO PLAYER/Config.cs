using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace MPT_AUDIO_PLAYER
{
    class Config
    {
        public string URL { get; set; }

        public void Load()
        {
            Network.URL = URL;
        }

        public void Save()
        {
            try
            {
                File.WriteAllText("config", JsonSerializer.Serialize(this));
            }
            catch
            {
                Error.Show("Could not save config file");
            }
        }

        public static Config Read()
        {
            try
            {
                return JsonSerializer.Deserialize<Config>(File.ReadAllText("config"));
            }
            catch
            {
                Error.Show("Could not save config file");
            }
            return null;
        }
    }
}
