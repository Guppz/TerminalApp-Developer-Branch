using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace LocationApp.Controllers
{
    public class LocationController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        LocationManager mng = new LocationManager();

        // GET ALL @ api/location
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        // GET BY ID @ api/location/{ID}
        [Route("api/location/{pidLocation}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdLocation)
        {
            try
            {
                var location = new Location
                {
                    IdLocation = pIdLocation
                };

                location = mng.RetrieveById(location);

                apiResp = new ApiResponse();
                apiResp.Data = location;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST @ api/location
        public IHttpActionResult Post(Location location)
        {
            try
            {
                mng.Create(location);

                apiResp = new ApiResponse();
                apiResp.Message = "Registro exitoso: Ubicacion";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // PUT @ api/location
        public IHttpActionResult Put(Location location)
        {
            try
            {
                //location = mng.RetrieveById(location);

                mng.Update(location);

                apiResp = new ApiResponse();
                apiResp.Message = "Actualizacion exitoso: Ubicacion.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // DELETE @ api/location
        
        public IHttpActionResult Delete(Location location)
        {
            try
            {
                mng.Delete(location);

                apiResp = new ApiResponse();
                apiResp.Message = "Eliminación exitoso: Ubicacion.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
