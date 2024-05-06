using DogsReviewApp.Data;
using DogsReviewApp.Interfaces;
using DogsReviewApp.Models;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;
using System.Linq;

namespace DogsReviewApp.Repository
{
    public class DogRepository : IDogRepository
    {
        public readonly DataContext _dataContext;
        public DogRepository(DataContext context)
        {
            _dataContext = context;
        }

        public ICollection<Dog> GetDogs()
        { 
            return _dataContext.Dogs.OrderBy(d => d.Id).ToList();
        }

        public Dog GetDog(int id)
        {
            return _dataContext.Dogs.Where(d => d.Id == id).FirstOrDefault();
        }

        public Dog GetDog(string name)
        {
            return _dataContext.Dogs.Where(d => d.Name == name).FirstOrDefault();
        }

        public decimal GetDogRating(int dogId)
        {
            var review = _dataContext.Reviews.Where(p => p.Dog.Id == dogId);

            if (review.Count() <= 0)
                return 0;

            return ((decimal)review.Sum(r => r.Rating) / review.Count());
        }

        public bool DogExists(int dogId)
        {
            return _dataContext.Dogs.Any(d => d.Id == dogId);
        }

        public bool CreateDog(int ownerId, int categoryId, Dog dog)
        {
            var dogOwnerEntity = _dataContext.Owners.Where(a => a.Id == ownerId).FirstOrDefault();
            var category = _dataContext.Categories.Where(a => a.Id == categoryId).FirstOrDefault();

            var dogOwner = new DogOwner
            {
                Owner = dogOwnerEntity,
                Dog = dog
            };

            var dogCategory = new DogCategory
            {
                Category = category,
                Dog = dog
            };

            _dataContext.Add(dogCategory);
            _dataContext.Add(dogOwner);
            _dataContext.Add(dog);

            return Save();
        }

        public bool UpdateDog(int? ownerId, int? categoryId, int dogId, Dog dog)
        {
            if (ownerId.HasValue && categoryId.HasValue)
            {
                var ownerExists = _dataContext.Owners.Any(o => o.Id == ownerId);
                var categoryExists = _dataContext.Categories.Any(c => c.Id == categoryId);

                if (!ownerExists || !categoryExists)
                    return false;

                var existingDogOwners = _dataContext.DogOwners.Where(d => d.DogId == dogId).ToList();
                var existingDogCategories = _dataContext.DogCategories.Where(dc => dc.DogId == dogId).ToList();

                _dataContext.DogOwners.RemoveRange(existingDogOwners);
                _dataContext.DogCategories.RemoveRange(existingDogCategories);

                var newDogOwner = new DogOwner { DogId = dogId, OwnerId = ownerId.Value };
                var newDogCategory = new DogCategory { DogId = dogId, CategoryId = categoryId.Value };

                _dataContext.DogOwners.Add(newDogOwner);
                _dataContext.DogCategories.Add(newDogCategory);
            }

            _dataContext.Update(dog);
            return Save();
        }

        public bool DeleteDog(Dog dog)
        {
            _dataContext.Remove(dog);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
