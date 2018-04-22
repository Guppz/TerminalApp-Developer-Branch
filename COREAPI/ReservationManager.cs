using DATAACCESS.CRUD;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class ReservationManager
    {
        private ReservationCrud CrudFactory;

        public ReservationManager()
        {
            CrudFactory = new ReservationCrud();
        }

        public List<Reservation> RetrieveAll()
        {
            throw new System.NotImplementedException();
        }

        public void Create(Reservation reservation)
        {

        }

        public void Update(Reservation reservation)
        {

        }

        public void Delete(Reservation reservation)
        {

        }

        public Reservation RetrieveById(int idReservation)
        {
            throw new System.NotImplementedException();
        }

        public List<Reservation> RetrieveReservationsByTerminal(int idTerminal)
        {
            throw new System.NotImplementedException();
        }

        public List<Reservation> RetrieveReservationsByDateRange(DateTime beginDate, DateTime endDate)
        {
            throw new System.NotImplementedException();
        }

        public List<Reservation> RetrieveReservationsByDateRangeAndTerminal(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            throw new System.NotImplementedException();
        }
    }
}
