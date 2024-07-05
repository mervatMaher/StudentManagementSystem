namespace StudentManagementSystem04.Model
{
    public class Like
    {
        public int Id { get; set; }
        public DateTime likeDate { get; set; }
        public int likeCount { get; set; }
        public string UserId { get; set; }
        public int UniProjectId { get; set; }
        public int PersonalProjectId { get; set; }
        public ApplicationUser User { get; set; }
        public UniProject UniProject { get; set; }
        public PersonalProject PersonalProject { get; set; }
    }
}
