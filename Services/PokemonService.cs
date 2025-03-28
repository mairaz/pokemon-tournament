using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokemon_tour.Models;

namespace pokemon_tour.Services
{
    public class PokemonService
    {
        private HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
        }

        public async Task<List<Pokemon>> FetchPokemons()
        {
            var random = new Random();
            var pokemonsIds = new HashSet<int>();
            var pokemon_number = 16;
            List<Pokemon> pokemon = new();

            while (pokemonsIds.Count < pokemon_number)
            {
                var randomId = random.Next(1, 151);
                pokemonsIds.Add(randomId);
            }

            foreach (var id in pokemonsIds)
            {
                pokemon.Add(await FetchPokemonsFromApi(id));
            }

            return pokemon;

        }
      
        private async Task<Pokemon> FetchPokemonsFromApi(int id)
        {

            try
            {
                var response = await _httpClient.GetAsync(id.ToString());
                var pokemon = await response.Content.ReadFromJsonAsync<PokemonApiResponse>();
           

                return new Pokemon
                {
                    Id = pokemon.id,
                    Name = pokemon.name,
                    Type = pokemon.types[0].type.name,
                    BaseExperience = pokemon.base_experience,
                    Wins = 0,
                    Losses = 0,
                    Ties = 0
                };
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

        }
    }
}