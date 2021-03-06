﻿using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace InformatikNet.Models
{
    public class Initializer : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {

        protected override void Seed(ApplicationDbContext context)
        {
            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(context));
            var UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(context));


            // In Startup iam creating first Admin Role and creating a default Admin User    
            if (!roleManager.RoleExists("Administratör"))
            {
                // first we create Admin role 
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Administratör"
                };
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser
                {
                    Name = "Admin Adminsson",
                    UserName = "admin@hotmail.com",
                    Email = "admin@hotmail.com"
                };

                string userPWD = "admin123!";

                var adminUser = UserManager.Create(user, userPWD);

                //Add default User to Role Admin   
                if (adminUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Administratör");

                }
            }
            if  (!roleManager.RoleExists("Forskare"))
            {
                var role = new IdentityRole();
                role.Name = "Forskare";
                roleManager.Create(role);

                var user = new ApplicationUser();
                user.Name = "Forskare Forsk";
                user.UserName = "forskare@mail.com";
                user.Email = "forskare@mail.com";

                string userPWD = "password";

                var forskUser = UserManager.Create(user, userPWD);

                if(forskUser.Succeeded)
                { var result1 = UserManager.AddToRole(user.Id, "Forskare");
                }
            }

            if (!roleManager.RoleExists("Lärare"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Lärare";
                roleManager.Create(role);

                //Here we create a User                 

                var user = new ApplicationUser();
                user.Name = "Esmeralda Exempelsson";
                user.UserName = "larare@hotmail.com";
                user.Email = "larare@hotmail.com";

                string userPWD = "larare123!";

                var userUser = UserManager.Create(user, userPWD);

                //Add default User to Role Anställd   
                if (userUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Lärare");

                }

            }


            // creating Creating Employee role    
            if (!roleManager.RoleExists("Anställd"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Anställd"
                };
                roleManager.Create(role);

                //Here we create a User                 

                var user = new ApplicationUser();
                user.Name = "User Usersson";
                user.UserName = "user@hotmail.com";
                user.Email = "user@hotmail.com";

                string userPWD = "user123!";

                var userUser = UserManager.Create(user, userPWD);

                //Add default User to Role Anställd   
                if (userUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Anställd");

                }



            }
            var forskning = new Category { Id = 1, CategoryName = "Forskning", Tags = null };
            context.Category.Add(forskning);

            var utbildning = new Category { Id = 2, CategoryName = "Utbildning", Tags = null };
            context.Category.Add(utbildning);

            var övrigt = new Category { Id = 3, CategoryName = "Övrigt", Tags = null };
            context.Category.Add(övrigt);

            var anslagForskning = new Category { Id = 4, CategoryName = "Anslag Forskning", Tags = null };
            context.Category.Add(anslagForskning);

            var anslagUtbildning = new Category { Id = 5, CategoryName = "Anslag Utbildning", Tags = null };
            context.Category.Add(anslagUtbildning);

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Id = i + 1,
                    Name = "forskning" + i,
                    Category = forskning,
                    CategoryString = "forskning"
                };
                context.Tag.Add(tag);
                
            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Id = i + 1,
                    Name = "utbildning" + i,
                    Category = utbildning,
                    CategoryString = "utbildning"
                };
                context.Tag.Add(tag);

            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Id = i + 1,
                    Name = "övrigt" + i,
                    Category = övrigt,
                    CategoryString = "övrigt"
                };
                context.Tag.Add(tag);

            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Id = i + 1,
                    Name = "Anslag Forskning" + i,
                    Category = anslagForskning,
                    CategoryString = "Anslag Forskning"
                };
                context.Tag.Add(tag);

            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag
                {
                    Id = i + 1,
                    Name = "Anslag Utbildning" + i,
                    Category = anslagUtbildning,
                    CategoryString = "Anslag Utbildning"
                };
                context.Tag.Add(tag);

            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}