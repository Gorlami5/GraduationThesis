using ReservationApp.Model;

namespace ReservationApp.Dto
{
    public class ReservationForListDto
    {
        public string CompanyName { get; set; }
        public string CityName { get; set; }
        public DateTime ReservationStartDate { get; set; }
        public DateTime ReservationEndDate { get; set; }
        public Photo CompanyMainPhoto { get; set; }
    }
}
