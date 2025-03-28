using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_tour.Models
{
   
    public class PokemonApiResponse
    {       
        public int base_experience { get; set; }        
        public int id { get; set; }      
        public string name { get; set; }       
        public List<Type> types { get; set; }
    }


    public class Type
    {
        public int slot { get; set; }
        public Type2 type { get; set; }
    }

    public class Type2
    {
        public string name { get; set; }
        public string url { get; set; }
    }

}