using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class FineController : ApiController
    {


        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Post(Fine fine)
        {
            try
            {
                var mngr = new FineManager();
                mngr.Create(fine);

                 apiResp = new ApiResponse
                {
                    Message = "La multa ha sido creada exitosamente"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Get()
        {
            var mngr = new FineManager();
            apiResp.Data = mngr.RetrieveAll();
            return Ok(apiResp);
        }

        public IHttpActionResult Put(Fine fine)
        {
            try
            {
                var mngr = new FineManager();
                mngr.Update(fine);
                apiResp.Message = "La multa ha sido actualizada exitosamente";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Fine fine)
        {
            try
            {
                var mngr = new FineManager();
                mngr.Delete(fine);
                apiResp.Message = "La multa ha sido eliminada exitosamente";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveById(int idFine)
        {
            try
            {
                var mngr = new FineManager();
                var fine = new Fine
                {
                    IdFine = idFine
                };
                fine = mngr.RetrieveById(fine);
                apiResp.Data = fine;
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [HttpGet]
        [Route("api/fine/RetrieveFinesTypesList")]

        public IHttpActionResult RetrieveFinesTypesList()
        {
            try
            {
                var mng = new FineManager();
                apiResp.Data = mng.RetrieveFinesTypes();

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

            public IHttpActionResult RetrieveFineByDateRange(DateTime beginDate, DateTime endDate)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveFineByDateAndCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveFinesByCompany(int idCompany)
        {
            return Ok();
        }
    }
}