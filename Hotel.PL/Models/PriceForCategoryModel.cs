using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Models
{
    public class PriceForCategoryModel
    {
        [Display(Name = "id of price")]
        public Guid id { get; set; }
        [Display(Name = "Name of price")]
        public string Name { get; set; }
        [Display(Name = "Category id")]
        public Guid CategoryId { get; set; }
        public CategoryModel Category { get; set; }
        [Display(Name = "cost per night")]
        public decimal Price { get; set; }
        [Display(Name = "Category name")]
        public string CategoryName { get; set; }
        [Display(Name = "startpoint date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "endpoint date")]
        public DateTime EndDate { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is PriceForCategoryModel)
            {
                var thatObject = obj as PriceForCategoryModel;
                return this.id == thatObject.id
                    && this.CategoryId == thatObject.CategoryId
                    && this.Price == thatObject.Price
                    && this.StartDate == thatObject.StartDate
                    && this.EndDate == thatObject.EndDate
                    && this.Name == thatObject.Name;
            }
            else return base.Equals(obj);
        }
    }
}

