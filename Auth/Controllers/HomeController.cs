using System;
using System.Collections.Generic;
using System.Web;
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
using System.Net;
using Microsoft.AspNetCore.Http;

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
        static string[] files { get; set; }
        static string fullPath { get; set; }
        static string previousPath { get; set; }
        public Dictionary<string, string> ext = new Dictionary<string, string>();
        private IHostingEnvironment _env;

        public HomeController (IHostingEnvironment env)
        {
            _env = env;
        }
        
        List<UserClients> userClients = new List<UserClients>();

        [Authorize]
        public async Task<IActionResult> Index(string _folder, string _action, string _file)
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
                case "OpenFolder":
                    fullPath += "\\" + _folder;
                    dirs = Directory.GetDirectories("wwwroot\\" + fullPath);
                    files = Directory.GetFiles("wwwroot\\" + fullPath);
                    getFileNameAndExtension();
                    break;
                case "OpenFile":
                    var webRoot = _env.WebRootPath;
                    // Путь к файлу
                    string file = System.IO.Path.Combine(webRoot + "\\" + fullPath, _file);
                    // Тип файла - content-type
                    string file_type = "application/txt";
                    // Имя файла - необязательно
                    string file_name = _file;

                    return PhysicalFile(file, file_type, file_name);

                case "Back":
                    int a = fullPath.LastIndexOf("\\");
                    int b = fullPath.Length;
                    fullPath = fullPath.Substring(0, a);
                    dirs = Directory.GetDirectories("wwwroot\\" + fullPath);
                    files = Directory.GetFiles("wwwroot\\" + fullPath);
                    getFileNameAndExtension();
                    break;
                case "GoToRoot":
                    fullPath = null;
                    dirs = Directory.GetDirectories("wwwroot");
                    files = Directory.GetFiles("wwwroot\\" + fullPath);
                    getFileNameAndExtension();
                    break;
                default:
                    dirs = Directory.GetDirectories("wwwroot");
                    files = Directory.GetFiles("wwwroot\\" + fullPath);
                    getFileNameAndExtension();
                    break;

            }

            ViewData["rootFolder"] = dirs;
            ViewData["Files"] = files;
            ViewData["fullPath"] = fullPath;
            ViewBag.Ext = ext;

            return View();
        }

        public void getFileNameAndExtension()
        {
            FileInfo fileExtension;
            foreach (var item in files)
            {
                fileExtension = new FileInfo(item);
                string extension = fileExtension.Extension;
                int a = item.LastIndexOf("\\");
                string s = item.Substring(a + 1);
                ext.Add(s, extension);
            }
        }

        [HttpPost]
        public IActionResult OpenFolder(string folder)
        {
            return RedirectToAction("Index", "Home", new { _folder = folder, _action = "OpenFolder" });
        }

        [HttpPost]
        public IActionResult OpenFile(string file)
        {
            return RedirectToAction("Index", "Home", new { _file = file, _action = "OpenFile" });
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
