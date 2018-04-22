using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class ParkingBillController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        public IHttpActionResult Post(ParkingBill pb)
        {
            try
            {
                apiResp = new ApiResponse
                {
                    Message = "Ha entrado el usuario al parqueo"
                };
                var mng = new ParkingBillManager();
                mng.CreateParkingBill(pb);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
          
        }
        public IHttpActionResult Put(ParkingBill pb)
        {
            try
            {
                apiResp = new ApiResponse
                {
                    Message = "Ha salido el usuario al parqueo"
                };
                var mng = new ParkingBillManager();
                mng.UpdateParkingBill(pb);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

        }


    }
}
