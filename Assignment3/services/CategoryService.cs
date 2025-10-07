using System.Collections.Generic;
using System.Linq;
using Assignment3.Models;

namespace Assignment3.Services
{
    public class CategoryService
    {
        private List<Category> categories = new List<Category>
        {
            new Category { Id = 1, Name = "Books" },
            new Category { Id = 2, Name = "Condiments" },
            new Category { Id = 3, Name = "Clothing" }
        };

        public List<Category> GetAllCategories()
        {
            return categories;
        }

        public Category? GetCategory(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }
    }
}
