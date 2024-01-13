namespace ReservationApp.Model
{
    public class Company
    {

        public Company()
        {
            CompanyPhotos = new List<CompanyPhoto>();
        }
        public int Id { get; set; }
        public int CityId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxReservationDayLength { get; set; }
        public City? City { get; set; }

        public List<CompanyPhoto>? CompanyPhotos { get; set; } //Add request atılırken null gönderilecek unutma!

    }
}
