using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Authorization;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;
using System.Reflection.Metadata;
using System.Security.Claims;

namespace ReservationApp.BusinessUnit
{
    
    public class CityBusinessUnit : ICityBusinessUnit
    {
        private readonly ICityDataAccess _cityDataAccess;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public CityBusinessUnit(ICityDataAccess cityDataAccess,IHttpContextAccessor httpContextAccessor)
        {
             _httpContextAccessor = httpContextAccessor;
             _cityDataAccess = cityDataAccess;
        }
        public Results.IResult Add(City city)
        {
            //ekleme yaparken Photolist null gönderilecek.
           var alreadyExistsCity = _cityDataAccess.GetCityById(city.Id);
            if(alreadyExistsCity != null)
            {
                if (alreadyExistsCity.Name == city.Name)
                {
                    return new ErrorResult(ConstantsMessages.alreadyExistsCity);
                }
            }
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            if(userId != null)
            {
                city.UserId = int.Parse(userId);
            }
            var result = _cityDataAccess.Add(city);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.CityAdded);
            }
            return new ErrorResult(ConstantsMessages.CityAddError); 
            
        }

        public Results.IResult Delete(City city)
        {
            var result =_cityDataAccess.Delete(city);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.CityDeleted);
            }
            return new ErrorResult(ConstantsMessages.CityDeleteError);
           
        }

        public Results.IDataResult<List<CityForListDto>> GetAllCities()
        {
            var cities = _cityDataAccess.GetAllCities();
            if(cities.Count == 0)
            {
                return new SuccessDataResult<List<CityForListDto>>(ConstantsMessages.CityNotFound);
            }
            List<CityForListDto> cityForListDtos = new List<CityForListDto>();
            foreach (var city in cities)
            {
                CityForListDto cityForList = new CityForListDto()
                {
                    Id = city.Id,
                    Name = city.Name,
                    Description = city.Description,
                    PhotoUrl = city.Photos.FirstOrDefault(p => p.IsMain == true)?.Url
                };
                
              cityForListDtos.Add(cityForList);
            }

            return new SuccessDataResult<List<CityForListDto>>(cityForListDtos,ConstantsMessages.CityListed);
            
        }

        public Results.IDataResult<City> GetCityById(int id)
        {
            var city = _cityDataAccess.GetCityById(id);
            if(city == null)
            {
                return new SuccessDataResult<City>(ConstantsMessages.CityNotFound);
            }
            CityForDetailDto cityForDetailDto = new CityForDetailDto()
            {
                Name = city.Name,
                Description = city.Description,
                Photos = city.Photos
            };
            return new SuccessDataResult<City>(city, ConstantsMessages.CityListed);

        }

        public Results.IResult Update(City updatedCity)
        {
            var result =_cityDataAccess.Update(updatedCity);
            if(result > 0)
            {
                return new SuccessResult(ConstantsMessages.CityUpdated);
            }
            return new ErrorResult(ConstantsMessages.CityUpdateError);
        }
    }
}
