using PokemonReviewApp.Models;

namespace DogsReviewApp.Interfaces
{
    public interface IDogRepository
    {
        ICollection<Dog> GetDogs();
        Dog GetDog(int id);
        Dog GetDog(string name);
        decimal GetDogRating(int dogId);
        bool DogExists(int dogId);
        bool CreateDog(int ownerId, int categoryId, Dog dog);
        bool UpdateDog(int? ownerId, int? categoryId, int dogId, Dog dog);
        bool DeleteDog(Dog dog);
        bool Save();
    }
}
