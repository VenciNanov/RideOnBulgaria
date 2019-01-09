using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using RideOnBulgaria.Data;
using RideOnBulgaria.Models;
using RideOnBulgaria.Services.Contracts;

namespace RideOnBulgaria.Services
{
    public class UsersService : IUsersService
    {
        private readonly UserManager<User> userManager;
        private readonly ApplicationDbContext context;

        public UsersService(UserManager<User> userManager, ApplicationDbContext context)
        {
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

        
        public bool DemoteUserToUserRole(string id)
        {
            var user = this.GetUserById(id);

            if (user == null)
            {
                return false;
            }

            var removeRole = userManager.RemoveFromRoleAsync(user, "Admin").GetAwaiter().GetResult();
            var promoteUser = userManager.AddToRoleAsync(user, "User").GetAwaiter().GetResult();

            return promoteUser.Succeeded;
        }

        public bool PromoteUserToAdminRole(string id)
        {
            var user = this.GetUserById(id);

            if (user==null)
            {
                return false;
            }

            var removeRole = userManager.RemoveFromRoleAsync(user, "User").GetAwaiter().GetResult();
            var promoteUser = userManager.AddToRoleAsync(user, "Admin").GetAwaiter().GetResult();

            return true;
        }
    }
}
