using System.Collections.Generic;
using Newtonsoft.Json;

namespace DvJobPickerRedux.Models;

public class TracksModel
{
    [JsonProperty("loading")]
    public List<Loading> Loading { get; set; } = null!;

    [JsonProperty("storage")]
    public List<Storage> Storage { get; set; } = null!;
}

public class Loading
{
    [JsonProperty("yardId")]
    public string YardId { get; set; } = null!;

    [JsonProperty("tracks")]
    public List<string> Tracks { get; set; } = null!;
}

public class Storage
{
    [JsonProperty("yardId")]
    public string YardId { get; set; } = null!;

    [JsonProperty("tracks")]
    public List<string> Tracks { get; set; } = null!;
}