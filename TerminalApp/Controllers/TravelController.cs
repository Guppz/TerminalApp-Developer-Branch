using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class TravelController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Post(Travel travel)
        {
            try
            {
                var Mngr = new TravelManager();
                Mngr.Create(travel);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Viaje registrado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Travel travel)
        {
            try
            {
                var Mngr = new TravelManager();
                Mngr.Update(travel);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Viaje actualizado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Travel travel)
        {
            try
            {
                var Mngr = new TravelManager();
                Mngr.Delete(travel);

                ApiResp = new ApiResponse
                {
                    Message = "Viaje eliminado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveById(int idTravel)
        {
            return Ok();
        }

        public IHttpActionResult Get()
        {
            try
            {
                var Mngr = new TravelManager();
                ApiResp.Data = Mngr.RetrieveAll();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveTravelsByRoute(int idRoute)
        {
            return Ok();
        }
    }
}