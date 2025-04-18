using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace pokemon_tour.Models
{

    public class PokemonApiResponse
    {
        [JsonPropertyName("base_experience")]
        public int BaseExperience { get; set; }
        [JsonPropertyName("id")]
        public int Id { get; set; }
        [JsonPropertyName("name")]
        public required string Name { get; set; }
        [JsonPropertyName("types")]
        public required List<Type> Types { get; set; }
        [JsonPropertyName("sprites")]
        public required Sprites Sprites { get; set; }
    }


    public class Type
    {
        [JsonPropertyName("type")]
        public required TypeDetail TypeInfo { get; set; }
    }

    public class TypeDetail
    {
        [JsonPropertyName("name")]
        public required string Name { get; set; }
    }

    public class Sprites
    {
        [JsonPropertyName("front_default")]
        public string? frontDefault { get; set; }
    }

}