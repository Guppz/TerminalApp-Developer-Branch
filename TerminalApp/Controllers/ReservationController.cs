using ENTITIES_POJO;
using System;
using System.Web.Http;

namespace TerminalApp.Controllers
{
    public class ReservationController : ApiController
    {
        public IHttpActionResult Post(Reservation reservation)
        {
            return Ok();
        }

        public IHttpActionResult Get()
        {
            return Ok();
        }

        public IHttpActionResult Put(Reservation reservation)
        {
            return Ok();
        }

        public IHttpActionResult Delete(Reservation reservatione)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveById(int idReservation)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveReservationsByTerminal(int idTerminal)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveReservationsByDateRange(DateTime beginDate, DateTime endDate)
        {
            return Ok();
        }

        public IHttpActionResult RetrieveReservationsByDateRangeAndTerminal(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            return Ok();
        }
    }
}