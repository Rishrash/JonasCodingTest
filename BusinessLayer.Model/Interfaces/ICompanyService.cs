using System.Collections.Generic;
using BusinessLayer.Model.Models;

namespace BusinessLayer.Model.Interfaces
{
    public interface ICompanyService
    {
        IEnumerable<CompanyInfo> GetAllCompanies();
        CompanyInfo GetCompanyByCode(string companyCode);
        bool SaveCompany(CompanyInfo companyInfo);
        bool UpdateCompany(CompanyInfo companyInfo, string companyCode);
        bool DeleteCompany(string companyCode);
    }
}
