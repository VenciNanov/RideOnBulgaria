using System;
using System.Linq;
using System.Threading.Tasks;
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

        public async Task<bool> Login(string username, string password)
        {
            var user = this.GetUser(username).Result;
            if (user == null) return false;

            var result =
                await this.signInManager.PasswordSignInAsync(user, password, isPersistent: false,
                    lockoutOnFailure: false);

            return result.Succeeded;
        }

        public async Task<bool> Register(string username, string password, string confirmPassword, string email, string firstName,
            string lastName)
        {
            if (username == null ||
                password == null ||
                confirmPassword == null ||
                firstName == null ||
                lastName == null ||
                email == null)
                return false;

            if (password != confirmPassword)
                return false;

            var user = new User
            {
                UserName = username,
                Email = firstName,
                FirstName = firstName,
                LastName = lastName
            };

            var userCreateResult = await this.userManager.CreateAsync(user, password);

            if (!userCreateResult.Succeeded) return false;

            IdentityResult addRoleResult = null;

            if (this.userManager.Users.Count() == 1)
            {
                addRoleResult = await this.userManager.AddToRoleAsync(user, "Admin");
            }
            else
            {
                addRoleResult = await this.userManager.AddToRoleAsync(user, "User");
            }

            if (!addRoleResult.Succeeded) return false;

            return true;
        }

        public async Task<User> GetUser(string username)
        {
            var user = await this.userManager.FindByNameAsync(username);

            return user;
        }

        public async void Logout()
        {
            await this.signInManager.SignOutAsync();
        }
    }
}
