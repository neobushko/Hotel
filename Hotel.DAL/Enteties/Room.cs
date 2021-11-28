using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Enteties
{
    public class Room
    {
        public Room()
        {
            id = Guid.NewGuid();
        }
        [Key]
        public Guid id { get; set; }
        [Required]
        public int Number { get; set; }
        public Guid CategoryId { get; set; }
        [ForeignKey("CategoryId")]
        public Category Category { get; set; }
        public string Description { get; set; }
        [Required]
        public bool IsActive { get; set; }
        public IEnumerable<Record> Records { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Room)
            {
                var thatObj = obj as Room;
                return this.id == thatObj.id
                    && this.Number == thatObj.Number
                    && this.CategoryId == thatObj.CategoryId
                    && this.Description == thatObj.Description;
            }
            else return base.Equals(obj);
        }
    }
}
