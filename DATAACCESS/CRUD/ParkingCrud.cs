using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class ParkingCrud: CrudFactory
    {
        private ParkingMapper Mapper;

        public ParkingCrud()
        {
            Mapper = new ParkingMapper();
            Dao = SqlDao.GetInstance();
        }

        public void ChangeOccupiedSpaces(BaseEntity entity)
        {

            var parking = (Parking)entity;
            var sqlOperation = Mapper.AddOcuppiedSpaces(parking);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Create(BaseEntity entity)
        {
            var parking = (Parking)entity;
            var sqlOperation = Mapper.GetCreateStatement(parking);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var parking = (Parking)entity;
            var sqlOperation = Mapper.GetDeleteStatement(parking);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstParking = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstParking.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstParking;
        }

        public override void Update(BaseEntity entity)
        {
            var parking = (Parking)entity;
            var sqlOperation = Mapper.GetUpdateStatement(parking);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public ParkingBill GenerateParkingBill(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (Parking)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveParkingByTerminal<T>( BaseEntity entity)
        {
            var lstParking = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetParkingByTerminal(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstParking.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstParking;
        }
    }
}
