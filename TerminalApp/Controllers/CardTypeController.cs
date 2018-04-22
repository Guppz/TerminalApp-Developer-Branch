using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using COREAPI;
using System;
using Exceptions;

namespace TerminalApp.Controllers
{
    public class CardTypeController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            try
            {
                var mngr = new CardTypeManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveAll()
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Post(CardType card)
        {
            try
            {
                var mngr = new CardTypeManager();
                mngr.Create(card);
                apiResp = new ApiResponse
                {
                    Message = "Action was executed"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }

        }

        public IHttpActionResult Put(CardType card)
        {
            try
            {
                var mngr = new CardTypeManager();
                mngr.Update(card);
                apiResp = new ApiResponse
                {
                    Message = "Action was executed"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Delete(CardType card)
        {
            try
            {
                var mngr = new CardTypeManager();
                mngr.Delete(card);
                apiResp = new ApiResponse
                {
                    Message = "Action was executed"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/CardType/GetByname")]
        public IHttpActionResult GetByname(string name)
        {
            try
            {
                CardType card = new CardType();
                card.CardName = name;
                var mngr = new CardTypeManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveByName(card)
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
