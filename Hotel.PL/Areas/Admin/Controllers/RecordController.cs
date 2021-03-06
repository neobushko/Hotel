using Microsoft.AspNetCore.Authorization;
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
using Hotel.PL.RequestModels;

namespace Hotel.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class RecordController : Controller
    {
        private IRecordService recordService;
        private IRoomService roomService;
        private ICategoryService categoryService;
        private IUserService userService;
         
        private IBaseService baseService;
        private IMapper mapper;

        public RecordController(IRecordService recordService, ICategoryService categoryService, IRoomService roomService, IUserService userService, IBaseService baseService, IPriceForCategoryService priceForCategoryService)
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
                cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                cfg.CreateMap<RecordModel, RecordDTO>();
                cfg.CreateMap<RecordModel, RecordRequestModel>().ReverseMap();
                cfg.CreateMap<RecordRequestModel, RecordDTO>();
            }).CreateMapper();
            this.recordService = recordService;
            this.categoryService = categoryService;
            this.roomService = roomService;
            this.userService = userService;
            this.baseService = baseService;
        }

        // GET: recordController
        public ActionResult Index()
        {
            return View();
        }

        // GET: recordController/Details/5
        public ActionResult Details(RecordModel record)
        {
            record = mapper.Map<RecordDTO, RecordModel>(recordService.Get(record.id));
            record.UserPhoneNumber = record.User.PhoneNumber;
            record.Benefit = baseService.BenefitForRecord(mapper.Map<RecordModel, RecordDTO>(record));
            return View("RecordDetails", record);
        }

        public ActionResult AllRecords()
        {
            return View(mapper.Map<IEnumerable<RecordDTO>, IEnumerable<RecordModel>>(recordService.GetAll().OrderBy(r => r.CheckIn)));
        }
        public ActionResult Create(RecordModel record)
        {
            return View("RecordCreate", record);
        }
        // GET: recordController/Create
        public ActionResult CreateRequest(RecordModel record)
        {
            var rr = mapper.Map<RecordModel, RecordRequestModel>(record);
            rr.users = userService.GetAll();
            rr.rooms = roomService.GetAll();
            return View("RecordCreateRequestModel", rr);
        }

        // POST: recordController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecordCreate(RecordModel record)
        {
            try
            {
                var recordDTO = mapper.Map<RecordModel, RecordDTO>(record);
                recordDTO.User = null;
                recordDTO.Room = null;
                recordService.Create(recordDTO);
                return RedirectToAction("AllRecords");
            }
            catch
            {
                return View();
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecordCreateRequestModel(RecordRequestModel record)
        {
            try
            {
                record.User = null;
                record.Room = null;
                var recordDTO = mapper.Map<RecordRequestModel, RecordDTO>(record);
                recordService.Create(recordDTO);
                return RedirectToAction("AllRecords");
            }
            catch
            {
                return View();
            }
        }

        // GET: recordController/Edit/5
        public ActionResult Edit(RecordModel record)
        {
            return View("RecordEdit", record);
        }

        // POST: recordController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult RecordEdit(RecordModel record)
        {
            try
            {
                var recordDTO = mapper.Map<RecordModel, RecordDTO>(record);
                recordService.Update(recordDTO);
                return RedirectToAction("AllRecords");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: recordController/Delete/5
        [HttpGet]
        public ActionResult Delete(RecordModel record)
        {
            try
            {
                recordService.Delete(record.id);
                var recordList = mapper.Map<IEnumerable<RecordDTO>, IEnumerable<RecordModel>>(recordService.GetAll().OrderBy(r => r.CheckIn));
                return View("AllRecords", recordList);
            }
            catch
            {
                return View("Error");
            }
        }

        [HttpGet]
        public ActionResult CheckFreeRooms()
        {
            return View();
        }
    }
}
