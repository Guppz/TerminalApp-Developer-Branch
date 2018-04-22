using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class PaymentCrud: CrudFactory
    {
        private PaymentMapper Mapper;

        public PaymentCrud()
        {
            Mapper = new PaymentMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public Payment CreateParkingPayment(BaseEntity entity) {
            var pay = (Payment)entity;
            var sqlOperation = Mapper.GetCreateParkingPaymentStatement(pay);
            var lstResult = Dao.ExecuteQueryProcedure(sqlOperation);
            pay.IdPayment = (int)lstResult[0]["PKIdPayment"];
            return pay;
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

        public List<T> RetrieveUserPayments<T>(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
