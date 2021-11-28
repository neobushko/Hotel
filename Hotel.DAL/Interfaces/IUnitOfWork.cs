using Hotel.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Interfaces
{
    public interface IUnitOfWork
    {
        IRepository<Category> Categories { get; }
        IRepository<Room> Rooms { get; }
        IRepository<User> Users { get; }
        IRepository<Record> Records { get; }
        IRepository<PriceForCategory> PriceForCategories { get; }

        void Save();
    }
}
