using System.ComponentModel.DataAnnotations.Schema;

namespace ReservationApp.Model
{
   
    public class City
    {
     
        public City()
        {
               Photos = new List<Photo>();
               Companies = new List<Company>();
        }
        
        public int Id { get; set; }   
        public int UserId { get; set; }    
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Company>? Companies { get; set; }
        public List<Photo>? Photos { get; set; }

    }
}
