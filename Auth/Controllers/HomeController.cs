using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Auth.Service;
using Auth.DAO;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();
        public string str { get; set; }
        public string user { get; set; }
        List<UserClients> userClients = new List<UserClients>();
        
        
        [Authorize]
        public async Task <IActionResult> Index()
        {
            string name = await managerAuth.isBookKeepingCompanyManager(User.Identity.Name);
            if (User.Identity.Name == "admin")
            {
                return RedirectToAction("Logout", "Account");
            }
            else if (User.Identity.Name == name) 
            {
                return RedirectToAction("AdminPanel", "BookKeepingCompany");
            }
            else
            {
                //string nameForView = await GetUserNameForView();
                //ViewData["User"] = "Вы вошли как: " + nameForView;
                userClients = db.UserClients.ToList();
            }
            return View(userClients);
        }

        
       
        [HttpGet]
        [Authorize]
        public IActionResult AddClient() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClientBtn(string CompanyName, string OwnershipType, string Adress, string LegalAdress, int CheckingAccount, string BankBin, int UNP,
            int OKPO, int ONPF, string FolderLanguage)
        {
            var parentCompany = User.Identity.Name;
            managerAuth.AddUserClientsInDb(CompanyName, OwnershipType, LegalAdress, Adress, CheckingAccount, BankBin, UNP, OKPO, ONPF, FolderLanguage, parentCompany);
            return View("AddClient");
        }

        
    }
}
