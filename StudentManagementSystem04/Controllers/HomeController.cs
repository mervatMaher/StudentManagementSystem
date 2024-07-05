using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem04.ViewModels;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;
using Microsoft.OpenApi.Any;
using Azure.Core;
using studentmanagementsystem04.methodheloper;


namespace StudentManagementSystem04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly StudentManagementSystemDbContext _context;
        public HomeController(StudentManagementSystemDbContext context)
        {
            _context = context;
        }


        // get all Categories
        [HttpGet("getCategories")]
        public IActionResult getCategories()
        {

            var result = _context.Categories.Select(c=> new
            {
                c.Id,
                c.Name
            }).ToList();
            return Ok(result);
        }

        //get All UniProjects with rate up 7 
        [HttpGet("getAllProjects")]
        public IActionResult getAllProjects()
        {
            var result = _context.UniProjects
                .Where(e => e.Rate >= 7)
                .Select(u=> new
                {
                    u.Id,
                    u.Title,
                    u.Description,
                    u.Link,
                    u.MainImage,
                    u.Rate,
                    u.Feedback,
                    u.UploadTime,
                    CategoryName = _context.Categories.Where(c=>c.Id == u.CategoryId).Select(c=>c.Name).FirstOrDefault(),                
                    DoctorName = _context.Users.Where(d=>d.Id == u.UserId).Select(u=>u.FullName).FirstOrDefault(),                
                    SubjectName = _context.Subjects.Where(s=>s.Id == u.SubjectId).Select(u => u.Name).FirstOrDefault(),
                    TsakName = _context.Tasks.Where(t=>t.Id == u.TaskId).Select(t=> t.Name).FirstOrDefault(),
                    StudentsInPrject =_context.StudentUniProjects.Where(uni=>uni.UniProjectId == u.Id)
                    .Select(s=> new
                    {
                        s.StudentId,
                        StudentName = _context.Students.Where(stu=>stu.Id == s.StudentId).Include(u=>u.User)
                        .Select(u=>u.User.FullName).FirstOrDefault(),
                        StudentProfile = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                        .Select(u=>u.User.ProfileImageUrl).FirstOrDefault()
                    }).ToList()
                })
                .ToList();      
            return Ok(result);
        }

        // get categoryPrjects 
        [HttpGet("GetCategoryPrjects")]
        public IActionResult GetCategoryPrjects(int CategId)
        {
            var category = _context.Categories.Find(CategId);
            if (category == null)
            {
                return NotFound(new { Message = "Category not found" });
            }

            var result = _context.UniProjects
                .Where(e => e.CategoryId == CategId && e.Rate >= 7)
                .Select( p=> new
                {
                    p.Id,
                    p.Title,
                    p.Description,
                    p.Link,
                    p.MainImage,
                    p.Rate,
                    p.Feedback,
                    p.CategoryId,
                    p.UserId,
                    p.SubjectId,
                    p.UploadTime,
                    SubjectName = _context.Subjects.Where(s=>s.Id == p.SubjectId).Select(s=>s.Name).FirstOrDefault(),
                    p.TaskId,
                    TaskName = _context.Tasks.Where(t=>t.Id == p.TaskId).Select(t=>t.Name).FirstOrDefault(),
                    StudentsInProject = _context.StudentUniProjects.Where(uni=>uni.UniProjectId == p.Id)
                    .Select(s=> new
                    {
                        s.StudentId,
                        StudentName = _context.Students.Where(stu=>stu.Id == s.StudentId).Include(u=>u.User)
                        .Select(s=>s.User.FullName).FirstOrDefault(),
                        StudentProfileImage = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                        .Select(s => s.User.ProfileImageUrl).FirstOrDefault()
                    }).ToList()
                }).ToList();

            return Ok(result);
            
        }

        // get project details
        [HttpGet("getUniProjectDetails")]
        public IActionResult getUniProjectDetails(int UniProjectId)
        {

            var UniProject = _context.UniProjects.Find(UniProjectId);
            if (UniProject == null)
            {
                return NotFound(new { Message = "UniPrject not found" });
            }
            var result = _context.UniProjects.SingleOrDefault(e => e.Id == UniProjectId);

            if (result != null)
            {
                var ProjectDetails = new
                {
                    ProjectId = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    Link = result.Link,
                    MainImage = result.MainImage,
                    Rate = result.Rate,
                    Feedback = result.Feedback,
                    UploadTime = result.UploadTime,

                    CategoryId = result.CategoryId,
                    CategoryName = _context.Categories.Where(c => c.Id == result.CategoryId).Select(c => c.Name).FirstOrDefault(),
                    UserId = result.UserId,
                    UserName = _context.Users.Where(u => u.Id == result.UserId).Select(u => u.FullName).FirstOrDefault(),
                    SubjectId = result.SubjectId,
                    SubjectName = _context.Subjects.Where(s => s.Id == result.SubjectId).Select(sub => sub.Name).FirstOrDefault(),
                    TaskId = result.TaskId,
                    TaskName = _context.Tasks.Where(t => t.Id == result.TaskId).Select(t => t.Name).FirstOrDefault(),
                    AttachedImagesInProject = _context.AttachedImages.Where(a => a.UniProjectId == result.Id)
                    .Select(a=>new
                    {
                        a.Id,
                        a.Image
                    }).ToList(),

                   StudentsInProject = _context.StudentUniProjects
                   .Where(uni=>uni.UniProjectId == result.Id)
                   .Select(s=> new
                   {
                       s.StudentId,
                       StudentName = _context.Students.Where(stu=>stu.Id == s.StudentId).Include(u=>u.User)
                       .Select(u=>u.User.FullName).FirstOrDefault(),
                       StudentImage = _context.Students.Where(stu=> stu.Id == s.StudentId).Include(u=>u.User)
                       .Select(u=>u.User.ProfileImageUrl).FirstOrDefault(),

                       StudentNumber = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                       .Select(u => u.User.PhoneNumber).FirstOrDefault()
                   }).ToList()
                };

                return Ok(ProjectDetails);
                 
            }
            else
            {
                var errorMessage = new
                {
                    Message = "There is no Project With this Id"
                };

                return NotFound(errorMessage);
            }

        }


        [HttpGet("getPersonalProjectDetails")]
        public IActionResult getPersonalProjectDetails(int PersonalProjectId)
        {
            var PersonalProject = _context.PersonalProjects.Find(PersonalProjectId);
            if (PersonalProject == null)
            {
                return NotFound(new { Message = "PersonalProject not found" });
            }

            var result = _context.PersonalProjects.SingleOrDefault(e => e.Id == PersonalProjectId);

            if (result != null)
            {
                var ProjectDetails = new
                {
                    ProjectId = result.Id,
                    Title = result.Title,
                    Description = result.Description,
                    Link = result.Link,
                    MainImage = result.MainImage, 
                    UploadTime = result.UploadTime,

                    CategoryId = result.CategoryId,
                    CategoryName = _context.Categories.Where(c => c.Id == result.CategoryId).Select(c => c.Name).FirstOrDefault(),

                    StudentName = _context.Students.Where(u => u.Id == result.StudentId)
                    .Include(s => s.User)
                    .Select(s => s.User.FullName).FirstOrDefault(),

                    StudentNumber = _context.Students.Where(u => u.Id == result.StudentId)
                    .Include(s=> s.User)
                    .Select(s =>s.User.PhoneNumber).FirstOrDefault(),
                
                   
                    AttachedImagesInProject = _context.AttachedImages.Where(a => a.PersonalProjectId == result.Id)
                    .Select(a => new
                    {
                        a.Id,
                        a.Image
                    }).ToList(), 
                };

                return Ok(ProjectDetails);

            }
            else
            {

                return NotFound();
            }

        }

        // get Profile 
        [HttpGet("GetStudentProfile")]

        public IActionResult GetStudentProfile(int StudentId)
        {

            var StudentProfile = _context.Students.Find(StudentId);
            if (StudentProfile == null)
            {
                return NotFound(new { Message = "StudentProgile not found" });
            }  
            var result = _context.Students.Include(e => e.User)
                .Where(e => e.Id == StudentId)
                .Select(e => new
                {
                    e.User.FullName,
                    e.User.ProfileImageUrl,
                    e.User.UserName,
                    e.User.PhoneNumber,
                    e.Career,
                    e.BornDate,
                    e.Gender,
                    e.CoverPhoto,

                    UniProjects = _context.StudentUniProjects
                    .Where(u => u.StudentId == StudentId)
                    .Include(u => u.UniProject)
                    .Select(uni => new
                    {
                        uni.UniProject.Id,
                        uni.UniProject.Title,
                        uni.UniProject.Description,
                        uni.UniProject.Link,
                        uni.UniProject.MainImage,
                        uni.UniProject.Feedback,
                        uni.UniProject.Rate,
                        uni.UniProject.UploadTime,
                        CategoryId = uni.UniProject.CategoryId,
                        CategoryName = _context.Categories.Where(c => c.Id == uni.UniProject.CategoryId).Select(c => c.Name).FirstOrDefault(),
                        UserId = uni.UniProject.UserId,
                        UserName = _context.Users.Where(u => u.Id == uni.UniProject.UserId).Select(u => u.FullName).FirstOrDefault(),
                        SubjectId = uni.UniProject.SubjectId,
                        SubjectName = _context.Subjects.Where(s => s.Id == uni.UniProject.SubjectId).Select(sub => sub.Name).FirstOrDefault(),
                        TaskId = uni.UniProject.TaskId,
                        TaskName = _context.Tasks.Where(t => t.Id == uni.UniProject.TaskId).Select(t => t.Name).FirstOrDefault(),

                        AttachedImagesInProject = _context.AttachedImages.Where(a => a.UniProjectId == uni.UniProject.Id)
                    .Select(a => new
                    {
                        a.Id,
                        a.Image
                    }).ToList(),

                   
                        StudentsInProject = _context.StudentUniProjects
                   .Where(uniPro => uniPro.UniProjectId == uni.UniProject.Id)
                   .Select(s => new
                   {
                       s.StudentId,
                       StudentName = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                       .Select(u => u.User.FullName).FirstOrDefault(),
                       StudentProfileImage = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u=>u.User)
                       .Select(u=>u.User.ProfileImageUrl).FirstOrDefault(),
                       StudentNumber = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                       .Select(u=> u.User.PhoneNumber).FirstOrDefault()

                   }).ToList()

                    }).ToList(),

                    PersonalProjects = _context.PersonalProjects.Where(p => p.StudentId == e.Id)
                    .Select(p => new
                    {
                        p.Id,
                        p.Title,
                        p.Description,
                        p.Link,
                        p.MainImage,
   

                    }).ToList()

                }).FirstOrDefault();
        
                return Ok(result); 

        }

        //Edit Profile 

        [HttpPut("EditProfile")]
        public async Task<IActionResult> EditProfile(string UserId, [FromForm] EditVisitorModel editVisitor)
        {

            var rootDirectory = Directory.GetCurrentDirectory();
            var UploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");

            if (!Directory.Exists(UploadsDirectory)) {
                Directory.CreateDirectory(UploadsDirectory);
            }

            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }
            var user = _context.Users.Find(UserId);
            var student = _context.Students.FirstOrDefault(s => s.UserId == UserId);


            if (user != null)
            {
                user.FullName = editVisitor.FullName;
                user.PasswordHash = editVisitor.Password;
                user.PhoneNumber = editVisitor.PhoneNumber;
               

                if (student != null )
                {
                    student.Career = editVisitor.Career;
                }
                if (editVisitor.ProfileImageUrl != null)
                {
                    var ProfileImagePath = Path.Combine(UploadsDirectory, editVisitor.ProfileImageUrl.FileName);

                    using (var stream = new FileStream(ProfileImagePath, FileMode.Create))
                    {
                        await editVisitor.ProfileImageUrl.CopyToAsync(stream);
                    }

                    user.ProfileImageUrl = UrlHelper.GetAbsoluteUrl(HttpContext.Request, "/uploads/" + editVisitor.ProfileImageUrl.FileName);
                }
                

                _context.SaveChanges();

            }

            var existUser = new
            {
                FullName = user.FullName,
                Password = user.PasswordHash,
                PhoneNumber = user.PhoneNumber,
                ProfileImage = user.ProfileImageUrl,
                Career = student?.Career
            };
            return Ok(existUser);
        }

        [HttpGet("GetEditInfo")]
        public IActionResult GetEditInfo(string UserId)
        {
            var user = _context.Users.Find(UserId);
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }
            var EditInfo = new
            {
                user.FullName,
                user.UserName,
                user.PasswordHash,
                user.ProfileImageUrl,
                user.PhoneNumber,
                StudentCareer = _context.Students.Where(s=>s.UserId == UserId).Select(s=>s.Career).FirstOrDefault()
            };
            return Ok(EditInfo);
        }

        // search
        [HttpGet("StudentSearch")]
        public IActionResult StudentSearch(string Usersearch)
        {
            if (string.IsNullOrEmpty(Usersearch))
            {
                var errorMessage = new
                {
                    Message = "the search input is empty, please write what you want to search about"
                };
                return NotFound(errorMessage);
            }

            var result = _context.Students.Include(u => u.User)
                .Where(s => s.User.FullName.Contains(Usersearch) || s.User.UserName.Contains(Usersearch))
                .Select(s => new
                {
                    UserId = s.User.Id,
                    StudentId =  s.Id,
                    s.User.FullName,
                    s.User.ProfileImageUrl,
                    s.User.UserName,
                    s.Career
                }).ToList();
            return Ok(result);
        }

        [HttpGet("ProjectSearch")]
        public IActionResult ProjectSearch(string projectSearch)
        {

            if (string.IsNullOrEmpty(projectSearch))
            {
                var errorMessage = new
                {
                    Message = "the search input is empty, please write what you want to search about"
                };
                return Ok(errorMessage);
            }


            var ProjectResult = _context.UniProjects
             .Where(e => e.Title.Contains(projectSearch) || e.Description.Contains(projectSearch))
             .Select(e => new
             {
                 e.Id,
                 e.Title,
                 e.MainImage,
                 e.Description,

             }).ToList(); // Materialize the query results into a list
            return Ok(ProjectResult);

        }

        //private string GetAbsoluteUrl(string relativeUrl)
        //{
        //    var request = HttpContext.Request;
        //    var absoluteUri = string.Concat(
        //        request.Scheme,
        //        "://",
        //        request.Host.ToUriComponent(),
        //        request.PathBase.ToUriComponent(),
        //        relativeUrl
        //    );
        //    return absoluteUri;
        //}

        //[HttpGet("ProjectInSearch")]
        //public IActionResult ProjectInSearch(string projectSearch = null)
        //{

        //    var uniProjectsQuery = _context.UniProjects.Select(e => new
        //    {
        //        e.Id,
        //        e.Title,
        //        e.MainImage,
        //        e.Description,
        //        ProjectType = "UniProject"
        //    });

        //    var personalProjectsQuery = _context.PersonalProjects.Select(e => new
        //    {
        //        e.Id,
        //        e.Title,
        //        e.MainImage,
        //        e.Description,
        //        ProjectType = "PersonalProject" 
        //    });

        //    if (!string.IsNullOrEmpty(projectSearch))
        //    {
        //        uniProjectsQuery.Where(e => e.Title.Contains(projectSearch) || e.Description.Contains(projectSearch));
        //        personalProjectsQuery.Where(e => e.Title.Contains(projectSearch) || e.Description.Contains(projectSearch));

        //    }


        //    var uniProjectsResult = uniProjectsQuery.ToList();
        //    var personalProjectsResult = personalProjectsQuery.ToList();

        //    var combinedResults = uniProjectsResult.Concat(personalProjectsResult).ToList();
        //    return Ok(combinedResults);

        //}


    }
}
