using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit.Interfaces
{
    public interface ICompanyDataAccess
    {
        public int Add(Company company);
        public int Update(Company updatedCompany);
        public int Delete(Company company);
        public List<Company> GetAllCompanies();

        public List<Company> GetCompaniesByCityId(int cityId);
        public Company GetCompanyById(int companyId);



    }
}
