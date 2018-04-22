using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace TerminalApp.Controllers
{
    public class ParkingController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new ParkingManager();
            apiResp.Data = mng.RetrieveAll();
            return Ok(apiResp);
        }

        public IHttpActionResult Put(Parking parking)
        {
            try
            {
                apiResp = new ApiResponse
                {
                    Message = "El parqueo ha sido actualizado"
                };
                var mng = new ParkingManager();
                mng.Update(parking);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(Parking parking)
        {
            try
            {
                apiResp = new ApiResponse
                {
                    Message = "El parqueo ha sido creado"
                };
                var mng = new ParkingManager();
                mng.Create(parking);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Parking parking)
        {
            apiResp = new ApiResponse
            {
                Message = "Se elimino existosamente"
            };
            var mng = new ParkingManager();
            mng.Delete(parking);
            return Ok(apiResp);
        }
        

        public IHttpActionResult RetrieveById(int idParking)
        {
            return Ok();
        }

        public IHttpActionResult GenerateParkingBill(int idReservation)
        {
            return Ok();
        }
    }
}
