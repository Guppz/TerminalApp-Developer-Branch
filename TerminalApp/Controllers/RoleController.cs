using COREAPI;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Web.Http;
using TerminalApp.Models;

namespace TerminalApp.Controllers
{
    public class RoleController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            var mngr = new RoleManager();
            apiResp.Data = mngr.RetrieveAll();
            return Ok(apiResp);
        }

        public IHttpActionResult Post(Role role)
        {
            try
            {
                var mngr = new RoleManager();
                mngr.Create(role);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "Rol creado exitosamente"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Role role)
        {
            try
            {
                var mngr = new RoleManager();
                mngr.Update(role);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "Rol actualizado exitosamente"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(Role role)
        {
            try
            {
                var mngr = new RoleManager();
                mngr.UpdateDesactivation(role);

                ApiResponse apiResp = new ApiResponse
                {
                    Message = "Rol desactivado"
                };

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult RetrieveById(int idRole)
        {
            var mngr = new RoleManager();
            apiResp.Data = mngr.RetrieveAll();
            return Ok(apiResp);
           
        }

        [Route("api/Role/PutActivacion")]
        [HttpPut]
        public IHttpActionResult PutActivacion(Role role)
        {   
            try
            {
                var mngr = new RoleManager();
                mngr.UpdateActivacion(role);
                apiResp = new ApiResponse
                {
                    Message = "Rol activado"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


    }
}