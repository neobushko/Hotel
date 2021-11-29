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
    public class UserService : IUserService
    {
        private IUnitOfWork _unit;
        IMapper mapper;
        public UserService(IUnitOfWork unit)
        {
            this._unit = unit;
            mapper = new MapperConfiguration(
                cfg => cfg.CreateMap<User, UserDTO>().ReverseMap())
                .CreateMapper();
        }
        public void Create(UserDTO item)
        {
            _unit.Users.Create(mapper.Map<UserDTO, User>(item));
            _unit.Save();
        }

        public void Delete(Guid id)
        {
            _unit.Users.Delete(id);
            _unit.Save();
        }

        public UserDTO Get(Guid id)
        {
            return mapper.Map<User, UserDTO>(_unit.Users.Get(id));
        }

        public IEnumerable<UserDTO> GetAll()
        {
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_unit.Users.GetAll());
        }

        public IEnumerable<UserDTO> GetByPartName(string part)
        {
            return mapper.Map<IEnumerable<User>, IEnumerable<UserDTO>>(_unit.Users.GetAll().Where(s => s.Name.ToLower().Contains(part.ToLower())));
        }
        public UserDTO GetByPhoneNumber(string part)
        {
            return mapper.Map<User, UserDTO>(_unit.Users.GetAll().SingleOrDefault(s => s.PhoneNumber.ToLower().Contains(part.ToLower())));
        }

        public void Update(UserDTO item)
        {
            _unit.Users.Update(mapper.Map<UserDTO, User>(item));
            _unit.Save();
        }
    }
}
