using Hotel.DAL.EF;
using Hotel.DAL.Enteties;
using Hotel.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Repositories
{
    class PriceForCategoryRepository : IRepository<PriceForCategory>
    {
        private HotelContext _hotelContext;

        public PriceForCategoryRepository(HotelContext hotelContext)
        {
            this._hotelContext = hotelContext;
        }

        public void Create(PriceForCategory item)
        {
            if (_hotelContext.Prices.Find(item.id) != null || item.Price < 0 || item.EndDate < item.StartDate)
                throw new ArgumentException("wrong info");
            else if(_hotelContext.Categories.Find(item.CategoryId) == null)
                throw new ArgumentException("wrong category id");
            _hotelContext.Prices.Add(item);
        }

        public void Delete(Guid id)
        {
            _hotelContext.Prices.Remove(Get(id));
        }

        public PriceForCategory Get(Guid id)
        {
            if (!_hotelContext.Prices.Any(p => p.id == id))
                throw new ArgumentException("wrong id");
            return _hotelContext.Prices.Include(u => u.Category).Single(c => c.id == id);
        }


        public IEnumerable<PriceForCategory> GetAll()
        {
            return _hotelContext.Prices.Include(u => u.Category).ToList();
        }

        public void Update(PriceForCategory item)
        {
            var price = Get(item.id);
            price.id = item.id;
            if (_hotelContext.Categories.Find(item.CategoryId) != null)
            {
                price.Category = item.Category;
                price.CategoryId = item.CategoryId;
            }
            if (item.EndDate > item.StartDate)
            {
                price.StartDate = item.StartDate;
                price.EndDate = item.EndDate;
            }
            if (item.Price <= 0)
                price.Price = item.Price;
            price.Name = item.Name ?? price.Name;
        }
    }
}
