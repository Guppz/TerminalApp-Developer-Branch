using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class FineTypeMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDFINETYPE = "PKIdFineType";
        private const string DESCRIPTION = "TypeDescription";
        private const string COST = "Cost";
        private const string NAME = "TypeName";
        private const string IDLIST = "IdList";
        private const string VALUE = "Value";
        private const string DSVIEW = "Description";
        
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var fineType = new FineType
            {
                IdType = GetIntValue(row, IDFINETYPE),
                TypeDescription = GetStringValue(row, DESCRIPTION),
                Cost = GetDoubleValue(row, COST),
                TypeName = GetStringValue(row, NAME)

            };
            return fineType;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
            foreach (var row in lstRows)
            {
                var fineType = BuildObject(row);
                lstResults.Add(fineType);
            }
            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateFineType" };
            var c = (FineType)entity;
            operation.AddVarcharParam(DESCRIPTION, c.TypeDescription);
            operation.AddDoubleParam(COST, c.Cost);
            operation.AddVarcharParam(NAME, c.TypeName);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateFineType" };
            var c = (FineType)entity;
            operation.AddIntParam(IDFINETYPE, c.IdType);
            operation.AddVarcharParam(DESCRIPTION, c.TypeDescription);
            operation.AddDoubleParam(COST, c.Cost);
            operation.AddVarcharParam(NAME, c.TypeName);
            return operation;
        }


        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteFineType" };
            var c = (FineType)entity;
            operation.AddIntParam(IDFINETYPE, c.IdType);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFinesTypesList" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFineTypeById" };
            var c = (FineType)entity;
            operation.AddIntParam(IDFINETYPE, c.IdType);
            return operation;
        }

        public SqlOperation GetCreateFineTypeStatement(BaseEntity entity)
        {
            throw new NotImplementedException();

        }

        public SqlOperation GetUpdateFinesSettingsStatement(int number)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetAllFinesTypesStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetFinesByDateRangeStatement(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetFinesByDateRangeAndCompanyStatement(int ideTerminal, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetFinesByCompanyStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }


    }
}
