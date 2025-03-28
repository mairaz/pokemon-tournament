using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace pokemon_tour.Models
{
    public class Pokemon
    {
        public int Id { get; set;}
        public string Name {get; set;}
        public string Type {get; set;}
        public int BaseExperience {get; set;}
        public int Wins {get; set;}
        public int  Losses {get; set;}
        public int Ties {get; set;}
    }
}


