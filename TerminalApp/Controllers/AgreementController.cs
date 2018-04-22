using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace AgreementApp.Controllers
{
    public class AgreementController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        AgreementManager mng = new AgreementManager();

        // GET ALL @ api/agreement
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        // GET BY ID @ api/agreement/{ID}
        [Route("api/agreement/{pidAgreement}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdAgreement)
        {
            try
            {
                var agreement = new Agreement
                {
                    IdAgreement = pIdAgreement
                };

                agreement = mng.RetrieveById(agreement);

                apiResp = new ApiResponse();
                apiResp.Data = agreement;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST @ api/agreement
        public IHttpActionResult Post(Agreement agreement)
        {
            try
            {
                mng.Create(agreement);

                apiResp = new ApiResponse();
                apiResp.Message = "Registro exitoso: Convenio";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // PUT @ api/agreement
        public IHttpActionResult Put(Agreement agreement)
        {
            try
            {
                mng.Update(agreement);

                apiResp = new ApiResponse();
                apiResp.Message = "Actualizacion exitosa: Convenio.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // DELETE @ api/agreement
        
        public IHttpActionResult Delete(Agreement agreement)
        {
            try
            {
                mng.Delete(agreement);

                apiResp = new ApiResponse();
                apiResp.Message = "Eliminación exitosa: Convenio.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
