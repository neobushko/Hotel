using Hotel.BLL.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Interfaces
{
    public interface IPriceForCategoryService
    {
        IEnumerable<PriceForCategoryDTO> GetAll();
        PriceForCategoryDTO Get(Guid id);
        void Create(PriceForCategoryDTO item);
        void Update(PriceForCategoryDTO item);
        void Delete(Guid id);
        IEnumerable<PriceForCategoryDTO> GetAllByPartName(string Name);
    }
}
