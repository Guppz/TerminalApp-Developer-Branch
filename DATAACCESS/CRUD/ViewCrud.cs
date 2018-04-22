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
    public class ViewCrud : CrudFactory
    {
        private ViewMapper Mapper;

        public ViewCrud()
        {
            Mapper = new ViewMapper();
            Dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
        public List<T> RetrieveViewsByUser<T>(BaseEntity entity)
        {
            var lstCategoria = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetViewPerUser(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCategoria.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCategoria;
        }

        public List<T> RetrieveRolesByView<T>(BaseEntity entity)
        {
            var lstRoles = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRolesByView(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstRoles.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstRoles;
        }
        public void DelteRolexView(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.DeleteRolesByView(role);

            Dao.ExecuteProcedure(sqlOperation);
        }
    }
}

