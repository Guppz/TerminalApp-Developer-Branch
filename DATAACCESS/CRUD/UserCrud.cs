using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class UserCrud: CrudFactory
    {
        private UserMapper Mapper;

        public UserCrud()
        {
            Mapper = new UserMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetCreateStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public User CreateUser(BaseEntity entity) {
            var user = (User)entity;
            var sqlOperation = Mapper.GetCreateStatement(user);
            var lstResult  = Dao.ExecuteQueryProcedure(sqlOperation);
            user.IdUser = (int)lstResult[0]["PKIdUser"];
            return user;
        }


        public override void Delete(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetDeleteStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (User)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }


        public  T RetrieveIdentification<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatementIdentification(entity));

            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (User)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstUsers = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsers;
        }
        public List<T> RetrieveAllByTerminal<T>(BaseEntity entity)
        {
            var lstUsers = new List<T>();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByTerminal(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstUsers.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstUsers;
        }

        public override void Update(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetUpdateStatement(user);

            Dao.ExecuteProcedure(sqlOperation);


        }

        public T UserExist<T>(BaseEntity entity) {
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
