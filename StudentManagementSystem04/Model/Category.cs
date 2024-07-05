namespace StudentManagementSystem04.Model
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<PersonalProject> PersonalProjects { get; set; }
        public ICollection<UniProject> UniProjects { get; set; }
        public ICollection<Task> Tasks { get; set; }
    }
}
