using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface ICityDataAccess
    {
        public int Add(City city);
        public int Update(City updatedCity);
        public int Delete(City city);
        public City GetCityById(int id);
        public List<City> GetAllCities();
    }
}
