using System;
using System.IO;
using DvJobPickerRedux.Models;
using Newtonsoft.Json;

namespace DvJobPickerRedux.Managers;

public static class TrackManager
{
    public static TracksModel Tracks { get; private set; } = new();

    public static void LoadTrackConfiguration()
    {
        try
        {
            var path = Path.Combine(Main.Mod?.Path ?? string.Empty, "tracks.json");

            if (!File.Exists(path))
            {
                Main.DebugLog("Tracks file could not be found.");

                return;
            }

            Tracks = JsonConvert.DeserializeObject<TracksModel>(File.ReadAllText(path))
                     ?? throw new JsonSerializationException("Could not deserialize the tracks file.");
        }
        catch (Exception ex)
        {
            Main.DebugLog($"Failed to read tracks file: {ex.Message}");
            Tracks = new TracksModel();
        }
    }
}