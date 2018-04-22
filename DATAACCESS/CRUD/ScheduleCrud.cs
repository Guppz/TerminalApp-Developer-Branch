using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class ScheduleCrud: CrudFactory
    {
        private ScheduleMapper Mapper;

        public ScheduleCrud()
        {
            Mapper = new ScheduleMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetCreateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            var schedule = (Schedule)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(schedule));
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
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());

            return GenerateList<T>(LstResults);
        }

        public override void Update(BaseEntity entity)
        {
            var schedule = (Schedule)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(schedule));
        }

        public List<T> RetrieveSchedulesByCompany<T>(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public T RetrieveScheduleByDayHour<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetScheduleByDayHourStatement(entity));

            return BuildScheduleObject<T>(LstResult);
        }

        public T BuildScheduleObject<T>(List<Dictionary<string, object>> lstResults)
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

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstSchedules = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstSchedules.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstSchedules;
        }
    }
}
