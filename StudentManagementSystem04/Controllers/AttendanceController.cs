using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StudentManagementSystem04.Data;
using StudentManagementSystem04.Model;

namespace StudentManagementSystem04.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttendanceController : ControllerBase
    {
        private readonly StudentManagementSystemDbContext _context;
        public AttendanceController(StudentManagementSystemDbContext context)
        {
            _context = context;
        }

        //Lectures Depends on Doctor



        // Change Attendace State
        [HttpPut("LectureAttendaceNumberState")]
        public IActionResult LectureAttendaceNumberState(int LectureId)
        {
            var result = _context.Lectures.Find(LectureId);
            if (result == null)
            {
                var errorMessage = new
                {
                    Message = "There is no Lecture with this ID"
                };

                return NotFound(errorMessage);
            };

            if (result.AttendanceState == false)
            {
                result.AttendanceState = true;
            }
            else
            {

                result.AttendanceState = false;

            }
            _context.SaveChanges();
            var attendanaceNum = new
            {
                AttendanceNumber =  result.AttendanceNumber,
                AttendaceState = result.AttendanceState

            };
            return Ok(attendanaceNum);

        }

        // student State 
        [HttpGet("StudentsInLecture")]
        public IActionResult StudentsInLecture(int LectureId)
        {

            var Lecture = _context.Lectures.Find(LectureId);
            if (Lecture == null)
            {
                return NotFound(new { Message = "Lecture not found" });
            }

            var studentsInLecture = _context.StudentLectures.Where(le => le.LectureId == LectureId)
                .Select(s => new
                {
                    s.StudentId,

                    StudentName = _context.Students.Where(e => e.Id == s.StudentId)
                    .Include(e => e.User)
                    .Select(e => e.User.FullName).FirstOrDefault(),

                    StudentImage = _context.Students.Where(e => e.Id == s.StudentId)
                    .Include(e => e.User)
                    .Select(e => e.User.ProfileImageUrl).FirstOrDefault(),

                    StudentAttdentace = s.AttdentaceNumberState
                }).ToList();

            return Ok(studentsInLecture);
        }

        // get all subjects that belong to doctor
        [HttpGet("DoctorSubjects")]
        public IActionResult DoctorSubjects(string DoctorId)
        {
            var Doctor = _context.Users.Find(DoctorId);
            if (Doctor == null)
            {
                return NotFound(new { Message = "Doctor not found" });
            }

            var result = _context.Subjects.Include(e => e.User)
                .Where(e => e.User.Id == DoctorId)
                .Select(e => new
                {
                    e.Id,
                    e.Name
                });
            return Ok(result);
        }

        // get all lectures in subject
        [HttpGet("lecturesInSubject")]
        public IActionResult lecturesInSubject(int subjectId)
        {

            var Subject = _context.Subjects.Find(subjectId);
            if (Subject == null)
            {
                return NotFound(new { Message = "Subject not found" });
            }

            var result = _context.Lectures
                .Where(e => e.SubjectId == subjectId)
                .Select(e => new
                {
                    e.Id,
                    e.DateTime,
                    e.EndDate,
                    e.AttendanceState
                });
            return Ok(result);
        }

        // get all student 
        [HttpGet("TotalStudentsInSubject")]
        public IActionResult TotalStudentsInSubject(int subjectId)
        {
            var SubjectId = _context.Subjects.Find(subjectId);
            if (SubjectId == null)
            {
                return NotFound(new { Message = "Subject not found" });
            }


            var subject = _context.Subjects.Include(e => e.Students)
                .FirstOrDefault(s => s.Id == subjectId);

            var totalStudentsInSubject = subject.Students.Count();

            var totalStudents = new
            {
                number = totalStudentsInSubject
            };
                return Ok(totalStudents);

        }

        // SearchStudentState
        
        [HttpGet("SearchStudentState")]
        public IActionResult SearchStudentState(string studentSearch)
        {
            var student = _context.Students.Include(e => e.User)
                .Where(f => f.User.FullName.Contains(studentSearch) || f.User.UserName.Contains(studentSearch))
                .Select(e => new
                {
                    StudentId = e.Id,
                    StudentName = e.User.FullName,

                    StudentProfileImage = e.User.ProfileImageUrl,

                    LectureState = _context.StudentLectures
                    .Where(sl => sl.StudentId == e.Id)
                    .Select(e => new
                    {
                        e.LectureId,
                        LectureDate = _context.Lectures.Where(lec => lec.Id == e.LectureId)
                        .Select(e => e.DateTime).FirstOrDefault(),
                        e.AttdentaceNumberState
                    }).ToList()
                }).ToList();
            return Ok(student);
        }

        // StudentInLectures
        [HttpGet("StudentInLectures")]
        public IActionResult StudentInLectures(int StudentId)
        {

            var Student = _context.Students.Find(StudentId);
            if (Student == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            var studentInLectures = _context.Students.Where(s => s.Id == StudentId)
                .Include(u => u.User)
                .Select(stu => new
                {
                    StudentID = StudentId,
                    StudentName = stu.User.FullName,
                    StudentImage = stu.User.ProfileImageUrl,
                    Lectures = _context.StudentLectures.Where(s=> s.StudentId == stu.Id)
                    .Select( lec => new
                    {
                        lec.LectureId,
                        LectureDate = _context.Lectures.Where(l=> l.Id == lec.LectureId)
                        .Select(l=>l.DateTime).FirstOrDefault(),
                        lec.AttdentaceNumberState 
                    }).ToList()
                }).ToList();
           
            return Ok(studentInLectures);
        }

        [HttpGet("StudentsInSubject")]
        public IActionResult StudentsInSubject (int SubjectID)
        {
            var Subject = _context.Subjects.Find(SubjectID);
            
            if (Subject == null)
            {
                return NotFound(new { Message = "Student not found" });
            }

            var StudentsInSubject = _context.Subjects.Where(sub => sub.Id == SubjectID)
                .Include(sub => sub.Students)
                .ThenInclude(sub => sub.lectures)
                .SelectMany(sub => sub.Students)
                .Select(s => new
                {
                    StudentId = s.Id,
                    StudentName = _context.Users.Where(u => u.Id == s.UserId).Select(u => u.FullName).FirstOrDefault(),
                    StudentImage = _context.Users.Where(u => u.Id == s.UserId).Select(u => u.ProfileImageUrl).FirstOrDefault(),
                    Lecture = _context.Lectures.Where(l => l.SubjectId == SubjectID)
                    .Select(lec => new
                    {
                        lec.Id,
                        lec.DateTime,
                        LectureAttendaceNumberState = _context.StudentLectures.Where(stu=>stu.StudentId == s.Id)
                        .Select(s=>s.AttdentaceNumberState).FirstOrDefault()
                    }).ToList()

                    //lectures = _context.StudentLectures.Where(l => l.StudentId == s.Id )
                    // .Select(lec => new
                    // {
                    //     lec.LectureId,
                    //     lecturedate = _context.Lectures.Where(l => l.Id == lec.LectureId)
                    //    .Select(l => l.DateTime).FirstOrDefault(),
                    //     lec.AttdentaceNumberState
                    // }).ToList()
                }).ToList();
            return Ok(StudentsInSubject);

        }

        // get all apsent Students

        //[HttpGet("AbsentStudentsInLecture")]
        //public IActionResult AbsentStudentsInLecture(int subjectId, int lectureId)
        //{
        //    var subject = _context.Subjects.Include(e => e.Students)
        //           .FirstOrDefault(s => s.Id == subjectId);

        //    var totalStudentsInSubject = subject.Students.Count();

        //    var presentStudentsCount = _context.StudentLectures
        //        .Count(e => e.LectureId == lectureId && e.AttdentaceNumberState);

        //    var totalAbsentStudents = totalStudentsInSubject - presentStudentsCount;

        //    return Ok(totalAbsentStudents);
        //}

    }
}
