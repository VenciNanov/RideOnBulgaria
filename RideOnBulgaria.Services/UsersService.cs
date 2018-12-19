using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class UsersService : IUsersService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
        }

        public async Task<User> GetUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            return user;
        }

        public User GetUserByUsername(string username)
        {
            return this.userManager.FindByNameAsync(username).GetAwaiter().GetResult();
        }
    }
}
