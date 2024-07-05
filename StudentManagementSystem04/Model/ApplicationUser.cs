using Microsoft.AspNetCore.Identity;

namespace StudentManagementSystem04.Model
{
    public class ApplicationUser : IdentityUser
    {
        public string FullName { get; set; }
        public string? ProfileImageUrl { get; set; }

        public Student student { get; set; }
        public ICollection<Level> Levels { get; set; }
        public ICollection<Comment> comments { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Task> Tasks { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<Lecture> Lectures { get; set; }
        public ICollection<UniProject> UniProjects { get; set; }


    }
}
