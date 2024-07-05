namespace StudentManagementSystem04.Model
{
    public class AttachedImage
    {
        public int Id { get; set; }
        public string Image { get; set; }
        public int? UniProjectId { get; set; }
        public int? PersonalProjectId { get; set; }
        public UniProject? UniProject { get; set; }
        public PersonalProject PersonalProject { get; set; }
    }
}
