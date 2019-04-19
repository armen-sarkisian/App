using Auth.DAO;
using Auth.DAO.Model;
using Auth.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service
{
    public class SqlCommand
    {
        ContextAuth contextAuth = null;
        List<UserClients> userClientsList = null;
        List<UserClientsEmployee> EmployeeList = null;
        UserClients userClients = null;
        UserClientsEmployee userClientsEmployee = null;

        public SqlCommand()
        {
            contextAuth = new ContextAuth();
        }

        public async Task<User> GetUserNameForView(string login)
        {
            User user = new User();
            foreach (var item in contextAuth.Users)
            {
                if (item.Login.Equals(login))
                {
                    user.CompanyName = item.CompanyName;
                    break;
                }

            }
            return user;
        }

        public async Task<User> GetUserInDb(string login, string password)
        {
            return await contextAuth.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<Admin> GetUserInDbAdmin(string login, string password)
        {
            return await contextAuth.Admin.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<User> GetTotalUsersInDb(int id)
        {
            return await contextAuth.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async void AddNewCompanyInDb(User user)
        {
            if (user != null)
            {
                await contextAuth.Users.AddAsync(user);
                await contextAuth.SaveChangesAsync();
            }
        }

        public async void AddNewClientsInDb(UserClients userClients)
        {
            if (userClients != null)
            {
                await contextAuth.UserClients.AddAsync(userClients);
                await contextAuth.SaveChangesAsync();
            }
        }

        /*public async void AddUserClientsInDb(UserClients userClients)
        {
            if (userClients != null)
            {
                await contextAuth.UserClients.AddAsync(userClients);
                await contextAuth.SaveChangesAsync();
            }
        }*/

        public async void AddUserClientsEmployeeInDb(UserClientsEmployee userClientsEmployee)
        {
            if (userClientsEmployee != null)
            {
                await contextAuth.UserClientsEmployee.AddAsync(userClientsEmployee);
                await contextAuth.SaveChangesAsync();
            }
        }
        
        public async void ArchivingUnarchiving(int id)
        {
            foreach (var item in contextAuth.Users)
            {
                if (item.Id == id)
                {
                    switch (item.isArchived)
                    { 
                        case true:
                            item.isArchived = false;
                            break;
                        case false:
                            item.isArchived = true;
                            DateTime date = DateTime.Now;
                            item.inArchive = date.ToString();
                            break;
                    }
                }
            }
            await contextAuth.SaveChangesAsync();
        }

        public async void ArchivingUnarchivingEmployee(int id)
        {
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Id == id)
                {
                    switch (item.isArchived)
                    {
                        case true:
                            item.isArchived = false;
                            break;
                        case false:
                            item.isArchived = true;
                            DateTime date = DateTime.Now;
                            item.inArchive = date.ToString();
                            break;
                    }
                }
                
            }
            await contextAuth.SaveChangesAsync();
        }
        

        public async void ArchivingUnarchivingUserClients(int id)
        {
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Id == id)
                {
                    switch (item.isArchived)
                    {
                        case true:
                            item.isArchived = false;
                            break;
                        case false:
                            item.isArchived = true;
                            DateTime date = DateTime.Now;
                            item.inArchive = date.ToString();
                            break;
                    }
                    break;
                }
            }
            await contextAuth.SaveChangesAsync();
        }

        public async void DeleteAllUsersCommand()
        {
            contextAuth.Users.RemoveRange(contextAuth.Users);
            
            
            await contextAuth.SaveChangesAsync();
        }

        public async void DeleteCompany(int Id)
        {
            
            var del = contextAuth.Users.SingleOrDefault(x => x.Id == Id);
            if (del!=null)
            {
                contextAuth.Users.Remove(del);
                await contextAuth.SaveChangesAsync();
            }
        }
        
        public async Task<User> GetCompanyInfoInDb(string companyName)
        {
            User user = new User();
            foreach (var item in contextAuth.Users)
            {
                if (item.CompanyName == companyName)
                {
                    user.Id = item.Id;
                    user.Login = item.Login;
                    user.Password = item.Password;
                    user.Date = item.Date;
                    user.CompanyName = item.CompanyName;
                    user.OwnershipType = item.OwnershipType;
                    user.Adress = item.Adress;
                    user.LegalAdress = item.LegalAdress;
                    user.CheckingAccount = item.CheckingAccount;
                    user.BankBin = item.BankBin;
                    user.UNP = item.UNP;
                    user.OKPO = item.OKPO;
                    user.ONPF = item.ONPF;
                    user.FolderLanguage = item.FolderLanguage;
                    break;
                }
            }
            return user;
        }

        public async Task<User> GetCompany(string Login, string Password)
        {
            User user = new User();
            foreach (var item in contextAuth.Users)
            {
                if (item.Login == Login && item.Password == Password)
                {
                    user.Login = item.Login;
                    user.Password = item.Password;
                    user.AccountType = item.AccountType;
                    break;
                }
            }
            return user;
        }

        public async Task<UserClients> GetCompanyClient(string Login, string Password)
        {
            UserClients userClients = new UserClients();
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login && item.Password == Password) 
                {
                    userClients.Login = item.Login;
                    userClients.Password = item.Password;
                    userClients.AccountType = item.AccountType;
                    break;
                }
            }
            return userClients;
        }

        public async Task<UserClientsEmployee> GetUserClientsEmployee(string Login, string Password)
        {
            UserClientsEmployee userClientsEmployee = new UserClientsEmployee();
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Login == Login && item.Password == Password)
                {
                    userClientsEmployee.Login = item.Login;
                    userClientsEmployee.Password = item.Password;
                    userClientsEmployee.AccountType = item.AccountType;
                    break;
                }
            }
            return userClientsEmployee;
        }

        public async Task<Boolean> CompanyIsExists(string Login)
        {
            bool flag = false;
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login)
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }
                
            }

            foreach (var item in contextAuth.Users)
            {
                if (item.Login == Login)
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }

            }
            return flag;
        }

        public async Task<Boolean> UserClientsEmployeeIsExists(string Login)
        {
            bool flag = false;
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Login == Login)
                {
                    flag = true;
                    break;
                }
                else
                {
                    flag = false;
                }

            }
            return flag;
        }

        public string UserClientsEmployeeIsExistsString(string Login)
        {
            string login = null;
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Login == Login)
                {
                    login = Login;
                    break;
                }
                else
                {
                    
                }

            }
            return login;
        }

        public async Task<String> isBookKeepingCompany(string Login)
        {
            string str = null;
            foreach (var item in contextAuth.Users)
            {
                if (item.Login == Login)
                {
                    str = Login;                     
                }

            }
            return str;
        }

        public List<UserClients> CompareToParentCompany(string AuthorizedName)
        {
            userClientsList = new List<UserClients>();
            foreach (var item in contextAuth.UserClients)
            {
               if (item.ParentCompany == AuthorizedName)
               {
                    userClients = new UserClients();
                    userClients.Id = item.Id;
                    userClients.Login = item.Login;
                    userClients.Password = item.Password;
                    userClients.Date = item.Date;
                    userClients.CompanyName = item.CompanyName;
                    userClients.isArchived = item.isArchived;
                    userClients.inArchive = item.inArchive;
                    userClientsList.Add(userClients);
               }

            }
            return userClientsList;
        }

        public List<UserClientsEmployee> GetEmployeeList(string ParentCompany)
        {
            EmployeeList = new List<UserClientsEmployee>();
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.ParentsCompanyName == ParentCompany)
                {
                    userClientsEmployee = new UserClientsEmployee();
                    userClientsEmployee.Id = item.Id;
                    userClientsEmployee.Login = item.Login;
                    userClientsEmployee.Password = item.Password;
                    userClientsEmployee.SurnameNameFathersName = item.SurnameNameFathersName;
                    userClientsEmployee.WorkPosition = item.WorkPosition;
                    userClientsEmployee.UsersType = item.UsersType;
                    userClientsEmployee.Phone = item.Phone;
                    userClientsEmployee.isArchived = item.isArchived;
                    userClientsEmployee.inArchive = item.inArchive;
                    userClientsEmployee.Date = item.Date;
                    EmployeeList.Add(userClientsEmployee);
                }

            }
            return EmployeeList;
        }

        public async Task<Boolean> isArchived(string Login, string Password)
        {
            bool flag = false;
            foreach(var item in contextAuth.Users)
            {
                if (item.Login == Login && item.Password == Password)
                {
                    if (item.isArchived == true)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public async Task<Boolean> isArchivedUserClients(string Login, string Password)
        {
            bool flag = false;
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login && item.Password == Password)
                {
                    if (item.isArchived == true)
                    {
                        flag = true;
                    }
                    else
                    {
                        flag = false;
                    }
                }
            }
            return flag;
        }

        public async Task<String> GetUserClientsName(string Login)
        {
            string companyName = "";
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login)
                {
                    companyName = item.CompanyName;
                }
            }

            return companyName;
        }

        public async Task<String> GetUserClientsCompanyName(string Login)
        {
            string companyName = "";
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login)
                {
                    companyName = item.CompanyName;
                }
            }

            return companyName;
        }
        



        public async Task<String> GetUserClientsEmployeeType(string login)
        {
            string UserType = "null";
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Login == login)
                {
                    UserType = item.UsersType;
                    break;
                }
                else
                {
                    UserType = "null";
                }
            }

            return UserType;
        }

        public async Task<String> GetUserClientsEmployeeParentsName(string login)
        {
            string ParentsName = "null";
            foreach (var item in contextAuth.UserClientsEmployee)
            {
                if (item.Login == login)
                {
                    ParentsName = item.ParentsCompanyName;
                    break;
                }
                else
                {
                    ParentsName = "null";
                }
            }

            return ParentsName;
        }

    }
}
