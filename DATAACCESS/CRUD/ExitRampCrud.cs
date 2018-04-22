using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class ExitRampCrud : CrudFactory
    {
        private ExitRampMapper Mapper;

        public ExitRampCrud()
        {
            Mapper = new ExitRampMapper();
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
            var lstExitRamps = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstExitRamps.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstExitRamps;
        }

        public override void Create(BaseEntity entity)
        {
            var exitRamp = (ExitRamp)entity;
            var sqlOperation = Mapper.GetCreateStatement(exitRamp);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var exitRamp = (ExitRamp)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(exitRamp));
        }

        public override void Delete(BaseEntity entity)
        {
            var exitRamp = (ExitRamp)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(exitRamp));
        }

        public List<T> RetrieveByTerminalId<T>(Terminal terminal)
        {
            var lstExitRamps = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByTerminalIdStatement(terminal));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstExitRamps.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstExitRamps;
        }
    }
}
