using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class UsersService : IUsersService
    {
        private readonly SignInManager<User> signInManager;
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;

        public UsersService(SignInManager<User> signInManager, UserManager<User> userManager, ApplicationDbContext context)
        {
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.context = context;
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

        public ICollection<User> GetAllUsers()
        {
            return this.userManager.Users.ToList();
        }

        public string GetUserRole(string username)
        {
            var user = this.userManager.FindByNameAsync(username).GetAwaiter().GetResult();

            var role = this.userManager.GetRolesAsync(user).GetAwaiter().GetResult().First();

            //var users = userManager.Users.ToList();

            //var result = new Dictionary<User,string>();

            //foreach (var user in users)
            //{
            //    var role = userManager.GetRolesAsync(user).Result.First();
            //    result[user] = role;
            //}

            return role;
        }

        public User GetUserById(string id)
        {
            var user = this.userManager.FindByIdAsync(id).GetAwaiter().GetResult();

            return user;
        }
    }
}
