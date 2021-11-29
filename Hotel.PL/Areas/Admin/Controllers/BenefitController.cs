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

namespace Hotel.PL.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BenefitController : Controller
    {
        private IBaseService baseService;
        private IRecordService recordService;
        IMapper mapper;
        public BenefitController(IBaseService baseService, IRecordService recordService, ICategoryService categoryService, IRoomService roomService, IUserService userService)
        {
            this.baseService = baseService;
            this.recordService = recordService;
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
                });
                cfg.CreateMap<UserDTO, UserModel>().ReverseMap();
                cfg.CreateMap<RoomDTO, RoomModel>().ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
                cfg.CreateMap<RecordModel, RecordDTO>();
                cfg.CreateMap<BenefitPeriod, BenefitModel>().ReverseMap();
            }).CreateMapper();
        }

        // GET: BaseController/Details/5
        public ActionResult BenefitWithRecords()
        {
            IEnumerable<RecordModel> records = mapper.Map<IEnumerable<RecordDTO>, IEnumerable<RecordModel>>(recordService.GetAll());
            return View("BenefitDateInput");
        }


        // POST: BaseController/Create
        [HttpGet]
        public ActionResult BenefitPeriodCalc(BenefitModel benefit)
        {
            try
            {
                benefit = mapper.Map<BenefitPeriod, BenefitModel>(baseService.BenefitForPeriod(benefit.StartPeriod, benefit.EndPeriod));
                return View("BenefitPeriod", benefit);
            }
            catch
            {
                return View();
            }
        }
    }
}
