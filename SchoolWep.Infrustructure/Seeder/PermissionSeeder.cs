using Microsoft.AspNetCore.Identity;
using SchoolWep.Data.Helper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static SchoolWep.Data.Constans.Permission;
using static SchoolWep.Data.Helper.Helpers;

namespace SchoolWep.Infrustructure.Seeder
{
    public static class PermissionSeeder
    {
        public static async Task SeedClaimsAsync(this RoleManager<IdentityRole> roleManager)
        {
            //// hna ana hgyb asm ale role ale ana 3awz a3wz adeha ale permission 
            var adminrole = await roleManager.FindByNameAsync("SuperAdmin");
            //code add permission Claims
            var modules = Enum.GetValues(typeof(EPermissionModuleName));
          
            foreach (var module in modules)
            {
                await roleManager.AddPermissionClaims(adminrole, module.ToString());
            }


        }


        public static async Task AddPermissionClaims(this RoleManager<IdentityRole> roleManager, IdentityRole role, string module)
        {
            // hna ana bgeb kol ale claims bt3t ale role ale ana 3awz a3mlha add ly ale claims
            var allclaims = await roleManager.GetClaimsAsync(role);
            // hwa hwa rg3ly kol ale permission ale ana ale mfrod h7otha ly ale claims
            var allpermissions = GeneratePermissionsFormModule(module);
            // hna ana h3ml foreach 3la ale allpermission we hs2l ale awl hl ale permission dh mowgod wla l2 lw mogdo 5las 
            //mt7tho4 lw l2 7toh gded
            foreach (var permissions in allpermissions)
            {
                if (!allclaims.Any(x => x.Type == Helpers.Permission && x.Value == permissions))
                {
                    await roleManager.AddClaimAsync(role, new Claim(Helpers.Permission, permissions));
                }
            }

        }
    }
}
