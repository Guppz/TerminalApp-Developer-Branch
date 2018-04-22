using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class DriverController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Post(Driver driver)
        {
            try
            {
                var mngr = new DriverManager();
                mngr.Create(driver);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "El chofer  ha sido creado exitosamente"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Driver driver)
        {
            try
            {
                var mngr = new DriverManager();
                mngr.Update(driver);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "El chofer  ha sido actualizado exitosamente"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Driver driver)
        {
            try
            {
                var mngr = new DriverManager();
                mngr.UpdateDesactivation(driver);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "Chofer desactivado"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/Driver/PutActivacion")]
        [HttpPut]
        public IHttpActionResult PutActivacion(Driver driver)
        {
            try
            {
                var mngr = new DriverManager();
                mngr.UpdateActivacion(driver);
                ApiResponse apiResp = new ApiResponse
                {
                    Message = "Chofer activado"
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
            var mngr = new DriverManager();
            ApiResp.Data = mngr.RetrieveAll();
            return Ok(ApiResp);
        }

        public IHttpActionResult RetrieveById(int idDriver)
        {
            try
            {
                var mngr = new DriverManager();
                var driver = new Driver
                {
                    CardNumber = idDriver
                };
                driver = mngr.RetrieveById(driver);
                ApiResp.Data = driver;
                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        //public IHttpActionResult AssignDriverToBus(Driver driver)
        //{
        //    return Ok();
        //}

        [HttpGet]
        [Route("api/driver/RetrieveDriversByCompany")]
        public IHttpActionResult RetrieveDriversByCompany(int idCompany)
        {
            try
            {
                var Mngr = new DriverManager();
                ApiResp.Data = Mngr.RetrieveDriversByCompany(new Company{ IdCompany = idCompany });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}