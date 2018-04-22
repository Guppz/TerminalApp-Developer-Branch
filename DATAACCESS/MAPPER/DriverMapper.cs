using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class DriverMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDENTIFICATION = "PKCardNumber";
        private const string NAME = "DriverName";
        private const string NAMEDR = "Name";
        private const string IDCOMPANY = "FKIdCompany";
        private const string CARDNUMBER = "CARDNUMBER";
        private const string IDCOMPANYCRT = "IDCOMPANY";
        private const string IDDRIVER = "IDDRIVER";
        private const string ACTIVE = "Active";



        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY)
            };

            var Driver = new Driver
            {
                CardNumber = GetIntValue(row, IDENTIFICATION),
                Name = GetStringValue(row, NAME),
                Company = Company,
                Status = GetIntValue(row, ACTIVE)
            };

            return Driver;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Driver = BuildObject(row);
                LstResults.Add(Driver);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateDriver" };
            var r = (Driver)entity;
            operation.AddIntParam(CARDNUMBER, r.CardNumber);
            operation.AddVarcharParam(NAMEDR, r.Name);
            operation.AddIntParam(IDCOMPANYCRT, r.Company.IdCompany);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveDriversList" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveDriverById" };

            var C = (Driver)entity;
            Operation.AddIntParam(IDENTIFICATION, C.CardNumber);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateDriver" };
            var r = (Driver)entity;
            operation.AddIntParam(CARDNUMBER, r.CardNumber);
            operation.AddVarcharParam(NAMEDR, r.Name);
            operation.AddIntParam(IDCOMPANYCRT, r.Company.IdCompany);
            return operation;
        }
        public SqlOperation ActivateStatus(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "ActivateDriver" };
            var r = (Driver)entity;
            operation.AddIntParam(IDDRIVER, r.CardNumber);

            return operation;
        }

        public SqlOperation DesactivateStatus(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DesactivateDriver" };
            var r = (Driver)entity;
            operation.AddIntParam(IDDRIVER, r.CardNumber);

            return operation;
        }
        public SqlOperation GetRetrieveDriversByCompanySatatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveDriversByCompany" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany );

            return Operation;
        }

        public SqlOperation GetUserExistStatement(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "UserDriverExist" };

            var c = (Driver)entity;
            operation.AddIntParam(IDENTIFICATION, c.CardNumber);


            return operation;
        }
    }
}
