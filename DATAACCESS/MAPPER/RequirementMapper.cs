using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class RequirementMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string PKIDBUS = "IdBus";
        private const string IDREQUIREMENT = "PKIdRequirement";
        private const string NAME = "RequirementName";
        private const string IDREQUIREMENTN = "IDREQUIREMENT";
        private const string Expiry = "Expiry";
        private const string STATUS = "STATUS";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Req = new Requirement
            {
                IdRequirement = GetIntValue(row, IDREQUIREMENT),
                Name = GetStringValue(row, NAME),
                Expiry = GetDateValue(row, Expiry)
            };

            return Req;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Bus = BuildObject(row);
                LstResults.Add(Bus);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            return null;
        }


        public SqlOperation GetCreateStatementForReqXBus(BaseEntity BusEntity, BaseEntity ReqEntity)
        {
            return null;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteBusReq" };
            var Bus = (Bus)entity;
            operation.AddIntParam(PKIDBUS, Bus.IdBus);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            return null;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            return null;
        }

        public SqlOperation GetRetriveStatementByBusxReq(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetriveRequirementByBus" };
            var C = (Bus)entity;
            Operation.AddIntParam(PKIDBUS, C.IdBus);
            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity, BaseEntity ReqEntity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateBusDateRequirements" };
            var C = (Bus)entity;
            var req = (Requirement)ReqEntity;
            Operation.AddIntParam(PKIDBUS, C.IdBus);
            Operation.AddIntParam(IDREQUIREMENTN, req.IdRequirement);
            Operation.AddDateParam(Expiry, req.Expiry);
            return Operation;
        }

        public SqlOperation GetUpdateExpiryStatement(BaseEntity entity, BaseEntity ReqEntity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateBusRequirement" };
            var C = (Bus)entity;
            var req = (Requirement)ReqEntity;
            Operation.AddIntParam(PKIDBUS, C.IdBus);
            Operation.AddIntParam(IDREQUIREMENTN, req.IdRequirement);
            Operation.AddIntParam(STATUS, 0);
            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }
}
