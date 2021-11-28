using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Enteties
{
    public class Record
    {
        public Record()
        {
            id = Guid.NewGuid();
        }
        [Key]
        public Guid id { get; set; }
        public Guid RoomId { get; set; }
        [ForeignKey("RoomId")]
        public Room Room { get; set; }
        public Guid UserId { get; set; }
        [ForeignKey("UserId")]
        public User User { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckIn { get; set; }
        [Required]
        [DataType(DataType.Date)]
        public DateTime CheckOut { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is Record)
            {
                var thatObj = obj as Record;
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
