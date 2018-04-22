using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class TravelCrud: CrudFactory
    {
        private TravelMapper Mapper;

        public TravelCrud()
        {
            Mapper = new TravelMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetCreateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(entity));
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());

            return GenerateList<T>(LstResults);
        }

        public override void Update(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(entity));
        }

        public List<T> RetrieveTravlesByRoute<T>(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstSanctions = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstSanctions.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstSanctions;
        }

        public List<T> RetrieveTravelByRoute<T>(Route route)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveTravelByRouteStatement(route));

            return GenerateList<T>(LstResults);
        }
    }
}
