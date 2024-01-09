using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Results;
using System.Security.Claims;

namespace ReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PhotoController : ControllerBase
    {
        private readonly IPhotoBusinessUnit _photoBusinessUnit;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PhotoController(IPhotoBusinessUnit photoBusinessUnit,IHttpContextAccessor httpContextAccessor)
        {
                _photoBusinessUnit = photoBusinessUnit;
                _httpContextAccessor = httpContextAccessor;
        }
        [HttpGet]
        [Route("GetPhoto")]
        public DataResult<PhotoForReturnDto> GetPhoto(int id)
        {
            var photoReturnDto = _photoBusinessUnit.GetPhotoById(id);
            return photoReturnDto;
        }
        [HttpPost]
        [Route("AddPhotoForCity")]
        public DataResult<PhotoForReturnDto> AddPhotoForCity(int cityId,[FromBody] PhotoForCreationDto photoForCreationDto)
        {
            //var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value); //HttpContextAccessor B.U içersinde kullanamadım.
            //if(currentUserId != cityId) 
            //{
            //    return new ErrorDataResult<PhotoForReturnDto>(ConstantsMessages.Unauthorize);
            //}
            var result = _photoBusinessUnit.AddPhotoForCity(cityId, photoForCreationDto);
            return result;            
        }
        [HttpPost]
        [Route("AddCompanyPhotoForCity")]
        public DataResult<CompanyPhotoForReturnDto> AddCompanyPhoto(int companyId, [FromBody] CompanyPhotoForCreationDto companyPhotoForCreationDto)
        {
            //var currentUserId = int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value); //HttpContextAccessor B.U içersinde kullanamadım.
            //if (currentUserId != companyId)
            //{
            //    return new ErrorDataResult<CompanyPhotoForReturnDto>(ConstantsMessages.Unauthorize);
            //}
            var result = _photoBusinessUnit.AddCompanyPhotoForCompany(companyId, companyPhotoForCreationDto);
            return result;
            //Photo eklemek için basit bir frontend yazıalcak.
            //claimler ile ilgili yazılan taraflar silinecek.
        }
        [HttpGet]
        [Route("GetCompanyPhoto")]
        public DataResult<CompanyPhotoForReturnDto> GetCompanyPhoto(int id)
        {
            var photoReturnDto = _photoBusinessUnit.GetCompanyPhotoById(id);
            return photoReturnDto;
        }
    }
}
