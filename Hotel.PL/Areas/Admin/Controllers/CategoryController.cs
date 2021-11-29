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
    public class CategoryController : Controller
    {
        private IMapper mapper;
        private IPriceForCategoryService priceForCategoryService;
        private ICategoryService categoryService;

        public CategoryController( IPriceForCategoryService _priceForCategoryService, ICategoryService categoryService)
        {
            this.categoryService = categoryService;
            this.priceForCategoryService = _priceForCategoryService;

            mapper = new MapperConfiguration(cfg => {
                cfg.CreateMap<CategoryDTO, CategoryModel>().
                ForMember("Price", rm =>
                {
                    try
                    {
                        rm.MapFrom(c => priceForCategoryService.GetAll().
                              SingleOrDefault(p => p.Category.id == c.id && p.EndDate > DateTime.Now && p.StartDate < DateTime.Now).Price);
                    }
                    catch (NullReferenceException ex)
                    {
                        rm.Ignore();
                    }
                });
                cfg.CreateMap<RoomModel, RoomDTO>();
                cfg.CreateMap<PriceForCategoryDTO, PriceForCategoryModel>().ReverseMap();
                cfg.CreateMap<CategoryModel, CategoryDTO>();
            }).CreateMapper();
        }
        // GET: CategoryController
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult AllCategories()
        {
            return View(mapper.Map<IEnumerable<CategoryDTO>, IEnumerable<CategoryModel>>(categoryService.GetAll()));
        }
        // GET: CategoryController/Details/5
        public ActionResult Details(CategoryModel category)
        {
            return View("CategoryDetails", category);
        }

        // GET: RoomController/Create
        public ActionResult Create(CategoryModel category)
        {
            return View("CategoryCreate", category);
        }

        // POST: RoomController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryCreate(CategoryModel category)
        {
            try
            {
                var categoryDTO = mapper.Map<CategoryModel, CategoryDTO>(category);
                categoryService.Create(categoryDTO);
                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View();
            }
        }

        // GET: RoomController/Edit/5
        public ActionResult Edit(CategoryModel category)
        {
            return View("CategoryEdit", category);
        }

        // POST: RoomController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CategoryEdit(CategoryModel category)
        {
            try
            {
                var categoryDTO = mapper.Map<CategoryModel, CategoryDTO>(category);
                categoryService.Update(categoryDTO);
                return RedirectToAction("AllCategories");
            }
            catch
            {
                return View("Error");
            }
        }

        // POST: CategoryController/Delete/5
        [HttpGet]
        public ActionResult Delete(CategoryModel category)
        {
            try
            {
                categoryService.Delete(category.id);
                return View("AllCategories");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
