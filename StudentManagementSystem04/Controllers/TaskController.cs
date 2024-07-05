using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using StudentManagementSystem04.ViewModels;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;
using Microsoft.EntityFrameworkCore;

namespace StudentManagementSystem04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TaskController : ControllerBase
    {
        private readonly StudentManagementSystemDbContext _context;
        public TaskController(StudentManagementSystemDbContext context)
        {
            _context = context;
        }
        // add Task
        [HttpPost("AddTask")]
        public IActionResult AddTask( string UserId,TaskModel request)
        {
            // search with subject id to get all tasks (still missing)

            var task = new Model.Task
            {
                Name = request.Name,
                Description = request.Description,
                StartDate = request.StartTime,
                EndDate = request.EndTime,
                ColorCode = request.ColorCode,
                CategoryId = request.CategoryId,
                SubjectId = request.SubjectId,
                UploadTime = request.UploadTime,
                UserId = UserId
            };

            if (!_context.Tasks.Any(e => e.Id == task.Id))
            {
                _context.Tasks.Add(task);
                _context.SaveChanges();
            }
            

            var addedTask = new
            {
                Id = task.Id,
                Name = task.Name,
                Description = task.Description,
                StartDate = task.StartDate,
                EndDate = task.EndDate,
                ColorCode = task.ColorCode,
                CategoryId = task.CategoryId,
                SubjectId = task.SubjectId,
                UserId = task.UserId,
                
            };

            return Ok(addedTask);
        }

        // Delete
        [HttpDelete]
        public IActionResult DeleteTask(int TaskId)
        {
            var task = _context.Tasks.Find(TaskId);
            if (task == null)
            {
                return NotFound(new { Message = "PersonalProject not found" });
            }
            _context.Tasks.Remove(task);
            _context.SaveChanges();
            return Ok("the task has been removed");
        }

        // Edit Task
        [HttpPut("EditTask")]
        public IActionResult EditTask(int TaskId, TaskModel taskModel)
        {
            if (!ModelState.IsValid)
            {

                return BadRequest(ModelState);

            }
            var exestingTask = _context.Tasks.Find(TaskId);

            if (exestingTask == null)
            {
                return NotFound(new { Message = "PersonalProject not found" });
            }

            exestingTask.Name = taskModel.Name;
            exestingTask.Description = taskModel.Description;
            exestingTask.StartDate = taskModel.StartTime;
            exestingTask.EndDate = taskModel.EndTime;
            exestingTask.ColorCode = taskModel.ColorCode;
            exestingTask.CategoryId = taskModel.CategoryId;
            exestingTask.SubjectId = taskModel.SubjectId;
            exestingTask.UploadTime = taskModel.UploadTime;


            _context.SaveChanges();
            var existTask = new
            {

                Id = exestingTask.Id,
                Name = exestingTask.Name,
                Description = exestingTask.Description,
                StartDate = exestingTask.StartDate,
                EndDate = exestingTask.EndDate,
                UploadTime = exestingTask.UploadTime,
                ColorCode = exestingTask.ColorCode,
                CategoryID = exestingTask.CategoryId,
                CategoryName = _context.Categories.Where(c => c.Id == exestingTask.CategoryId).Select(c => c.Name).FirstOrDefault(),
                SubjectId = exestingTask.SubjectId,
                SubjectName = _context.Subjects.Where(s=>s.Id == exestingTask.SubjectId).Select(s=>s.Name).FirstOrDefault()

            };
            
            return Ok(existTask);
        }
        // get all Tasks 
        [HttpGet("GetTasks")]
        public IActionResult GetTasks(string user)
        {
            var Doctor = _context.Users.Find(user);
            if (Doctor == null)
            {
                return NotFound(new { Message = "Doctor not found" });
            }


            var tasks = _context.Tasks
                .Where(t=>t.UserId == user).Select(t=> new
            {
                    t.Id,
                    t.Name,
                    t.StartDate,
                    t.EndDate,
                    t.UploadTime,
                    t.ColorCode,
                    t.UserId,
                    t.CategoryId,
                    t.SubjectId
            }).ToList();
          
            return Ok(tasks);
        }

        [HttpGet("SubjectTasks")]
        public IActionResult SubjectTasks(int subjectId)
        {
            var Subject = _context.Subjects.Find(subjectId);
            if (Subject == null)
            {
                return NotFound(new { Message = "Subject not found" });
            }

            var subjectTasks = _context.Tasks.Where(t=>t.SubjectId ==  subjectId)
                .Select(t=> new
                {
                    t.Id,
                    t.Name,
                    t.Description,
                    t.StartDate,
                    t.EndDate,
                    t.UploadTime,
                    t.ColorCode,
                    t.UserId,
                    t.CategoryId,
                    t.SubjectId
                }).ToList();
         
            return Ok(subjectTasks);
        }
        // get all UniProjects in spacific task
        [HttpGet("GetUniProjectsInTask")]
        public IActionResult GetUniProjectsInTask(int TaskId)
        {
            var task = _context.Tasks.Find(TaskId);
            if (task == null)
            {
                return NotFound(new { Message = "Task not found" });
            }

            var result = _context.UniProjects
                .Where(e => e.TaskId == TaskId)
                .Select(u=>  new
                {
                    u.Id,
                    u.Title,
                    u.Description,
                    u.MainImage,
                    u.UploadTime,
                    u.Rate,
                    u.Feedback,
                    u.CategoryId, 
                    CategoryName = _context.Categories.Where(c=>c.Id == u.CategoryId).Select(c=>c.Name).FirstOrDefault(),
                    u.UserId,
                    DoctorName = _context.Users.Where(user=>user.Id == u.UserId).Select(u=>u.FullName).FirstOrDefault(),
                    u.SubjectId,
                    SubjectName = _context.Subjects.Where(s=>s.Id == u.SubjectId).Select(s=>s.Name).FirstOrDefault(),
                    u.TaskId,
                    TaskName= _context.Tasks.Where(t => t.Id == u.TaskId).Select(t => t.Name).FirstOrDefault(),

                    StudentsInProject = _context.StudentUniProjects
                   .Where(uni => uni.UniProjectId == u.Id)
                   .Select(s => new
                   {
                       s.StudentId,
                       StudentName = _context.Students.Where(stu => stu.Id == s.StudentId)
                       .Include(s=>s.User).Select(u=>u.User.FullName).FirstOrDefault(),
                       StudentImage = _context.Students.Where(stu => stu.Id == s.StudentId).Include(u => u.User)
                       .Select(u => u.User.ProfileImageUrl).FirstOrDefault()
                   }).ToList(),

                    AttachedImagesInProject = _context.AttachedImages.Where(a => a.UniProjectId == u.Id)
                    .Select(a => new
                    {
                        a.Id,
                        a.Image
                    }).ToList()

                }).ToList();

          
            return Ok(result);
        }

        // Add rate and feedback to Uniproject
        [HttpPut("RateAndFeedback")]
        public IActionResult RateAndFeedback(int UniProjectId, UniProjectModel uniProjectModel)
        {
            var UniPorject = _context.UniProjects.Find(UniProjectId);

            if (UniPorject == null )
            {
                var errorMessage = new
                {
                    Message = "There is no UniProject with this Id"
                };
                return NotFound(errorMessage);
            }
    
            else
            {
                UniPorject.Rate = uniProjectModel.Rate;
                UniPorject.Feedback = uniProjectModel.Feedback;
                _context.SaveChanges();
            }
            return Ok(UniPorject);
        }


    }
}
