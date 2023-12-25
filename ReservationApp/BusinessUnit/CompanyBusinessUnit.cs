using ReservationApp.BusinessUnit.Interfaces;
using ReservationApp.DataAccessUnit.Interfaces;
using ReservationApp.Dto;
using ReservationApp.Extensions;
using ReservationApp.Model;
using ReservationApp.Results;
using System.Reflection.Metadata.Ecma335;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace ReservationApp.BusinessUnit
{
    public class CompanyBusinessUnit : ICompanyBusinessUnit
    {
        private readonly ICompanyDataAccess _companyDataAccess;
        public CompanyBusinessUnit(ICompanyDataAccess companyDataAccess)
        {
            _companyDataAccess = companyDataAccess;
        }
        public Result Add(Company company)
        {
            var company1 = _companyDataAccess.GetCompanyById(company.Id);
            if(company1 != null)
            {
                if (company1.Name == company.Name)
                {
                    return new ErrorResult(ConstantsMessages.CompanyAddError);
                }
            }
           
            var result = _companyDataAccess.Add(company); //Aynı isimle ekleyemezsiniz kontrolü yapılmalı.
            if(result>0)
            {
                return new SuccessResult(ConstantsMessages.CompanyAdded);
            }
            return new ErrorResult(ConstantsMessages.CompanyAddError);
        }

        public Result Delete(Company company)
        {
            var result = _companyDataAccess.Delete(company);
            if (result > 0)
            {
                return new SuccessResult(ConstantsMessages.CompanyDeleted);
            }
            return new ErrorResult(ConstantsMessages.CompanyDeleteError);
        }

        public DataResult<List<CompanyForListDto>> GetAllCompany()
        {
            var companyList = _companyDataAccess.GetAllCompanies();
            if(companyList.Count == 0)
            {
                return new SuccessDataResult<List<CompanyForListDto>>("No company found");
            }
            List<CompanyForListDto> result = new List<CompanyForListDto>();
            foreach (var company in companyList)
            {
                CompanyForListDto companyForListDto = new CompanyForListDto()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description,
                    PhotoUrl = company.CompanyPhotos.FirstOrDefault(p => p.IsMain == true).Url
                };

            result.Add(companyForListDto);
            }
            return new SuccessDataResult<List<CompanyForListDto>>(result, "Listed success");
        }

        public DataResult<Company> GetCompanyById(int id)
        {
            var company = _companyDataAccess.GetCompanyById(id);

            if(company == null)
            {
                return new SuccessDataResult<Company>(ConstantsMessages.CompanyNotFound);
            }
            return new SuccessDataResult<Company>(company);
        }

        public DataResult<List<CompanyForListDto>> GetCompanyListByCityId(int cityId)
        {
            var companyList = _companyDataAccess.GetCompaniesByCityId(cityId);
            if(companyList.Count == 0)
            {
                return new ErrorDataResult<List<CompanyForListDto>>(ConstantsMessages.ThisCityNoHaveCompany);
            }
            List<CompanyForListDto> result = new List<CompanyForListDto>();
            foreach (var company in companyList)
            {
                CompanyForListDto companyForListDto = new CompanyForListDto()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description,
                    PhotoUrl = company.CompanyPhotos.FirstOrDefault(p => p.IsMain == true).Url
                   
                };

                result.Add(companyForListDto);
            }
            return new SuccessDataResult<List<CompanyForListDto>>(result, "Listed success");
        }

        public Result Update(Company updatedCompany)
        {
            var result = _companyDataAccess.Update(updatedCompany);
            if (result > 0)
            {
                return new SuccessResult(ConstantsMessages.CompanyUpdated);
            }
            return new ErrorResult(ConstantsMessages.CompanyUpdateError);
        }
        public DataResult<List<CompanyForListDto>> GetCompanyListByCity(int cityId)
        {
            List<CompanyForListDto> result = new List<CompanyForListDto>();
            var city = _companyDataAccess.GetCityById(cityId);
            if (city == null)
            {
                return new ErrorDataResult<List<CompanyForListDto>>(ConstantsMessages.CityNotFound);
            }
            foreach(var company in city.Companies)
            {
                CompanyForListDto companyForListDto = new CompanyForListDto()
                {
                    Id = company.Id,
                    Name = company.Name,
                    Description = company.Description,
                    PhotoUrl = company.CompanyPhotos.FirstOrDefault(p => p.IsMain == true).Url

                };
                result.Add(companyForListDto);
            }
            return new SuccessDataResult<List<CompanyForListDto>>(result, "Listed success");
        }
    }
}
