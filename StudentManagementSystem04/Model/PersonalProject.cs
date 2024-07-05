using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem04.Model
{
    public class PersonalProject
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public string MainImage { get; set; }
        [NotMapped]
        public string AppName { get; set; }
        public int CategoryId { get; set; }
        public int StudentId { get; set; }
        public DateTime? UploadTime {  get; set; }

        public Student Student { get; set; }

        public ICollection<AttachedImage> attachedImages { get; set; }
        public ICollection<Like> Likes { get; set; }
        public ICollection<Comment> Comments { get; set; }
        public Category Category { get; set; }
    }
}
