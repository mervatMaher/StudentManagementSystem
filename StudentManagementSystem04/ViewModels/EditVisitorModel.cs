namespace StudentManagementSystem04.ViewModels
{
    public class EditVisitorModel
    {
        public string FullName {  get; set; }
        public string Password { get; set; }
        public IFormFile ProfileImageUrl { get; set; }
        public string PhoneNumber { get; set; }
        public string Career { get; set; } = null;

    }
}
