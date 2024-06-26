using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyRecipts.WebApiHelperLib.Models;

public class ConfigRecipeHepler
{
    [JsonPropertyName("apiKey")]
    public string ApiKey { get; set; }

    [JsonPropertyName("baseUri")]
    public string BaseUri { get; set; }

    [JsonPropertyName("rawRequestUri")]
    public string RawRequestUri { get; set; }

    [JsonPropertyName("apiKeyConnector")]
    public string ApiKeyConnector { get; set; }

    [JsonPropertyName("ingredientsConnector")]
    public string IngredientsConnector { get; set; }

    [JsonPropertyName("connector")]
    public string Connector { get; set; }
    [JsonPropertyName("pathToLogger")]
    public string PathToLogger { get; set; }
}
