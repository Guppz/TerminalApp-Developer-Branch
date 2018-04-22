using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class ComplaintController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveAll();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(Complaint complaint)
        {
            try
            {
                var Mngr = new ComplaintManager();
                Mngr.Create(complaint);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Queja registrada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Complaint complaint)
        {
            try
            {
                var Mngr = new ComplaintManager();
                Mngr.Update(complaint);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Queja actualizada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Complaint complaint)
        {
            try
            {
                var Mngr = new ComplaintManager();
                Mngr.Delete(complaint);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Queja eliminada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveById")]
        public IHttpActionResult RetrieveById(int idComplaint)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveById(new Complaint { IdComplaint = idComplaint });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("api/complaint/UpdateComplaintsSettings")]
        public IHttpActionResult UpdateComplaintsSettings(double number, int idParam, string name)
        {
            try
            {
                var Mngr = new ComplaintManager();
                Mngr.UpdateComplaintsSettings(number, idParam, name);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Action was executed"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveComplaintsByDateRange")]
        public IHttpActionResult RetrieveComplaintsByDateRange(DateTime beginDate, DateTime endDate)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveComplaintsByDateRange(beginDate, endDate);

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveComplaintsByDateRangeAndTerminal")]
        public IHttpActionResult RetrieveComplaintsByDateRangeAndTerminal(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveComplaintsByDateRangeAndTerminal(idTerminal, beginDate, endDate);

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveComplaintsByTerminal")]
        public IHttpActionResult RetrieveComplaintsByTerminal(int idTerminal)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveComplaintsByTerminal(new Terminal { IdTerminal = idTerminal });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveComplaintsByCompany")]
        public IHttpActionResult RetrieveComplaintsByCompany(int idCompany)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveComplaintsByCompany(new Company { IdCompany = idCompany });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/complaint/RetrieveComplaintsByDateRangeAndCompany")]
        public IHttpActionResult RetrieveComplaintsByDateRangeCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            try
            {
                var Mngr = new ComplaintManager();
                ApiResp.Data = Mngr.RetrieveComplaintsByDateRangeAndCompany(beginDate, endDate, idCompany);

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}