﻿using Microsoft.AspNetCore.Identity;

namespace CursoMOD129.Data.SeedDatabase
{
    public class SeedDatabase
    {
        public static void Seed(ApplicationDbContext context, UserManager<IdentityUser> userManager, 
                                RoleManager<IdentityRole> roleManager) 
        {
            SeedRoles(roleManager).Wait();
            SeedUsers(userManager).Wait();

        }

        private static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            var roleCheck = await roleManager.RoleExistsAsync(CursoMOD129Constants.ROLES.ADMIN);

            if (!roleCheck)
            {
                var adminRole = new IdentityRole
                {
                    Name = CursoMOD129Constants.ROLES.ADMIN
                };

                await roleManager.CreateAsync(adminRole);
            }

            roleCheck = await roleManager.RoleExistsAsync(CursoMOD129Constants.ROLES.ADMINISTRATIVE);

            if (!roleCheck)
            {
                var administrativeRole = new IdentityRole
                {
                    Name = CursoMOD129Constants.ROLES.ADMINISTRATIVE
                };

                await roleManager.CreateAsync(administrativeRole);
            }

        }

        private static async Task SeedUsers(UserManager<IdentityUser> userManager)
        {
            var dbAdmin = await userManager.FindByNameAsync(CursoMOD129Constants.USERS.ADMIN.USERNAME);

            if (dbAdmin == null)
            {
                var result = await userManager.CreateAsync(
                    new IdentityUser
                    {
                        UserName = CursoMOD129Constants.USERS.ADMIN.USERNAME,
                        Email = CursoMOD129Constants.USERS.ADMIN.USERNAME
                    },

                    CursoMOD129Constants.USERS.ADMIN.PASSWORD
                    );

                if (result.Succeeded)
                {
                    dbAdmin = await userManager.FindByEmailAsync(CursoMOD129Constants.USERS.ADMIN.USERNAME);
                    await userManager.AddToRoleAsync(dbAdmin!, CursoMOD129Constants.ROLES.ADMIN);
                }
            }

            var dbAdministrative = await userManager.FindByNameAsync(CursoMOD129Constants.USERS.ADMINISTRATIVE.USERNAME);

            if (dbAdministrative == null)
            {
                var result = await userManager.CreateAsync(
                    new IdentityUser
                    {
                        UserName = CursoMOD129Constants.USERS.ADMINISTRATIVE.USERNAME,
                        Email = CursoMOD129Constants.USERS.ADMINISTRATIVE.USERNAME
                    },

                    CursoMOD129Constants.USERS.ADMINISTRATIVE.PASSWORD
                    );

                if (result.Succeeded)
                {
                    dbAdministrative = await userManager.FindByEmailAsync(CursoMOD129Constants.USERS.ADMINISTRATIVE.USERNAME);
                    await userManager.AddToRoleAsync(dbAdministrative!, CursoMOD129Constants.ROLES.ADMINISTRATIVE);
                }
            }
        }
    }
}
