namespace ReservationApp.Dto
{
    public class CompanyPhotoForCreationDto
    {
        public string Url { get; set; }
        public string Description { get; set; }
        public IFormFile file { get; set; }
        public DateTime DateAdded { get; set; } = DateTime.Now;
        public string PublicId { get; set; }
    }
}
