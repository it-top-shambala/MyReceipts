using System.Text.Json.Serialization;

namespace NewApiTest.models;

 public class Equipment
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("localizedName")]
        public string LocalizedName { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }

    public class Ingredient
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("localizedName")]
        public string LocalizedName { get; set; }

        [JsonPropertyName("image")]
        public string Image { get; set; }
    }

    public class Length
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("unit")]
        public string Unit { get; set; }
    }

    public class Instruction
    {
        [JsonPropertyName("name")]
        public string Name { get; set; }

        [JsonPropertyName("steps")]
        public List<Step> Steps { get; set; }
    }

    public class Step
    {
        [JsonPropertyName("number")]
        public int Number { get; set; }

        [JsonPropertyName("step")]
        public string StepDiscription { get; set; }

        [JsonPropertyName("ingredients")]
        public List<Ingredient> Ingredients { get; set; }

        [JsonPropertyName("equipment")]
        public List<Equipment> Equipment { get; set; }

        [JsonPropertyName("length")]
        public Length Length { get; set; }
    }