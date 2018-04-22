using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace AgreementTypeApp.Controllers
{
    public class AgreementTypeController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        AgreementTypeManager mng = new AgreementTypeManager();

        // GET ALL @ api/agreementType
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        // GET BY ID @ api/agreementType/{ID}
        [Route("api/agreementType/{pidAgreementType}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdAgreementType)
        {
            try
            {
                var agreementType = new AgreementType
                {
                    IdAgreementType = pIdAgreementType
                };

                agreementType = mng.RetrieveById(agreementType);

                apiResp = new ApiResponse();
                apiResp.Data = agreementType;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST @ api/agreementType
        public IHttpActionResult Post(AgreementType agreementType)
        {
            try
            {
                mng.Create(agreementType);

                apiResp = new ApiResponse();
                apiResp.Message = "Registro exitoso: Tipo de Convenio";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // PUT @ api/agreementType
        public IHttpActionResult Put(AgreementType agreementType)
        {
            try
            {
                mng.Update(agreementType);

                apiResp = new ApiResponse();
                apiResp.Message = "Actualización exitosa: Tipo de Convenio.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // DELETE @ api/agreementType
        
        public IHttpActionResult Delete(AgreementType agreementType)
        {
            try
            {
                mng.Delete(agreementType);

                apiResp = new ApiResponse();
                apiResp.Message = "Eliminación exitosa: Tipo de Convenio.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
