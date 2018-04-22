using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class ReservationCrud: CrudFactory
    {
        private ReservationMapper Mapper;

        public ReservationCrud()
        {
            Mapper = new ReservationMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public List<Reservation> RetrieveReservationsByTerminal(BaseEntity entity)
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
