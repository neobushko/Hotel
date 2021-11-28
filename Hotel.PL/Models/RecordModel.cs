using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class RecordModel
    {
        public Guid id { get; set; }
        public Guid RoomId { get; set; }
        public RoomModel Room { get; set; }
        public int RoomNumber { get; set; }
        public string CategoryName { get; set; }
        public Guid UserId { get; set; }
        public UserModel User { get; set; }
        public string UserPhoneNumber { get; set; }
        public string UserName { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public decimal Benefit { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is RecordModel)
            {
                var thatObj = obj as RecordModel;
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
