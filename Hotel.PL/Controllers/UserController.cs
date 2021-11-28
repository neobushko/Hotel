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
    public class UserController : Controller
    {
        private IUserService userService;
        private IMapper mapper;

        public UserController(IUserService userService)
        {
            mapper = new MapperConfiguration(cfg => cfg.CreateMap<UserDTO, UserModel>().ReverseMap()).CreateMapper();
            this.userService = userService;
        }

        // GET: userController
        public ActionResult Index()
        {
            return View();
        }

        // GET: userController/Details/5
        public ActionResult Details(UserModel user)
        {
            return View("UserDetails", user);
        }

        public ActionResult AllUsers()
        {
            return View(mapper.Map<IEnumerable<UserDTO>, IEnumerable<UserModel>>(userService.GetAll()));
        }

        // GET: userController/Create
        public ActionResult Create(UserModel user)
        {
            return View("UserCreate", user);
        }

        // POST: userController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserCreate(UserModel user)
        {
            try
            {
                var userDTO = mapper.Map<UserModel, UserDTO>(user);
                userService.Create(userDTO);
                return RedirectToAction("AllUsers");
            }
            catch
            {
                return View();
            }
        }

        // GET: userController/Edit/5
        public ActionResult Edit(UserModel user)
        {
            return View("UserEdit", user);
        }

        // POST: userController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult UserEdit(UserModel user)
        {
            try
            {
                var userDTO = mapper.Map<UserModel, UserDTO>(user);
                userService.Update(userDTO);
                return RedirectToAction("AllUsers");
            }
            catch
            {
                return View("Error");
            }
        }

        // GET: userController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(UserModel user)
        {
            try
            {
                userService.Delete(user.id);
                return View("AllUsers");
            }
            catch
            {
                return View("Error");
            }
        }
    }
}
