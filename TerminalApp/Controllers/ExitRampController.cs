using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace ExitRampApp.Controllers
{
    public class ExitRampController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        ExitRampManager mng = new ExitRampManager();

        // GET ALL @ api/exitRamp
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Message = "Consulta Exitosa: Rampas de Salida";
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        // GET BY ID @ api/exitRamp/{ID}
        [Route("api/exitRamp/{pidExitRamp}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdExitRamp)
        {
            try
            {
                var exitRamp = new ExitRamp
                {
                    IdExitRamp = pIdExitRamp
                };

                exitRamp = mng.RetrieveById(exitRamp);

                apiResp = new ApiResponse();
                apiResp.Message = "Consulta Exitosa: Rampa de Salida";
                apiResp.Data = exitRamp;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST @ api/exitRamp
        public IHttpActionResult Post(ExitRamp exitRamp)
        {
            try
            {
                mng.Create(exitRamp);

                apiResp = new ApiResponse();
                apiResp.Message = "Registro Exitoso: Rampa de Salida";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // PUT @ api/exitRamp
        public IHttpActionResult Put(ExitRamp exitRamp)
        {
            try
            {
                //exitRamp = mng.RetrieveById(exitRamp);

                mng.Update(exitRamp);

                apiResp = new ApiResponse();
                apiResp.Message = "Actualización Exitosa: Rampa de Salida";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // DELETE @ api/exitRamp
        
        public IHttpActionResult Delete(ExitRamp exitRamp)
        {
            try
            {
                mng.Delete(exitRamp);

                apiResp = new ApiResponse();
                apiResp.Message = "Eliminación Exitosa: Rampa de Salida";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/exitRamp/terminal/{pIdTerminal}")]
        [HttpGet]
        public IHttpActionResult RetrieveByTerminalId(int pIdTerminal)
        {
            try
            {
                var terminal = new Terminal
                {
                    IdTerminal = pIdTerminal
                };

                apiResp = new ApiResponse();
                apiResp.Message = "Consulta Exitosa: Rampas de Salida por Terminal";
                apiResp.Data = mng.RetrieveByTerminalId(terminal);
                
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
