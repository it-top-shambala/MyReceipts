using System.Text.Json.Serialization;

namespace MyRecipts.WebApiHelperLib.Models;
/// <summary>
/// Класс описывающий сущность конфига для HttpHelper
/// </summary>
public class ConfigHttpHelper
{
    [JsonPropertyName("pathToLogger")]
    public string PathToLogger { get; set; }
}
