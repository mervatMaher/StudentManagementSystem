using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem03.ViewModels;
using studentmanagementsystem04.methodheloper;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;
using StudentManagementSystem04.ViewModels;
using System.Linq;
using System.Threading.Tasks;

namespace StudentManagementSystem04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly StudentManagementSystemDbContext _context;
        public StudentController(StudentManagementSystemDbContext context)
        {
            _context = context;
        }

        // get all lectures 
        // i want to send all the lectures that in   subject that this user in 

        [HttpGet("GetAllLectures")]
        public IActionResult GetAllLectures(int studentId)
        {
            var student = _context.Students.Find(studentId);

            if (student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            var lectures = _context.Students
                .Where(s => s.Id == studentId)
                .Include(s => s.Subjects)
                .ThenInclude(s => s.Lectures)
                .SelectMany(s => s.Subjects)
                .SelectMany(s => s.Lectures)
                .Select(lec => new
                {
                    lec.Id,
                    SubjectName = _context.Subjects.FirstOrDefault(s => s.Id == lec.SubjectId).Name,
                    DoctorName = _context.ApplicationUsers.FirstOrDefault(d => d.Id == lec.UserId).FullName,
                    DoctorImage = _context.ApplicationUsers.FirstOrDefault(i => i.Id == lec.UserId).ProfileImageUrl,
                    lec.AttendanceNumber,
                    lec.AttendanceState,
                    lec.DateTime,
                    lec.EndDate
                })
                .ToList();

            return Ok(lectures);
        }



        

        [HttpPost("StudentAttendace")]
        public IActionResult StudentAttendace(int LectureId, int StudentId, bool studentAttendanceFingerPrint)
        {
            var LectureState = _context.Lectures.Find(LectureId).AttendanceState;
            var lecture = _context.Lectures.Find(LectureId);

            var StudentAttendLcture = new StudentLecture
            {
                StudentId = StudentId,
                LectureId = LectureId
            };
            if (LectureState == true)
            {
                if (studentAttendanceFingerPrint == true)
                {
                    var existingAttendance = _context.StudentLectures
                        .FirstOrDefault(sl => sl.StudentId == StudentId && sl.LectureId == LectureId);

                    if (existingAttendance != null)
                    {
                        var response = new
                        {
                            message = "This student has already taken attendance."
                        };
                        return Ok(response);
                    }


                    _context.StudentLectures.Add(StudentAttendLcture);
                    _context.SaveChanges();
                }

            }

            var addedStudentInLecture = new
            {
                studentId = StudentAttendLcture.StudentId,
                LectureId = StudentAttendLcture.LectureId,
                LectureNumber = lecture.AttendanceNumber
            };
            return Ok(addedStudentInLecture);
        }

        //get task info
        [HttpPut("StudentPutLectureNumber")] 
        public IActionResult StudentPutLectureNumber (int LectureId, int StudentId, bool LectureNumberState)
        {
            
            var StudentInLecture = _context.StudentLectures
                .FirstOrDefault(l=> l.LectureId == LectureId && l.StudentId == StudentId);
            if (StudentInLecture == null)
            {
                var errorMessage = new
                {
                    Message = "This Student Didnt attend this lecture"
                };

                return NotFound(errorMessage);
            }
            if (LectureNumberState == true)
            {
                StudentInLecture.AttdentaceNumberState = LectureNumberState;
            }
            var SucceccedMessage = new
            {
                Message = "The student has been successfully attended In this Lecture"
            };
            return Ok(SucceccedMessage);
        }

        [HttpGet("GetTaskInfo")]
        public IActionResult GetTaskInfo(int Taskid)
        {
            var TaskId = _context.Tasks.Find(Taskid);
            if (TaskId == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            var task = _context.Tasks.Where(t => t.Id == Taskid)
                .Select(e => new
                {
                    e.Name,
                    e.Description,
                    e.StartDate,
                    e.EndDate,
                    e.CategoryId, 
                    CategoryName = _context.Categories.Where(c=>c.Id == e.CategoryId).Select(c=>c.Name).FirstOrDefault(),
                    e.ColorCode,
                    DoctorName = _context.Users.Where(u => u.Id == e.UserId).Select(e => e.FullName).FirstOrDefault(),
                    DoctorImage = _context.Users.Where(u=>u.Id == e.UserId).Select(u => u.ProfileImageUrl).FirstOrDefault(),
                    SubjectName = _context.Subjects.Where(s => s.Id == e.SubjectId).Select(s => s.Name).FirstOrDefault(),
                }).FirstOrDefault();

            return Ok(task);
        }

        //post Project

        [HttpPost("PostUniProject")]
        public async Task<IActionResult> PostUniProject(int TaskId , [FromForm] UploadProjectModel uploadProject)
        {
            var rootDirectory = Directory.GetCurrentDirectory();

            // Combine the root directory with the relative path to 'wwwroot/uploads'
            var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");

            // Ensure the 'wwwroot/uploads' directory exists, if not, create it
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }

            var task = _context.Tasks.Find(TaskId);
            var UniProject = new UniProject
            {
                Title = uploadProject.Title,
                Description = uploadProject.Description,
                Link = uploadProject.Link,   
                UploadTime = uploadProject.UploadTime,
                TaskId = TaskId,
                UserId = task.UserId,
                SubjectId = task.SubjectId,
                CategoryId = _context.Categories
                .Where(c => c.Id == task.CategoryId)
                .Select(e => e.Id).FirstOrDefault(),
            };

            if (!_context.UniProjects.Any(u => u.Id == UniProject.Id))
            {
                if (uploadProject.MainImage != null)
                {
                    //var uniqueFilename = Guid.NewGuid().ToString() + "_" + Path.GetFileName(uploadProject.MainImage.FileName);
                    var mainImagePath = Path.Combine(uploadsDirectory, uploadProject.MainImage.FileName);

                    using (var stram = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await uploadProject.MainImage.CopyToAsync(stram);
                    }

                    UniProject.MainImage = UrlHelper.GetAbsoluteUrl(HttpContext.Request, "/uploads/" + uploadProject.MainImage.FileName);
                  
                }

                _context.UniProjects.Add(UniProject);
                await _context.SaveChangesAsync();

                if (uploadProject.AttachedImages != null && uploadProject.AttachedImages.Any())
                {
                    foreach (var Image in uploadProject.AttachedImages)
                    {
                        var ImagePath = Path.Combine(uploadsDirectory, Image.FileName);
                        using (var stream = new FileStream(ImagePath, FileMode.Create))
                        {
                           await Image.CopyToAsync(stream);
                        }
                        
                        var attachedImage = new AttachedImage
                        {
                            Image = UrlHelper.GetAbsoluteUrl(HttpContext.Request ,"/uploads/" + Image.FileName),
                            UniProjectId = UniProject.Id

                        };
                        _context.AttachedImages.Add(attachedImage);
                    }
                    await _context.SaveChangesAsync();
                }

                 
                if (uploadProject.Students != null && uploadProject.Students.Any())
                {
                    foreach (var studentId in uploadProject.Students)
                    {
                        var student = _context.Students.Where(s => s.Id == studentId)
                            .Select(s => s.Id).FirstOrDefault();

                        if (student != default)
                        {

                            var studentUni = new StudentUniProject
                            {
                                StudentId = student,
                                UniProjectId = UniProject.Id
                            };
                            _context.StudentUniProjects.Add(studentUni);
                        }
                    }
 
                    await _context.SaveChangesAsync();
                }
            }

            var allStudents = await _context.StudentUniProjects
                .Where(s => s.UniProjectId == UniProject.Id)
                .Select(e => new
                {
                    e.StudentId,
                    UserId = _context.Students.Where(stu => stu.Id == e.StudentId)
                    .Include(u=>u.User)
                    .Select(user=> user.User.Id).FirstOrDefault(),
                    StudentName = _context.Students.Where(stu => stu.Id == e.StudentId)
                    .Include(u => u.User)
                    .Select(stuName => stuName.User.FullName).FirstOrDefault()

                }).ToListAsync();


            var AllImages = await _context.AttachedImages
               .Where(img => img.UniProjectId == UniProject.Id)
               .Select(e => e.Image)
               .ToListAsync();

            var addedProject = new
            {
                UniProjectId = UniProject.Id,
                Title = UniProject.Title,
                Description = UniProject.Description,
                Link = UniProject.Link,
                UploadTime = UniProject.UploadTime,
                MainImage = UniProject.MainImage,
                CategoryId = UniProject.CategoryId,
                AttachedImages = AllImages,
                Students = allStudents
            };
            return Ok(addedProject);
        }


        [HttpPost("PostPersonalProject")]
        public async Task<IActionResult> PostPersonalProject(int StudentId, [FromForm] UploadPersonalProjectModel uploadProject)
        {
            var rootDirectory = Directory.GetCurrentDirectory();

            // Combine the root directory with the relative path to 'wwwroot/uploads'
            var uploadsDirectory = Path.Combine(rootDirectory, "wwwroot", "uploads");

            // Ensure the 'wwwroot/uploads' directory exists, if not, create it
            if (!Directory.Exists(uploadsDirectory))
            {
                Directory.CreateDirectory(uploadsDirectory);
            }


            var PersonalProject = new PersonalProject
            {
                Title = uploadProject.Title,
                Description = uploadProject.Description,
                Link = uploadProject.Link,
                CategoryId = uploadProject.CategoryId,
                StudentId = StudentId,
                UploadTime = uploadProject.UploadTime,
            };

            if (!_context.PersonalProjects.Any(u => u.Id == PersonalProject.Id))
            {
                if (uploadProject.MainImage != null)
                {
                  
                    var mainImagePath = Path.Combine(uploadsDirectory, uploadProject.MainImage.FileName);
                    using (var stram = new FileStream(mainImagePath, FileMode.Create))
                    {
                        await uploadProject.MainImage.CopyToAsync(stram);
                    }

                    PersonalProject.MainImage = UrlHelper.GetAbsoluteUrl(HttpContext.Request, "/uploads/" + uploadProject.MainImage.FileName);

                }

                _context.PersonalProjects.Add(PersonalProject);
                await _context.SaveChangesAsync();


                if (uploadProject.AttachedImages != null && uploadProject.AttachedImages.Any())
                {
                    foreach (var Image in uploadProject.AttachedImages)
                    {
                        var ImagePath = Path.Combine(uploadsDirectory, Image.FileName);
                        using (var stream = new FileStream(ImagePath, FileMode.Create))
                        {
                            await Image.CopyToAsync(stream);
                        }

                        var attachedImage = new AttachedImage
                        {
                            Image = UrlHelper.GetAbsoluteUrl(HttpContext.Request, "/uploads/" + Image.FileName),
                            PersonalProjectId = PersonalProject.Id

                        };
                        _context.AttachedImages.Add(attachedImage);
                    }
                    await _context.SaveChangesAsync();
                }

            }

            var AllImages = await _context.AttachedImages
                .Where(img => img.PersonalProjectId == PersonalProject.Id)
                .Select(e =>e.Image).ToListAsync();

            var addedProject = new
            {
                PersonalProjectId = PersonalProject.Id,
                Title = PersonalProject.Title,
                Description = PersonalProject.Description,
                Link = PersonalProject.Link,
                
                MainImage = PersonalProject.MainImage,

                CategoryId = PersonalProject.CategoryId,
                StudentId = StudentId,
                UploadTime = PersonalProject.UploadTime,
                StudentName = _context.Students.Where(s => s.Id == StudentId)
                .Include(u => u.User)
                .Select(u => u.User.FullName).FirstOrDefault(),
                AttachedImages = AllImages
            };

            return Ok(addedProject);

        }
        

        [HttpGet("GetAllTasksForStudent")]
        public IActionResult GetAllTasksForStudent (int StudentId)
        {
            var student = _context.Students.Find(StudentId);

            if (student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            var tasks = _context.Students.Where(s => s.Id == StudentId)
                .Include(s => s.Subjects)
                .SelectMany(s => s.Subjects)
                .SelectMany(s => s.Tasks)
                .Select(t => new
                {
                    t.Id,
                    t.Name,
                    t.Description,
                    t.StartDate,
                    t.EndDate,
                    t.ColorCode,
                    DoctorName = _context.Users.Where(u=>u.Id == t.UserId).Select(u=>u.FullName).FirstOrDefault(),
                    DoctorProfileImage = _context.Users.Where(u => u.Id == t.UserId).Select(u => u.ProfileImageUrl).FirstOrDefault(),
                    SubjectName = _context.Subjects.Where(sub => sub.Id == t.SubjectId).Select(sub => sub.Name).FirstOrDefault(),
                    DoctorUserName = _context.Users.Where(u => u.Id == t.UserId).Select(u => u.UserName).FirstOrDefault(),
                    t.UploadTime
                }).ToList();
            return Ok(tasks);
        }

    } 
}
