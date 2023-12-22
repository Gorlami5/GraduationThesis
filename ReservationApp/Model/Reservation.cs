namespace ReservationApp.Model
{
    public class Reservation
    {
        public int Id { get; set; }
        public int CompanyId { get; set; }
        public int UserId { get; set; }
        public string CompanyName { get; set; }
        public string CityName { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set;}
        public int ReservationStatus { get; set;}
        public Photo CompanyMainPhoto { get; set; }

        public int PersonCount { get; set; }

    }
}
