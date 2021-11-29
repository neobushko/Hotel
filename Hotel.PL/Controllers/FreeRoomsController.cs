using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.PL.Models;
using Hotel.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Controllers
{
    [Authorize(Roles = "Admin, Customer")]
    public class FreeRoomsController : Controller
    {
        IRoomService roomService;
        IBaseService baseService;
        IPriceForCategoryService priceCategoryService;
        IMapper mapper;

        public FreeRoomsController(IRoomService roomService, IBaseService baseService, IPriceForCategoryService priceCategoryService)
        {
            this.roomService = roomService;
            this.baseService = baseService;
            this.priceCategoryService = priceCategoryService;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RoomDTO, RoomModel>().ForMember
                    ("Price", rm =>
                    {
                        try
                        {
                            rm.MapFrom(c => priceCategoryService.GetAll().
                                  SingleOrDefault(p => p.Category.id == c.CategoryId && p.EndDate > DateTime.Now && p.StartDate < DateTime.Now).Price);
                        }
                        catch (NullReferenceException ex)
                        {
                            rm.Ignore();
                        }
                    });
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                }).CreateMapper();
        }

        [HttpGet]
        public ActionResult FreeRoomsNow()
        {
            var rooms = baseService.FreeRoomsForDate(DateTime.Now, DateTime.Today.AddDays(1));
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View("FreeRoomsNow", RoomModels);
        }

        [HttpGet]
        public ActionResult FreeRoomsByDate()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateResult(DateTime date)
        {
            var rooms = baseService.FreeRoomsForDate(date, date.AddDays(1));
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View(RoomModels);
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateRange()
        {
            return View();
        }

        [HttpGet]
        public ActionResult FreeRoomsByDateRangeResult(DateTime FirstDate, DateTime SecondDate)
        {
            var rooms = baseService.FreeRoomsForDate(FirstDate, SecondDate);
            var RoomModels = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(rooms);
            return View(RoomModels);
        }
    }
}
