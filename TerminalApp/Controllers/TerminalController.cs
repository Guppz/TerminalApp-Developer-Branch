using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using Exceptions;
using System;

namespace TerminalApp.Controllers
{
    public class TerminalController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();
        TerminalManager mng = new TerminalManager();

        // GET ALL @ api/terminal
        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            apiResp.Message = "Consulta Exitosa: Terminales";
            apiResp.Data = mng.RetrieveAll();

            return Ok(apiResp);
        }
        // GET BY ID @ api/terminal/{ID}
        [Route("api/terminal/{pidTerminal}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int pIdTerminal)
        {
            try
            {
                var terminal = new Terminal
                {
                    IdTerminal = pIdTerminal
                };

                terminal = mng.RetrieveById(terminal);

                apiResp = new ApiResponse();
                apiResp.Message = "Consulta Exitosa: Terminal";
                apiResp.Data = terminal;

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // POST @ api/terminal
        public IHttpActionResult Post(Terminal terminal)
        {
            try
            {
                mng.Create(terminal);

                apiResp = new ApiResponse();
                apiResp.Message = "Registro Exitoso: Terminal";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // PUT @ api/terminal
        public IHttpActionResult Put(Terminal terminal)
        {
            try
            {
                //terminal = mng.RetrieveById(terminal);

                mng.Update(terminal);

                apiResp = new ApiResponse();
                apiResp.Message = "Actualización Exitosa: Terminal";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        // DELETE @ api/terminal

        public IHttpActionResult Delete(Terminal terminal)
        {
            try
            {
                mng.Delete(terminal);

                apiResp = new ApiResponse();
                apiResp.Message = "Eliminación Exitosa: Terminal";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // GET REPORT BY TERMINAL ID
        [Route("api/terminal/report/{pidTerminal}")]
        [HttpGet]
        public IHttpActionResult RetrieveProfitsReports(int pIdTerminal)
        {
            apiResp = new ApiResponse();
            apiResp.Message = "REPORT TERMINAL PROFIT - Method Not Implemented";

            return Ok(apiResp);
        }
    }
}
