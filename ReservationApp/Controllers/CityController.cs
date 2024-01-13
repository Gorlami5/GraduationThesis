using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;
using System.Security.Claims;

namespace ReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityBusinessUnit _cityBusinessUnit;
        public CityController(ICityBusinessUnit cityBusinessUnit)
        {
            _cityBusinessUnit = cityBusinessUnit;
        }
        [Authorize]
        [HttpPost]
        [Route("Add")]
        public Results.IResult AddCity([FromBody] City city)
        {
            var result = _cityBusinessUnit.Add(city);
            return result;
        }
        [HttpPost]
        [Route("Delete")]
        public Results.IResult DeleteCity([FromBody] City city)
        {
            return _cityBusinessUnit.Delete(city);
           
        }
        [HttpPost]
        [Route("Update")]
        public Results.IResult UpdateCity([FromBody] City updatedCity)
        {
            return _cityBusinessUnit.Update(updatedCity);
           
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllCities")]
        public Results.IDataResult<List<CityForListDto>> GetAllCities()
        {
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var result = _cityBusinessUnit.GetAllCities();
            return result;
        }
        [HttpGet]
        [Route("GetCityById")]
        public Results.IDataResult<City> GetCityById(int id) //IActionResult kalıtım alıp Results dönme durumuna bak!!!
        {
            var result = _cityBusinessUnit.GetCityById(id);
            return result;
        }
    }
}
