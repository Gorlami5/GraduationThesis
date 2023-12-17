namespace ReservationApp.Model
{
    public class Photo
    {
        public int Id { get; set; }
        public int CityId { get; set; }
        public int CompanyId { get; set; } // Db'ye eklemeyi unutma
        public int ReservationId { get; set; } // eklenecek
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime DateAdded { get; set; }
        public bool IsMain { get; set; }
        public string PublicId { get; set; }
        public City? City { get; set; }
        public Company Company { get; set; }
        public Reservation Reservations { get; set; }
    }
}
