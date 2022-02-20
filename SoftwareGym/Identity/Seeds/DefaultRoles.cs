using Identity.Models;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using Application.Enums;

namespace Identity.Seeds
{
    public static class  DefaultRoles
    {
        //ApplicationUser es un Model que contiene propiedades Nombre y Apellido y ademas hereda otras de IdentityUser para crear un usuario
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager,RoleManager<IdentityRole> roleManager)
        {
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(Roles.Basic.ToString()));


        }



    }
}
