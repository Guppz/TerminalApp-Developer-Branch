using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class PaymentTerminalCrud : CrudFactory
    {

        private PaymentTerminalMapper Mapper;

        public PaymentTerminalCrud()
        {
            Mapper = new PaymentTerminalMapper();
            Dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var paymentTerminal = (PaymentTerminal)entity;
            var sqlOperation = Mapper.GetCreateStatement(paymentTerminal);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
