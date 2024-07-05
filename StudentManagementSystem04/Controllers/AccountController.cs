using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem03.ViewModels;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;
using StudentManagementSystem04.ViewModels;

namespace StudentManagementSystem04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly StudentManagementSystemDbContext _context;
        private readonly IServiceProvider _serviceProvider;
        private readonly SignInManager<ApplicationUser> _signIn;


        public AccountController(StudentManagementSystemDbContext context, IServiceProvider serviceProvider)
        {
            _roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            _userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            _signIn = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();
            _context = context;
            _serviceProvider = serviceProvider;

        }

        [HttpPut("AddVisitor")]
        public async Task<IActionResult> AddVisitor(VisitorModel request)
        {
            var rolesName = new[] { "Admin", "Doctor", "Student", "Visitor" };

            foreach (var role in rolesName)
            {

                if (!_context.Roles.Any(e => e.Name == role))
                {
                    try
                    {
                        await _roleManager.CreateAsync(new IdentityRole(role));
                    }
                    catch (Exception ex)
                    {

                        Console.WriteLine($"Error creating role {role}: {ex.Message}");
                    }
                }
            }

            var user = new ApplicationUser
            {
                FullName = request.FullName,
                UserName = request.UserName,
                PasswordHash = request.Password
           
            };
           


            var result = await _userManager.CreateAsync(user);
            await _context.SaveChangesAsync();
            if (result.Succeeded)
            {
               await _signIn.SignInAsync(user, true);
            }

            

            await _userManager.AddToRoleAsync(user, "Visitor");

            var roles = await _userManager.GetRolesAsync(user);
            var roleString = String.Join(", ", roles);

            var userInfo = new
            {
                UserId = user.Id,
                FullName = user.FullName,
                UserName = user.UserName,
                ProfileImageUrl = user.ProfileImageUrl,
                Role = roleString
            };

           

            await _context.SaveChangesAsync();

            return Ok(userInfo);

        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login(LogInModel logInModel)
        {
            var user = _context.Users.FirstOrDefault(u => u.UserName == logInModel.UserName);

            if (user != null)
            {
                if (user.PasswordHash == logInModel.Password)
                {
                    var StudentUser = _context.Students.FirstOrDefault(s => s.UserId == user.Id);
                    var roles = await _userManager.GetRolesAsync(user);
                    var roleString = String.Join(", ", roles);


                    if (StudentUser != null)
                    {
                        var studentInfo = _context.Students
                       .Where(s => s.UserId == user.Id).Include(u => u.User)
                       .Select(s => new
                       {
                           UserId = s.User.Id,
                           StudentId = s.Id,
                           FullName = s.User.FullName,
                           UserName = s.User.UserName,
                           ProfileImageUrl = s.User.ProfileImageUrl,
                           PhoneNumber = s.User.PhoneNumber,

                           BornDate = s.BornDate,
                           Gender = s.Gender,
                           Career = s.Career,
                           Role = roleString
                       }).FirstOrDefault();

                        return Ok(studentInfo);
                    }
                    var userInfo = new
                    {
                        UserId = user.Id,
                        FullName = user.FullName,
                        UserName = user.UserName,
                        ProfileImageUrl = user.ProfileImageUrl,
                        PhoneNumber = user.PhoneNumber,
                        Role = roleString
                    };

                    return Ok(userInfo);
                }

            }

            var errorMessage = new
            {
                Message = "There is no User with this Name"
            };
            return NotFound(errorMessage);
        }
    }
}
