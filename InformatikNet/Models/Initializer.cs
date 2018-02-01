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
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Administratör";
                roleManager.Create(role);

                //Here we create a Admin super user who will maintain the website                  

                var user = new ApplicationUser();
                user.Name = "Admin Adminsson";
                user.UserName = "admin@hotmail.com";
                user.Email = "admin@hotmail.com";

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
                user.UserName = "lärare@hotmail.com";
                user.Email = "lärare@hotmail.com";

                string userPWD = "lärare123!";

                var userUser = UserManager.Create(user, userPWD);

                //Add default User to Role Anställd   
                if (userUser.Succeeded)
                {
                    var result1 = UserManager.AddToRole(user.Id, "Anställd");

                }

            }


            // creating Creating Employee role    
            if (!roleManager.RoleExists("Anställd"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Anställd";
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

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag();
                tag.Id = i + 1;
                tag.Name = "forskning" + i;
                tag.Category = forskning;
                tag.CategoryString = "forskning";
                context.Tag.Add(tag);
                
            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag();
                tag.Id = i + 1;
                tag.Name = "utbildning" + i;
                tag.Category = utbildning;
                tag.CategoryString = "utbildning";
                context.Tag.Add(tag);

            }

            for (int i = 0; i < 10; i++)
            {
                var tag = new Tag();
                tag.Id = i + 1;
                tag.Name = "övrigt" + i;
                tag.Category = övrigt;
                tag.CategoryString = "övrigt";
                context.Tag.Add(tag);

            }

            context.SaveChanges();

            base.Seed(context);
        }
    }
}