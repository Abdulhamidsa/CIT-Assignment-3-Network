using System.Collections.Generic;
using System.Linq;
using Assignment3.Models;

namespace Assignment3.Services
{
    // simple service for working with categories
    public class CategoryService
    {
        // pretend this is our small database of categories
        private List<Category> categories = new()
        {
            new Category { Id = 1, Name = "Books" },
            new Category { Id = 2, Name = "Condiments" },
            new Category { Id = 3, Name = "Clothing" }
        };

        // return all categories
        public List<Category> GetCategories()
        {
            return categories;
        }

        // find category by id
        public Category GetCategory(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        // add new category if id not used
        public bool CreateCategory(int id, string name)
        {
            var exists = categories.Any(c => c.Id == id);
            if (exists)
            {
                return false;
            }

            categories.Add(new Category { Id = id, Name = name });
            return true;
        }

        // change the name of a category
        public bool UpdateCategory(int id, string newName)
        {
            var cat = GetCategory(id);
            if (cat == null)
            {
                return false;
            }

            cat.Name = newName;
            return true;
        }

        // remove category from list
        public bool DeleteCategory(int id)
        {
            var cat = GetCategory(id);
            if (cat == null)
            {
                return false;
            }

            categories.Remove(cat);
            return true;
        }
    }
}
