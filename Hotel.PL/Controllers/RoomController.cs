using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Controllers
{
    public class RoomController : Controller
    {
        private IRoomService roomService;
        private IMapper mapper;
        private IPriceForCategoryService priceForCategoryService;
        private ICategoryService categoryService;

        public RoomController(IRoomService roomService, IPriceForCategoryService priceForCategoryService, ICategoryService categoryService)
        {
            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<RoomDTO, RoomModel>()
                .ForMember("Price", rm =>
                {
                    try
                    {
                        rm.MapFrom(c => priceForCategoryService.GetAll().
                              SingleOrDefault(p => p.Category.id == c.Category.id && p.EndDate > DateTime.Now && p.StartDate < DateTime.Now).Price);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                });
                cfg.CreateMap<RoomModel, RoomDTO>();
                cfg.CreateMap<CategoryModel, CategoryDTO>().ReverseMap();
            }).CreateMapper();
            this.roomService = roomService;
            this.categoryService = categoryService;
        }

        // GET: RoomController
        public ActionResult Index()
        {
            return View();
        }

        // GET: RoomController/Details/5
        public ActionResult Details(RoomModel room)
        {
            room.Category = mapper.Map<CategoryDTO, CategoryModel>(categoryService.Get(room.CategoryId));
            return View("RoomDetails", room);
        }

        public ActionResult AllRooms()
        {
            return View(mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.GetAll().Where(r => r.IsActive == true)));
        }
    }
}
