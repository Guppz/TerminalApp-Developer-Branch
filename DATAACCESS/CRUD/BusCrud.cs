using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class BusCrud : CrudFactory
    {
        private BusMapper Mapper;

        public BusCrud()
        {
            Mapper = new BusMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var Bus = (Bus)entity;
            var sqlOperation = Mapper.GetCreateStatement(Bus);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public  void CreateBusXReq(BaseEntity BusEntity, BaseEntity ReqEntity)
        {

            var sqlOperation = Mapper.GetCreateStatementForReqXBus(BusEntity,ReqEntity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var Bus = (Bus)entity;
            var sqlOperation = Mapper.GetDeleteStatement(Bus);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            return GenerateList<T>(LstResults);
            
        }

        public override void Update(BaseEntity entity)
        {
            var Bus = (Bus)entity;
            var sqlOperation = Mapper.GetUpdateStatement(Bus);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public  void UpdateDriver(BaseEntity entity)
        {
            var Bus = (Bus)entity;
            var sqlOperation = Mapper.GetUpdateDriverStatement(Bus);
            Dao.ExecuteProcedure(sqlOperation);
        }
        //Cambiar mapper
        public List<T> RetrieveTravelsByBus<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));
            return GenerateList<T>(LstResults);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }


        public  T RetrieveByPlate<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatementByPlate(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (Bus)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveBusesByCompany<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveBusesByCompanySatatement(entity));
            return GenerateList<T>(LstResults);
        }


        public List<T> RetrieveBusesByCompanyUser<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveBusesByCompanyUserSatatement(entity));
            return GenerateList<T>(LstResults);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstBuses = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstBuses.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstBuses;
        }
    }
}
