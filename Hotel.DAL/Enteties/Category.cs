using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.DAL.Enteties
{
    public class Category
    {
        public Category()
        {
            id = Guid.NewGuid();
        }
        [Key]
        public Guid id { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(2, ErrorMessage = "Минимальная длина имени 2 символов!")]
        public string Name { get; set; }
        public string Description { get; set; }
        public IEnumerable<Room> Rooms { get; set; }
        public IEnumerable<PriceForCategory> Prices { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is Category)
            {
                var thatObj = obj as Category;
                return this.id == thatObj.id
                    && this.Description == thatObj.Description
                    && this.Name == thatObj.Name;
            }
            else return base.Equals(obj);
        }

    }
}
