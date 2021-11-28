using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Enteties;
using Hotel.DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class PriceForCategoryService : IPriceForCategoryService
    {
        private IUnitOfWork _unit;
        IMapper mapper;

        public PriceForCategoryService(IUnitOfWork unit)
        {
            mapper = new MapperConfiguration(
                 cfg => {
                     cfg.CreateMap<PriceForCategory, PriceForCategoryDTO>().ReverseMap();
                     cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                 }
                ).CreateMapper();
            this._unit = unit;
        }
        public void Create(PriceForCategoryDTO item)
        {
            _unit.PriceForCategories.Create(mapper.Map<PriceForCategoryDTO, PriceForCategory>(item));
            _unit.Save();
        }

        public void Delete(Guid id)
        {
            _unit.PriceForCategories.Delete(id);
            _unit.Save();
        }

        public PriceForCategoryDTO Get(Guid id)
        {
            return mapper.Map<PriceForCategory, PriceForCategoryDTO>(_unit.PriceForCategories.Get(id));
        }

        public IEnumerable<PriceForCategoryDTO> GetAll()
        {
            return mapper.Map<IEnumerable<PriceForCategory>, IEnumerable<PriceForCategoryDTO>>(_unit.PriceForCategories.GetAll());
        }

        public IEnumerable<PriceForCategoryDTO> GetAllByPartName(string Name)
        {
            return mapper.Map<IEnumerable<PriceForCategory>, IEnumerable<PriceForCategoryDTO>>(_unit.PriceForCategories.GetAll().Where(c => c.Name.ToLower().Contains(Name.ToLower())));
        }

        public void Update(PriceForCategoryDTO item)
        {
            _unit.PriceForCategories.Update(mapper.Map<PriceForCategoryDTO, PriceForCategory>(item));
            _unit.Save();
        }
    }
}
