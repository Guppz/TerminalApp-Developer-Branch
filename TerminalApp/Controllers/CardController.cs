using System.Web.Http;
using TerminalApp.Models;
using ENTITIES_POJO;
using Exceptions;
using System;
using COREAPI;

namespace TerminalApp.Controllers
{
    public class CardController : ApiController
    {
        ApiResponse apiResp = new ApiResponse();

        public IHttpActionResult Get()
        {
            try
            {
                var mngr = new CardManager();
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

        public IHttpActionResult Post(Card card)
        {
            try
            {
                var mngr = new CardManager();
                mngr.Create(card);
                apiResp = new ApiResponse
                {
                    Message = "Tarjeta GIN exitosamente creada."
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        public IHttpActionResult Put(Card card)
        {
            try
            {
                var mngr = new CardManager();
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
        [Route("api/Card/PutActivacion")]
        [HttpPut]
        public IHttpActionResult PutActivacion(Card card)
        {
            try
            {
                var mngr = new CardManager();
                mngr.UpdateActivacion(card);
                apiResp = new ApiResponse
                {
                    Message = "Tarjeta activada"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/PutNotification")]
        [HttpPut]
        public IHttpActionResult PutNotification(Card card)
        {
            try
            {
                var mngr = new CardManager();
                mngr.UpdateNotificasion(card);
                apiResp = new ApiResponse
                {
                    Message = "El tiempo para ser notificado fue cambiado"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/PutNewBalance")]
        [HttpPut]
        public IHttpActionResult PutNewBalance(Card card)
        {
            try
            {
                var mngr = new CardManager();
                mngr.UpdateBalance(card);
                apiResp = new ApiResponse
                {
                    Message = "Se a ingresado nuevo saldo"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                SystemParam sy = new SystemParam();
                var spm = new SystemParamManager();
                sy.IdSystemParam = 3;
                sy = spm.RetrieveById(sy);
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message+" "+ sy.Value));
            }
        }

        public IHttpActionResult Delete(Card card)
        {
            try
            {
                var mngr = new CardManager();
                mngr.Delete(card);
                apiResp = new ApiResponse
                {
                    Message = "Se a desactivo una tarjeta"
                };
                return Ok(apiResp);

            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/RetrieveByTerminal")]
        [HttpGet]
        public IHttpActionResult RetrieveByTerminal(int idTerminal)
        {
            try
            {
                Card Card = new Card();
                Terminal terminal = new Terminal();
                terminal.IdTerminal = idTerminal;
                Card.Terminal= terminal;

                var mngr = new CardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveByTerminal(Card)
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/RetrieveByStudiant")]
        [HttpGet]
        public IHttpActionResult RetrieveByStudiant(int idTerminal)
        {
            try
            {
                Card Card = new Card();
                Terminal terminal = new Terminal();
                terminal.IdTerminal = idTerminal;
                Card.Terminal = terminal;

                var mngr = new CardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveStudiant(Card)
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/RetrieveByStudiantCardDisables")]
        [HttpGet]
        public IHttpActionResult RetrieveByStudiantCardDisables(int idTerminal)
        {
            try
            {
                Card Card = new Card();
                Terminal terminal = new Terminal();
                terminal.IdTerminal = idTerminal;
                Card.Terminal = terminal;
                var mngr = new CardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveStudiantCardDisabled(Card)
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }

        [Route("api/Card/RetrieveById")]
        [HttpGet]
        public IHttpActionResult RetrieveById(string idCard)
        {
            try
            {
                Card Card = new Card();
                Card.IdCard = idCard;
                var mngr = new CardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveById(Card)
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/Card/RetrieveUserCards")]
        [HttpGet]
        public IHttpActionResult RetrieveUserCards(int idUser)
        {
            try
            {
                Card Card = new Card();
                User u = new User();
                u.IdUser = idUser;
                Card.User = u;
                var mngr = new CardManager();
                apiResp = new ApiResponse
                {
                    Data = mngr.RetrieveUserCards(Card)
                };
                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
        [Route("api/Card/CardRequest")]
        public IHttpActionResult CardRequest(string fileName)
        {
            return Ok();
        }

        [Route("api/card/recovery")]
        [HttpPost]
        public IHttpActionResult ReIssueCard(CardReIssue pCardReIssue)
        {
            try
            {
                var mng = new CardManager();
                mng.ReIssueCard(pCardReIssue);

                apiResp = new ApiResponse();
                apiResp.Message = "Se ha re-expedido la tarjeta.";

                return Ok(apiResp);
            }
            catch (BusinessException bex)
            {
                return InternalServerError(new Exception(bex.ExceptionId + "-" + bex.AppMessage.Message));
            }
        }
    }
}
