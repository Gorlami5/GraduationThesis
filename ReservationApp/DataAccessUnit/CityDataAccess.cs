using Microsoft.EntityFrameworkCore;
using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit
{
    public class CityDataAccess:ICityDataAccess
    {
        private readonly PostgreDbConnection _connection;

        public CityDataAccess(PostgreDbConnection connection)
        {
                _connection = connection;
        }
        public int Add(City city)
        {
            _connection.Add(city);
            return _connection.SaveChanges();
        }

        public int Update(City updatedCity)
        {
            _connection.Update(updatedCity);
            return _connection.SaveChanges();
        }
        public int Delete(City city)
        {
            _connection.Remove(city);
             return  _connection.SaveChanges();
        }
        public City GetCityById(int id)
        {
            var city = _connection.Cities.Where(c => c.Id == id).FirstOrDefault();
            return city;
        }
        public List<City> GetAllCities() 
        {
            var cities = _connection.Cities.Include(c=> c.Photos).ToList(); //FK
            return cities;
        }
    }
}
