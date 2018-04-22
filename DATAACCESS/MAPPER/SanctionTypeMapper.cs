using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class SanctionTypeMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDTYPE = "PKIdSanctionType";
        private const string NAME = "TypeName";
        private const string DESCRIPTION = "TypeDescription";
        private const string COST = "Cost";
        private const string STATUS = "Status";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Type = new SanctionType
            {
                IdType = GetIntValue(row, IDTYPE),
                Name = GetStringValue(row, NAME),
                Description = GetStringValue(row, DESCRIPTION),
                Cost = GetDoubleValue(row, COST),
                Status = GetIntValue(row, STATUS)
            };

            return Type;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();


            foreach (var row in lstRows)
            {
                var SancType = BuildObject(row);
                LstResults.Add(SancType);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateSanctionType" };

            var C = (SanctionType)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddDoubleParam(COST, C.Cost);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteSanctionType" };

            var C = (SanctionType)entity;
            Operation.AddIntParam(IDTYPE, C.IdType);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionsTypesList" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionTypeById" };

            var C = (SanctionType)entity;
            Operation.AddIntParam(IDTYPE, C.IdType);

            return Operation;
        }

        public SqlOperation GetRetriveByNameStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionTypeByName" };

            var C = (SanctionType)entity;
            Operation.AddVarcharParam(NAME, C.Name);

            return Operation;
        }

        public SqlOperation GetRetriveByDescriptionStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSanctionTypeByDescription" };

            var C = (SanctionType)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateSanctionType" };

            var C = (SanctionType)entity;
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddDoubleParam(COST, C.Cost);
            Operation.AddIntParam(IDTYPE, C.IdType);

            return Operation;
        }
    }
}
