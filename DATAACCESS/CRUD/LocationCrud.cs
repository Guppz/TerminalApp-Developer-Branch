using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class LocationCrud : CrudFactory
    {
        private LocationMapper Mapper;

        public LocationCrud()
        {
            Mapper = new LocationMapper();
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
            var lstLocations = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstLocations.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstLocations;
        }
          public  List<T> RetrieveAllByRoute<T>(BaseEntity entity)
        {
            var lstLocations = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByRouteStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstLocations.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstLocations;
        }

        public override void Create(BaseEntity entity)
        {
            var location = (Location)entity;
            var sqlOperation = Mapper.GetCreateStatement(location);

             Dao.ExecuteProcedure(sqlOperation);
           
           

        }

        public Location CreateLocation(BaseEntity entity)
        {

            var location = (Location)entity;
            var sqlOperation = Mapper.GetCreateStatement(location);

            var lstResult = Dao.ExecuteQueryProcedure(sqlOperation);
            location.IdLocation = (int)lstResult[0]["PKIdLocation"];
            return location;
        }

        public void AddLocationToRoute(Location newLocation, Route route)
        {
            Dao.ExecuteProcedure(Mapper.AddLocationToRoute(newLocation, route));
        }


        public override void Update(BaseEntity entity)
        {
            var location = (Location)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(location));
        }

        public override void Delete(BaseEntity entity)
        {
            var location = (Location)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(location));
        }

        public T RetrieveLast<T>()
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveLastStatement());
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
