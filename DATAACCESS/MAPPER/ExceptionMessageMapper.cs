using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class ExceptionMessageMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDMESSAGE = "PKIdMessage";
        private const string MESSAGE = "ExceptionMessage";
        private const string HOUR = "Hour";
        private const string DATE = "Date";
        private const string STACKTRACE = "StackTrace";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var appMessage = new AppMessage
            {
                Id = GetIntValue(row, IDMESSAGE),
                Message = GetStringValue(row, MESSAGE)
            };

            return appMessage;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var appMessage = BuildObject(row);
                lstResults.Add(appMessage);
            }

            return lstResults;
        }

        public SqlOperation GetRegistException(Exception bex)
        {
            var operation = new SqlOperation { ProcedureName = "RegisterException" };

            operation.AddVarcharParam(HOUR, DateTime.Now.ToString("HH:mm:ss"));
            operation.AddDateParam(DATE, DateTime.Now);
            operation.AddVarcharParam(MESSAGE, bex.Message);
            operation.AddVarcharParam(STACKTRACE, bex.StackTrace);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveExcepcionMessages" };

            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
