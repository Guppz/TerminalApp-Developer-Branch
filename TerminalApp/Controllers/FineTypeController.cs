using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;


namespace TerminalApp.Controllers
{
    public class FineTypeController:ApiController
    {
        ApiResponse apiResp = new ApiResponse();
            
        public IHttpActionResult Post(FineType fineType)
           {
            try
            {
                var mngr = new FineManager();
                mngr.CreateFineType(fineType);

                apiResp = new ApiResponse
                {
                    Message = "El tipo de multa ha sido creado exitosamente"
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

        public IHttpActionResult Put(FineType fineType)
        {
            try
            {
                var mng = new FineManager();
                mng.UpdateFinesSettings(fineType);

                apiResp = new ApiResponse
                {
                    Message = "El tipo de multa ha sido actualizado"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        //[HttpDelete]
        //[Route("api/FineType/DeleteFineType")]
        public IHttpActionResult Delete(FineType fineType)
        {
            apiResp = new ApiResponse
            {
                Message = "Se elimino existosamente"
            };
            var mng = new FineManager();
            mng.DeleteFineType(fineType);
            return Ok(apiResp);
        }
    }

}