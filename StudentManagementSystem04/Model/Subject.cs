namespace StudentManagementSystem04.Model
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int LevelId { get; set; }

    
        public ICollection<Student> Students { get; set; }

        public Level Level { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<Lecture> Lectures { get; set; }

        public ICollection<Task> Tasks { get; set; }

        public ICollection<UniProject> UniProjects { get; set; }
    }
}
