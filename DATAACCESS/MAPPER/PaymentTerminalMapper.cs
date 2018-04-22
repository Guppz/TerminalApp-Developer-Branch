using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class PaymentTerminalMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string IDPAYMENT = "PKIdRegister";
        private const string PERCENTAGEUSED = "PercentageUsed";
        private const string AMOUT = "Amount";
        private const string FKIDPAYMENT = "FKIdPayment";
        private const string FKIDTERMINAL = "FKIdTerminal";
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
            var operation = new SqlOperation { ProcedureName = "CreateTerminalPayment" };
            var c = (PaymentTerminal)entity;


            operation.AddDoubleParam(PERCENTAGEUSED, c.PercentageUsed);
            operation.AddIntParam(AMOUT, c.Amount);
            operation.AddIntParam(FKIDPAYMENT, c.PaymentGot.IdPayment);
            operation.AddIntParam(FKIDTERMINAL, c.TerminalPayed.IdTerminal);


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
    }
}
