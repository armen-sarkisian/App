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

        public async void AddCompanyToArchive(UsersArchive usersArchive)
        {
            if (usersArchive != null)
            {
                await contextAuth.UsersArchive.AddAsync(usersArchive);
                await contextAuth.SaveChangesAsync();
            }
        }


        public async void AddUserClientsInDb(UserClients userClients)
        {
            if(userClients != null)
            {
                await contextAuth.UserClients.AddAsync(userClients);
                await contextAuth.SaveChangesAsync();
            }
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

        public async Task<User> CompanyIsExists(string Login, string Password)
        {
            User user = new User();
            foreach (var item in contextAuth.Users)
            {
                if (item.Login == Login && item.Password == Password)
                {
                    user.Login = item.Login;
                    user.Password = item.Password;
                    break;
                }
            }
            return user;
        }

        public async Task<UserClients> CompanyClientIsExists(string Login, string Password)
        {
            UserClients userClients = new UserClients();
            foreach (var item in contextAuth.UserClients)
            {
                if (item.Login == Login && item.Password == Password) // дописать тут логику
                {
                    userClients.Login = item.Login;
                    userClients.Password = item.Password;
                    break;
                }
            }
            return userClients;
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

        public async Task<UsersArchive> GetUserForArchiving(int id)
        {
            UsersArchive usersArchive = new UsersArchive();
            foreach (var item in contextAuth.Users)
            {
                if (item.Id == id)
                {
                    usersArchive.Login = item.Login;
                    usersArchive.Password = item.Password;
                    usersArchive.Archived = item.Date;
                    usersArchive.CompanyName = item.CompanyName;
                    usersArchive.OwnershipType = item.OwnershipType;
                    usersArchive.Adress = item.Adress;
                    usersArchive.LegalAdress = item.LegalAdress;
                    usersArchive.CheckingAccount = item.CheckingAccount;
                    usersArchive.BankBin = item.BankBin;
                    usersArchive.UNP = item.UNP;
                    usersArchive.OKPO = item.OKPO;
                    usersArchive.ONPF = item.ONPF;
                    usersArchive.FolderLanguage = item.FolderLanguage;
                    break;
                }

            }
            return usersArchive;
        }

    }
}
