using pokemon_tour.Models;

namespace pokemon_tour.Services
{
    public class PokemonService : IPokemonService
    {
        private HttpClient _httpClient;

        public PokemonService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/");
        }

        private Dictionary<string, string> PokemonType = new Dictionary<string, string>
    {
        {"water", "fire" },
        {"fire", "grass" },
        {"grass", "electric" },
        {"electric", "water" },
        {"ghost", "psychic" },
        {"psychic", "fighting" },
        {"fighting", "dark" },
        {"dark", "ghost" }
    };

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
            return PokemonBattle(pokemon);

        }

        private async Task<Pokemon> FetchPokemonsFromApi(int id)
        {
            try
            {
                var response = await _httpClient.GetAsync(id.ToString());
                response.EnsureSuccessStatusCode();
                var pokemon = await response.Content.ReadFromJsonAsync<PokemonApiResponse>();


                return new Pokemon
                {
                    Id = pokemon.id,
                    Name = pokemon.name,
                    Type = pokemon.types[0].type.name,
                    BaseExperience = pokemon.base_experience,
                    Image = pokemon.sprites.front_default,
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

        public List<Pokemon> SortPokemons(List<Pokemon> pokemons, string sortBy, string sortDirection)
        {
            bool isAscending = sortDirection.ToLower() == "asc";

            switch (sortBy.ToLower())
            {
                case "name":
                    return isAscending
                     ? pokemons.OrderBy(pokemon => pokemon.Name).ToList()
                     : pokemons.OrderByDescending(pokemon => pokemon.Name).ToList();
                case "id":
                    return isAscending
                     ? pokemons.OrderBy(pokemon => pokemon.Id).ToList()
                     : pokemons.OrderByDescending(pokemon => pokemon.Id).ToList();
                case "wins":
                    return isAscending
                    ? pokemons.OrderBy(pokemon => pokemon.Wins).ToList()
                    : pokemons.OrderByDescending(pokemon => pokemon.Wins).ToList();
                case "losses":
                    return isAscending
                    ? pokemons.OrderBy(pokemon => pokemon.Losses).ToList()
                    : pokemons.OrderByDescending(pokemon => pokemon.Losses).ToList();
                case "ties":
                    return isAscending
                    ? pokemons.OrderBy(pokemon => pokemon.Ties).ToList()
                    : pokemons.OrderByDescending(pokemon => pokemon.Ties).ToList();
                default:
                    return new List<Pokemon>();
            }
        }

        private List<Pokemon> PokemonBattle(List<Pokemon> pokemons)
        {

            for (int i = 0; i < pokemons.Count; i++)
            {
                for (int j = i + 1; j < pokemons.Count; j++)
                {
                    if (PokemonType.ContainsKey(pokemons[i].Type) && PokemonType[pokemons[i].Type] == pokemons[j].Type)
                    {
                        pokemons[i].Wins++;
                        pokemons[j].Losses++;

                    }
                    else if (PokemonType.ContainsKey(pokemons[j].Type) && PokemonType[pokemons[j].Type] == pokemons[i].Type)
                    {
                        pokemons[j].Wins++;
                        pokemons[i].Losses++;

                    }
                    else if (pokemons[i].BaseExperience > pokemons[j].BaseExperience)
                    {
                        pokemons[i].Wins++;
                        pokemons[j].Losses++;

                    }
                    else if (pokemons[j].BaseExperience > pokemons[i].BaseExperience)
                    {
                        pokemons[j].Wins++;
                        pokemons[i].Losses++;
                    }
                    else
                    {
                        pokemons[j].Ties++;
                        pokemons[i].Ties++;
                    }
                }
            }
            return pokemons;
        }

    }
}