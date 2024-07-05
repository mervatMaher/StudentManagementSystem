using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection;
using static StudentManagementSystem04.Enums.Enumeration;

namespace StudentManagementSystem04.Model
{
    public class Student
    {
        public int Id { get; set; }
        public DateTime BornDate { get; set; }
        public Gender Gender { get; set; }
        public string Career { get; set; }
        public bool StudentAttendaceState { get; set; }
        public string? CoverPhoto { get; set; }
        public string UserId { get; set; }
        public int levelId { get; set; }


        public ApplicationUser User { get; set; }
        public Level Level { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<PersonalProject> PersonalProjects { get; set; }
        public ICollection<StudentUniProject> StudentUniProjects { get; set; }
        public ICollection<Lecture> lectures { get; set; }
    }
}
