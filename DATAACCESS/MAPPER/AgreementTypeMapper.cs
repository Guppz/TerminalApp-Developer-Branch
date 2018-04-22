using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class AgreementTypeMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "PkIdAgreementType";
        private const string DB_COL_NAME = "AgreementTypeName";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var agreementType = new AgreementType
            {
                IdAgreementType = GetIntValue(row, DB_COL_ID),
                AgreementTypeName = GetStringValue(row, DB_COL_NAME)
            };

            return agreementType;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var agreementType = BuildObject(row);
                lstResults.Add(agreementType);
            }

            return lstResults;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllAgreementType" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAgreementTypeById" };

            var be = (AgreementType)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreementType);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateAgreementType" };

            var be = (AgreementType)entity;
            operation.AddVarcharParam(DB_COL_NAME, be.AgreementTypeName);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateAgreementType" };

            var be = (AgreementType)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreementType);
            operation.AddVarcharParam(DB_COL_NAME, be.AgreementTypeName);
           
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteAgreementType" };

            var be = (AgreementType)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreementType);

            return operation;
        }
    }
}
