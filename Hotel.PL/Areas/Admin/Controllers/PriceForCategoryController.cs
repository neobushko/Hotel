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
    public class PriceForCategoryController : Controller
    {
        private IPriceForCategoryService priceCategoryService;
        private IMapper mapper;

        public PriceForCategoryController(IPriceForCategoryService priceCategoryService)
        {
            mapper = new MapperConfiguration(cfg => 
            { 
                cfg.CreateMap<PriceForCategoryDTO, PriceForCategoryModel>().ReverseMap();
                cfg.CreateMap<CategoryDTO, CategoryModel>().ReverseMap();
            }).CreateMapper();
            this.priceCategoryService = priceCategoryService;
        }

        // GET: priceCategoryController
        public ActionResult Index()
        {
            return View();
        }

        // GET: priceCategoryController/Details/5
        public ActionResult Details(PriceForCategoryModel priceCategory)
        {
            priceCategory = mapper.Map<PriceForCategoryDTO, PriceForCategoryModel>(priceCategoryService.Get(priceCategory.id));
            return View("PriceDetails", priceCategory);
        }

        public ActionResult AllPrices()
        {
            return View(mapper.Map<IEnumerable<PriceForCategoryDTO>, IEnumerable<PriceForCategoryModel>>(priceCategoryService.GetAll()));
        }

        // GET: priceCategoryController/Create
        public ActionResult Create(PriceForCategoryModel priceCategory)
        {
            return View("PriceCreate", priceCategory);
        }

        // POST: priceCategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PriceCreate(PriceForCategoryModel priceCategory)
        {
            try
            {
                var priceCategoryDTO = mapper.Map<PriceForCategoryModel, PriceForCategoryDTO>(priceCategory);
                priceCategoryDTO.Category = null;
                priceCategoryService.Create(priceCategoryDTO);
                return RedirectToAction("AllPrices");
            }
            catch
            {
                return View();
            }
        }

        // GET: priceCategoryController/Edit/5
        public ActionResult Edit(PriceForCategoryModel priceCategory)
        {
            return View("PriceEdit", priceCategory);
        }

        // POST: priceCategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult PriceEdit(PriceForCategoryModel priceCategory)
        {
            try
            {
                var priceCategoryDTO = mapper.Map<PriceForCategoryModel, PriceForCategoryDTO>(priceCategory);
                priceCategoryService.Update(priceCategoryDTO);
                return RedirectToAction("AllPrices");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: priceCategoryController/Delete/5
        [HttpGet]
        public ActionResult Delete(PriceForCategoryModel price)
        {
            try
            {
                priceCategoryService.Delete(price.id);
                return View("AllPrices");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
