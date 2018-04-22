using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using Exceptions;
using System;
using COREAPI;

namespace TerminalApp.Controllers
{
    public class RouteController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();
        RouteManager Mngr = new RouteManager();

        public IHttpActionResult Get()
        {
            ApiResp = new ApiResponse();
            ApiResp.Data = Mngr.RetrieveAll();

            return Ok(ApiResp);
        }

        public IHttpActionResult Post(Route route)
        {
            try
            {
                var Mngr = new RouteManager();
                Mngr.Create(route);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Ruta registrada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Route route)
        {
            try
            {
                var Mngr = new RouteManager();
                Mngr.Update(route);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Ruta actualizada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Route route)
        {
            try
            {
                string stat = "";
                var Mngr = new RouteManager();
                Mngr.Delete(route);
                if (route.Status == 1)
                {
                    stat = "desactivada";
                }
                else {
                    stat = "activada";
                }
                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Ruta " +stat+"  exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveById(int idRoute)
        {
            return Ok();
        }

        [HttpGet]
        [Route("api/route/RetrieveRoutesByCompany")]
        public IHttpActionResult RetrieveRoutesByCompany(int idCompany)
        {
            try
            {
                var Mngr = new RouteManager();
                ApiResp.Data = Mngr.RetrieveRoutesByCompany(new Company { IdCompany = idCompany });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [HttpGet]
        [Route("api/route/RetrieveRoutesByTerminal")]
        public IHttpActionResult RetrieveRoutesByTerminal(int idTerminal)
        {
            try
            {
                var Mngr = new RouteManager();
                ApiResp.Data = Mngr.RetrieveAllByTerminal(new Terminal { IdTerminal = idTerminal });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/route/terminal/{pIdTerminal}")]
        public IHttpActionResult RetrieveRouteByTerminal(int pIdTerminal)
        {
            try
            {
                var Mngr = new RouteManager();
                ApiResp.Data = Mngr.RetrieveAllByTerminal(new Terminal { IdTerminal = pIdTerminal });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        /*public IHttpActionResult ShowBusCurrentLocation(Bus bus)
        {
            return Ok();
        }*/
    }
}
