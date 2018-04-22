using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class SystemParamCrud : CrudFactory
    {
        private SystemParamMapper Mapper;

        public SystemParamCrud()
        {
            Mapper = new SystemParamMapper();
            Dao = SqlDao.GetInstance();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstSystemParams = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstSystemParams.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstSystemParams;
        }

        public override void Create(BaseEntity entity)
        {
            var systemParam = (SystemParam)entity;
            var sqlOperation = Mapper.GetCreateStatement(systemParam);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var systemParam = (SystemParam)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(systemParam));
        }

        public override void Delete(BaseEntity entity)
        {
            var systemParam = (SystemParam)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(systemParam));
        }

        public  T GetPricePerKM<T>()
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetPricePerKMStatement());

            return BuildObject<T>(LstResult);
        }

        public T BuildObject<T>(List<Dictionary<string, object>> lstResults)
        {
            var Dic = new Dictionary<string, object>();

            if (lstResults.Count > 0)
            {
                Dic = lstResults[0];
                var Objs = Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveIssuerEmailInfo <T>()
        {
            var EmailInformation = new List<T>();

            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetIssuerEmailInfoStatement());

            if (LstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(LstResult);
                foreach (var c in objs)
                {
                    EmailInformation.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return EmailInformation;
        }
    }
}
