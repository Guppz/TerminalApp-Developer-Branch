using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class CardTypeMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDCARDTYPE = "PKIdCardType";
        private const string CARDTYPENAME = "CardTypeName";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var cardType = new CardType
            {
                IdCardType = GetIntValue(row, IDCARDTYPE),
                CardName = GetStringValue(row, CARDTYPENAME),
            };
            return cardType;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var cardType = BuildObject(row);
                lstResults.Add(cardType);
            }

            return lstResults;
        }


        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateCardType" };
            var cardType = (CardType)entity;
            operation.AddVarcharParam(IDCARDTYPE, cardType.CardName);
            return operation;
        }


        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardsType" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateCardType" };
            var cardType = (CardType)entity;
            operation.AddIntParam(IDCARDTYPE, cardType.IdCardType);
            operation.AddVarcharParam(CARDTYPENAME, cardType.CardName);
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardsTypeByName" };
            var cardType = (CardType)entity;
            operation.AddVarcharParam(CARDTYPENAME, cardType.CardName);
            return operation;
        }

        public SqlOperation GetRetriveStatementById(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardsTypeById" };
            var cardType = (CardType)entity;
            operation.AddIntParam(IDCARDTYPE, cardType.IdCardType);
            return operation;
        }


        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteCardType" };
            var cardType = (CardType)entity;
            operation.AddIntParam(CARDTYPENAME, cardType.IdCardType);
            return operation;
        }
    }
}
