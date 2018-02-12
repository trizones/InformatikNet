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
                var role = new IdentityRole
                {
                    Name = "Forskare"
                };
                roleManager.Create(role);

                var user = new ApplicationUser
                {
                    Name = "Forskare Forsk",
                    UserName = "forskare@mail.com",
                    Email = "forskare@mail.com"
                };

                string userPWD = "password";

                var forskUser = UserManager.Create(user, userPWD);

                if(forskUser.Succeeded)
                { var result1 = UserManager.AddToRole(user.Id, "Forskare");
                }
            }

            if (!roleManager.RoleExists("Lärare"))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole
                {
                    Name = "Lärare"
                };
                roleManager.Create(role);

                //Here we create a User                 

                var user = new ApplicationUser
                {
                    Name = "Esmeralda Exempelsson",
                    UserName = "larare@hotmail.com",
                    Email = "larare@hotmail.com"
                };

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
            
             
            var planering = new Tag
            {
                Name = "Planering",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(planering);

            var seminarium = new Tag
            {
                Name = "Seminarium",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(seminarium);

            var frukost = new Tag
            {
                Name = "Frukostmöte",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(frukost);

            var kurs = new Tag
            {
                Name = "Kurs",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(kurs);

            var forelasning = new Tag
            {
                Name = "Föreläsning",
                Category = Informatik,
                CategoryString = "Informatik"
            };
            context.Tag.Add(forelasning);

            var semester = new Tag
            {
                Name = "Semester",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(semester);

            var fritidsaktivitet = new Tag
            {
                Name = "Fritidsaktivitet",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(fritidsaktivitet);

            var afterWork = new Tag
            {
                Name = "After Work",
                Category = Others,
                CategoryString = "Others"
            };
            context.Tag.Add(afterWork);

            var system = new Tag
            {
                Name = "System",
                Category = anslagForskning,
                CategoryString = "anslag forskning"
            };
            context.Tag.Add(system);

            var itTrend = new Tag
            {
                Name = "IT trend",
                Category = anslagForskning,
                CategoryString = "anslag forskning"
            };
            context.Tag.Add(itTrend);

            var lillsupen = new Tag
            {
                Name = "Lillsupen",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(lillsupen);

            var sql = new Tag
            {
                Name = "SQL",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(sql);

            var java = new Tag
            {
                Name = "Java",
                Category = anslagUtbildning,
                CategoryString = "anslag utbildning"
            };
            context.Tag.Add(java);
            
            context.SaveChanges();

            var AllUsers = context.Users.ToList();

            var today = DateTime.Now.AddHours(1);
            var möte1 = new ConfirmedMeeting
            {
                Title = "Informatikmöte",
                ConfirmedDate = today,
                Creator = AllUsers.First(),
                Recievers = AllUsers,
                UserNames = GetUserNames(AllUsers)
            };
            context.ConfirmedMeeting.Add(möte1);

            var möte2 = new ConfirmedMeeting
            {
                Title = "Seminarium",
                ConfirmedDate = Convert.ToDateTime("2018-02-12 13:00"),
                Creator = AllUsers.First(),
                Recievers = AllUsers,
                UserNames = GetUserNames(AllUsers)
            };
            context.ConfirmedMeeting.Add(möte2);

            var möte3 = new ConfirmedMeeting
            {
                Title = "Möte",
                ConfirmedDate = Convert.ToDateTime("2018-02-14 11:00"),
                Creator = AllUsers.First(),
                Recievers = AllUsers,
                UserNames = GetUserNames(AllUsers)
            };
            context.ConfirmedMeeting.Add(möte3);
                
            context.SaveChanges();
            base.Seed(context);
        }

        public string GetUserNames(List<ApplicationUser> users)
        {
            var db = new ApplicationDbContext();
            var list2 = new List<string>();
            var longFellow = "";
            

            foreach (var item in users)
            {
                var user = db.Users.Where(u => u.Id == item.Id).Single();
                list2.Add(user.Name);
               
            }
            longFellow = string.Join(", ", list2);

            return (longFellow);
        }
    }
       
}