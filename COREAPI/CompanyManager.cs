using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace COREAPI
{
    public class CompanyManager
    {
        private CompanyCrud CrudFactory;

        public CompanyManager()
        {
            CrudFactory = new CompanyCrud();
        }

        public void Create(Company company)
        {
            try
            {
                ValidateIsNotExistingCompany(company);
                ValidateCorpId(company);
                CrudFactory.Create(company);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Delete(Company company)
        {
            try
            {
                CrudFactory.Delete(company);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public List<Company> RetrieveAll()
        {
            List<Company> LstCompanies = null;

            try
            {
                LstCompanies = CrudFactory.RetrieveAll<Company>();
                GenerateList(LstCompanies);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstCompanies;
        }

        public void Update(Company company)
        {
            try
            {
                ValidateCorpId(company);
                CrudFactory.Update(company);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }
        public Company RetrieveCompanyByUser(User userCompany)
        {
            Company NewCompany = CrudFactory.RetrieveCompanyByUser<Company>(userCompany);

            //BuildObjects(NewCompany);

            return NewCompany;
        }
        public Company RetrieveById(Company company)
        {
            ValidateCorpId(company);
            Company NewCompany = CrudFactory.Retrieve<Company>(company);

            //BuildObjects(NewCompany);

            return NewCompany;
        }

        public Company RetrieveByCorpId(Company company)
        {
            Company NewCompany = null;

            try
            {
                NewCompany = CrudFactory.RetrieveCompanyByCorpId<Company>(company);
                //BuildObjects(NewCompany);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return NewCompany;
        }

        public Company RetrieveByInfo(Company company)
        {
            Company NewCompany = null;

            try
            {
                Company Name = CrudFactory.RetrieveCompanyByName<Company>(company);
                Company CorpId = CrudFactory.RetrieveCompanyByCorpId<Company>(company);
                Company Email = CrudFactory.RetrieveCompanyByEmail<Company>(company);

                NewCompany = BuildObjectInfo(Name, CorpId, Email);
                //BuildObjects(NewCompany);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return NewCompany;
        }

        public Company BuildObjectInfo(Company name, Company corpId, Company email)
        {
            Company Info = new Company();

            if(name != null)
            {
                Info.Name = name.Name;
            }       

            if(corpId != null)
            {
                Info.CorpIdentification = corpId.CorpIdentification;
            }
            
            if (email != null)
            {
                Info.Email = email.Email;
            }
            
            return Info;
        }

        public List<Complaint> RetrieveProfitsReport(int idCompany)
        {
            throw new System.NotImplementedException();
        }

        public List<Company> RetrieveCompaniesByTerminal(Terminal terminal)
        {
            List<Company> LstCompanies = null;

            try
            {
                LstCompanies = CrudFactory.RetrieveCompaniesByTerminal<Company>(terminal);
                GenerateList(LstCompanies);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstCompanies;
        }

        public void ValidateIsNotExistingCompany(Company company)
        {
            Company NewCompany = CrudFactory.RetrieveCompanyByInfo<Company>(company);

            if (NewCompany != null)
            {
                throw new BusinessException(52);
            }

            CreateSystemUser(company);
        }

        public void CreateSystemUser(Company company)
        {
            var User = new User
            {
                Name = company.Name,
                LastName = "",
                Identification = company.CorpIdentification,
                Email = company.Email,
                BirthDate = DateTime.Now,
                UserTerminal = new Terminal { IdTerminal = -1},
                Roleslist = new List<Role>()
            };

            new UserManager().Create(User);
        }

        public void AddCompanyToTerminal(Company company)
        {
            try
            {
                ValidateIsNotAlreadyRegisteredInTheTerminal(company);
                CrudFactory.AddCompanyToTerminal(company);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void ValidateCorpId(Company company)
        {
            if (!Regex.IsMatch(company.CorpIdentification, @"^[1-9][0-9]*(\.[0-9]+)?|0+\.[0-9]*[1-9][0-9]*$"))
            {
                throw new BusinessException(47);
            }
        }

        public void ValidateIsNotAlreadyRegisteredInTheTerminal(Company company)
        {
            Company NewCompany = CrudFactory.RetrieveCompanyByTerminalId<Company>(company);

            if (NewCompany != null)
            {
                throw new BusinessException(50);
            }
        }

        public void GenerateList(List<Company> lstCompanies)
        {
            foreach (Company company in lstCompanies)
            {
                BuildObjects(company);
            }
        }

        public void BuildObjects(Company company)
        {
            company.BusesList = new List<Bus>();
            company.DriversList = new List<Driver>();
            company.TerminalsList = new List<Terminal>();
            company.RoutesList = new List<Route>();
        }
    }
}
