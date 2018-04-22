using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class ScheduleMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDSCHEDULE = "PKIdSchedule";
        private const string DAY = "Day";
        private const string DEPARTHOUR = "DepartHour";
        private const string IDROUTE = "FKIdRoute";
        private const string AVAILABLE = "Available";
        private const string AVAILABLESTATUS = "IsAvailable";
        private const string DEPARTHOURASMINUTES = "DepartHourAsMinutes";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Route = new Route
            {
                IdRoute = GetIntValue(row, IDROUTE)
            };

            var Schedule = new Schedule
            {
                IdSchedule = GetIntValue(row, IDSCHEDULE),
                Day = GetStringValue(row, DAY),
                DepartHour = GetStringValue(row, DEPARTHOUR),
                DepartHourAsMinutes = GetIntValue(row, DEPARTHOURASMINUTES),
                Route = Route,
                Available = GetIntValue(row, AVAILABLE)
            };

            return Schedule;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Schedule = BuildObject(row);
                LstResults.Add(Schedule);
            }

            return LstResults;
        }
        
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateSchedule" };

            var C = (Schedule)entity;
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddVarcharParam(DAY, C.Day);
            Operation.AddVarcharParam(DEPARTHOUR, C.DepartHour);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteSchedule" };

            var C = (Schedule)entity;
            Operation.AddIntParam(IDSCHEDULE, C.IdSchedule);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveAllSchedule" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveScheduleById" };

            var C = (Schedule)entity;
            Operation.AddIntParam(IDSCHEDULE, C.IdSchedule);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateSchedule" };

            var C = (Schedule)entity;
            Operation.AddIntParam(IDSCHEDULE, C.IdSchedule);
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddVarcharParam(DAY, C.Day);
            Operation.AddVarcharParam(DEPARTHOUR, C.DepartHour);

            return Operation;
        }

        public SqlOperation GetSchedulesByCompanyStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetScheduleByDayHourStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveScheduleByDayHour" };

            var C = (Schedule)entity;
            Operation.AddVarcharParam(DAY, C.Day);
            Operation.AddVarcharParam(DEPARTHOUR, C.DepartHour);
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);

            return Operation;
        }
    }
}
