using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{ 
    public interface ICheckRooms
    {
        public bool IsFreeRoom(Guid roomId, DateTime checkIn, DateTime checkOut);
        public IEnumerable<RoomDTO> FreeRoomsForDate(DateTime checkIn, DateTime checkOut);
    }
}
