using Hotel.DAL.EF;
using Hotel.DAL.Enteties;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class CategoryRepository : IRepository<Category>
    {

        private HotelContext context;

        public CategoryRepository(HotelContext context)
        {
            this.context = context;
        }


        public void Create(Category item)
        {
            item.id = Guid.NewGuid();
            context.Categories.Add(item);
        }

        public void Delete(Guid id)
        {
            context.Categories.Remove(Get(id));
        }


        public Category Get(Guid id)
        {
            if (!context.Categories.Any(c => c.id == id))
                throw new ArgumentException("wrong id");
            return context.Categories.Single(c => c.id == id);
        }

        public IEnumerable<Category> GetAll()
        {
            var temp = context.Categories.ToList();
            return temp;
        }

        public void Update(Category item)
        {
            var category = Get(item.id);
            category.id = item.id;
            category.Name = item.Name ?? category.Name;
            category.Description = item.Description ?? category.Description;
        }
    }
}
