using DogsReviewApp.Data;
using DogsReviewApp.Interfaces;
using Microsoft.EntityFrameworkCore;
using PokemonReviewApp.Models;

namespace DogsReviewApp.Repository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly DataContext _dataContext;

        public CategoryRepository(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public bool CategoriesExists(int id)
        {
            return _dataContext.Categories.Any(c => c.Id == id);
        }

        public ICollection<Category> GetCategories()
        {
            return _dataContext.Categories.ToList();
        }

        public Category GetCategory(int id)
        {
            return _dataContext.Categories.Where(c => c.Id == id).FirstOrDefault();
        }

        public ICollection<Dog> GetDogByCategory(int categoryId)
        {
            return _dataContext.DogCategories.Where(dc => dc.CategoryId == categoryId).Select(d => d.Dog).ToList();
        }

        public bool CreateCategory(Category category)
        {
            _dataContext.Add(category);
            return Save();
        }

        public bool UpdateCategory(Category category)
        {
            _dataContext.Update(category);
            return Save();
        }

        public bool DeleteCategory(Category category)
        {
            _dataContext.Remove(category);
            return Save();
        }

        public bool Save()
        {
            var saved = _dataContext.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
