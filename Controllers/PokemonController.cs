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
        private PokemonService _pokemonService;
        public PokemonController(PokemonService pokemonService){

            _pokemonService = pokemonService;
        }
      [HttpGet("statistics")]
        public async Task<ActionResult> GetPokemonStatistics([FromQuery] string sortOption, [FromQuery] string sortDirection){

            var pokemons = await _pokemonService.FetchPokemons();
            var sortedPokemons =  _pokemonService.SortPokemons(pokemons,sortOption, sortDirection );
            return Ok(sortedPokemons);
        }


        
    }
}