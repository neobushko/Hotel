using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Enteties;
using Hotel.PL.Models;
using Hotel.PL.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Controllers
{
    [Authorize(Roles = "Admin, Customer")]
    public class MyAccountController : Controller
    {
        IUserService userService;
        IRecordService recordService;
        IMapper mapper;
        UserManager<User> userManager;
        SignInManager<User> signInManager;



        public MyAccountController(IUserService userService, IRecordService recordService, UserManager<User> userManager, SignInManager<User> signInManager, ICategoryService categoryService, IPriceForCategoryService priceForCategoryService, IRoomService roomService)
        {
            this.userService = userService;
            this.recordService = recordService;
            this.userManager = userManager;
            this.signInManager = signInManager;

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<UserDTO, UserModel>();
                    cfg.CreateMap<UserDTO, User>();
                    cfg.CreateMap<RecordModel, RecordDTO>();
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
                    cfg.CreateMap<RoomDTO, RoomModel>().ForMember("Price", rm =>
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
                    cfg.CreateMap<CategoryDTO, CategoryModel>();
                }).CreateMapper();


        }

        // GET: MyAccountController
        public ActionResult Index()
        {
            var user = userService.GetAll().SingleOrDefault(u => u.PhoneNumber == User.Identity.Name);

            return View(mapper.Map<UserDTO, UserModel>(user));
        }


        // GET: MyAccountController/Edit/5
        [HttpGet]
        public ActionResult Edit()
        {
            var user = userService.GetByPhoneNumber(User.Identity.Name);
            return View(mapper.Map<UserDTO, UserModel>(user));
        }

        [HttpPost]
        public async Task<IActionResult> Edit(UserModel model)
        {
            if (ModelState.IsValid)
            {
                User user = userManager.FindByNameAsync(User.Identity.Name).Result;
                if (user != null)
                {
                    var OldName = user.UserName;

                    user.Name = model.Name;
                    user.UserName = model.PhoneNumber;
                    user.PhoneNumber = model.PhoneNumber;
                    user.Email = model.Email;


                    var result = await userManager.UpdateAsync(user);
                    if (result.Succeeded)
                    {
                        if (OldName != user.UserName)
                            await signInManager.SignOutAsync();

                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError("PhoneNumber", "Пользователь с таким номером телефона уже существует");
                        }
                    }
                }
            }
            return View(model);
        }


        [HttpGet]
        public ActionResult ChangePassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                User user = await userManager.FindByNameAsync(User.Identity.Name);
                if (user != null)
                {
                    var _passwordValidator =
                        HttpContext.RequestServices.GetService(typeof(IPasswordValidator<User>)) as IPasswordValidator<User>;
                    var _passwordHasher =
                        HttpContext.RequestServices.GetService(typeof(IPasswordHasher<User>)) as IPasswordHasher<User>;

                    IdentityResult result =
                        await userManager.ChangePasswordAsync(user, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        user.PasswordHash = _passwordHasher.HashPassword(user, model.NewPassword);
                        await userManager.UpdateAsync(user);
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        foreach (var error in result.Errors)
                        {
                            ModelState.AddModelError(string.Empty, error.Description);
                        }
                    }
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "User is not found");
                }
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult MyRecords()
        {
            var myRecords = recordService.GetAll().Where(b => b.User.PhoneNumber == User.Identity.Name);
            var myBokingsModel = mapper.Map<IEnumerable<RecordDTO>, IEnumerable<RecordModel>>(myRecords);

/*            foreach (var bm in myBokingsModel)
            {
                bm.Price = (bm.Id);
            }*/

            return View(myBokingsModel);
        }
    }
}
