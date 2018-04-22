using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using Exceptions;
using System;
using COREAPI;

namespace TerminalApp.Controllers
{
    public class BusController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();
        BusManager mng = new BusManager();

        public IHttpActionResult Get()
        {
            try
            {
                var mngr = new BusManager();
                ApiResp = new ApiResponse
                {
                    Data = mngr.RetrieveAll()
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(Bus bus)
        {
            try
            {
                var mngr = new BusManager();
                mngr.Create(bus);
                ApiResp = new ApiResponse
                {
                    Message = "Bus a sido creado exitosamente."
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Bus bus)
        {
            try
            {
                var mngr = new BusManager();
                mngr.Update(bus);
                ApiResp = new ApiResponse
                {
                    Message = "Bus a sido actualizado exitosamente."
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [HttpPut]
        [Route("api/Bus/putDriverBus")]
        public IHttpActionResult PutDriverBus(Bus bus)
        {
            try
            {
                var mngr = new BusManager();
                mngr.UpdateDriver(bus);
                ApiResp = new ApiResponse
                {
                    Message = "Se a cambiado el chofer."
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Bus bus)
        {
            try
            {
                var mngr = new BusManager();
                mngr.Delete(bus);
                ApiResp = new ApiResponse
                {
                    Message = "Bus a sido eliminado exitosamente."
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [HttpGet]
        [Route("api/Bus/RetrieveById")]
        public IHttpActionResult RetrieveById(int idBus)
        {
            try
            {
                Bus Bus = new Bus();
                Bus.IdBus = idBus;
                var mngr = new BusManager();
                ApiResp = new ApiResponse
                {
                    Data = mngr.RetrieveById(Bus)
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveTravelsByBus(int idBus)
        {
            return null;
        }

        [HttpGet]
        [Route("api/bus/RetrieveBusesByCompany")]
        public IHttpActionResult RetrieveBusesByCompany(int idCompany)
        {
            try
            {
                Company Company = new Company();
                Company.IdCompany = idCompany;
                var mngr = new BusManager();
                ApiResp = new ApiResponse
                {
                    Data = mngr.RetrieveBusesByCompany(Company)
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

        }

        [HttpGet]
        [Route("api/bus/RetrieveBusesByCompanyUser")]
        public IHttpActionResult RetrieveBusesByCompanyUser(String CorpIdentification)
        {
            try
            {
                Company Company = new Company();
                Company.CorpIdentification = CorpIdentification;
                var mngr = new BusManager();
                ApiResp = new ApiResponse
                {
                    Data = mngr.RetrieveBusesByCompanyUser(Company)
                };
                return Ok(ApiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

        }
    }
}
