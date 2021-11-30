using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class RoomModel
    {
        [Display(Name = "Room id")]
        public Guid id { get; set; }
        [Display(Name = "Room number")]
        public int Number { get; set; }
        [Display(Name = "Category id")]
        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        [Display(Name = "Price per night")]
        public decimal Price { get; set; }
        [Display(Name = "Room description")]
        public string Description { get; set; }
        [Display(Name = "Does this room works?")]
        public bool IsActive { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is RoomModel)
            {
                var thatObj = obj as RoomModel;
                return this.id == thatObj.id
                    && this.Number == thatObj.Number
                    && this.CategoryId == thatObj.CategoryId
                    && this.Description == thatObj.Description;
            }
            else return base.Equals(obj);
        }
    }
}
