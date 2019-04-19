using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Auth.DAO;
using Auth.DAO.Model;
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
        List<User> list = new List<User>();
        
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
            if (User.Identity.Name == "Admin" || User.Identity.Name == "admin")
            {
                displayUsers = db.Users.ToList();
                return View(displayUsers);
            }
            else
            {
                return RedirectToAction("Logout", "Account");
            }
        }

        [HttpGet]
        [Authorize]
        public IActionResult GetCompanyInfo()
        {
            return View(list.ToList());
        }


        [HttpPost]
        public async Task<IActionResult> GetCompanyInfo(string CompanyName)
        {
            User user = await managerAuth.GetCompanyInfoManager(CompanyName);
            list.Add(user);

            return View(list.ToList());
        }

        [HttpGet]
        [Authorize]
        public IActionResult AddNewCompany()
        {
            return View("AddNewCompany");
        }

        [HttpPost]
        public async Task<IActionResult> AddNewCompany(User user, string Login, string Password, string CompanyName, string OwnershipType, string Adress, string LegalAdress, string CheckingAccount, string BankName, string BankBin, string UNP,
            string OKPO, string ONPF, string FolderLanguage)
        {
            if (ModelState.IsValid)
            {
                bool flag = await managerAuth.CompanyIsExistsManager(Login);
                if (flag == false)
                {
                    managerAuth.AddNewCompanyInDb(Login, Password, CompanyName, OwnershipType, LegalAdress, Adress, CheckingAccount, BankName, BankBin, UNP, OKPO, ONPF, FolderLanguage);
                    ViewData["Successfull"] = "Компания успешно сохранена!";
                }
                else
                {
                    ViewData["Unsuccessfull"] = "Этот логин занят. Введите другой логин для компании.";
                }
            }
            else
            {
                ViewData["Successfull"] = "Ошибка";
            }
            return View("AddNewCompany");
        }

        [HttpPost]
        public IActionResult DeleteAllUsers()
        {
            managerAuth.DeleteAllUsersManager();
            return RedirectToAction("Index", "Admin");
        }


        [HttpPost]
        public async Task<IActionResult> ArchivingUnarchivingCompany(int id)
        {
            managerAuth.ArchivingUnarchivingManager(id);
            return RedirectToAction("Index", "Admin");
        }

    }
}