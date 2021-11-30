using Microsoft.AspNetCore.Authorization;
using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.BLL.Services;
using Hotel.PL.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Hotel.PL.RequestModels;

namespace Hotel.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
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
                        rm.MapFrom(r => priceForCategoryService.GetAll().
                              SingleOrDefault(p => p.Category.id == r.Category.id && p.EndDate > DateTime.Now && p.StartDate < DateTime.Now).Price);
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
            return View("RoomDetails",room);
        }

        public ActionResult AllRooms()
        {
            return View(mapper.Map<IEnumerable<RoomDTO>,IEnumerable<RoomModel>>(roomService.GetAll()));
        }

        // GET: RoomController/Create
        public ActionResult Create(RoomModel room)
        {
            return View("RoomCreate", room);
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomCreate(RoomModel room)
        {
            try
            {
                var roomDTO = mapper.Map<RoomModel, RoomDTO>(room);
                roomService.Create(roomDTO);
                return RedirectToAction("AllRooms");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(RoomModel room)
        {
            return View("RoomEdit",room);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RoomEdit( RoomModel room)
        {
            try
            {
                var roomDTO = mapper.Map<RoomModel, RoomDTO>(room);
                roomDTO.Category = categoryService.Get(room.CategoryId);
                roomService.Update(roomDTO);
                return RedirectToAction("AllRooms");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: RoomController/Delete/5
        [HttpGet]
        public ActionResult Delete(RoomModel room)
        {
            try
            {
                roomService.Delete(room.id);
                var roomList = mapper.Map<IEnumerable<RoomDTO>, IEnumerable<RoomModel>>(roomService.GetAll());
                return View("AllRooms", roomList);
            }
            catch
            {
                return View("Error");
            }
        }

       
    }
}
