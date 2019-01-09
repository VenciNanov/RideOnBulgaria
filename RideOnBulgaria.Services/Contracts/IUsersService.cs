using RideOnBulgaria.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace RideOnBulgaria.Services.Contracts
{
    public interface IUsersService
    {
        Task<User> GetUser(string username);
        ICollection<User> GetAllUsers();
        string GetUserRole(string username);
        User GetUserByUsername(string username);
        User GetUserById(string id);
        bool PromoteUserToAdminRole(string id);
        bool DemoteUserToUserRole(string id);
    }
}
