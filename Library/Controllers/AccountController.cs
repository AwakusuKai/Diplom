using BusinessLogicLayer.DTO;
using BusinessLogicLayer.Interfaces;
using BusinessLogicLayer.Mappers;
using Library.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Library.Controllers
{
    public class AccountController : Controller
    {

        IUserService UserService;

        public AccountController(IUserService userService)
        {
            UserService = userService;
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                if (UserService.IsUserIdUnique(model.Id))
                {
                    // добавляем пользователя в бд
                    User user = new User { Id = model.Id, Name = model.Name, Surname = model.Surname, Password = model.Password, RoleId = 2};
                    user.Role = new Role { Id = 2, Name = "user" };
                    UserDTO userDTO = new UserDTO { Id = model.Id, Name = model.Name, Surname = model.Surname, Password = model.Password, RoleId = 2 };
                    UserService.CreateUser(userDTO);

                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Home");
                }
                else
                    ModelState.AddModelError("", "Такой пользователь уже существует");
            }
            return View(model);
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                UserDTO userDTO = UserService.GetUser(model.Id);
                User user = Mapper.Convert<UserDTO, User>(userDTO);
                user.Role = Mapper.Convert<RoleDTO, Role>(userDTO.Role);
                if (user != null && (user.Password == model.Password))
                {
                    await Authenticate(user); // аутентификация

                    return RedirectToAction("Index", "Book");
                }
                ModelState.AddModelError("", "Некорректные логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(User user)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.Id.ToString()),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role?.Name.Replace(" ", ""))
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType,
                ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
