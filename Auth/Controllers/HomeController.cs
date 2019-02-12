﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Auth.Models;
using Microsoft.AspNetCore.Authorization;
using Auth.Service;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        ManagerAuth managerAuth = new ManagerAuth();
        public string str { get; set; }
        public string user { get; set; }


        [Authorize]
        public IActionResult Index(string user)
        {
            ViewData["User"] = "Вы вошли как: " + User.Identity.Name;
           return View();
        }

        public void OnPost(string name, int age)
        {
            user = $"Имя: {name}  Возраст: {age}";
        }

        [HttpGet]
        public IActionResult AddClient() 
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddClientBtn(string CompanyName, string OwnershipType, string Adress, string LegalAdress, int CheckingAccount, string BankBin, int UNP,
            int OKPO, int ONPF, string FolderLanguage)
        {
            managerAuth.AddUserClientsInDb(CompanyName, OwnershipType, LegalAdress, Adress, CheckingAccount, BankBin, UNP, OKPO, ONPF, FolderLanguage);
            return View("AddClient");
        }

        
    }
}
