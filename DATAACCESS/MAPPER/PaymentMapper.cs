using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class PaymentMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDPAYMENT= "PKIdPayment";
        private const string DATE= "PaymentDate";
        private const string AMOUT= "Amount";
        private const string DETAIL= "Detail";
        private const string FKIDTERMINAL = "FKIdTerm";
        private const string FKIdIssuerUser = "FKIdIssueUser";
        private const string FKGIN = "FKGIN";
        private const string FKTERM = "FKIdTerm";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            throw new NotImplementedException();
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
        public SqlOperation GetCreateParkingPaymentStatement(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "CreateParkingPayment" };
            var c = (Payment)entity;
            operation.AddDateParam(DATE, c.Date);
            operation.AddIntParam(AMOUT, c.Amount);
            operation.AddVarcharParam(DETAIL, c.Detail);
            operation.AddIntParam(FKIdIssuerUser,c.IssuerUser.IdUser);
            operation.AddVarcharParam(FKGIN, c.Card.IdCard);
            operation.AddIntParam(FKTERM, c.PaymentType.PkIdTerm);

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
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUserPaymentsStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
