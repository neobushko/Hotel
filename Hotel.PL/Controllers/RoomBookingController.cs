using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.PL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Controllers
{
    [Authorize(Roles = "Customer, Admin")]
    public class RoomBookingController : Controller
    {
        IMapper mapper;
        IRecordService recordService;
        IRoomService roomService;
        IUserService userService;
        IPriceForCategoryService priceCategoryService;
        ICategoryService categoryService;

        public RoomBookingController(IRecordService bookingService, IRoomService roomService, IUserService userService, IPriceForCategoryService priceCategoryService, ICategoryService categoryService, IPriceForCategoryService priceForCategoryService)
        {
            mapper = new MapperConfiguration(cfg =>
            {
                cfg.CreateMap<RecordDTO, RecordModel>().ForMember("CategoryName", rm =>
                {
                    try
                    {
                        rm.MapFrom(c => categoryService.GetAll().
                              SingleOrDefault(p => p.id == c.Room.CategoryId).Name);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                }).ForMember("RoomNumber", rm =>
                {
                    try
                    {
                        rm.MapFrom(c => roomService.Get(c.RoomId).Number);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                }).ForMember("UserName", rm =>
                {
                    try
                    {
                        rm.MapFrom(c => userService.Get(c.UserId).Name);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                }).ForMember("Price", rm =>
                {
                    try
                    {
                        rm.MapFrom(record => priceForCategoryService.GetAll().
                              SingleOrDefault(p => p.Category.id == record.Room.Category.id && p.EndDate > DateTime.Now && p.StartDate < DateTime.Now).Price);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                });
                cfg.CreateMap<UserDTO, UserModel>().ReverseMap();
                cfg.CreateMap<RoomDTO, RoomModel>().ForMember("Price", rm =>
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
                cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                cfg.CreateMap<RecordModel, RecordDTO>();
                
            }).CreateMapper();

            this.recordService = bookingService;
            this.userService = userService;
            this.roomService = roomService;
            this.priceCategoryService = priceCategoryService;
            this.categoryService = categoryService;

        }


        [HttpGet]
        public ActionResult DoRecordRoom(string id)
        {
            var roomModel = mapper.Map<RoomDTO, RoomModel>(roomService.Get(new Guid(id)));
            var recordModel = new RecordModel();
            recordModel.RoomId = roomModel.id;
            recordModel.CategoryName = roomModel.Category.Name;
            recordModel.Price = roomModel.Price;
            recordModel.RoomNumber = roomModel.Number;
            return View("DoRecord", recordModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult DoRecord(RecordModel record)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    record.UserId = mapper.Map<UserDTO, UserModel>(userService.GetByPhoneNumber(User.Identity.Name)).id;
                    recordService.Create(mapper.Map<RecordModel, RecordDTO>(record));
                    return RedirectToAction("AllRooms", "Room");
                }
                catch (ArgumentException ex)
                {
                    ModelState.AddModelError("record.CheckIn", "Room is booked already for this dates");
                }
            }
            else
            {

            }
            return View(record);
        }
    }
}
