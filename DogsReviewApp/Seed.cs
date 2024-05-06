using DogsReviewApp.Data;
using DogsReviewApp.Models;
using PokemonReviewApp.Models;

namespace DogsReviewApp
{
    public class Seed
    {
        private readonly DataContext dataContext;
        public Seed(DataContext context)
        {
            this.dataContext = context;
        }
        public void SeedDataContext()
        {
            if (!dataContext.DogOwners.Any())
            {
                var dogOwners = new List<DogOwner>()
                {
                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Barsik",
                            BirthDate = new DateTime(1903,1,1),
                            DogCategories = new List<DogCategory>()
                            {
                                new DogCategory { Category = new Category() { Name = "German Shepherd"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Golden Retriever",Text = "Golden Retriever is the best dog, because it is friendly", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Golden Retriever", Text = "Golden Retriever is the best at fetching", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Golden Retriever",Text = "Golden Retriever, Golden Retriever, Golden Retriever", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Jack",
                            LastName = "London",
                            Gym = "Brocks Gym",
                            Country = new Country()
                            {
                                Name = "Kanto"
                            }
                        }
                    },
                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Sharik",
                            BirthDate = new DateTime(1903,1,1),
                            DogCategories = new List<DogCategory>()
                            {
                                new DogCategory { Category = new Category() { Name = "Labrador"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title= "Labrador Retriever", Text = "Labrador Retriever is the best dog, because it is friendly", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title= "Labrador Retriever",Text = "Labrador Retriever is the best at fetching", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title= "Labrador Retriever", Text = "Labrador Retriever, Labrador Retriever, Labrador Retriever", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Harry",
                            LastName = "Potter",
                            Gym = "Mistys Gym",
                            Country = new Country()
                            {
                                Name = "Saffron City"
                            }
                        }
                    },
                    new DogOwner()
                    {
                        Dog = new Dog()
                        {
                            Name = "Dina",
                            BirthDate = new DateTime(1903,1,1),
                            DogCategories = new List<DogCategory>()
                            {
                                new DogCategory { Category = new Category() { Name = "Chihuahua"}}
                            },
                            Reviews = new List<Review>()
                            {
                                new Review { Title="Chihuahua",Text = "Chihuahua is the best dog, because it is intelligent", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Teddy", LastName = "Smith" } },
                                new Review { Title="Chihuahua",Text = "Chihuahua is the best at obedience training", Rating = 5,
                                Reviewer = new Reviewer(){ FirstName = "Taylor", LastName = "Jones" } },
                                new Review { Title="Chihuahua",Text = "Chihuahua", Rating = 1,
                                Reviewer = new Reviewer(){ FirstName = "Jessica", LastName = "McGregor" } },
                            }
                        },
                        Owner = new Owner()
                        {
                            FirstName = "Ash",
                            LastName = "Ketchum",
                            Gym = "Ashs Gym",
                            Country = new Country()
                            {
                                Name = "Millet Town"
                            }
                        }
                    }
                };
                dataContext.DogOwners.AddRange(dogOwners);
                dataContext.SaveChanges();
            }
        }
    }
}