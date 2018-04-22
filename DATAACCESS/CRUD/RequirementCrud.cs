using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class RequirementCrud : CrudFactory
    {
        private RequirementMapper Mapper;

        public RequirementCrud()
        {
            Mapper = new RequirementMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {

        }

        public override void Delete(BaseEntity entity)
        {
            var Bus = (Bus)entity;
            var sqlOperation = Mapper.GetDeleteStatement(Bus);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            return null;
        }

        public  List<T> RetrieveAllREQ<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatementByBusxReq(entity));
            return GenerateList<T>(LstResults);
        }

        public override void Update(BaseEntity entity)
        {
        }
        public  void Update(BaseEntity entity, BaseEntity Reqentity)
        {
            //var Bus = (Bus)entity;
            //var sqlOperation = Mapper.GetUpdateStatement(Bus,Reqentity);
            //Dao.ExecuteProcedure(sqlOperation);
        }

        public void UpdateReq(BaseEntity entity, BaseEntity Reqentity)
        {
            //var Bus = (Bus)entity;
            //var sqlOperation = Mapper.GetUpdateExpiryStatement(Bus, Reqentity);
            //Dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (Bus)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
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
