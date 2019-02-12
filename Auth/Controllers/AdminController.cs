using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auth.DAO;
using Auth.DAO.Model;
using Auth.Models;
using Auth.Service;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class AdminController : Controller
    {
        private ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Index()
        {

            ViewData["User2"] = "Вы вошли как администратор";

            List<User> displayUsers = db.Users.ToList();
            return View(displayUsers);

            //return View("AdminPanel");
        }

    }
}