using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace TerminalApp.Controllers
{
    public class CompanyController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            try
            {
                var Mngr = new CompanyManager();
                ApiResp.Data = Mngr.RetrieveAll();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        public IHttpActionResult GetCompanyByUser(int id)
        {
            try
            {
                var Mngr = new CompanyManager();
                var user = new User { IdUser = id };
                ApiResp.Data = Mngr.RetrieveCompanyByUser(user);
                ApiResp.Message = "Usuario encontrado";
                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        public IHttpActionResult Put(Company company)
        {
            try
            {
                var Mngr = new CompanyManager();
                Mngr.Update(company);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Compañía actualizada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Company company)
        {
            try
            {
                var Mngr = new CompanyManager();
                Mngr.Delete(company);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Compañía eliminada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(Company company)
        {
            try
            {
                var Mngr = new CompanyManager();
                Mngr.Create(company);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Compañía registrada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/company/RetrieveById")]
        public IHttpActionResult RetrieveById(int idCompany)
        {
            try
            {
                var Mngr = new CompanyManager();
                ApiResp.Data = Mngr.RetrieveById(new Company { IdCompany = idCompany });


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/company/RetrieveByInfo")]
        public IHttpActionResult RetrieveByInfo(string name, string corpIdentification,string email)
        {
            try
            {
                var Mngr = new CompanyManager();
                ApiResp.Data = Mngr.RetrieveByInfo(new Company { Name = name,CorpIdentification = corpIdentification, Email = email});

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/company/RetrieveByCorpId")]
        public IHttpActionResult RetrieveByCorpId(string corpId)
        {
            try
            {
                var Mngr = new CompanyManager();
                ApiResp.Data = Mngr.RetrieveByCorpId(new Company { CorpIdentification = corpId });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/company/RetrieveCompaniesByTerminal")]
        public IHttpActionResult RetrieveCompaniesByTerminal(int idTerminal)
        {
            try
            {
                var Mngr = new CompanyManager();
                ApiResp.Data = Mngr.RetrieveCompaniesByTerminal(new Terminal { IdTerminal = idTerminal });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("api/company/AddCompanyToTerminal")]
        public IHttpActionResult AddCompanyToTerminal(Company company)
        {
            try
            {
                var Mngr = new CompanyManager();
                Mngr.AddCompanyToTerminal(company);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Compañía agregada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveProfitsReport(int idCompany)
        {
            return Ok();
        }
    }
}
