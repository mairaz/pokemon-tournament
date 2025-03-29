namespace pokemon_tour.Models
{
    public class Pokemon
    {
        public int Id { get; set;}
        public required string Name {get; set;}
        public required string Type {get; set;}
        public required string Image {get; set;}
        public int BaseExperience {get; set;}
        public int Wins {get; set;} = 0;
        public int  Losses {get; set;} =0;
        public int Ties {get; set;} =0;

    }
}


