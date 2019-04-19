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

        public List<UserClientsEmployee> GetEmployeeListManager(string ParentCompany)
        {
            return sqlCommand.GetEmployeeList(ParentCompany);
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
            user.AccountType = "BookKeepingCompany";
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
            userClients.AccountType = "Company";
            sqlCommand.AddNewClientsInDb(userClients);
        }

        public async void AddUserClientsEmployeeManager(string Login, string Password, string SurnameNameFathersName, string WorkPosition, string UsersType, string Phone, string Email, string ParentsCompanyName)
        {
            UserClientsEmployee userClientsEmployee = new UserClientsEmployee();
            userClientsEmployee.Login = Login;
            userClientsEmployee.Password = Password;
            DateTime date = DateTime.Now;
            userClientsEmployee.Date = date.ToString();
            userClientsEmployee.SurnameNameFathersName = SurnameNameFathersName;
            userClientsEmployee.WorkPosition = WorkPosition;
            userClientsEmployee.UsersType = UsersType;
            userClientsEmployee.Phone = Phone;
            userClientsEmployee.Email = Email;
            userClientsEmployee.ParentsCompanyName = ParentsCompanyName;
            userClientsEmployee.AccountType = "Employee";
            sqlCommand.AddUserClientsEmployeeInDb(userClientsEmployee);
        }

        public List<UserClients> CompareToParentCompanyManager(string AuthorizedName)
        {
           return sqlCommand.CompareToParentCompany(AuthorizedName);
        }

        public async void ArchivingUnarchivingManager(int id)
        {
            sqlCommand.ArchivingUnarchiving(id);
        }

        public async void ArchivingUnarchivingEmployeeManager(int id)
        {
            sqlCommand.ArchivingUnarchivingEmployee(id);
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

        public async Task<UserClientsEmployee> GetUserClientsEmployeeManager(string Login, string Password)
        {
            return await sqlCommand.GetUserClientsEmployee(Login, Password);
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

        public async Task<Boolean> UserClientsEmployeeIsExistsManager(string Login)
        {
            return await sqlCommand.UserClientsEmployeeIsExists(Login);
        }

        public string UserClientsEmployeeIsExistsStringManager(string Login)
        {
            return sqlCommand.UserClientsEmployeeIsExistsString(Login);
        }

        public async Task<String> GetUserClientsNameManager(string Login)
        {
            return await sqlCommand.GetUserClientsName(Login);
        }

        public async Task<String> GetUserClientsCompanyNameManager(string Login)
        {
            return await sqlCommand.GetUserClientsCompanyName(Login);
        }

        public async Task<String> GetUserClientsEmployeeParentsNameManager(string Login)
        {
            return await sqlCommand.GetUserClientsEmployeeParentsName(Login);
        }

        public async Task<String> GetUserClientsEmployeeTypeManager(string login)
        {
            return await sqlCommand.GetUserClientsEmployeeType(login);
        }
    }
}
