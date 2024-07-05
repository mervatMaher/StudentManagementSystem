namespace StudentManagementSystem04.Model
{
    public class Level
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Student> Students { get; set; }
        public ICollection<Subject> Subjects { get; set; }
        public ICollection<ApplicationUser> ApplicationUsers { get; set; }
        //public ICollection<DoctorLevel> DoctorLevels { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
