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

        public async Task<User> GetUserInDb(string login, string password)
        {
            return await contextAuth.Users.FirstOrDefaultAsync(u => u.Login == login && u.Password == password);
        }

        public async Task<User> GetTotalUsersInDb(int id)
        {
            return await contextAuth.Users.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async void AddUserClientsInDb(UserClients userClients)
        {
            if(userClients != null)
            {
                await contextAuth.UserClients.AddAsync(userClients);
                await contextAuth.SaveChangesAsync();
            }
        }
    }
}
