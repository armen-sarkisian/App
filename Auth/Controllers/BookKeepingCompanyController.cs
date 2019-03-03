using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.DAO;
using Auth.Models;
using Auth.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Controllers
{
    public class BookKeepingCompanyController : Controller
    {
        ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();
        public string user { get; set; }
        public string name { get; set; }
        List<UserClients> userClients = new List<UserClients>();

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> AdminPanel()
        {
            await GetUserNameForView();
            ViewData["User"] = "Вы зашли в админку компаний как: " + name;
            userClients = db.UserClients.ToList();
            return View(userClients);
        }


        public async Task<String> GetUserNameForView()
        {
            User user = await managerAuth.GetUserNameForViewManager(User.Identity.Name);
            name = user.CompanyName.ToString();
            return name;
        }

    }
}