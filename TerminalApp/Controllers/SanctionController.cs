using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class SanctionController : ApiController
    {
        ApiResponse ApiResp = new ApiResponse();

        public IHttpActionResult Post(Sanction sanction)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.Create(sanction);

                ApiResponse ApiResp = new ApiResponse
                {
                    Message = "Sanción registrada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPost]
        [Route("api/sanction/CreateSanctionType")]
        public IHttpActionResult CreateSanctionType(SanctionType sanctionType)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.CreateSanctionType(sanctionType);

                ApiResp = new ApiResponse
                {
                    Message = "Tipo registrado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionsTypes")]
        public IHttpActionResult RetrieveSanctionsTypes()
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionsTypes();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpPut]
        [Route("api/sanction/UpdateSanctionType")]
        public IHttpActionResult UpdateSanctionType(SanctionType type)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.UpdateSanctionType(type);

                ApiResp = new ApiResponse
                {
                    Message = "Tipo actualizado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpDelete]
        [Route("api/sanction/DeleteSanctionType")]
        public IHttpActionResult DeleteSanctionType(SanctionType type)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.DeleteSanctionType(type);

                ApiResp = new ApiResponse
                {
                    Message = "Tipo eliminado exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Sanction sanction)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.Update(sanction);

                ApiResp = new ApiResponse
                {
                    Message = "Sanción actualizada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Sanction sanction)
        {
            try
            {
                var Mngr = new SanctionManager();
                Mngr.Delete(sanction);

                ApiResp = new ApiResponse
                {
                    Message = "Sanción eliminada exitosamente"
                };

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveById")]
        public IHttpActionResult RetrieveById(int idSanction)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveById(new Sanction { IdSanction = idSanction });


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionTypeById")]
        public IHttpActionResult RetrieveSanctionTypeById(int idType)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionTypeById(new SanctionType { IdType = idType });


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionsByType")]
        public IHttpActionResult RetrieveSanctionsByType(int idType)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionsByType(new SanctionType { IdType = idType });


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionTypeByName")]
        public IHttpActionResult RetrieveSanctionTypeByName(string value)
        {

            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionTypeByName(new SanctionType { Name = value });

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionTypeByDescription")]
        public IHttpActionResult RetrieveSanctionTypeByDescription(string value)
        {

            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionTypeByDescription(new SanctionType { Description = value });

                return Ok(ApiResp);
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
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveAll();

                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionsByDateRange")]
        public IHttpActionResult RetrieveSanctionsByDateRange(DateTime beginDate, DateTime endDate)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionsByDateRange(beginDate, endDate);


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionsByDateRangeAndCompany")]
        public IHttpActionResult RetrieveSanctionsByDateAndCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionsByDateRangeAndCompany(beginDate, endDate, idCompany);


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [HttpGet]
        [Route("api/sanction/RetrieveSanctionsByCompany")]
        public IHttpActionResult RetrieveSanctionsByCompany(int idCompany)
        {
            try
            {
                var Mngr = new SanctionManager();
                ApiResp.Data = Mngr.RetrieveSanctionsByCompany(new Company { IdCompany = idCompany });


                return Ok(ApiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}