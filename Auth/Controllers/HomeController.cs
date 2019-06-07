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
using System.Security.Principal;
using System.Security.Claims;
using System.Threading;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Auth.Controllers
{
    public class HomeController : Controller
    {
        ManagerAuth managerAuth = new ManagerAuth();
        private ContextAuth db = new ContextAuth();
        public string str { get; set; }
        public string AccountName { get; set; }
        public string AccountRole { get; set; }
        public string webRootPath = "/wwwroot";
        string folder { get; set; }
        static string[] dirs { get; set; }
        static string[] files { get; set; }
        static string fullPath { get; set; }
        static string previousPath { get; set; }
        public Dictionary<string, string> ext = new Dictionary<string, string>();
        private IHostingEnvironment _env;
        string UsersRootFolder { get; set; }
        string UserClientsCompanyName { get; set; }
        string UserClientsEmployeeType { get; set; }
        List<UserClientsEmployee> Employee;


        public HomeController (IHostingEnvironment env)
        {
            _env = env;
        }
        
        List<UserClients> userClients = new List<UserClients>();

        public void GetAccountInfo()
        {
            AccountName = User.Identity.Name;
            AccountRole = User.Claims.Where(c => c.Type == ClaimTypes.Role).Select(c => c.Value).SingleOrDefault();
        }


        [Authorize]
        public async Task<IActionResult> Index(string _folder, string _action, string _file)
        {
            GetAccountInfo();
            UserClientsEmployeeType = await getUserClientsEmployeeType(AccountName);

            if (UserClientsEmployeeType == "Специалист")
            {
                GetCurrentPathForEmployeeAsync();
                ViewData["Role"] = UserClientsEmployeeType;
            }
            else if (UserClientsEmployeeType == "Руководитель")
            {
                GetCurrentPathForEmployeeAsync();
            }
            else
            {
                GetCurrentPathAsync();
            }            
            

            string name = await managerAuth.isBookKeepingCompanyManager(AccountName);
            
            if (AccountName == "admin")
            {
                return RedirectToAction("Logout", "Account");
            }
            else if (AccountName == name)
            {
                return RedirectToAction("AdminPanel", "BookKeepingCompany");
            }
            else
            {
                ViewData["UserName"] = "Здравствуйте! Вы вошли как: " + AccountName;
            }

            switch (_action)
            {
                case "OpenFolder":
                    fullPath += "/" + _folder;
                    RenderPage();
                    break;
                case "OpenFile":
                    // Путь к файлу
                    string file = System.IO.Path.Combine(UsersRootFolder + fullPath, _file);
                    // Тип файла - content-type
                    string file_type = "application/txt";
                    // Имя файла - необязательно
                    string file_name = _file;

                    return PhysicalFile(file, file_type, file_name);

                case "Back":
                    int a = fullPath.LastIndexOf("/");
                    int b = fullPath.Length;
                    fullPath = fullPath.Substring(0, a);
                    RenderPage(); //render all files and folders in view
                    break;
                case "GoToRoot":
                    fullPath = null;
                    RenderPage();
                    break;
                case "Stay":
                    RenderPage();
                    break;
                default:
                    string folder = UsersRootFolder;
                    if (AccountRole != "Employee")
                    {
                        IsExistsFolder(folder);
                        IsExistsFolder(folder + "\\" + "Первичные документы");
                        IsExistsFolder(folder + "\\" + "Отчетность");
                    }
                    RenderPage();
                    break;
            }

            ViewData["rootFolder"] = dirs;
            ViewData["Files"] = files;
            ViewData["fullPath"] = fullPath;
            ViewData["UserClientsEmployeeType"] = UserClientsEmployeeType;
            ViewData["AccountRole"] = AccountRole;
            
           
            //setting file extension 
            ViewBag.Ext = ext;

            return View(userClients);
        }

        public void IsExistsFolder(string path)
        {
           if (!Directory.Exists(path))
           {
             Directory.CreateDirectory(path);
           }
        }

        public async Task GetCurrentPathAsync()
        {
            UserClientsCompanyName = await managerAuth.GetUserClientsNameManager(AccountName);
            UsersRootFolder = _env.WebRootPath + "\\App_Data\\" + UserClientsCompanyName;
        }

        public async Task GetCurrentPathForEmployeeAsync()
        {
            UserClientsCompanyName = await managerAuth.GetUserClientsEmployeeParentsNameManager(AccountName);
            UsersRootFolder = _env.WebRootPath + "\\App_Data\\" + UserClientsCompanyName;
        }

        public void RenderPage()
        {
            dirs = Directory.GetDirectories(UsersRootFolder + fullPath);
            files = Directory.GetFiles(UsersRootFolder + fullPath);
            
            getFileNameAndExtension();
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
                  
        public PartialViewResult AddClient()
        {
            return PartialView("AddClient");
        }

        [HttpPost]
        public async Task<IActionResult> AddUserClientsEmployee(UserClientsEmployee userClientsEmployee, string Login, string Password, string SurnameNameFathersName, string WorkPosition, string UsersType, string Phone, string Email)
        {
            bool LoginExists = true;
            UserClientsCompanyName = await managerAuth.GetUserClientsNameManager(User.Identity.Name);

            
            if (ModelState.IsValid)
            {
                while (LoginExists == true)
                {
                    bool LoginExistsInBase = await managerAuth.UserClientsEmployeeIsExistsManager(Login);
                    if (LoginExistsInBase == false)
                    {
                        managerAuth.AddUserClientsEmployeeManager(
                            Login, 
                            Password, 
                            SurnameNameFathersName, 
                            WorkPosition, 
                            UsersType, 
                            Phone, 
                            Email, 
                            UserClientsCompanyName);
                        LoginExists = false;
                        return RedirectToAction("Index", "Home");

                    }
                    else
                    {
                      ViewData["Allert"] = "Пользователь существует"; //здесь указать allert что пользователь уже существует  
                    }
                }
            }
            else
            {
                IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            }

            return RedirectToAction("Index", "Home");

        }

        //method checks if Employee login already exists in base
        //and returns result in model for validating
        [AcceptVerbs("Get", "Post")]
        public IActionResult CheckLogin(string login) {
            string loginInBase = IsLoginExists(login);
            if (login == loginInBase)
                return Json(false);
            return Json(true);
        }


        public string IsLoginExists(string login)
        {
            return managerAuth.UserClientsEmployeeIsExistsStringManager(login);
        }

        [HttpPost]
        public async Task<IActionResult> AddFile(IFormFile uploadedFile, string root)
        {
            string path = null;
            GetAccountInfo();
            UserClientsEmployeeType = await getUserClientsEmployeeType(AccountName); //Узнаем тип аккаунта сотрудника - руководитель или специалист
            
            if (UserClientsEmployeeType == "Специалист")
            {
                GetCurrentPathForEmployeeAsync();
            }
            else
            {
                GetCurrentPathAsync();
            }
            
            
            if (uploadedFile != null)
            {
                // путь к папке Files
                if (fullPath != null)
                {
                    path = UsersRootFolder + "\\" + fullPath + "\\" + uploadedFile.FileName;
                }
                else
                {
                     path = UsersRootFolder + "\\" + uploadedFile.FileName;
                }
                // сохраняем файл по пути: App_Data + папка, название которой равно имени компании 
                using (var fileStream = new FileStream(path, FileMode.Create))
                {
                    await uploadedFile.CopyToAsync(fileStream);
                }
                
            }

            return RedirectToAction("Index", "Home", new { _action = "Stay" });
        }

        
        public ActionResult AddNewEmployee()
        {
            return PartialView("AddNewEmployee");
        }

        public ActionResult AddNewFile()
        {
            return PartialView("AddNewFile");
        }

        //работает лишь для userClientsEmployee
        public async Task<string> getUserClientsEmployeeType(string login)
        {
            string s = await managerAuth.GetUserClientsEmployeeTypeManager(login);
            return s.ToString();
        }

        public ActionResult Settings()
        {
            return PartialView("Settings");
        }

        
        public ActionResult EmployeeList(int id)
        {
            string companyName = managerAuth.GetUserClientsCompanyNameManager(User.Identity.Name);
            Employee = db.UserClientsEmployee.Where(a => a.ParentsCompanyName.Contains(companyName)).ToList();
            return PartialView(Employee);
        }

        
        public ActionResult ArchivingUnarchivingEmployee(int id)
        {
            managerAuth.ArchivingUnarchivingEmployeeManager(id);
            return RedirectToAction("EmployeeList", "Home");
        }

        public ActionResult ChangeAccountType(int no)
        {
            managerAuth.ChangeAccountTypeManager(no);
            return RedirectToAction("EmployeeList", "Home");
        }
    }
}
