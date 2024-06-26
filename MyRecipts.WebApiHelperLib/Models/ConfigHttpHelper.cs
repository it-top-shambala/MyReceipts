using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MyRecipts.WebApiHelperLib.Models;

public class ConfigHttpHelper
{
    [JsonPropertyName("pathToLogger")]
    public string PathToLogger { get; set; }
}
