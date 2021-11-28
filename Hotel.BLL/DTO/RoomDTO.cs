using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class RoomDTO
    {
        public Guid id { get; set; }
        public int Number { get; set; }
        public Guid CategoryId { get; set; }
        public CategoryDTO Category { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is RoomDTO)
            {
                var thatObj = obj as RoomDTO;
                return this.id == thatObj.id
                    && this.Number == thatObj.Number
                    && this.CategoryId == thatObj.CategoryId
                    && this.Description == thatObj.Description;
            }
            else return base.Equals(obj);
        }
    }
}
