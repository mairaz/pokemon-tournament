using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using pokemon_tour.Services;

namespace pokemon_tour.Controllers
{
    [ApiController]
    [Route("[controller]/tournament")]
    public class PokemonController : ControllerBase
    {
        private IPokemonService _pokemonService;
        public PokemonController(IPokemonService pokemonService)
        {

            _pokemonService = pokemonService;
        }
        [HttpGet("statistics")]
        public async Task<ActionResult> GetPokemonStatistics([FromQuery] string sortBy, [FromQuery] string sortDirection = "asc")
        { 
            var sortOptions = new[] { "wins", "losses", "id", "name", "ties" };
            var sortDirectionOptions = new[] { "asc", "desc" };       

            if (string.IsNullOrEmpty(sortBy))
            {
                return BadRequest(new { error = "sortBy parameter is required" });
            }

            if (!sortOptions.Contains(sortBy.ToLower()))
            {
                return BadRequest(new { error = "sortBy parameter is invalid" });
            }
            if (!sortDirectionOptions.Contains(sortDirection.ToLower()))
            {
                return BadRequest(new { error = "sortDirection parameter is invalid" });
            }

            var pokemons = await _pokemonService.FetchPokemons();
            var sortedPokemons = _pokemonService.SortPokemons(pokemons, sortBy, sortDirection);
            return Ok(sortedPokemons);
        }
        



    }
}