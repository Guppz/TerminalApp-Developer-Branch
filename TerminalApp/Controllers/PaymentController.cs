using ENTITIES_POJO;
using System.Web.Http;

namespace TerminalApp.Controllers
{
    public class PaymentController : ApiController
    {
        public IHttpActionResult Post(Payment payment)
        {
            return Ok();
        }

        public IHttpActionResult Put(Payment payment)
        {
            return Ok();
        }

        public IHttpActionResult Delete(Payment payment)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveById(int idPayment)
        {
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        public IHttpActionResult RetrieveUserPayments(int idUser)
        {
            return null;
        }
    }
}