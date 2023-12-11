namespace ReservationApp.Model
{
    public class Company
    {

        public Company()
        {
            Photos = new List<Photo>();
        }
        public int Id { get; set; }
        public int CityId { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Photo> Photos { get; set; }

    }
}
