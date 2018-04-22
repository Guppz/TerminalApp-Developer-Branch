using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class ViewMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string ID_VIEW = "PKIdView";
        private const string VIEW_NAME = "ViewName";
        private const string VIEW_GROUP = "ViewGroup";
        private const string ID_USER = "PKIdUser";
        private const string IDROLE = "PkIdRole";
        private const string VIEWPAGE = "ViewEspanol";


        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var select = new View
            {
                IdView = GetIntValue(row, ID_VIEW),
                ViewName = GetStringValue(row, VIEW_NAME),
                ViewGroup = GetStringValue(row, VIEW_GROUP),
                ViewPage = GetStringValue(row, VIEWPAGE)

            };
            return select;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var listarow = BuildObject(row);
                lstResults.Add(listarow);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetViewPerUser(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "GetViewByUser" };
            var user = (User)entity;

            operation.AddIntParam(ID_USER, user.IdUser);
            return operation;
        }
        public SqlOperation GetRolesByView(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RetrieveRolesByView" };

            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);

            return operation;
        }
        public SqlOperation DeleteRolesByView(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "DeleteRolxViewByViewId" };

            var r = (Role)entity;
            operation.AddIntParam(IDROLE, r.IdRole);

            return operation;
        }

    }
}