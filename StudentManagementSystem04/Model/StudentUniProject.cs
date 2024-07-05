namespace StudentManagementSystem04.Model
{
    public class StudentUniProject
    {
        public int StudentId { get; set; }
        public int UniProjectId { get; set; }
        public Student Student { get; set; }
        public UniProject UniProject { get; set; }
    }
}
