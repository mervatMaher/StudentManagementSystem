using Microsoft.AspNetCore.Identity;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Enums;
using StudentManagementSystem04.Model;
using System.Threading.Tasks;

namespace StudentManagementSystem04.seedData
{
    public class Seeding
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StudentManagementSystemDbContext _context;
        private readonly IServiceProvider _serviceProvider;
       
        public Seeding(StudentManagementSystemDbContext context, IServiceProvider serviceProvider)
        {
            using var scope = serviceProvider.CreateScope();

            _serviceProvider = serviceProvider;
            _roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _context = context;
        }
        public async System.Threading.Tasks.Task SeedingData ()
        {

            // add Categories 
            var Categories = new[]
            {
                    new Category { Name = "Programming" },
                    new Category { Name = "Graphic Design" },
                    new Category { Name = "Video Editing" },
                    new Category { Name = "Motion Graphic" },
                    new Category {  Name = "Voice Over" },
                    new Category { Name = "Content creator" }
            };

            foreach (var category in Categories)
            {
                if (!_context.Categories.Any(e => e.Name == category.Name))
                {
                    _context.Categories.Add(category);
                }
            }
            _context.SaveChanges();

            // add Levels
            var Levels = new[]
            {
                  new Level { Name = "Level 1" },
                  new Level { Name = "Level 2" },
                  new Level {  Name = "Level 3" },
                  new Level { Name = "Level 4" }
            };

            foreach (var level in Levels)
            {
                if (!_context.Levels.Any(e => e.Name == level.Name))
                {
                    _context.Levels.Add(level);
                }
            }
            _context.SaveChanges();

            // Doctors 
            var Doctors = new[]
            {
                new {UserName = "@Dr/salyAhmed123", Password = "@Track123"},
                new {UserName = "@Dr/ahmedFahmy123", Password = "@Track123"},
                new {UserName = "@Dr/rababSalah123", Password = "@Track123"},
                new {UserName = "@Dr/doaaMahmoud123", Password = "@Track123"},
                new {UserName = "@Dr/mohammedRamadan123", Password = "@Track123"},
                new {UserName = "@Dr/ahmedSalah123", Password = "@Track123"},
                new {UserName = "@Dr/abeerKamal123", Password = "@Track123"},
                new {UserName = "@Dr/ahmedBadr123", Password = "@Track123"},
            };

            foreach (var doctor in Doctors)
            {
                var existingDoctor = await _userManager.FindByNameAsync(doctor.UserName);
                if(existingDoctor != null)
                {
                    var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingDoctor, doctor.Password);
                    existingDoctor.PasswordHash = newPasswordHash;
                    await _userManager.UpdateAsync(existingDoctor);

                    if (!await _userManager.IsInRoleAsync(existingDoctor, "Doctor"))
                    {
                        await _userManager.AddToRoleAsync(existingDoctor, "Doctor");
                        _context.SaveChangesAsync();
                    }
                }
            };



            //add User
              var Users = new[]
            {
                new ApplicationUser
                {
                    FullName = "Mohamed Ahmed",
                    UserName = "@mohammedAhmed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201027373612",

                },
                new ApplicationUser {
                    FullName = "Mohamed Radi",
                    UserName = "@mohammedRadi123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201143974436"
                },
                new ApplicationUser {
                    FullName = "Abdelrahman Mohamed",
                    UserName = "@abdelrahmanMohamed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201090299673"
                },
                new ApplicationUser {
                    FullName = "Yousef Mohamed",
                    UserName = "@yousefMohamed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201098599953"
                },
                new ApplicationUser {
                    FullName = "Eslam AbdelHamid",
                    UserName = "@eslamAbdelHamid123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201028838133"
                },

                new ApplicationUser {
                    FullName = "Merna Yasser",
                    UserName = "@mernaYasser123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201025264276"
                },
                new ApplicationUser {
                    FullName = "Mervat Maher",
                    UserName = "@mervatMaher123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201017858328"
                },
                new ApplicationUser {
                    FullName = "Shahd Mohamed",
                    UserName = "@shahdMohamed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201002324726"
                },
                new ApplicationUser {
                    FullName = "Ashraqat Ahmed",
                    UserName = "@ashraqatAhmed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+20102407951"
                },
                new ApplicationUser {
                    FullName = "Amira Khaled",
                    UserName = "@amiraKhaled123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201061210094"
                },


                new ApplicationUser {
                    FullName = "Shimaa Mahmoud",
                    UserName = "@shimaaMahmoud123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201110868059"
                },
                new ApplicationUser {
                    FullName = "Sara Ahmed",
                    UserName = "@saraAhmed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201022840491"
                },
                new ApplicationUser {
                    FullName = "Lamiaa Younes",
                    UserName = "@lamiaaYounes123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201026474421"
                },
                new ApplicationUser {
                    FullName = "Ahmed Maher",
                    UserName = "@ahmedMaher123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201090465601"
                },
                new ApplicationUser {
                    FullName = "Mostafa Maher",
                    UserName = "@mostafaMaher123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201005344369"
                },


                new ApplicationUser {
                    FullName = "Sara Maher",
                    UserName = "@saraMaher123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201067101886"
                },
                new ApplicationUser {
                    FullName = "Sondos Yasser",
                    UserName = "@sondosYasser123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201069195649"
                },
                new ApplicationUser {
                    FullName = "Amr Yasser",
                    UserName = "@amrYasser123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201012954805"
                },
                new ApplicationUser {
                    FullName = "Belal Yasser",
                    UserName = "@belalYasser123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201013876982"
                },
                new ApplicationUser {
                    FullName = "Salma Hassan",
                    UserName = "@salmaHassan123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201096652728"
                },


                new ApplicationUser {
                    FullName = "Mohammed Ramadan",
                    UserName = "@Dr/mohammedRamadan123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201091002793"
                },
                new ApplicationUser {
                    FullName = "Ahmed Badr",
                    UserName = "@Dr/ahmedBadr123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201090807060"
                },
                new ApplicationUser {
                    FullName = "Ahmed Saleh",
                    UserName = "@Dr/ahmedSalah123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201057894256"
                },
                new ApplicationUser {
                    FullName = "Saly Ahmed",
                    UserName = "@Dr/salyAhmed123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201065789412"
                },
                new ApplicationUser {
                    FullName = "Rabab Salah",
                    UserName = "@Dr/rababSalah123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201048976542"
                },
                new ApplicationUser {
                    FullName = "Doaa Mahmoud",
                    UserName = "@Dr/doaaMahmoud123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201049856278"
                },
                new ApplicationUser {
                    FullName = "Abeer Kamal",
                    UserName = "@Dr/abeerKamal123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201098657245"
                },
                 new ApplicationUser {
                    FullName = "Ahmed fahmy",
                    UserName = "@Dr/ahmedFahmy123",
                    PasswordHash = "@Track123",
                    PhoneNumber = "+201098965247"
                }
            };
            foreach (var user in Users)
            {
                if (user.UserName != null)
                {
                    var existingUser = await _userManager.FindByNameAsync(user.UserName);

                    if (existingUser != null)
                    {
                        var newPasswordHash = _userManager.PasswordHasher.HashPassword(existingUser, user.PasswordHash);
                        existingUser.PasswordHash = newPasswordHash;
                        await _userManager.UpdateAsync(existingUser);

                    }
                }
            }
            _context.SaveChanges();
            foreach (var user in Users)
            {
                if (!_context.Users.Any(e => e.UserName == user.UserName))
                {
                    _context.Users.Add(user);
                }

                //AssignRole(_serviceProvider, "@Dr/abeerKamal123", "Doctor");
            }
            _context.SaveChanges();

            // add Lectures 
            var Lectures = new[]
            {
               new Lecture {
               DateTime = new DateTime(2025, 5, 2),
               SubjectId = 1,
               UserId = "144964e1-4765-42e3-aa19-be198b39017c",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)

               },
               new Lecture {
               DateTime = new DateTime(2025, 5, 9),
               SubjectId = 1,
               UserId = "144964e1-4765-42e3-aa19-be198b39017c",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
               },
               new Lecture {
               DateTime = new DateTime(2025, 5, 16),
               SubjectId = 1,
               UserId = "144964e1-4765-42e3-aa19-be198b39017c",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
               },
               new Lecture {
               DateTime = new DateTime(2025, 5, 23),
               SubjectId = 1,
               UserId = "144964e1-4765-42e3-aa19-be198b39017c",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
               },
               new Lecture {
               DateTime = new DateTime(2025, 5, 30),
               SubjectId = 1,
               UserId = "144964e1-4765-42e3-aa19-be198b39017c",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
               },


               new Lecture {
               DateTime = new DateTime(2025, 5, 5),
               SubjectId = 2,
               UserId = "39700b90-e498-4414-9d99-9014a02bd60e",
                StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)

               },
           new Lecture {
               DateTime = new DateTime(2025, 5, 12),
               SubjectId = 2,
               UserId = "39700b90-e498-4414-9d99-9014a02bd60e",
                StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 19),
               SubjectId = 2,
               UserId = "39700b90-e498-4414-9d99-9014a02bd60e",
                StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 26),
               SubjectId = 2,
               UserId = "39700b90-e498-4414-9d99-9014a02bd60e",
                StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 2),
               SubjectId = 2,
               UserId = "39700b90-e498-4414-9d99-9014a02bd60e",
                StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },

           new Lecture {
               DateTime = new DateTime(2025, 5, 2),
               SubjectId = 3,
               UserId = "7373542e-66ee-413b-a87c-5d6acee83dd5",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 9),
               SubjectId = 3,
               UserId = "7373542e-66ee-413b-a87c-5d6acee83dd5",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 16),
               SubjectId = 3,
               UserId = "7373542e-66ee-413b-a87c-5d6acee83dd5",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 23),
               SubjectId = 3,
               UserId = "7373542e-66ee-413b-a87c-5d6acee83dd5",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 30),
               SubjectId = 3,
               UserId = "7373542e-66ee-413b-a87c-5d6acee83dd5",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },


           new Lecture {
               DateTime = new DateTime(2025, 5, 5),
               SubjectId = 4,
               UserId = "80a9daef-73a5-43e3-963c-d626dddae074",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 12),
               SubjectId = 4,
               UserId = "80a9daef-73a5-43e3-963c-d626dddae074",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 19),
               SubjectId = 4,
               UserId = "80a9daef-73a5-43e3-963c-d626dddae074",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 26),
               SubjectId = 4,
               UserId = "80a9daef-73a5-43e3-963c-d626dddae074",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 2),
               SubjectId = 4,
               UserId = "80a9daef-73a5-43e3-963c-d626dddae074",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },


           new Lecture {
               DateTime = new DateTime(2025, 5, 3),
               SubjectId = 5,
               UserId = "c389c56d-3f1a-41d5-bc6e-397be2c82cea",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 10),
               SubjectId = 5,
               UserId = "c389c56d-3f1a-41d5-bc6e-397be2c82cea",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 17),
               SubjectId = 5,
               UserId = "c389c56d-3f1a-41d5-bc6e-397be2c82cea",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 24),
               SubjectId = 5,
               UserId = "c389c56d-3f1a-41d5-bc6e-397be2c82cea",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 31),
               SubjectId = 5,
               UserId = "c389c56d-3f1a-41d5-bc6e-397be2c82cea",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },

           new Lecture {
               DateTime = new DateTime(2025, 5, 6),
               SubjectId = 6,
               UserId = "ce976dc4-b663-4fdb-aa6c-08d87af5ceab",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 13),
               SubjectId = 6,
               UserId = "ce976dc4-b663-4fdb-aa6c-08d87af5ceab",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 20),
               SubjectId = 6,
               UserId = "ce976dc4-b663-4fdb-aa6c-08d87af5ceab",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 27),
               SubjectId = 6,
               UserId = "ce976dc4-b663-4fdb-aa6c-08d87af5ceab",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 3),
               SubjectId = 6,
               UserId = "ce976dc4-b663-4fdb-aa6c-08d87af5ceab",
               StartTime = TimeSpan.FromHours(10),
               EndTime = TimeSpan.FromHours(12)
           },


           new Lecture {
               DateTime = new DateTime(2025, 5, 3),
               SubjectId = 7,
               UserId = "eacdb3cd-d111-43b7-8f43-eced297241e0",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 10),
               SubjectId = 7,
               UserId = "eacdb3cd-d111-43b7-8f43-eced297241e0",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 17),
               SubjectId = 7,
               UserId = "eacdb3cd-d111-43b7-8f43-eced297241e0",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)

           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 24),
               SubjectId = 7,
               UserId = "eacdb3cd-d111-43b7-8f43-eced297241e0",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 7, 1),
               SubjectId = 7,
               UserId = "eacdb3cd-d111-43b7-8f43-eced297241e0",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },


           new Lecture {
               DateTime = new DateTime(2025, 5, 6),
               SubjectId = 8,
               UserId = "f00d205f-5f80-4089-9a16-3ba2d4d21392",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 13),
               SubjectId = 8,
               UserId = "f00d205f-5f80-4089-9a16-3ba2d4d21392",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 20),
               SubjectId = 8,
               UserId = "f00d205f-5f80-4089-9a16-3ba2d4d21392",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 5, 27),
               SubjectId = 8,
               UserId = "f00d205f-5f80-4089-9a16-3ba2d4d21392",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           },
           new Lecture {
               DateTime = new DateTime(2025, 6, 3),
               SubjectId = 8,
               UserId = "f00d205f-5f80-4089-9a16-3ba2d4d21392",
               StartTime = TimeSpan.FromHours(12),
               EndTime = TimeSpan.FromHours(2)
           }

   };
            foreach (var lecture in Lectures)
            {
                if (!_context.Lectures.Any(e => e.DateTime == lecture.DateTime &&
                                     e.SubjectId == lecture.SubjectId &&
                                     e.UserId == lecture.UserId) )
                {
                    _context.Lectures.Add(lecture);
                }
            }
            _context.SaveChanges();

            //add Students 

            var Students = new[]
            {
                      new Student {

                          BornDate = new DateTime(2000, 1, 1),
                          Gender = (Enumeration.Gender)1,
                          Career = "Flutter Developer",
                          levelId = 1,
                          UserId = "0224106c-db2e-4eb6-83ed-e89ed51678e9"
                      },
                      new Student {

                          BornDate = new DateTime(2002, 7, 1),
                          Gender = (Enumeration.Gender)1,
                          Career = "Flutter Developer",
                          levelId = 1,
                          UserId = "1d36a993-9dcf-410b-b1ce-80c1568f366a"

                       },
                      new Student {

                          BornDate = new DateTime(1999, 3, 5),
                          Gender = (Enumeration.Gender)1,
                          Career = "Front-End Developer",
                          levelId = 1,
                          UserId = "41b53cfb-7c1c-4729-a821-29012347947f"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 4, 1),
                          Gender = (Enumeration.Gender)1,
                          Career = "Flutter Developer",
                          levelId = 1,
                          UserId = "668dc553-f5c6-46ca-b40e-3842011d25f1"
                      },
                      new Student {

                          BornDate = new DateTime(2001, 5, 1),
                          Gender = (Enumeration.Gender)1,
                          Career = "Front-End Developer",
                          levelId = 1,
                          UserId = "67623a13-dd29-4b86-905b-4b8e72ee9dd8"
                      },



                      new Student {

                          BornDate = new DateTime(1998, 6, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Front-End Developer",
                          levelId = 2,
                          UserId = "550f0739-5767-4f87-aafa-9b1df2157d9e"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 7, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Asp.Net Developer",
                          levelId = 2,
                          UserId = "dc733731-add7-4ed3-ac2f-b43748a090dd"

                      },
                      new Student {

                          BornDate = new DateTime(2002, 8, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Front-End Developer",
                          levelId = 2,
                          UserId = "e7dcaef1-ff15-49d5-a10e-352c667241fa"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 9, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Ui/Ux",
                          levelId = 2,
                          UserId = "c655449f-0776-45ee-a516-99b56a66e20b"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 10, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Flutter Developer",
                          levelId = 2,
                          UserId = "7d207186-9b19-4e99-8597-139a73cc2c12"
                      },

                      new Student {

                          BornDate = new DateTime(2000, 11, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Graphic Designer",
                          levelId = 3,
                          UserId = "907b2522-1821-438e-a6e5-4a2f7145bfe4"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 12, 1),
                          Gender = (Enumeration.Gender)2,
                          Career = "Flutter Developer",
                          levelId = 3,
                          UserId = "d2763c3c-e776-44e8-9ead-cc45ff1f0b42"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 5),
                          Gender = (Enumeration.Gender)2,
                          Career = "Graphic Designer",
                          levelId = 3,
                          UserId = "9009c045-fda4-48b5-8640-d97be415fa38"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 10),
                          Gender = (Enumeration.Gender)1,
                          Career = "Network Developer",
                          levelId = 3,
                          UserId = "1fe27334-09f7-4e77-86b1-0b03b16dce26"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 16),
                          Gender = (Enumeration.Gender)1,
                          Career = "Embeded system Developer",
                          levelId = 3,
                          UserId = "89918968-7d07-48d9-ae64-d383349be06a"
                      },

                      new Student {

                          BornDate = new DateTime(2000, 1, 20),
                          Gender = (Enumeration.Gender)2,
                          Career = "Graphic Designer",
                          levelId = 4,
                          UserId = "8f5947a5-d58d-4971-a689-beee02010fb8"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 13),
                          Gender = (Enumeration.Gender)2,
                          Career = "ASP.Net Developer",
                          levelId = 4,
                          UserId = "0b889bfa-108f-4c41-8c32-dad6ba0c61ec"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 25),
                          Gender = (Enumeration.Gender)1,
                          Career = "Front-End Developer",
                          levelId = 4,
                          UserId = "dbd11856-1939-493d-9f09-1822ca26f1d3"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 16),
                          Gender = (Enumeration.Gender)1,
                          Career = "Ui/Ux",
                          levelId = 4,
                          UserId = "ee71444e-d150-43d6-bd20-61e078ecde21"
                      },
                      new Student {

                          BornDate = new DateTime(2000, 1, 18),
                          Gender = (Enumeration.Gender)2,
                          Career = "Ui/Ux",
                          levelId = 4,
                          UserId = "fc450442-ccc2-4443-b5c4-0bc1d434f20f"
                      }
            };
            foreach (var student in Students)
            {
                if (!_context.Students.Any(e => e.UserId == student.UserId))
                {
                    _context.Students.Add(student);
                }
            }
            _context.SaveChanges();

        }
    }
}
