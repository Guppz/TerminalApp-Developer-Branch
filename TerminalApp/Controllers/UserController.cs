using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using System.Web.Http.Cors;
using Exceptions;
using System;
using System.Collections.Generic;

namespace TerminalApp.Controllers
{

    public class UserController : ApiController
    {

        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Post(User user)
        {
            try
            {
                apiResp = new ApiResponse
                {
                    Message = "El usuario ha sido creado"
                };
                var mng = new UserManager();
                user.BirthDate = Convert.ToDateTime(user.BirthDateString);
                mng.Create(user);
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/User/PostUserConvenio")]
        [HttpPost]
        public IHttpActionResult PostUserConvenio(User user)
        {
            try
            {
                var mng = new UserManager();
                DateTime value = new DateTime(1992, 1, 1);
                //user.BirthDate = Convert.ToDateTime(user.BirthDateString);
                user.BirthDate = value;
                apiResp = new ApiResponse();
                var exite = mng.CreateAgreementUser(user);
                if (exite == 0)
                {
                    apiResp.Message = "Los usuario  ha sido creado exitosamente";
                }
                else
                {
                    apiResp.Message = "Algunos usarios ya exiten en el sistema.";
                }
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }


        public IHttpActionResult Put(User user)
        {
            apiResp = new ApiResponse
            {
                Message = "El usuario ha sido actualizado"
            };
            var mng = new UserManager();
            user.BirthDate = Convert.ToDateTime(user.BirthDateString);
            mng.Update(user);

            return Ok(apiResp);
        }

        public IHttpActionResult RetrieveById(int idUser)
        {
            return Ok();
        }

        public IHttpActionResult Get()
        {
            apiResp = new ApiResponse();
            var mng = new UserManager();
            apiResp.Data = mng.RetrieveAll();
            return Ok(apiResp);
        }

        public IHttpActionResult Delete(User user)
        {
            string status = "";
            if (user.Status == 1) {
                status = "desactivo";

            }
            else {
                status = "activo";
            }
             
            apiResp = new ApiResponse
            {
                Message = "Se"+ status + "existosamente"
            };
            var mng = new UserManager();
            mng.Delete(user);
            return Ok(apiResp);
        }
        [Route("api/User/Login")]
        [HttpGet]
        public IHttpActionResult Login(string email, string password)
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new UserManager();
                UserPassword up = new UserPassword
                {
                    Password = password
                };

                User user = new User
                {
                    Email = email,
                    Password = up,
                    Identification = "",
                    
                    
                };
               apiResp.Data = mng.LoginUser(user);
                
            

                apiResp.Message = "Se ha encontrado el usuario";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/User/ModifyPassword")]
        [HttpPost]
        public IHttpActionResult ModifyPasword(User us)
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new UserManager();
                var newPassword = us.Name;
                us.Password = new UserPassword();
                us.Password.Password = us.Identification;
                us.Identification = "";
                mng.ModifyPassword(us, newPassword);
                apiResp.Message = "Se ha modificado la contraseña, vuelva a la pantalla de login";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/User/RecoverPassword")]
        [HttpPost]
        public IHttpActionResult RecoverPassword(User user)
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new UserManager();
                user.Identification = "";
               
                mng.RecoverPassword(user);
                apiResp.Message = "Se ha creado una nueva contraseña, revise su correo";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/User/{terminal}")]
        [HttpGet]
        public IHttpActionResult GetUserByTerminal(int terminal)
        {
            try
            {
                apiResp = new ApiResponse();
                var mng = new UserManager();
                var user = new User
                {
                    UserTerminal = new Terminal { IdTerminal = terminal }
                   
                };

                apiResp.Data = mng.RetrieveByTerminal(user);



                apiResp.Message = "Se ha encontrado los usuarios";
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
