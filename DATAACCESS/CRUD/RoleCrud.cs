using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class RoleCrud : CrudFactory
    {
        private RoleMapper Mapper;

        public RoleCrud()
        {
            Mapper = new RoleMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = Mapper.GetCreateStatement(entity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public Role CreateRole(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.GetCreateStatement(role);

            var lstResult = Dao.ExecuteQueryProcedure(sqlOperation);
            role.IdRole = (int)lstResult[0]["PkIdRole"];
            return role;
        }

        public override void Delete(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.GetDeleteStatement(role);
            Dao.ExecuteProcedure(sqlOperation);
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
            var lstRoles = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
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

        public override void Update(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.GetUpdateStatement(role);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public void ActivateRole(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.ActivateStatus(role);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public void DesactiveRole(BaseEntity entity)
        {
            var role = (Role)entity;
            var sqlOperation = Mapper.DesactivateStatus(role);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveRolesByUser<T>(BaseEntity entity)
        {
            var lstRoles = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRolesByUser(entity));
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

        public void CreateRolexUser(BaseEntity entity, BaseEntity entity2)
        {

            var sqlOperation = Mapper.CreateRolesPerUser(entity, entity2);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public void CreateRolexView(BaseEntity entity, BaseEntity entity2)
        {

            var sqlOperation = Mapper.CreateRolesPerView(entity, entity2);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public void DelteRolexUser(BaseEntity entity)
        {
            var user = (User)entity;
            var sqlOperation = Mapper.DeleteRolesByUser(user);

            Dao.ExecuteProcedure(sqlOperation);
        }
        public T RoleExist<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRoleExistStatement(entity));
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

