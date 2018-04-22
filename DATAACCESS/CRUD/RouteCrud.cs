using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class RouteCrud: CrudFactory
    {
        private RouteMapper Mapper;

        public RouteCrud()
        {
            Mapper = new RouteMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetCreateStatement(entity));
        }

        public Route CreateRoute(BaseEntity entity)
        {
            Route Route = new Route();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetCreateStatement(entity));
            Route.IdRoute = (int)lstResult[0]["PKIdRoute"];

            return Route;
        }

        public override void Delete(BaseEntity entity)
        {
            var Route = (Route)entity;
            var sqlOperation = Mapper.GetDeleteStatement(Route);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstRoutes = new List<T>();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());

            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoutes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoutes;
        }
        public  List<T> RetrieveAllByCompany<T>(BaseEntity entity)
        {
            var lstRoutes = new List<T>();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByCompanyStatement(entity));

            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoutes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoutes;
        }
        public  List<T> RetrieveAllByTerminal<T>(BaseEntity entity)
        {
            var lstRoutes = new List<T>();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByTerminalStatement(entity));

            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoutes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoutes;
        }

        public override void Update(BaseEntity entity)
        {
            var Route = (Route)entity;
            var sqlOperation = Mapper.GetUpdateStatement(Route);
            Dao.ExecuteProcedure(sqlOperation);
        }
        public  void DeleteLocation(BaseEntity entity)
        {
            var Route = (Route)entity;
            var sqlOperation = Mapper.GetDeleteLocationsStatement(Route);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public void DeleteLocationByRoute(BaseEntity entity)
        {
            var Route = (Route)entity;
            var sqlOperation = Mapper.GetDeleteLocationsStatement(Route);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public string ShowBusCurrentLocation()
        {
            throw new System.NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public T RetrieveByName<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByNameStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public List<T> RetrieveRoutesByCompany<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByCompanyStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstRoutes = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstRoutes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstRoutes;
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
    }
}
