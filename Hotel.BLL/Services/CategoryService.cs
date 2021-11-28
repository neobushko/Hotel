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
    public class CategoryService : ICategoryService
    {
        private IUnitOfWork _unit;
        IMapper mapper;

        public CategoryService(IUnitOfWork unit)
        {
            mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<Category, CategoryDTO>().ReverseMap()
                ).CreateMapper();

            this._unit = unit;
        }
        public void Create(CategoryDTO item)
        {
            _unit.Categories.Create(mapper.Map<CategoryDTO, Category>(item));
            _unit.Save();
        }

        public void Delete(Guid id)
        {
            _unit.Categories.Delete(id);
            _unit.Save();
        }

        public IEnumerable<CategoryDTO> GetAllByPartName(string partName)
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_unit.Categories.GetAll().Where(c => c.Name.ToLower().Contains(partName.ToLower())));
        }



        public CategoryDTO Get(Guid id)
        {
            return mapper.Map<Category, CategoryDTO>(_unit.Categories.Get(id));
        }

        public IEnumerable<CategoryDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Category>, IEnumerable<CategoryDTO>>(_unit.Categories.GetAll());
        }
        public void Update(CategoryDTO item)
        {
            _unit.Categories.Update(mapper.Map<CategoryDTO, Category>(item));
            _unit.Save();
        }
    }
}
