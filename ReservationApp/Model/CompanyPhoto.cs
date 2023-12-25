namespace ReservationApp.Model
{
    public class CompanyPhoto
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public string Url { get; set; }
        public string? Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public Company Company { get; set; }
    }
}
