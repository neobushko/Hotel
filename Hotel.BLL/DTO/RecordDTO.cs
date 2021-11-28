using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RecordDTO
    {
        public Guid id { get; set; }
        public Guid RoomId { get; set; }
        public RoomDTO Room { get; set; }
        public Guid UserId { get; set; }
        public UserDTO User { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is RecordDTO)
            {
                var thatObj = obj as RecordDTO;
                return this.id == thatObj.id
                    && this.RoomId == thatObj.RoomId
                    && this.UserId == thatObj.UserId
                    && this.CheckIn == thatObj.CheckIn
                    && this.CheckOut == thatObj.CheckOut;
            }
            else return base.Equals(obj);
        }
    }
}
