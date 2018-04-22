using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using System;
using Exceptions;

namespace TerminalApp.Controllers
{
    public class CredidCardController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();


        [Route("api/CredidCard/GetCredidCard")]
        public IHttpActionResult GetCredidCard(int idUser)
        {
            try
            {
                User user = new User();
                user.IdUser = idUser;
                CredidCard cc = new CredidCard();
                cc.User = user;
                var mngr = new CredidCardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveAll(cc)
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(CredidCard card)
        {
            try
            {
                var mngr = new CredidCardManager();
                mngr.Create(card);
                apiResp = new ApiResponse
                {
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
