using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebAPIInMemoryDB.Model;

namespace WebAPIInMemoryDB
{
    public class APIContext : DbContext
    {
        public DbSet<Category> Categories { get; set; }

        public APIContext(DbContextOptions options) : base(options)
        {
            LoadCategories();
        }

        public void LoadCategories()
        {
            Category category = new Category(){CategoryName = "Category1"};
            Categories.Add(category);
            Category category1 = new Category() { CategoryName = "Category2" };
            Categories.Add(category1);
        }

        public List<Category> GetCategories()
        {
            return Categories.Local.ToList<Category>();
        }
    }
}
