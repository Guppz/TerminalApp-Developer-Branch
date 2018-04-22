using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace SystemParamApp.Controllers
{
    public class SystemParamController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        SystemParamManager mng = new SystemParamManager();

        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }

        [Route("api/systemParam/{pIdSystemParam}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdSystemParam)
        {
            try
            {
                var systemParam = new SystemParam
                {
                    IdSystemParam = pIdSystemParam
                };

                systemParam = mng.RetrieveById(systemParam);

                apiResp = new ApiResponse();
                apiResp.Data = systemParam;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(SystemParam systemParam)
        {
            try
            {
                mng.Create(systemParam);

                apiResp = new ApiResponse();
                apiResp.Message = "Parámetro registrado exitosamente";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(SystemParam systemParam)
        {
            try
            {
                mng.Update(systemParam);

                apiResp = new ApiResponse();
                apiResp.Message = "Parámetro actualizado exitosamente";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(SystemParam systemParam)
        {
            try
            {
                mng.Delete(systemParam);

                apiResp = new ApiResponse();
                apiResp.Message = "Párametro eliminado exitosamente";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
