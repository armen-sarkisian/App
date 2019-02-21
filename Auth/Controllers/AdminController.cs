using System;
using System.Collections.Generic;
using System.Linq;
using Auth.DAO;
using Auth.Models;
using Auth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;


namespace Auth.Controllers
{
    public class AdminController : Controller
    {
        private ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();
        List<User> displayUsers;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult CheckName(User user)
        {
            if (string.IsNullOrEmpty(user.Login))
            {
                ModelState.AddModelError("Login", "Некорректное имя");
            }
            else if (user.Login.Length > 5)
            {
                ModelState.AddModelError("Login", "Недопустимая длина строки");
            }
            if (ModelState.IsValid)
            {
                return View("AddNewCompany");
            }

            return View(user);
        }


        [Authorize]
        public IActionResult Index()
        {
            //ViewData["AdminLogged"] = "Вы вошли как администратор";

            displayUsers = db.Users.ToList();

            return View(displayUsers);

        }
        
       

        [HttpGet]
        public IActionResult AddNewCompany()
        {
            return View("AddNewCompany");
        }

        [HttpPost]
        
        public IActionResult AddNewCompany(User user, string Login, string Password)
        {
            
            if (string.IsNullOrEmpty(user.Login) || string.IsNullOrEmpty(user.Password))
            {
                ModelState.AddModelError("Login", "Вы не ввели поле");
            }
            else if (user.Login.Length < 5 || user.Login.Length > 15 || user.Password.Length < 5 || user.Password.Length > 15)
            {
                ModelState.AddModelError("Login", "Логин и/или пароль должны быть от 5 до 15 символов");
            }
            if (ModelState.IsValid)
            {
                managerAuth.AddNewCompanyInDb(Login, Password);
                ViewData["Successfull"] = "Компания успешно сохранена!";
            }   return View("AddNewCompany");

        }

        [HttpPost]
        public IActionResult DeleteAllUsers()
        {
            managerAuth.DeleteAllUsersManager();
            return RedirectToAction("Index", "Admin");
            //return View("Index");
        }

        
        [HttpPost]
        public IActionResult DeleteCompany(int Id, string submit)
        {
            managerAuth.DeleteCompanyManager(Id);
            //ViewBag.Message = "Form submitted.";
            return RedirectToAction("Index", "Admin");
        }

    }
}