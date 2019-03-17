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
            
            
            return View(CompareToParentCompany(User.Identity.Name));
        }

        public List<UserClients> CompareToParentCompany(string AuthorizedName)
        {
            return managerAuth.CompareToParentCompanyManager(AuthorizedName);
        }

        public async Task<String> GetUserNameForView()
        {
            User user = await managerAuth.GetUserNameForViewManager(User.Identity.Name);
            name = user.CompanyName.ToString();
            return name;
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddNewClient()
        {
            return View("AddNewClient");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewClient(UserClients userClients, string Login, string Password, string CompanyName, string OwnershipType, string Adress, string LegalAdress, string CheckingAccount, string BankName, string BankBin, string UNP,
            string OKPO, string ONPF, string FolderLanguage)
        {

            if (ModelState.IsValid)
            {
                bool flag = await managerAuth.CompanyIsExistsManager(Login);
                if (flag == false)
                {
                    managerAuth.AddNewClientInDb(Login, Password, CompanyName, OwnershipType, LegalAdress, Adress, CheckingAccount, BankName, BankBin, UNP, OKPO, ONPF, FolderLanguage, User.Identity.Name);
                    ViewData["Successfull"] = "Клиент успешно сохранен!";
                }
                else
                {
                    ViewData["Unsuccessfull"] = "Этот логин занят. Введите другой логин для клиента.";
                }
            }
            else
            {
                ViewData["Successfull"] = "Ошибка";
            }
            return View("AddNewClient");
        }

        [HttpPost]
        public async Task<IActionResult> ArchivingUnarchivingUserClients(int id)
        {
            managerAuth.ArchivingUnarchivingUserClientsManager(id);
            return RedirectToAction("AdminPanel", "BookKeepingCompany");
        }

    }
}