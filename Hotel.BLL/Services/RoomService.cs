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
    public class RoomService : IRoomService
    {

        private IUnitOfWork _unit;
        IMapper mapper;
        public RoomService(IUnitOfWork unit)
        {
            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                cfg.CreateMap<CategoryDTO, Category>().ReverseMap();
            }).CreateMapper();
            this._unit = unit;
        }
        public void Create(RoomDTO item)
        {
            _unit.Rooms.Create(mapper.Map<RoomDTO, Room>(item));
            _unit.Save();
        }

        public void Delete(Guid id)
        {
            _unit.Rooms.Delete(id);
            _unit.Save();
        }

        public RoomDTO Get(Guid id)
        {
            return mapper.Map<Room, RoomDTO>(_unit.Rooms.Get(id));
        }

        public IEnumerable<RoomDTO> GetAll()
        {
            return mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(_unit.Rooms.GetAll());
        }

        public IEnumerable<RoomDTO> GetByNumber(int Number)
        {
            return mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(_unit.Rooms.GetAll().Where(c => c.Number == Number));
        }

        public void Update(RoomDTO item)
        {
            _unit.Rooms.Update(mapper.Map<RoomDTO, Room>(item));
            _unit.Save();
        }
    }
}
