namespace StudentManagementSystem04.ViewModels
{
    public class UploadPersonalProjectModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime UploadTime { get; set; }
      
        public IFormFile MainImage { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> AttachedImages { get; set; }
    }

}
