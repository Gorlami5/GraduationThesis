using ReservationApp.Dto;
using ReservationApp.Model;
using ReservationApp.Results;

namespace ReservationApp.BusinessUnit.Interfaces
{
    public interface ICompanyBusinessUnit
    {
        public Result Add(Company company);
        public Result Delete(Company company);
        public Result Update(Company updatedCompany);
        public DataResult<Company> GetCompanyById(int id);  
        public DataResult<List<CompanyForListDto>> GetCompanyListByCityId(int cityId);
        public DataResult<List<CompanyForListDto>> GetAllCompany();
    }
}
