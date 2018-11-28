using RideOnBulgaria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IUsersService
    {
        Task<bool> Login(string username, string password);

        Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName,
            string lastName);

        Task<User> GetUser(string username);

        void Logout();
    }
}
