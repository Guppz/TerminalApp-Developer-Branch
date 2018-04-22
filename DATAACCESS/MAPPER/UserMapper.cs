using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class UserMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDUSER = "PKIdUser";
        private const string NAME = "UserName";
        private const string LASTNAME = "LastName";
        private const string EMAIL = "Email";
        private const string PASSWORD = "Password";
        private const string IDENTIFICATION = "Identification";
        private const string FKIDTERMINAL = "FKIdTerminal";
        private const string BIRTHDATE = "BirthDate";
        private const string STATUS = "Active";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {


            var user = new User
            {
                IdUser = GetIntValue(row, IDUSER),
                Name = GetStringValue(row, NAME),
                LastName = GetStringValue(row, LASTNAME),
                Email = GetStringValue(row, EMAIL),
                Identification = GetStringValue(row, IDENTIFICATION),
                BirthDate = GetDateValue(row, BIRTHDATE),
                UserTerminal = new Terminal { IdTerminal = GetIntValue(row, FKIDTERMINAL) },
                Status = GetIntValue(row, STATUS)
                

            };
            return user;
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
            var operation = new SqlOperation { ProcedureName = "CreateUser" };
            var c = (User)entity;
           
            
            operation.AddVarcharParam(NAME, c.Name);
            operation.AddVarcharParam(LASTNAME, c.LastName);
            operation.AddVarcharParam(EMAIL, c.Email);
            operation.AddVarcharParam(IDENTIFICATION, c.Identification);
            operation.AddDateParam(BIRTHDATE, c.BirthDate);
            operation.AddIntParam(FKIDTERMINAL, c.UserTerminal.IdTerminal);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllUser" };
            return operation;
        }

        public SqlOperation GetRetrieveByTerminal(BaseEntity entity)
        {
            var c = (User)entity;
            var operation = new SqlOperation { ProcedureName = "RetrieveUserByTerminal" };
            operation.AddIntParam(FKIDTERMINAL, c.UserTerminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateUser" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            operation.AddVarcharParam(NAME, c.Name);
            operation.AddVarcharParam(LASTNAME, c.LastName);
            operation.AddVarcharParam(EMAIL, c.Email);
            operation.AddDateParam(BIRTHDATE, c.BirthDate);
            operation.AddVarcharParam(IDENTIFICATION, c.Identification);
            operation.AddIntParam(FKIDTERMINAL, c.UserTerminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteUser" };
            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            operation.AddIntParam(STATUS, c.Status);


            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveUserById" };
            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);
            return operation;
        }

        public SqlOperation GetRetriveStatementIdentification(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetriveUserByIdCard" };
            var c = (User)entity;
            operation.AddVarcharParam(IDENTIFICATION, c.Identification);
            return operation;
        }

        public SqlOperation GetUserExistStatement(BaseEntity entity) {

            var operation = new SqlOperation { ProcedureName = "UserExist" };

            var c = (User)entity;
            operation.AddVarcharParam(EMAIL, c.Email);
            operation.AddVarcharParam(IDENTIFICATION, c.Identification);

            return operation;
        }

      
    }
}
