using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class CategoryModel
    {
        public Guid id { get; set; }
        [Display(Name = "Name of category")]
        public string Name { get; set; }
        [Display(Name = "Category Description")]
        public string Description { get; set; }
        [Display(Name = "Active price per night")]
        public decimal Price { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is CategoryModel)
            {
                var thatObj = obj as CategoryModel;
                return this.id == thatObj.id
                    && this.Description == thatObj.Description
                    && this.Name == thatObj.Name;
            }
            else return base.Equals(obj);
        }
    }
}
