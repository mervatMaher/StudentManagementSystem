using StudentManagementSystem04.Model;

namespace StudentManagementSystem04.ViewModels
{
    public class TaskModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime UploadTime { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string ColorCode { get; set; }
        public int CategoryId { get; set; }
        public int SubjectId { get; set; }
    }
}
