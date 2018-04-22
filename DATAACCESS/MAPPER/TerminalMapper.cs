using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS
{
    public class TerminalMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "PKIdTerminal";
        private const string DB_COL_NAME = "TerminalName";
        private const string DB_COL_ID_LOCATION = "FKIdLocation";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var location = new Location
            {
                IdLocation = GetIntValue(row, DB_COL_ID_LOCATION)
            };

            var terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, DB_COL_ID),
                Name = GetStringValue(row, DB_COL_NAME),
                Location = location
            };

            return terminal;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var terminal = BuildObject(row);
                lstResults.Add(terminal);
            }

            return lstResults;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllTerminal" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveTerminalById" };

            var be = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, be.IdTerminal);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateTerminal" };

            var be = (Terminal)entity;
            operation.AddVarcharParam(DB_COL_NAME, be.Name);
            operation.AddIntParam(DB_COL_ID_LOCATION, be.Location.IdLocation);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateTerminal" };

            var be = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, be.IdTerminal);
            operation.AddVarcharParam(DB_COL_NAME, be.Name);
            operation.AddIntParam(DB_COL_ID_LOCATION, be.Location.IdLocation);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteTerminal" };

            var be = (Terminal)entity;
            operation.AddIntParam(DB_COL_ID, be.IdTerminal);
            
            return operation;
        }

        public SqlOperation GetProfitsStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRegistConventionStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRetrieveCompaniesByTerminalStatement(BaseEntity entity)
        {

            var Operation = new SqlOperation { ProcedureName = "RetrieveCompaniesByTerminal" };

            var C = (Terminal)entity;
           Operation.AddIntParam(DB_COL_ID, C.IdTerminal);

            return Operation;
        }
    }
}
