using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class AgreementCrud: CrudFactory
    {
        private AgreementMapper Mapper;

        public AgreementCrud()
        {
            Mapper = new AgreementMapper();
            Dao = SqlDao.GetInstance();
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
            var lstAgreements = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstAgreements.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstAgreements;
        }

        public override void Create(BaseEntity entity)
        {
            var agreement = (Agreement)entity;
            var sqlOperation = Mapper.GetCreateStatement(agreement);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var agreement = (Agreement)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(agreement));
        }

        public override void Delete(BaseEntity entity)
        {
            var agreement = (Agreement)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(agreement));
        }

        public T RetrieveByEmailAndType<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByEmailAndTypeStatement(entity));
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
