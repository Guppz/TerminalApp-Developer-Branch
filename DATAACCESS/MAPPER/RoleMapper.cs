using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class RoleMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDROLE = "PkIdRole";
        private const string NAME = "RoleName";
        private const string IDUSER = "PKIdUser";
        private const string IDVIEW = "PKIdView";
        private const string ID_VIEW = "PKIdView";
        private const string ACTIVE = "Active";
        private const string IDROLEV = "IdRole";



        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var role = new Role
            {
                IdRole = GetIntValue(row, IDROLE),
                Name = GetStringValue(row, NAME),
                Status = GetIntValue(row, ACTIVE)


            };

            return role;

        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
            foreach (var row in lstRows)
            {
                var role = BuildObject(row);
                lstResults.Add(role);
            }
            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateRole" };
            var r = (Role)entity;
            operation.AddVarcharParam(NAME, r.Name);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DesactivateRole" };
            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllRole" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveRoleById" };
            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);
            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateRole" };
            var r = (Role)entity;
            operation.AddIntParam(IDROLEV, r.IdRole);
            operation.AddVarcharParam(NAME, r.Name);
            return operation;
        }

        public SqlOperation ActivateStatus(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "ActivateRole" };
            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);

            return operation;
        }

        public SqlOperation DesactivateStatus(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DesactivateRole" };
            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);
       
            return operation;
        }

        public SqlOperation GetRolesByUser(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RetrieveRolesByUser" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);

            return operation;
        }
     

        public SqlOperation DeleteRolesByUser(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "DeleteRolxUserByUserId" };

            var c = (User)entity;
            operation.AddIntParam(IDUSER, c.IdUser);

            return operation;
        }

        public SqlOperation CreateRolesPerUser(BaseEntity entity, BaseEntity entity2)
        {
            var operation = new SqlOperation { ProcedureName = "CreateRolxUser" };

            var c = (Role)entity;
            var d = (User)entity2;
            operation.AddIntParam(IDROLE, c.IdRole);
            operation.AddIntParam(IDUSER, d.IdUser);
         


            return operation;
        }
        
        public SqlOperation CreateRolesPerView(BaseEntity entity, BaseEntity entity2)
        {
            var operation = new SqlOperation { ProcedureName = "CreateRolxView" };

            var d = (View)entity;
            var c = (Role)entity2;
            operation.AddIntParam(IDVIEW, d.IdView);
            operation.AddIntParam(IDROLE, c.IdRole);



            return operation;
        }

        public SqlOperation GetRoleExistStatement(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RoleExist" };

            var r = (Role)entity;
            operation.AddVarcharParam(NAME, r.Name);

            return operation;
        }

    }
}
