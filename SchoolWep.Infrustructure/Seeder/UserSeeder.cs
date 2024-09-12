using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SchoolWep.Infrustructure.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SchoolWep.Infrustructure.Seeder
{
    public static class UserSeeder
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> _userManager)
        {
            var CountUsers = await _userManager.Users.CountAsync();

            if(CountUsers <= 0)
            {
                var DefultUser = new ApplicationUser()
                {
                    UserName = "UserSuperAdmin",
                    Email = "SuperAdmin@gmail.com",
                    FirstName = "User",
                    LastName = "Admin",
                    EmailConfirmed = true,
                    Id = Guid.NewGuid().ToString(),
                    AccountCreatedDate = DateTime.Now,
                };
                var res = await _userManager.CreateAsync(DefultUser,"SuperAdmin100");
                if (res.Succeeded)
                {
                    await _userManager.AddToRoleAsync(DefultUser, "SuperAdmin");

                }

            }
            
        }

    }
}
