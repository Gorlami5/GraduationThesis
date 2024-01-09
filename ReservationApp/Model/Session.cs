namespace ReservationApp.Model
{
    public class Session
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string? AccessToken { get; set; }
        public bool IsActive { get; set; }
        public string? CreateDate { get; set; }
    }
}
