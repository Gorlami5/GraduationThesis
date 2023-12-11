using Microsoft.EntityFrameworkCore;
using ReservationApp.Context;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Model;

namespace ReservationApp.DataAccessUnit
{
    public class CompanyDataAccess : ICompanyDataAccess
    {
        private readonly PostgreDbConnection _connection;
        public CompanyDataAccess(PostgreDbConnection connection)
        {
            _connection = connection;    
        }
        public int Add(Company company)
        {
            _connection.Add(company);
            return _connection.SaveChanges();
        }

        public int Delete(Company company)
        {
            _connection.Remove(company);
            return _connection.SaveChanges();
        }

        public List<Company> GetAllCompanies()
        {
            var companyList = _connection.Companies.Include(c => c.Photos).ToList(); //Fk ayarları!!
            return companyList;
        }

        public List<Company> GetCompaniesByCityId(int cityId)
        {
            var companyList = _connection.Companies.Include(c=>c.Photos).Where(c=> c.Id == cityId).ToList(); //Fk ayarlarını unutma.
            return companyList;
        }

        public int Update(Company updatedCompany)
        {

            _connection.Update(updatedCompany);
            return _connection.SaveChanges();
        }
        public Company GetCompanyById(int companyId)
        {
            var company = _connection.Companies.Where(c => c.Id == companyId).FirstOrDefault();
            return company;
        }
    }
}
