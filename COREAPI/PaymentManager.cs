using DATAACCESS.CRUD;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace COREAPI
{
    public class PaymentManager
    {
        private PaymentCrud CrudFactory;

        public PaymentManager()
        {
            CrudFactory = new PaymentCrud();
        }

        public void Create(Payment Payment)
        {

        }

        public void Update(Payment Payment)
        {

        }

        public Payment RetrieveById(int idPayment)
        {
            throw new System.NotImplementedException();
        }

        public List<Payment> RetrieveAll()
        {
            throw new System.NotImplementedException();
        }

        public void Delete(Payment Payment)
        {

        }

        public List<Payment> RetrieveUserPayments(int idUser)
        {
            throw new System.NotImplementedException();
        }
    }
}
