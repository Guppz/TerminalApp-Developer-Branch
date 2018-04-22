using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ENTITIES_POJO;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
namespace DATAACCESS.CRUD
{
    public class ParkingBillCrud : CrudFactory
    {
        private ParkingBillMapper Mapper;

        public ParkingBillCrud()
        {
            Mapper = new ParkingBillMapper();
            Dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var parkingBill = (ParkingBill)entity;
            var sqlOperation = Mapper.GetCreateStatement(parkingBill);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = Mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            var card = (ParkingBill)entity;
            var sqlOperation = Mapper.GetUpdateStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public T RetrieveParkingBillByCard<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByCardStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = Mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }
    }
}
