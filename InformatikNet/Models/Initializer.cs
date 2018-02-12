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
                var user = new ApplicationUser
                {
                    Name = "Hampus Nordström",
                    UserName = "hampus@hotmail.com",
                    Email = "hampus@hotmail.com"
                };

                var user2 = new ApplicationUser
                {
                    Name = "Caroline Ellwyn",
                    UserName = "caroline@hotmail.com",
                    Email = "caroline@hotmail.com"
                };

                var user3 = new ApplicationUser
                {
                    Name = "Elliot Högberg",
                    UserName = "elliot@hotmail.com",
                    Email = "elliot@hotmail.com"
                };

                var user4 = new ApplicationUser
                {
                    Name = "Viktor Linq",
                    UserName = "viktor@hotmail.com",
                    Email = "viktor@hotmail.com"
                };

                var user5 = new ApplicationUser
                {
                    Name = "Adrian Karlekvist",
                    UserName = "martin@hotmail.com",
                    Email = "martin@hotmail.com"
                };

                var user6 = new ApplicationUser
                {
                    Name = "Johan Fransson",
                    UserName = "johan@hotmail.com",
                    Email = "johan@hotmail.com"
                };

                var user7 = new ApplicationUser
                {
                    Name = "Joakim Holm",
                    UserName = "joakim@hotmail.com",
                    Email = "joakim@hotmail.com"
                };

                string userPWD = "user123!";
                var userUser = UserManager.Create(user, userPWD);
                var userUser2 = UserManager.Create(user2, userPWD);
                var userUser3 = UserManager.Create(user3, userPWD);
                var userUser4 = UserManager.Create(user4, userPWD);
                var userUser5 = UserManager.Create(user5, userPWD);
                var userUser6 = UserManager.Create(user6, userPWD);
                var userUser7 = UserManager.Create(user7, userPWD);

                //Add default User to Role Anställd   
                if (userUser.Succeeded && userUser2.Succeeded && userUser3.Succeeded && userUser4.Succeeded && userUser5.Succeeded && userUser6.Succeeded && userUser7.Succeeded)
                {
                    UserManager.AddToRole(user.Id, "Anställd");
                    UserManager.AddToRole(user2.Id, "Anställd");
                    UserManager.AddToRole(user3.Id, "Anställd");
                    UserManager.AddToRole(user4.Id, "Anställd");
                    UserManager.AddToRole(user5.Id, "Anställd");
                    UserManager.AddToRole(user6.Id, "Anställd");
                    UserManager.AddToRole(user7.Id, "Anställd");
                }
            }


            var Informatik = new Category { Id = 1, CategoryName = "Informatik", Tags = null };
            context.Category.Add(Informatik);

            var Others = new Category { Id = 3, CategoryName = "Others", Tags = null };
            context.Category.Add(Others);

            var anslagForskning = new Category { Id = 4, CategoryName = "Anslag Forskning", Tags = null };
            context.Category.Add(anslagForskning);

            var anslagUtbildning = new Category { Id = 5, CategoryName = "Anslag Utbildning", Tags = null };
            context.Category.Add(anslagUtbildning);

            DateTime nu = DateTime.Now;
            var nyttMöte = new ConfirmedMeeting { Title = "Seminarium", Creator = null, ConfirmedDate = nu, ConfirmedMeetingId = 1 };
            context.ConfirmedMeeting.Add(nyttMöte);

            var nyttMöte2 = new ConfirmedMeeting { Title = "Presentation", Creator = null, ConfirmedDate = Convert.ToDateTime("2018-02-16 13:00"), ConfirmedMeetingId = 1, UserNames = "Johan Fransson, Caroline Ellwyn" };
            context.ConfirmedMeeting.Add(nyttMöte2);

            var nyttMöte3 = new ConfirmedMeeting { Title = "Scrumbeer", Creator = null, ConfirmedDate = Convert.ToDateTime("2018-02-17 19:00"), ConfirmedMeetingId = 1, UserNames = "Martin Sarling, Elliot Högberg" };
            context.ConfirmedMeeting.Add(nyttMöte3);

            var Post = new Post
            {
                Title = "Dokument från mötet 2018-02-16",
                Content = "Dokumenten",
                PublishDate = Convert.ToDateTime("2018-02-16 13:00"),
                Categories = Informatik
                
            };
             
            var tag = new Tag
            {
                Name = "Planering",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(tag);

            var tag2 = new Tag
            {
                Name = "Seminarium",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(tag2);

            var tag3 = new Tag
            {
                Name = "Frukostmöte",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(tag3);

            var tag4 = new Tag
            {
                Name = "Kurs",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(tag4);

            var tag5 = new Tag
            {
                Name = "Föreläsning",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(tag5);

            var tag6 = new Tag
            {
                Name = "Semester",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(tag6);

            var tag7 = new Tag
            {
                Name = "Fritidsaktivitet",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(tag7);

            var tag8 = new Tag
            {
                Name = "After Work",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(tag8);

            var tag9 = new Tag
            {
                Name = "System",
                Category = anslagForskning,
                CategoryString = "anslag forskning"
            };
            context.Tag.Add(tag9);

            var tag10 = new Tag
            {
                Name = "IT trend",
                Category = anslagForskning,
                CategoryString = "anslag forskning"
            };
            context.Tag.Add(tag10);

            var tag11 = new Tag
            {
                Name = "Lillsupen",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(tag11);

            var tag12 = new Tag
            {
                Name = "SQL",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(tag12);

            var tag13 = new Tag
            {
                Name = "Java",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(tag13);


            

            context.SaveChanges();

            base.Seed(context);
        }
    }
}