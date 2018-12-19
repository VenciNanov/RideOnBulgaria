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

        User GetUserByUsername(string username);

    }
}
