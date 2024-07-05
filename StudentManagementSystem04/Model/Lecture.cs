using System.ComponentModel.DataAnnotations.Schema;

namespace StudentManagementSystem04.Model
{
    public class Lecture
    {
        public int Id { get; set; }
        [Column("StartDate")]
        public DateTime DateTime { get; set; }
        public DateTime? EndDate { get; set; }
        // add New EndTime

        // new thing
        [NotMapped]
        public TimeSpan? StartTime { get; set; }

        [NotMapped]
        public TimeSpan? EndTime { get; set; }
        public int? AttendanceNumber { get; set; }
        public bool AttendanceState { get; set; }
        public int SubjectId { get; set; }
        public string UserId { get; set; }
        public ICollection<Student> Students { get; set; }
        public ApplicationUser User { get; set; }
        public Subject Subject { get; set; }
    }
}
