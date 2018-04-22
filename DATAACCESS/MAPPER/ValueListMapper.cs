using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    class ValueListMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDSELECT = "IdList";
        private const string VALUE = "Value";
        private const string DESCRPTION = "Description";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Select = new ValueListSelect
            {
                IdList = GetStringValue(row, IDSELECT),
                Value = GetStringValue(row, VALUE),
                Description = GetStringValue(row, DESCRPTION)
            };

            return Select;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var listarow = BuildObject(row);
                LstResults.Add(listarow);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateValueRow" };
            var c = (ValueListSelect)entity;


            operation.AddVarcharParam(IDSELECT, c.IdList);
            operation.AddVarcharParam(VALUE, c.Value);
            operation.AddVarcharParam(DESCRPTION, c.Description);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteValueList" };
            var c = (ValueListSelect)entity;


            operation.AddVarcharParam(IDSELECT, c.IdList);
            operation.AddVarcharParam(VALUE, c.Value);
            

            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            throw new NotImplementedException();
        }

       
        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateValueList" };
            var c = (ValueListSelect)entity;


            operation.AddVarcharParam(IDSELECT, c.IdList);
            operation.AddVarcharParam(VALUE, c.Value);
            operation.AddVarcharParam(DESCRPTION, c.Description);

            return operation;
        }
    

      
        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "GetValueList" };

            var valuesList = (ValueListSelect)entity;
            operation.AddVarcharParam(IDSELECT, valuesList.IdList);

            return operation;
        }

       
    }
}
