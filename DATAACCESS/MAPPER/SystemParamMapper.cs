using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class SystemParamMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDPARAM = "PKIdParam";
        private const string PARAMNAME = "ParamName";
        private const string PARAMVALUE = "ParamValue";
        private const string IDPARAMTYPE = "FKIdParamType";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var ParamType = new SystemParamType
            {
                IdParamType = GetIntValue(row,IDPARAMTYPE)
            };

            var Param = new SystemParam
            {
                IdSystemParam = GetIntValue(row, IDPARAM),
                Name = GetStringValue(row, PARAMNAME),
                Value = GetStringValue(row, PARAMVALUE),
                ParamType = ParamType
            };

            return Param;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Param = BuildObject(row);
                LstResults.Add(Param);
            }

            return LstResults;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveAllSystemParam" };
            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveSystemParamById" };

            var C = (SystemParam)entity;
            Operation.AddIntParam(IDPARAM, C.IdSystemParam);

            return Operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateSystemParam" };

            var C = (SystemParam)entity;
            Operation.AddVarcharParam(PARAMNAME, C.Name);
            Operation.AddVarcharParam(PARAMVALUE, C.Value);
            Operation.AddIntParam(IDPARAMTYPE, C.ParamType.IdParamType);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateSystemParam" };

            var C = (SystemParam)entity;
            Operation.AddIntParam(IDPARAM, C.IdSystemParam);
            Operation.AddVarcharParam(PARAMNAME, C.Name);
            Operation.AddVarcharParam(PARAMVALUE, C.Value);
            Operation.AddIntParam(IDPARAMTYPE, C.ParamType.IdParamType);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteSystemParam" };

            var C = (SystemParam)entity;
            Operation.AddIntParam(IDPARAM, C.IdSystemParam);

            return Operation;
        }

        public SqlOperation GetProfitsStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetRegistConventionStatement(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetIssuerEmailInfoStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveIssuerEmailInfo" };

            return Operation;
        }

        public SqlOperation GetPricePerKMStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrievePricePerKM" };

            return Operation;
        }     
    }
}
