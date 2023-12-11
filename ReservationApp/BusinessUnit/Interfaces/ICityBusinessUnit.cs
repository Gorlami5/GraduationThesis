using ReservationApp.Dto;
using ReservationApp.Model;

namespace ReservationApp.BusinessUnit.Interfaces
{
    public interface ICityBusinessUnit
    {
        public Results.IResult Add(City city);
        public Results.IResult Delete(City city);
        public Results.IDataResult<List<CityForListDto>> GetAllCities();
        public Results.IDataResult<City> GetCityById(int id);
        public Results.IResult Update(City updatedCity);
    }
}
