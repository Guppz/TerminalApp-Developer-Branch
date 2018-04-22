using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    class UserPasswordMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string PASSWORD = "Password";
        private const string DATEEXPIRY = "PasswordExpiration";
        private const string IDUSER = "PKIdUser";
        private const string MINUSDAYS = "MinusDays";
        private const string IDUSERPASSWORD = "PKIdUserPassword";
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var pass = new UserPassword
            {
                Password = GetStringValue(row, PASSWORD),
                DateExpiry = GetDateValue(row, DATEEXPIRY),
                IdPassword = GetIntValue(row, IDUSERPASSWORD)
            };

            return pass; 
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var customer = BuildObject(row);
                lstResults.Add(customer);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreatePassword" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            operation.AddVarcharParam(PASSWORD, c.Password.Password);
            operation.AddDateParam(DATEEXPIRY, c.Password.DateExpiry);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCurrentPassword" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            return operation;
        }
        public SqlOperation GetRetrievePaaswordsByUser(BaseEntity entity) {

            var operation = new SqlOperation { ProcedureName = "RetrievePasswordsByUser" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            return operation;
        }
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdatePassword" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            operation.AddVarcharParam(PASSWORD, c.Password.Password);
            operation.AddIntParam(IDUSERPASSWORD, c.Password.IdPassword);
            return operation;
        }

        public SqlOperation GetResetStatement(BaseEntity entity, int minusDays)
        {
            var operation = new SqlOperation { ProcedureName = "DateExpiryPasswordReset" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            operation.AddIntParam(IDUSERPASSWORD, c.Password.IdPassword);
            operation.AddIntParam(MINUSDAYS, minusDays);

            return operation;
        }
    }
}
