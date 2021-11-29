using AutoMapper;
using Hotel.BLL.DTO;
using Hotel.BLL.Interfaces;
using Hotel.DAL.Enteties;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel.BLL.Services
{
    public class AccountService : IAccountService
    {
        SignInManager<User> signInManager;
        UserManager<User> userManager;
        RoleManager<IdentityRole<Guid>> roleManager;
        IMapper mapper;
        public AccountService(SignInManager<User> signInManager, UserManager<User> userManager, RoleManager<IdentityRole<Guid>> roleManager)
        {

            mapper = new MapperConfiguration(
                cfg =>
                {
                    cfg.CreateMap<RegistrationModelDTO, User>();
                }).CreateMapper();

            this.signInManager = signInManager;
            this.userManager = userManager;
            this.roleManager = roleManager;
        }
    }
}
