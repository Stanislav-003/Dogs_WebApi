﻿using PokemonReviewApp.Models;

namespace DogsReviewApp.Models
{
    public class DogOwner
    {
        public int DogId { get; set; }  
        public int OwnerId { get; set; }
        public Dog Dog { get; set; }
        public Owner Owner { get; set; }
    }
}
