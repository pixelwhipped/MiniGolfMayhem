using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Xml.Serialization;
using Windows.Storage;
using Windows.Storage.Streams;
using Microsoft.Xna.Framework;
//using Microsoft.Xna.Framework.Storage;
using MiniGolfMayhem.Arena;
using MiniGolfMayhem.GameElements;
using MiniGolfMayhem.Graphics;
using MiniGolfMayhem.Utilities;
using Newtonsoft.Json;

namespace MiniGolfMayhem.Levels
{
    public class SMap : IMap
    {
        public string LevelGUID { get; set; }
        public int StartLayer { get; set; }
        public Side StartSide { get; set; }
        public Vector2 Start { get; set; }
        public int MapWidth { get; set; }
        public int MapHeight { get; set; }
        public Vector2 End { get; set; }
        public Side EndSide { get; set; }
        public int EndLayer { get; set; }
        public int[] Layer1 { get; set; }
        public int[] Layer2 { get; set; }
    }
    public class CustomMaps

    {
        public SMap[] Maps;
    }

    public class GameSettings
    {
        public float MusicVolume { get; set; }
        public float EffectVolume { get; set; }

        public int TileSet { get; set; }

        public int Unlocked { get; set; }

        public string Player1 { get; set; }
        public int Player1Color { get; set; }
        public string Player2 { get; set; }
        public int Player2Color { get; set; }
        public string Player3 { get; set; }
        public int Player3Color { get; set; }
        public string Player4 { get; set; }                     
        public int Player4Color { get; set; }

        public int Players { get; set; }
    }

    public class CustomGameStorage
    {
        public StorageFolder Folder;

        public static readonly string DirectorySeparator = System.IO.Path.DirectorySeparatorChar.ToString();

        public bool SaveSetting(GameSettings settings)
        {
            settings.Players = (int)MathHelper.Clamp(settings.Players, 1, 4);
            return Serialize(settings, "settings.json");
        }

        public GameSettings LoadSettings()
        {
            return Exists("settings.json")
                ? Deserialize<GameSettings>("settings.json")
                : new GameSettings()
                {
                    MusicVolume = 1f,
                    EffectVolume = 1f,
                    TileSet = 0,
                    Unlocked = 0,
                    Player1 = Strings.Player1,
                    Player1Color = 0,
                    Player2 = Strings.Player2,
                    Player2Color = 0,
                    Player3 = Strings.Player3,
                    Player3Color = 0,
                    Player4 = Strings.Player4,
                    Player4Color = 0,
                    Players = 1,
                };
        }


        public bool AddMap(SMap map)
        {
            if (map == null) return false;
            var maps = LoadMaps();
            var i = Array.FindIndex(maps.Maps, p => p.LevelGUID == map.LevelGUID);
            if (i > 0)
            {
                maps.Maps[i] = map;
            }
            else
            {
                var l = maps.Maps.ToList();
                l.Add(map);
                maps.Maps = l.ToArray();
            }
            return SaveMaps(maps);
        }

        public bool RemoveMap(SMap map)
        {
            if (map == null) return false;
            var maps = LoadMaps();
            var i = Array.FindIndex(maps.Maps, p => p.LevelGUID == map.LevelGUID);
            if (i > 0)
            {
                var l = maps.Maps.ToList();
                l.RemoveAll(p=>p.LevelGUID==map.LevelGUID);
                maps.Maps = l.ToArray();
                return SaveMaps(maps);
            }
            return false;
        }

        public bool SaveMaps(CustomMaps maps) => Serialize(maps, "custom.json");

        public CustomMaps LoadMaps() => Exists("custom.json") ? Deserialize<CustomMaps>("custom.json") : new CustomMaps() {Maps = new SMap[0] };

        public bool Exists(string path)
            => path.Equals(DirectorySeparator) || Directory.Exists(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, path)) ||
               File.Exists(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, path));

        /// <summary>
        ///     Helper Json Deserializer
        /// </summary>
        /// <typeparam name="T">The type of the object to deserialize</typeparam>
        /// <param name="file">The file to deserialize form</param>
        /// <param name="decrypt">Was the file Encrypted</param>
        /// <returns>The Object</returns>
        public T Deserialize<T>(string file)
        {
            T ret;
            var serializer = new JsonSerializer { NullValueHandling = NullValueHandling.Ignore };
            using (var jsonTextReader = new JsonTextReader(new StreamReader(OpenFile(file, FileAccess.Read))))
            {
                ret = serializer.Deserialize<T>(jsonTextReader);
            }
            return ret;
        }

        public Stream CreateFile(string path)
        {
            return new FileStream(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, path), FileMode.Create, FileAccess.ReadWrite);
        }

        public Stream OpenFile(string path, FileAccess access)
        {
            return new FileStream(System.IO.Path.Combine(ApplicationData.Current.LocalFolder.Path, path), FileMode.Open, access);
        }

        /// <summary>
        ///     Helper Json Serializer
        /// </summary>
        /// <typeparam name="T">The Type of the object to serialize.</typeparam>
        /// <param name="data">The object to serialize</param>
        /// <param name="file">The file to serialize to</param>
        /// <param name="encrypt">Should the file be encrypted</param>
        /// <returns>the name of the serialized file or null on failure</returns>
        public bool Serialize<T>(T data, string file)
        {
            var serializer = new JsonSerializer { NullValueHandling = NullValueHandling.Ignore };
            using (var sw = new StreamWriter(CreateFile(file)))
            {
                using (JsonWriter writer = new JsonTextWriter(sw))
                {
                    serializer.Serialize(writer, data);
                }
            }
            return Exists(file);
        }

        public void SaveState(SaveState saveState)
        {
            Serialize(saveState, "state.json");
        }

        public SaveState LoadState() => Exists("state.json") ? Deserialize<SaveState>("state.json") : null;
    }
}