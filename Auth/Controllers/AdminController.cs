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
        List<User> newUsers;
        List<User> list = new List<User>();
        UsersArchive usersArchive;

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
            displayUsers = db.Users.ToList();
            return View(displayUsers);
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

        public IActionResult AddNewCompany(User user, string Login, string Password, string CompanyName, string OwnershipType, string Adress, string LegalAdress, int CheckingAccount, string BankBin, int UNP,
            int OKPO, int ONPF, string FolderLanguage)
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
                managerAuth.AddNewCompanyInDb(Login, Password, CompanyName, OwnershipType, LegalAdress, Adress, CheckingAccount, BankBin, UNP, OKPO, ONPF, FolderLanguage);
                ViewData["Successfull"] = "Компания успешно сохранена!";
            }
            else { ViewData["Successfull"] = "Ошибка"; }
            return View("AddNewCompany");
        }

        [HttpPost]
        public IActionResult DeleteAllUsers()
        {
            managerAuth.DeleteAllUsersManager();
            return RedirectToAction("Index", "Admin");
            //return View("Index");
        }


        [HttpPost]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            //managerAuth.DeleteCompanyManager(Id);
            //ViewBag.Message = "Form submitted.";

            usersArchive = new UsersArchive();
            usersArchive = await managerAuth.GetUserForArchivingManager(id);
            managerAuth.AddCompanyToArchiveManager(usersArchive);
            managerAuth.DeleteCompanyManager(id);


            return RedirectToAction("Index", "Admin");
        }

        public void Arhive(int id)
        {

        }

        /*[HttpPost]
        public async Task<IActionResult> Archiving(int Id)
        {
            usersArchive = new UsersArchive();
            usersArchive = await managerAuth.GetUserForArchivingManager(Id);
            managerAuth.AddCompanyToArchiveManager(usersArchive);
            managerAuth.DeleteCompanyManager(Id);
            return RedirectToAction("Index", "Admin");
        }*/
    }
}