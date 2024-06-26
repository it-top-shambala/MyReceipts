using System.Text.Json.Serialization;

namespace MyRecipts.WebApiHelperLib.Models;

public class ConfigHttpHelper
{
    [JsonPropertyName("pathToLogger")]
    public string PathToLogger { get; set; }
}
