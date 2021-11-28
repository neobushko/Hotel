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
    public class BaseService : IBaseService
    {
        private IUnitOfWork _unit;
        IMapper mapper;

        public BaseService(IUnitOfWork _unit)
        {
            this._unit = _unit;

            mapper = new MapperConfiguration(
            cfg =>
            {
                cfg.CreateMap<Record, RecordDTO>().ReverseMap();
                cfg.CreateMap<Room, RoomDTO>().ReverseMap();
                cfg.CreateMap<User, UserDTO>().ReverseMap();
                cfg.CreateMap<Category, CategoryDTO>().ReverseMap();
                cfg.CreateMap<PriceForCategory, PriceForCategoryDTO>().ReverseMap();
            }).CreateMapper();
        }
        /// <summary>
        /// First of all gets all the necessary PriceForCategories for all records in db
        /// Then by using foreach for each record multiply price (from PriceForCategory) on amount of days
        /// , that equals to number days this PriceForCategory in this record (0 <. number days <=. days in record)
        /// </summary>
        /// <param name="startPeriod"></param>
        /// <param name="endPeriod"></param>
        /// <returns>new entity with property Benefit equals benefit for BD in period (startPeriod, endPeriod)</returns>
        public BenefitPeriod BenefitForPeriod(DateTime startPeriod, DateTime endPeriod)
        {

            if (startPeriod > endPeriod)
                throw new ArgumentException("start time can't be after end time");
            //получаем все необходимые данные из бд
            var records = mapper.Map<IEnumerable<Record>, IEnumerable<RecordDTO>>(_unit.Records.GetAll());
            var prices = mapper.Map<IEnumerable<PriceForCategory>, IEnumerable<PriceForCategoryDTO>>(_unit.PriceForCategories.GetAll());
            decimal benefitMain = default(Decimal);
            foreach (var record in records)
            {
                //point of start into our periodStart
                IEnumerable<PriceForCategoryDTO> pricesRecord;
                try
                {
                    pricesRecord = prices.Where(p => p.CategoryId == record.Room.CategoryId && this.InInterval(record.CheckIn, record.CheckOut, p.StartDate, p.EndDate));
                }
                catch
                {
                    throw new ArgumentException();
                }
                foreach (var price in pricesRecord)
                {
                    benefitMain += (price.Price * (LowestData(record.CheckOut, price.EndDate, endPeriod) - BiggestData(record.CheckIn, price.StartDate, startPeriod)).Days);
                }
            }
            return new BenefitPeriod() { Benefit = benefitMain, Records = records, StartPeriod = startPeriod, EndPeriod = endPeriod };
        }

        public decimal BenefitForRecord(RecordDTO record)
        {
            decimal benefit = 0;
            var prices = mapper.Map<IEnumerable<PriceForCategory>, IEnumerable<PriceForCategoryDTO>>(_unit.PriceForCategories.GetAll());
            IEnumerable<PriceForCategoryDTO> pricesRecord;
            try
            {
                pricesRecord = prices.Where(p => p.CategoryId == record.Room.CategoryId && this.InInterval(record.CheckIn, record.CheckOut, p.StartDate, p.EndDate));
            }
            catch
            {
                throw new ArgumentException();
            }
            foreach (var price in pricesRecord)
            {
                benefit += (price.Price * ((record.CheckOut < price.EndDate ? record.CheckOut : price.EndDate) - (record.CheckIn > price.StartDate ? record.CheckIn : price.StartDate)).Days);
            }
            return benefit;
        }

        public DateTime LowestData(DateTime first, DateTime second, DateTime third)
        {
            DateTime lowest = new DateTime();
            if (first <= second && first <= third)
                lowest = first;
            else if (second <= first && second <= third)
                lowest = second;
            else
                lowest = third;
            return lowest;
        }
        public DateTime BiggestData(DateTime first, DateTime second, DateTime third)
        {
            DateTime biggest = new DateTime();
            if (first >= second && first >= third)
                biggest = first;
            else if (second >= first && second >= third)
                biggest = second;
            else
                biggest = third;
            return biggest;
        }
        public IEnumerable<RoomDTO> FreeRoomsForDate(DateTime checkIn, DateTime checkOut)
        {
            IEnumerable<RoomDTO> rooms = mapper.Map<IEnumerable<Room>, IEnumerable<RoomDTO>>(_unit.Rooms.GetAll());
            List<RoomDTO> freeRooms = new List<RoomDTO>();
            foreach (var room in rooms)
            {
                if (IsFreeRoom(room.id, checkIn, checkOut))
                    freeRooms.Add(room);
            }
            return freeRooms;
        }


        public bool IsFreeRoom(Guid roomId, DateTime checkIn, DateTime checkOut)
        {
            bool isFree;
            IEnumerable<RecordDTO> records = mapper.Map<IEnumerable<Record>, IEnumerable<RecordDTO>>(_unit.Records.GetAll());

            isFree = !records.Any(c => c.RoomId == roomId && InInterval(c.CheckIn, c.CheckOut, checkIn, checkOut));
            return isFree;
        }
        /// <summary>
        /// Checks does the 'other' interval of time(from otherStart to otherEnd) intersects with the 'main' interval of time
        /// </summary>
        /// <param name="mainStart"></param>
        /// <param name="mainEnd"></param>
        /// <param name="otherStart"></param>
        /// <param name="otherEnd"></param>
        /// <returns> true - if 'other' intersects 'main', and vice versa</returns>
        public bool InInterval(DateTime mainStart, DateTime mainEnd, DateTime otherStart, DateTime otherEnd)
        {
            bool inInterval = false;
            if ((otherEnd >= mainStart && otherEnd <= mainEnd)
                || (otherStart >= mainStart && otherStart <= mainEnd)
                || (otherStart <= mainStart && otherEnd >= mainEnd))
                inInterval = true;
            return inInterval;
        }

    }
}
