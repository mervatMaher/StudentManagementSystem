namespace StudentManagementSystem04.Model
{
    public class Comment
    {
        public int Id { get; set; }
        public string CommentContent { get; set; }
        public DateTime CommentDate { get; set; }
        public int PersonalProjectId { get; set; }
        public string UserId { get; set; }
        public int UniProjectId { get; set; }
        public ApplicationUser User { get; set; }
        public UniProject UniProject { get; set; }
        public PersonalProject PersonalProject { get; set; }
    }
}
