using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class ReservationMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDRESERVATION = "PKIdReservation";
        private const string AMOUNT = "Quantity";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            throw new NotImplementedException();
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetReservationsByTerminalStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetReservationsDateRangeStatement(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetReservationsByDateRangeAndTerminalStatement(DateTime beginDate, DateTime endDate, int idTerminal)
        {
            throw new NotImplementedException();
        }
    }
}
