using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Enteties;
using Hotel.PL.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Hotel.PL.Controllers
{
    public class AccountController : Controller
    {
        private IMapper mapper;
        private IUserService userService;
        private IAccountService accountService;
        SignInManager<User> signInManager;
        UserManager<User> userManager;
        RoleManager<IdentityRole<Guid>> roleManager;

        public AccountController(IAccountService accountService, IUserService userService, SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {
            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RegisterViewModel, User>();
                    cfg.CreateMap<RegisterViewModel, RegistrationModelDTO>();
                    cfg.CreateMap<LoginViewModel, LoginModelDTO>();
                    cfg.CreateMap<UserDTO, User>();
                }).CreateMapper();

            this.userService = userService;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.accountService = accountService;
            this.roleManager = roleManager;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel regModel)
        {
            try
            {
                var user = mapper.Map<RegisterViewModel, User>(regModel);
                user.UserName = regModel.PhoneNumber;

                var result = await userManager.CreateAsync(user, regModel.Password);

                var UserRoles = from r in roleManager.Roles.ToList()
                                where r.Name == "Customer"
                                select r.Name;

                var result2 = await userManager.AddToRolesAsync(user, UserRoles);

                if (result.Succeeded && result2.Succeeded)
                {

                }
                else
                {
                    throw new ArgumentException();
                }

                return RedirectToAction("Index", "Home");
            }
            catch
            {
                return RedirectToAction("Index", "Home");
            }
        }

        [HttpGet]
        public IActionResult Login(string returnUrl = null)
        {
            return View(new LoginViewModel { ReturnUrl = returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel loginModel)
        {
            if (ModelState.IsValid)
            {
                var result = await signInManager.PasswordSignInAsync(loginModel.PhoneNumber, loginModel.Password, loginModel.RememberMe, false);

                if (result.Succeeded)
                {
                    var user = userService.GetAll().SingleOrDefault(u => u.PhoneNumber == loginModel.PhoneNumber);

                    if (!string.IsNullOrEmpty(loginModel.ReturnUrl) && Url.IsLocalUrl(loginModel.ReturnUrl))
                    {
                        return Redirect(loginModel.ReturnUrl);
                    }
                    else
                    {
                        if (userManager.IsInRoleAsync(mapper.Map<UserDTO, User>(user), "Admin").Result)
                        {
                            return RedirectToAction("Index", "Home", new { Area = "Admin" });
                        }
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Неправильный логин и (или) пароль");
                }
            }
            return View(loginModel);
        }



        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
