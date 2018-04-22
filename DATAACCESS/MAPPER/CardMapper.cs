using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class CardMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDCARD = "PKGIN";
        private const string BALANCE = "Balance";
        private const string DAYSFORNOTIFICATION = "DaysForNotification";
        private const string STATUS = "Status";
        private const string DATEEXPIRY = "DateExpiry";
        private const string IDTETMINAL = "IdTerminal"; 
        private const string IDUSER = "IdUser";
        private const string IDCARDTYPE = "IdCardType";
        private const string NOTIFICATION = "Notification";
        private const string FKIDCARDTYPE = "FKIdCardType";
        private const string FKIDUSER = "FKIdUser";
        private const string FKIDTETMINAL = "FKIdTerminal";
        private const string FKIAGREEMENT = "FKIdAgreement";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Agreement = new Agreement
            {
                IdAgreement = GetIntValue(row, FKIAGREEMENT)
            };
            var User = new User {
                IdUser = GetIntValue(row,FKIDUSER)
            };
            var Terminal = new Terminal
            {
                IdTerminal = GetIntValue(row,FKIDTETMINAL)
            };

            CardType CardType = new CardType
            {
                IdCardType = GetIntValue(row,FKIDCARDTYPE)
            };       
            var card = new Card
            {
               IdCard = GetStringValue(row, IDCARD),
               Balance = GetDoubleValue(row, BALANCE),
               DaysForNotification = GetIntValue(row, DAYSFORNOTIFICATION),
               Status = GetIntValue(row, STATUS),
               ExpiryDate =GetDateValue(row, DATEEXPIRY),
               Notification = GetDateValue(row, NOTIFICATION),
               Terminal = Terminal,
               User = User,
               CrType = CardType
            };
            return card;
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
            var operation = new SqlOperation { ProcedureName = "CreateCard" };
            var Card = (Card)entity;
            operation.AddDoubleParam(BALANCE, Card.Balance);
            operation.AddIntParam(DAYSFORNOTIFICATION, Card.DaysForNotification);
            operation.AddDateParam(NOTIFICATION, Card.Notification);
            operation.AddDateParam(DATEEXPIRY, Card.ExpiryDate);
            operation.AddIntParam(IDTETMINAL, Card.Terminal.IdTerminal);
            operation.AddIntParam(IDUSER, Card.User.IdUser);
            operation.AddIntParam(IDCARDTYPE, Card.CrType.IdCardType);
            return operation;
        }

        public SqlOperation GetCreateStatementAgreement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateCardAgreement" };
            var Card = (Card)entity;
            operation.AddDoubleParam(BALANCE, Card.Balance);
            operation.AddIntParam(DAYSFORNOTIFICATION, Card.DaysForNotification);
            operation.AddDateParam(NOTIFICATION, Card.Notification);
            operation.AddDateParam(DATEEXPIRY, Card.ExpiryDate);
            operation.AddIntParam(IDTETMINAL, Card.Terminal.IdTerminal);
            operation.AddIntParam(IDUSER, Card.User.IdUser);
            operation.AddIntParam(IDCARDTYPE, Card.CrType.IdCardType);
            operation.AddIntParam(FKIAGREEMENT, Card.agreement.IdAgreement);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardsList" };
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateCard" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            operation.AddDoubleParam(BALANCE, Card.Balance);
            operation.AddIntParam(DAYSFORNOTIFICATION, Card.DaysForNotification);
            operation.AddDateParam(NOTIFICATION, Card.Notification);
            operation.AddIntParam(STATUS, Card.Status);
            return operation;
        }

        public SqlOperation GetUpdateNotiStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateCardNotification" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            operation.AddIntParam(DAYSFORNOTIFICATION, Card.DaysForNotification);
            operation.AddDateParam(NOTIFICATION, Card.Notification);
            return operation;
        }

        public SqlOperation GetUpdateBalanceStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "UpdateCardBalance" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            operation.AddDoubleParam(BALANCE, Card.Balance);
            return operation;

        }

        public SqlOperation GetUpdateSatutsStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateStatusCard" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            return operation;
        }

        public SqlOperation GetUserCarsStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveUserCards" };
            var c = (Card)entity;
            User user = c.User;
            operation.AddIntParam(IDUSER, user.IdUser);
            return operation;
        }

        public SqlOperation GetUserCardsByTerminalStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveUserCardsByTerminal" };
            var c = (Card)entity;
            User user = c.User;
            Terminal terminal = c.Terminal;
            operation.AddIntParam(IDUSER, user.IdUser);
            operation.AddIntParam(IDTETMINAL, terminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetStudiantStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveStudiantCard" };
            var Card = (Card)entity;
            Terminal terminal = Card.Terminal;
            operation.AddIntParam(IDTETMINAL, terminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetStudiantCardDisabledStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "ReturnStudiantsCardDisabled" };
            var Card = (Card)entity;
            Terminal terminal = Card.Terminal;
            operation.AddIntParam(IDTETMINAL, terminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteCard" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardById" };
            var Card = (Card)entity;
            operation.AddVarcharParam(IDCARD, Card.IdCard);
            return operation; ;
        }

        public SqlOperation GetCardsByTerminalStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardsByTerminal" };
            var Card = (Card)entity;
            Terminal terminal = Card.Terminal;
            operation.AddIntParam(IDTETMINAL, terminal.IdTerminal);
            return operation;
        }

        public SqlOperation GetReIssueCardStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveCardById" };
            var Card = (Card)entity;

            operation.AddDoubleParam(BALANCE, Card.Balance);
            operation.AddIntParam(DAYSFORNOTIFICATION, Card.DaysForNotification);
            operation.AddDateParam(NOTIFICATION, Card.Notification);
            operation.AddDateParam(DATEEXPIRY, Card.ExpiryDate);
            operation.AddIntParam(IDTETMINAL, Card.Terminal.IdTerminal);
            operation.AddIntParam(IDUSER, Card.User.IdUser);
            operation.AddIntParam(IDCARDTYPE, Card.CrType.IdCardType);

            return operation;
        }
    }
}
