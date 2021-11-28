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
    class RecordRepository : IRepository<Record>
    {
        private HotelContext context;

        public RecordRepository(HotelContext context)
        {
            this.context = context;
        }

        public void Create(Record item)
        {
            if (context.Records.Find(item.id) != null)
                throw new ArgumentException("wrong id");
            else if (context.Users.Find(item.UserId) == null)
                throw new ArgumentException("wrong user id");
            else if (context.Rooms.Find(item.RoomId) == null)
                throw new ArgumentException("wrong room id");
            context.Records.Add(item);
        }

        public void Delete(Guid id)
        {
            context.Records.Remove(Get(id));
        }

        public Record Get(Guid id)
        {
            if (!context.Records.Any(s => s.id == id))
                throw new ArgumentException();
            return context.Records.Include(u => u.User).Include(u => u.Room).ThenInclude(b => b.Category).Single(c => c.id == id); 
        }

        public IEnumerable<Record> GetAll()
        {
            return context.Records.Include(u => u.User).Include(u => u.Room).ThenInclude(b => b.Category).ToList();
        }

        public void Update(Record item)
        {
            var record = Get(item.id);
            record.id = item.id;
            if (context.Rooms.Find(item.RoomId) != null)
            {
                record.RoomId = item.RoomId;
                record.Room = context.Rooms.Find(item.RoomId);
            }
            if (context.Users.Find(item.UserId) != null)
            {
                record.UserId = item.UserId;
                record.User = context.Users.Find(item.UserId);
            }
            record.CheckIn = item.CheckIn.Date;
            record.CheckOut = item.CheckOut.Date;
            
        }
    }
}
