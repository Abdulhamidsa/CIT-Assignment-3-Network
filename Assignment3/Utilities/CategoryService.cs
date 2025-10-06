using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Assignment3.Utilities
{

    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Category(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
    public class CategoryService
    {

        private List<Category> categories = new List<Category>
    {
        new Category(1, "Beverages"),
        new Category(2, "Condiments"),
        new Category(3, "Confections")
    };

        public List<Category> GetCategories()
        {
            return categories;
        }

        public Category? GetCategory(int id)
        {
            return categories.FirstOrDefault(c => c.Id == id);
        }

        public bool CreateCategory(int id, string name)
        {
            if (categories.Any(c => c.Id == id))
                return false;

            categories.Add(new Category(id, name));
            return true;
        }

        public bool UpdateCategory(int id, string newName)
        {
            var category = GetCategory(id);
            if (category == null)
                return false;

            category.Name = newName;
            return true;
        }

        public bool DeleteCategory(int id)
        {
            var category = GetCategory(id);
            if (category == null)
                return false;

            categories.Remove(category);
            return true;
        }
    }
}
