
using pokemon_tour.Models;

namespace pokemon_tour.Services
{
    public interface IPokemonService
    {
        Task<List<Pokemon>> FetchPokemons();        
        List<Pokemon> SortPokemons(List<Pokemon> pokemons, string sortBy, string sortDirection);
    }
}