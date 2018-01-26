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
        public Initializer()
        {
            ApplicationDbContext context = new ApplicationDbContext();
            Seed(context);
        }
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
            // creating Creating Employee role    
            if (!roleManager.RoleExists("Anställd"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = "Anställd";
                roleManager.Create(role);

            }
            context.SaveChanges();

            base.Seed(context);
        }
    }
}