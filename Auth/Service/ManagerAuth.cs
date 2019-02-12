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

        public async Task<User> GetUserAsync(string login, string password)
        {
            return await sqlCommand.GetUserInDb(login, password);
        }

        public async Task<User> GetTotalUserAsync(int id)
        {
            return await sqlCommand.GetTotalUsersInDb(id);
        }

       
        public async void AddUserClientsInDb(string CompanyName, string OwnershipType, string Adress, string LegalAdress, int CheckingAccount, string BankBin, int UNP,
            int OKPO, int ONPF, string FolderLanguage)
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
            sqlCommand.AddUserClientsInDb(userClients);
        }
    }
}
