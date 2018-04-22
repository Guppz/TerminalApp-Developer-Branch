using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class RouteMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDROUTE = "PKIdRoute";
        private const string NAME = "RouteName";
        private const string DISTANCE = "Distance";
        private const string PRICE = "Price";
        private const string ESTIMATEDTIME = "EstimatedTime";
        private const string IDCOMPANY = "FKIdCompany";
        private const string IDTERMINAL = "FKIdTerminal";
        private const string STATUS = "Active";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, IDTERMINAL)
            };

            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY)
            };

            var Route = new Route
            {
                IdRoute = GetIntValue(row, IDROUTE),
                Name = GetStringValue(row, NAME),
                Distance = GetDoubleValue(row, DISTANCE),
                Price = GetDoubleValue(row, PRICE),
                EstimatedTime = GetDoubleValue(row, ESTIMATEDTIME),
                Status = GetIntValue(row, STATUS),
                RouteTerminal = new Terminal { IdTerminal = GetIntValue(row, IDTERMINAL) },
                RouteCompany = new Company {IdCompany = GetIntValue(row, IDCOMPANY) }
                
            };

            return Route;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Route = BuildObject(row);
                LstResults.Add(Route);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateRoute" };

            var C = (Route)entity;
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddDoubleParam(DISTANCE, C.Distance);
            Operation.AddDoubleParam(ESTIMATEDTIME, C.EstimatedTime);
            Operation.AddIntParam(IDCOMPANY, C.RouteCompany.IdCompany);
            Operation.AddIntParam(IDTERMINAL, C.RouteTerminal.IdTerminal);
            Operation.AddDoubleParam(PRICE, C.Price);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var C = (Route)entity;
            var Operation = new SqlOperation { ProcedureName = "ActivateDeactivateRoute" };
            Operation.AddIntParam(STATUS, C.Status);
            Operation.AddIntParam(IDROUTE, C.IdRoute);
            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveAllRoute" };
            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveRouteById" };

            var C = (Route)entity;
            Operation.AddIntParam(IDROUTE, C.IdRoute);
            

            return Operation;
        }

        public SqlOperation GetRetriveByCompanyStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveRouteByCompany" };

            var C = (Route)entity;
            Operation.AddIntParam(IDCOMPANY, C.RouteCompany.IdCompany);


            return Operation;
        }
        public SqlOperation GetRetriveByTerminalStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveRouteByTerminal" };

            var C = (Terminal)entity;
            Operation.AddIntParam(IDTERMINAL, C.IdTerminal);


            return Operation;
        }
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateRoute" };

            var C = (Route)entity;
            Operation.AddIntParam(IDROUTE, C.IdRoute);
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddDoubleParam(DISTANCE, C.Distance);
            Operation.AddDoubleParam(ESTIMATEDTIME, C.EstimatedTime);
            Operation.AddIntParam(IDCOMPANY, C.RouteCompany.IdCompany);
            Operation.AddIntParam(IDTERMINAL, C.RouteTerminal.IdTerminal);
            Operation.AddDoubleParam(PRICE, C.Price);

            return Operation;
        }

        public SqlOperation GetRetrieveByCompanyStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveRoutesByCompany" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetRetrieveByNameStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveRouteByName" };

            var C = (Route)entity;
            Operation.AddVarcharParam(NAME, C.Name);

            return Operation;
        }

        public SqlOperation GetDeleteLocationsStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteLocationRoute" };

            var C = (Route)entity;
            Operation.AddIntParam(IDROUTE, C.IdRoute);

            return Operation;
        }
    }
}
