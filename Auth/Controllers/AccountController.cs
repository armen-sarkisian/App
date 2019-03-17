using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.DAO.Model;
using Auth.Models;
using Auth.Service;
using Auth.ViewModels;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Auth.Controllers
{
    public class AccountController : Controller
    {
        private ManagerAuth managerAuth = new ManagerAuth();
        List<User> list = new List<User>();
        
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
                if (!(string.IsNullOrWhiteSpace(model.Email) || string.IsNullOrWhiteSpace(model.Password)))
                {
                    User user = await managerAuth.GetCompanyManager(model.Email, model.Password);
                    UserClients userClients = await managerAuth.GetCompanyClientManager(model.Email, model.Password);
                    if (model.Email == "admin" && model.Password == "admin")
                    {
                        Admin admin = await managerAuth.GetUserAsyncAdmin(model.Email, model.Password);
                        await Authenticate(model.Email);
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (model.Email == user.Login && model.Password == user.Password)
                    {
                        bool flag = await managerAuth.isArchivedManager(model.Email, model.Password);
                        if (flag == false)
                        {
                            await Authenticate(model.Email); // аутентификация 
                            return RedirectToAction("AdminPanel", "BookKeepingCompany");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Доступ в панель администратора запрещен.");
                        }
                    }
                    else if (model.Email == userClients.Login && model.Password == userClients.Password)
                    {
                        bool flag = await managerAuth.isArchivedUserClientsManager(model.Email, model.Password);
                        if (flag == false)
                        {
                            await Authenticate(model.Email); // аутентификация 
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError("Error", "Вход запрещен администратором.");
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("Error", "некорректные логин и(или) пароль");
                    }
                }
            }
            else
            {
                ModelState.AddModelError("Error", "Вы не ввели логин и(или) пароль");
            }
            return View(model);
        }

        private async Task Authenticate(string userName)
        {
            
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
           
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}