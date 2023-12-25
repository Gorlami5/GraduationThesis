using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ReservationApp.BusinessUnit;
using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyBusinessUnit _companyBusinessUnit;
        public CompanyController(ICompanyBusinessUnit companyBusinessUnit)
        {
                _companyBusinessUnit = companyBusinessUnit;
        }
        [HttpPost]
        [Route("Add")]
        public Result AddCity([FromBody] Company company)
        {
            var result = _companyBusinessUnit.Add(company);
            return result;
        }
        [HttpPost]
        [Route("Delete")]
        public Result DeleteCity([FromBody] Company company)
        {
            return _companyBusinessUnit.Delete(company);

        }
        [HttpPost]
        [Route("Update")]
        public Result UpdateCity([FromBody] Company updatedCompany)
        {
            return _companyBusinessUnit.Update(updatedCompany);

        }
        [HttpGet]
        [Route("GetAllCompany")]
        public DataResult<List<CompanyForListDto>> GetAllCompany()
        {
            var result = _companyBusinessUnit.GetAllCompany();
            return result;
        }
        [HttpGet]
        [Route("GetCompanyListByCityId")]
        public DataResult<List<CompanyForListDto>> GetCompanyListByCityId(int cityId) 
        {
            var result = _companyBusinessUnit.GetCompanyListByCityId(cityId);
            return result;
        }
        [HttpGet]
        [Route("GetCompanyById")]
        public DataResult<Company> GetCompanyById(int id)
        {
            var result = _companyBusinessUnit.GetCompanyById(id);
            return result;
        }
        [HttpGet]
        [Route("GetCompanyListByCity")]
        public DataResult<List<CompanyForListDto>> GetCompanyListByCity(int id)
        {
            var result = _companyBusinessUnit.GetCompanyListByCity(id);
            return result;
        }
    }
}
