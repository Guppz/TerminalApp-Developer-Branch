using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class AgreementMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string DB_COL_ID = "PkIdAgreement";
        private const string DB_COL_NAME = "InstituteName";
        private const string DB_COL_EMAIL = "InstituteEmail";
        private const string DB_COL_ID_TERMINAL = "FkIdTerminal";
        private const string DB_COL_ID_TYPE = "FkIdAgreementType";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, DB_COL_ID_TERMINAL)
            };

            var agreementType = new AgreementType
            {
                IdAgreementType = GetIntValue(row, DB_COL_ID_TYPE)
            };

            var agreement = new Agreement
            {
                IdAgreement = GetIntValue(row, DB_COL_ID),
                InstituteName = GetStringValue(row, DB_COL_NAME),
                InstituteEmail = GetStringValue(row, DB_COL_EMAIL),
                Terminal = terminal,
                AgreementType = agreementType
            };

            return agreement;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var agreement = BuildObject(row);
                lstResults.Add(agreement);
            }

            return lstResults;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllAgreement" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAgreementById" };

            var be = (Agreement)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreement);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateAgreement" };

            var be = (Agreement)entity;
            operation.AddVarcharParam(DB_COL_NAME, be.InstituteName);
            operation.AddVarcharParam(DB_COL_EMAIL, be.InstituteEmail);
            operation.AddIntParam(DB_COL_ID_TERMINAL, be.Terminal.IdTerminal);
            operation.AddIntParam(DB_COL_ID_TYPE, be.AgreementType.IdAgreementType);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateAgreement" };

            var be = (Agreement)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreement);
            operation.AddVarcharParam(DB_COL_NAME, be.InstituteName);
            operation.AddVarcharParam(DB_COL_EMAIL, be.InstituteEmail);
            operation.AddIntParam(DB_COL_ID_TERMINAL, be.Terminal.IdTerminal);
            operation.AddIntParam(DB_COL_ID_TYPE, be.AgreementType.IdAgreementType);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteAgreement" };

            var be = (Agreement)entity;
            operation.AddIntParam(DB_COL_ID, be.IdAgreement);

            return operation;
        }

        public SqlOperation GetRetrieveByEmailAndTypeStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAgreementByEmailAndType" };

            var be = (Agreement)entity;
            operation.AddVarcharParam(DB_COL_EMAIL, be.InstituteEmail);
            operation.AddIntParam(DB_COL_ID_TYPE, be.AgreementType.IdAgreementType);

            return operation;
        }
    }
}
