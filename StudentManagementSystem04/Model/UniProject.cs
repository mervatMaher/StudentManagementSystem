using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem04.Model
{
    public class UniProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string MainImage { get; set; }
        public DateTime? UploadTime {  get; set; }
        [NotMapped]
        public string AppName { get; set; }
        public int? Rate { get; set; }
        public string? Feedback { get; set; }
        public int CategoryId { get; set; }
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public int TaskId { get; set; }
       
        public ICollection<Student> Students { get; set; }
        public ICollection<StudentUniProject> StudentUniProjects { get; set; }
        public Category Category { get; set; }
        public ICollection<AttachedImage> AttachedImages { get; set; }

        public ApplicationUser User { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Subject Subject { get; set; }
        public Task task { get; set; }
      
    }
}
