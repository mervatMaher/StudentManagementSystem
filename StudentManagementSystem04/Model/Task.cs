using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem04.Model
{
    public class Task
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string ColorCode { get; set; }
        public string UserId { get; set; }
        public int CategoryId { get; set; }
        public int SubjectId { get; set; }
        public DateTime? UploadTime { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Student> Students { get; set; }
        public Category Category { get; set; }
        public Subject Subject { get; set; }
        public ICollection<UniProject> UniProjects { get; set; }
    }
}
