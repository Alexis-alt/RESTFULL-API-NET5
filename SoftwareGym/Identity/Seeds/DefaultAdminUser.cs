using Application.Enums;
using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Identity.Seeds
{
   public static class DefaultAdminUser
    {


        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {

            var defaultUser = new ApplicationUser()
            {
                    UserName = "adminUser",
                    Email ="userAdmin@mail.com",
                    Nombre = "Alexis",
                    Apellido = "Martínez",
                    EmailConfirmed = true,
                    PhoneNumberConfirmed = true
                    

                

            };

            if (userManager.Users.All(u => u.Id != defaultUser.Id))
            {

                var user = await userManager.FindByEmailAsync(defaultUser.Email);

                if(user == null)
                {

                    await userManager.CreateAsync(defaultUser,"123Password");
                    await userManager.AddToRoleAsync(defaultUser,Roles.Admin.ToString());
                    await userManager.AddToRoleAsync(defaultUser, Roles.Basic.ToString());


                }



            }
           




        }



    }
}
