using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class ScheduleController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Post(Schedule schedule)
        {
            try
            {
                var Mngr = new ScheduleManager();
                Mngr.Create(schedule);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Horario registrado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Schedule schedule)
        {
            try
            {
                var Mngr = new ScheduleManager();
                Mngr.Update(schedule);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Horario actualizado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Schedule schedule)
        {
            try
            {
                var Mngr = new ScheduleManager();
                Mngr.Delete(schedule);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Horario eliminado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        // GET BY ID @ api/terminal/{ID}
        [Route("api/terminal/{idSchedule}")]
        [HttpGet]
        public IHttpActionResult RetrieveById(int idSchedule)
        {
            try
            {
                var Mngr = new ScheduleManager();
                var schedule = new Schedule
                {
                    IdSchedule = idSchedule
                };

                ApiResponse apiResp = new ApiResponse();
                apiResp.Message = "Consulta Exitosa: Horario";
                apiResp.Data = Mngr.RetrieveById(schedule);

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
                var Mngr = new ScheduleManager();
                ApiResp.Data = Mngr.RetrieveAll();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveSchedulesByCompany(int idCompany)
        {
            throw new System.NotImplementedException();
        }

        [HttpGet]
        [Route("api/schedule/RetrieveScheduleByDayHour")]
        public IHttpActionResult RetrieveScheduleByDayHour(string day, string departHour,int idRoute)
        {
            try
            {
                var Mngr = new ScheduleManager();
                var Schedule = new Schedule
                {
                    Day = day,
                    DepartHour = departHour,
                    Route = new Route { IdRoute = idRoute }
                };

                ApiResp.Data = Mngr.RetrieveScheduleByDayHour(Schedule);

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}