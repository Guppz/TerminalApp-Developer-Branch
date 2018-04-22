using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class DriverCrud : CrudFactory
    {
        private DriverMapper Mapper;

        public DriverCrud()
        {
            Mapper = new DriverMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = Mapper.GetCreateStatement(entity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (Driver)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCards.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstCards;
        }

        public override void Update(BaseEntity entity)
        {
            var sqlOperation = Mapper.GetUpdateStatement(entity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public void AssignDriverToBus(Driver driver)
        {

        }
        public void Activate(BaseEntity entity)
        {
            var driver = (Driver)entity;
            var sqlOperation = Mapper.ActivateStatus(driver);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public void Desactive(BaseEntity entity)
        {
            var driver = (Driver)entity;
            var sqlOperation = Mapper.DesactivateStatus(driver);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveDriversByCompany<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveDriversByCompanySatatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstDrivers = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstDrivers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstDrivers;
        }

        public T UserExist<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetUserExistStatement(entity));
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
