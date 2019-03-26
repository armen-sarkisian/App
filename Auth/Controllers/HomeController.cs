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
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting;
using System.IO;
using System.Collections;
using System.Net.Http;
using Auth.DAO.Model;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();
        public string str { get; set; }
        public string user { get; set; }
        public string webRootPath = "\\wwwroot";
        string folder { get; set; }
        static string[] dirs { get; set; }
        static string fullPath { get; set; }
        static string previousPath { get; set; }

        
        List<UserClients> userClients = new List<UserClients>();

        [Authorize]
        public async Task<IActionResult> Index(string _folder, string _action)
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
                userClients = db.UserClients.ToList();
            }

            
            switch (_action)
            {
                case "Open":
                    fullPath += "\\" + _folder;
                    dirs = Directory.GetDirectories("wwwroot\\" + fullPath);
                    break;
                case "Back":
                    int a = fullPath.LastIndexOf("\\");
                    int b = fullPath.Length;
                    fullPath = fullPath.Substring(0, a);
                    dirs = Directory.GetDirectories("wwwroot\\" + fullPath);
                    break;
                case "GoToRoot":
                    fullPath = null;
                    dirs = Directory.GetDirectories("wwwroot");
                    break;
                default:
                    dirs = Directory.GetDirectories("wwwroot");
                    break;
            }
            ViewData["ChooseFolder"] = dirs;
            ViewData["fullPath"] = fullPath;

                 
            /*foreach (var i in dirs)
            {
                int index = i.LastIndexOf("\\");
                list.Add(i.Substring(index + 1));
            }

            /*foreach (string dir in Directory.GetDirectories(webRootPath, "*", SearchOption.AllDirectories))
            {
                if (Directory.GetFiles(dir, "*.png").Length > 0)
                    picFolders.Add((Directory.GetFiles(dir, "*.png").ToString()));
            }*/


            return View();
        }

        [HttpPost]
        public IActionResult OpenFolder(string folder)
        {
            return RedirectToAction("Index", "Home", new { _folder = folder, _action = "Open" });
        }

        [HttpPost]
        public IActionResult Back()
        {
            return RedirectToAction("Index", "Home", new { _action = "Back" });
        }

        [HttpPost]
        public IActionResult GoToRoot()
        {
            return RedirectToAction("Index", "Home", new { _action = "GoToRoot" });
        }


        [HttpGet]
        [Authorize]
        public IActionResult AddClient() 
        {
            return View();
        }

    }
}
