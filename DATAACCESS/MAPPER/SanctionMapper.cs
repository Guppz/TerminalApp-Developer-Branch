using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class SanctionMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDSANCTION = "PKIdSanction";
        private const string DESCRIPTION = "SanctionDescription";
        private const string DATE = "SanctionDate";
        private const string IDROUTE = "FKIdRoute";
        private const string IDCOMPANY = "FKIdCompany";
        private const string IDTERMINAL = "FKIdTerminal";
        private const string IDSANCTIONTYPE = "FKIdSanctionType";
        private const string DATEINIT = "DateInit";
        private const string DATEEND = "DateEnd";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY)
            };

            var Route = new Route
            {
                IdRoute = GetIntValue(row, IDROUTE)
            };

            var Terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, IDTERMINAL)
            };

            var SanctionType = new SanctionType
            {
                IdType = GetIntValue(row, IDSANCTIONTYPE)
            };

            var Sanction = new Sanction
            {
                IdSanction = GetIntValue(row, IDSANCTION),
                Date = GetStringValue(row, DATE),
                Description = GetStringValue(row, DESCRIPTION),
                Company = Company,
                Route = Route,
                Terminal = Terminal,
                Type = SanctionType
            };

            return Sanction;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Sanction = BuildObject(row);
                LstResults.Add(Sanction);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateSanction" };

            var C = (Sanction)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddIntParam(IDTERMINAL, C.Terminal.IdTerminal);
            Operation.AddIntParam(IDSANCTIONTYPE, C.Type.IdType);
            Operation.AddIntParam(IDCOMPANY, C.Company.IdCompany);
            Operation.AddDateParam(DATE, DateTime.Now);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteSanction" };

            var C = (Sanction)entity;
            Operation.AddIntParam(IDSANCTION, C.IdSanction);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsList" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionById" };

            var C = (Sanction)entity;
            Operation.AddIntParam(IDSANCTION, C.IdSanction);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateSanction" };

            var C = (Sanction)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddIntParam(IDROUTE, C.Route.IdRoute);
            Operation.AddIntParam(IDTERMINAL, C.Terminal.IdTerminal);
            Operation.AddIntParam(IDSANCTIONTYPE, C.Type.IdType);
            Operation.AddIntParam(IDCOMPANY, C.Company.IdCompany);
            Operation.AddDateParam(DATE, DateTime.Now);
            Operation.AddIntParam(IDSANCTION, C.IdSanction);

            return Operation;
        }

        public SqlOperation GetSanctionsByDateRangeStatement(DateTime beginDate, DateTime endDate)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsByDateRange" };

            Operation.AddDateParam(DATEINIT, beginDate);
            Operation.AddDateParam(DATEEND, endDate);

            return Operation;
        }

        public SqlOperation GetSanctionsByDateRangeAndCompanyStatement(DateTime beginDate, DateTime endDate, int idCompany)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsByDateRangeAndCompanyId" };

            Operation.AddDateParam(DATEINIT, beginDate);
            Operation.AddDateParam(DATEEND, endDate);
            Operation.AddIntParam(IDCOMPANY, idCompany);

            return Operation;
        }

        public SqlOperation GetSanctionsByCompanyStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsByCompanyId" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetSanctionsByTypeStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsByType" };

            var C = (SanctionType)entity;
            Operation.AddIntParam(IDSANCTIONTYPE, C.IdType);

            return Operation;
        }
    }
}
