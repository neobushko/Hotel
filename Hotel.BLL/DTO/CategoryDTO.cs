using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.DTO
{
    public class CategoryDTO
    {
        public Guid id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public override bool Equals(object obj)
        {
            if (obj is CategoryDTO)
            {
                var thatObj = obj as CategoryDTO;
                return this.id == thatObj.id
                    && this.Description == thatObj.Description
                    && this.Name == thatObj.Name;
            }
            else return base.Equals(obj);
        }
    }
}
