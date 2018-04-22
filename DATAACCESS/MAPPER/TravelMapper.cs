using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class TravelMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDTRAVEL = "PKIdTravel";
        private const string DATE = "TravelDate";
        private const string PASSENGERS = "PassengersOnBoard";
        private const string IDBUS = "FKIdBus";
        private const string IDDRIVER = "FKIdDriver";
        private const string IDROUTE = "FKIdRoute";
        private const string IDSCHEDULE = "FKIdSchedule";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Schedule = new Schedule
            {
                IdSchedule = GetIntValue(row, IDSCHEDULE)
            };

            var Driver = new Driver
            {
                CardNumber = GetIntValue(row, IDDRIVER)
            };

            var Bus = new Bus
            {
                IdBus = GetIntValue(row, IDBUS),
                Driver = Driver
            };

            var Route = new Route
            {
                IdRoute = GetIntValue(row, IDROUTE)
            };

            var Travel = new Travel
            {
                IdTravel = GetIntValue(row, IDTRAVEL),
                Schedule = Schedule,
                Bus = Bus,
                Route = Route
            };

            return Travel;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Travel = BuildObject(row);
                LstResults.Add(Travel);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateTravel" };

            var C = (Travel)entity;       
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddIntParam(IDBUS, C.Bus.IdBus);
            Operation.AddIntParam(IDSCHEDULE, C.Schedule.IdSchedule);
            Operation.AddDateParam(DATE, DateTime.Now);

            return Operation;
        }

        internal SqlOperation GetRetrieveTravelByRouteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveTravelByRouteId" };

            var be = (Route)entity;
            Operation.AddIntParam(IDROUTE, be.IdRoute);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteTravel" };

            var C = (Travel)entity;
            Operation.AddIntParam(IDTRAVEL, C.IdTravel);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveTravelsList" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateTravel" };

            var C = (Travel)entity;
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddIntParam(IDSCHEDULE, C.Schedule.IdSchedule);
            Operation.AddIntParam(IDBUS, C.Bus.IdBus);
            Operation.AddIntParam(IDTRAVEL, C.IdTravel);

            return Operation;
        }

        public SqlOperation GetTravelsByRouteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
