using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
namespace DATAACCESS.CRUD
{
    public class UserPasswordCrud : CrudFactory
    {
        private UserPasswordMapper Mapper;

        public UserPasswordCrud()
        {
            Mapper = new UserPasswordMapper();
            Dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetCreateStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
        public  List<T> RetrieveAllPasswordsByUser<T>(BaseEntity entity)
        {
            var lstPassword = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrievePaaswordsByUser(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstPassword.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstPassword;
        }
        public override void Update(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetUpdateStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public  void ResetPasswords(BaseEntity entity, int minusDays)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.GetResetStatement(user, minusDays);

            Dao.ExecuteProcedure(sqlOperation);
        }

    }
}
