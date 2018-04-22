using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS
{
    public class ExitRampMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "PKIdExitRamp";
        private const string DB_COL_NAME = "ExitRampName";
        private const string DB_COL_ID_TERMINAL = "FKIdTerminal";
        private const string DB_COL_ID_CURRENT_BUS = "FKCurrentBus";
        private const string DB_COL_ID_NEXT_BUS = "FKNextBus";
        private const string DB_COL_ID_ROUTE = "FKIdRoute";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            var terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, DB_COL_ID_TERMINAL)
            };

            var currentBus = new Bus
            {
                IdBus = GetIntValue(row, DB_COL_ID_CURRENT_BUS)
            };

            var nextBus = new Bus
            {
                IdBus = GetIntValue(row, DB_COL_ID_NEXT_BUS)
            };

            var route = new Route
            {
                IdRoute = GetIntValue(row, DB_COL_ID_ROUTE)
            };

            var exitRamp = new ExitRamp
            {
                IdExitRamp = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
                Terminal = terminal,
                Route = route
            };

            return exitRamp;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var exitRamp = BuildObject(row);
                lstResults.Add(exitRamp);
            }

            return lstResults;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllExitRamp" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveExitRampById" };

            var be = (ExitRamp)entity;
            operation.AddIntParam(DB_COL_ID, be.IdExitRamp);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateExitRamp" };

            var be = (ExitRamp)entity;
            operation.AddVarcharParam(DB_COL_NAME, be.Name);
            operation.AddIntParam(DB_COL_ID_TERMINAL, be.Terminal.IdTerminal);

            if (be.Route?.IdRoute > 0)
            {
                operation.AddIntParam(DB_COL_ID_ROUTE, be.Route.IdRoute);
            }        

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateExitRamp" };

            var be = (ExitRamp)entity;
            operation.AddIntParam(DB_COL_ID, be.IdExitRamp);
            operation.AddVarcharParam(DB_COL_NAME, be.Name);

            if (be.Route?.IdRoute > 0)
            {
                operation.AddIntParam(DB_COL_ID_ROUTE, be.Route.IdRoute);
            }

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteExitRamp" };

            var be = (ExitRamp)entity;
            operation.AddIntParam(DB_COL_ID, be.IdExitRamp);
            
            return operation;
        }

        public SqlOperation GetRetrieveByTerminalIdStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveExitRampByTerminalId" };

            var be = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID_TERMINAL, be.IdTerminal);

            return operation;
        }
    }
}
