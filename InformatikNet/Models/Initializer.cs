using Microsoft.AspNet.Identity;
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

            var utbildning = new Category { Id = 1, CategoryName = "Utbildning", Tags = null };
            context.Category.Add(utbildning);

            var övrigt = new Category { Id = 1, CategoryName = "Övrigt", Tags = null };
            context.Category.Add(övrigt);

            var anslagForskning = new Category { Id = 1, CategoryName = "AnslagForskning", Tags = null };
            context.Category.Add(anslagForskning);

            var anslagUtbildning = new Category { Id = 1, CategoryName = "AnslagUtbildning", Tags = null };
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

            context.SaveChanges();

            base.Seed(context);
        }
    }
}