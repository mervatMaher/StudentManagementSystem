using StudentManagementSystem04.Model;

namespace StudentManagementSystem03.ViewModels
{
    public class UploadProjectModel
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Link { get; set; }
        public DateTime UploadTime { get; set; }
   
        public IFormFile MainImage { get; set; }
        public int CategoryId { get; set; }
        public List<IFormFile> AttachedImages { get; set; }
        public List<int> Students { get; set; }

    }
}
