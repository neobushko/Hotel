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
    class RoomRepository : IRepository<Room>
    {
        private HotelContext context;

        public RoomRepository(HotelContext context)
        {
            this.context = context;
        }

        public void Create(Room item)
        {
            item.id = Guid.NewGuid();
            item.Category = null;
            if (context.Categories.Find(item.CategoryId) == null)
                throw new ArgumentException($"there is no Category with id: {item.CategoryId}");
            item.Records = context.Records.ToList();
            context.Rooms.Add(item);
        }

        public void Delete(Guid id)
        {
            context.Rooms.Remove(Get(id));
        }

        public Room Get(Guid id)
        {
            if (!context.Rooms.Any(c => c.id == id))
                throw new ArgumentException();
            return context.Rooms.Include(u => u.Category).Single(c => c.id == id); ;
        }

        public IEnumerable<Room> GetAll()
        {
            return context.Rooms.Include(u => u.Category).ToList();
        }

        public void Update(Room item)
        {
            var room = Get(item.id);
            room.id = item.id;
            room.Number = item.Number > 0 ? item.Number : throw new ArgumentException("wrong Number");
            if (context.Categories.Find(item.CategoryId) == null)
            {
                room.CategoryId = item.CategoryId;
                room.Category = context.Categories.Find(item.CategoryId);
            }
            room.Description = item.Description ?? room.Description;
        }
    }
}
