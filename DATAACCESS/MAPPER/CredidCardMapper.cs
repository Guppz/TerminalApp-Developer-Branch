using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class CredidCardMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDCC = "IDCC";
        private const string CREDIDCARD = "CredidCard";
        private const string FKIDUSER = "FKIdUser";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {

            var User = new User {
                IdUser = GetIntValue(row,FKIDUSER)
            };
            
            var Credidcard = new CredidCard
            {
               IDCC = GetIntValue(row, IDCC),
               credidCardNum= GetStringValue(row , CREDIDCARD),
               User = User
            };
            return Credidcard;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var card = BuildObject(row);
                lstResults.Add(card);
            }

            return lstResults;
        }
    
        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateCredidCard" };
            var Card = (CredidCard)entity;
            operation.AddVarcharParam(CREDIDCARD, Card.credidCardNum);
            operation.AddIntParam(FKIDUSER, Card.User.IdUser);
            return operation;
        }

        public SqlOperation GetUserCardsStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetriveCredidCard" };
            var Card = (CredidCard)entity;
            User user = Card.User;
            operation.AddIntParam(FKIDUSER, user.IdUser);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new System.NotImplementedException();
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
