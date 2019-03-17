using Auth.DAO.Model;
using Auth.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Auth.Service
{
    public class ManagerAuth
    {
        private SqlCommand sqlCommand = null;

        public ManagerAuth()
        {
            sqlCommand = new SqlCommand();
        }

        public async Task<User> GetUserNameForViewManager(string login)
        {
            return await sqlCommand.GetUserNameForView(login);
        }

        public async Task<User> GetUserAsync(string login, string password)
        {
            return await sqlCommand.GetUserInDb(login, password);
        }

        public async Task<Admin> GetUserAsyncAdmin(string login, string password)
        {
            return await sqlCommand.GetUserInDbAdmin(login, password);
        }

        public async Task<User> GetTotalUserAsync(int id)
        {
            return await sqlCommand.GetTotalUsersInDb(id);
        }

        public async void AddNewCompanyInDb(string Login, string Password, string CompanyName, string OwnershipType, string Adress, string LegalAdress, string CheckingAccount, string BankName, string BankBin, string UNP,
            string OKPO, string ONPF, string FolderLanguage)
        {
            User user = new User();
            user.Login = Login;
            user.Password = Password;
            DateTime date = DateTime.Now;
            user.Date = date.ToString();
            user.Adress = Adress;
            user.CompanyName = OwnershipType + " «" + CompanyName + "»";
            user.OwnershipType = OwnershipType;
            user.LegalAdress = LegalAdress;
            user.CheckingAccount = CheckingAccount;
            user.BankName = BankName;
            user.BankBin = BankBin;
            user.UNP = UNP;
            user.OKPO = OKPO;
            user.ONPF = ONPF;
            user.FolderLanguage = FolderLanguage;
            sqlCommand.AddNewCompanyInDb(user);
        }

        public async void AddNewClientInDb(string Login, string Password, string CompanyName, string OwnershipType, string Adress, string LegalAdress, string CheckingAccount, string BankName, string BankBin, string UNP,
            string OKPO, string ONPF, string FolderLanguage, string ParentCompany)
        {
            UserClients userClients = new UserClients();
            userClients.Login = Login;
            userClients.Password = Password;
            DateTime date = DateTime.Now;
            userClients.Date = date.ToString();
            userClients.Adress = Adress;
            userClients.CompanyName = OwnershipType + " «" + CompanyName + "»";
            userClients.OwnershipType = OwnershipType;
            userClients.LegalAdress = LegalAdress;
            userClients.CheckingAccount = CheckingAccount;
            userClients.BankName = BankName;
            userClients.BankBin = BankBin;
            userClients.UNP = UNP;
            userClients.OKPO = OKPO;
            userClients.ONPF = ONPF;
            userClients.FolderLanguage = FolderLanguage;
            userClients.ParentCompany = ParentCompany;
            sqlCommand.AddNewClientsInDb(userClients);
        }


        /*public async void AddUserClientsInDb(string CompanyName, string OwnershipType, string Adress, string LegalAdress, string CheckingAccount, string BankBin, string UNP,
            string OKPO, string ONPF, string FolderLanguage, string ParentCompany)
        {
            UserClients userClients = new UserClients();
            userClients.Adress = Adress;
            userClients.CompanyName = CompanyName;
            userClients.OwnershipType = OwnershipType;
            userClients.LegalAdress = LegalAdress;
            userClients.CheckingAccount = CheckingAccount;
            userClients.BankBin = BankBin;
            userClients.UNP = UNP;
            userClients.OKPO = OKPO;
            userClients.ONPF = ONPF;
            userClients.FolderLanguage = FolderLanguage;
            userClients.ParentCompany = ParentCompany;
            sqlCommand.AddUserClientsInDb(userClients);
        }*/


        public List<UserClients> CompareToParentCompanyManager(string AuthorizedName)
        {
           return sqlCommand.CompareToParentCompany(AuthorizedName);
        }


        public async void ArchivingUnarchivingManager(int id)
        {
            sqlCommand.ArchivingUnarchiving(id);
        }

        public async void ArchivingUnarchivingUserClientsManager(int id)
        {
            sqlCommand.ArchivingUnarchivingUserClients(id);
        }

        public async void DeleteAllUsersManager()
        {
            sqlCommand.DeleteAllUsersCommand();
        }

        public async void DeleteCompanyManager(int Id)
        {
            sqlCommand.DeleteCompany(Id);
        }

        public async Task<User> GetCompanyInfoManager(string CompanyName)
        {
            return await sqlCommand.GetCompanyInfoInDb(CompanyName);
        }

        public async Task<User> GetCompanyManager(string Login, string Password)
        {
            return await sqlCommand.GetCompany(Login, Password);
        }

        public async Task<UserClients> GetCompanyClientManager(string Login, string Password)
        {
            return await sqlCommand.GetCompanyClient(Login, Password);
        }

        public async Task<String> isBookKeepingCompanyManager(string Login)
        {
            return await sqlCommand.isBookKeepingCompany(Login);
        }

        public async Task<Boolean> isArchivedManager(string Login, string Password)
        {
            return await sqlCommand.isArchived(Login, Password);
        }

        public async Task<Boolean> isArchivedUserClientsManager(string Login, string Password)
        {
            return await sqlCommand.isArchivedUserClients(Login, Password);
        }

        public async Task<Boolean> CompanyIsExistsManager(string Login)
        {
            return await sqlCommand.CompanyIsExists(Login);
        }
    }
}
