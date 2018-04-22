using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class FineMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDFINE = "PKIdFine";
        private const string DESCRIPTION = "FineDescription";
        private const string DATE = "FineDate";
        private const string IDCOMPANY = "FKIdCompany";
        private const string IDTERMINAL = "FKIdTerminal";
        private const string IDFINETYPE = "FKIdFineType";
        private const string IDFTYPE = "PKIdFineType";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY)
            };
            var Terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, IDTERMINAL)
            };
            var FineType = new FineType
            {
                IdType = GetIntValue(row, IDFINETYPE)
            };
            var Fine = new Fine
            {
                IdFine = GetIntValue(row, IDFINE),
                FineDescription = GetStringValue(row, DESCRIPTION),
                FineDate = GetDateValue(row, DATE),
                Company = Company,
                Terminal = Terminal,
                FType = FineType
            };
            return Fine;
        }
       
        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();
            foreach (var row in lstRows)
            {
                var fine = BuildObject(row);
                lstResults.Add(fine);
            }
            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateFine" };
            var c = (Fine)entity;
            operation.AddVarcharParam(DESCRIPTION, c.FineDescription);
            operation.AddDateParam(DATE, DateTime.Now);
            operation.AddIntParam(IDCOMPANY, c.Company.IdCompany);
            operation.AddIntParam(IDTERMINAL, c.Terminal.IdTerminal);
            operation.AddIntParam(IDFINETYPE, c.FType.IdType);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteFine" };
            var c = (Fine)entity;
            operation.AddIntParam(IDFINE, c.IdFine);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFinesList" };
            return operation;
        }

        public SqlOperation RetrieveAllFineComboBox()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFineValuesType" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFineById" };
            var c = (Fine)entity;
            operation.AddIntParam(IDFINE, c.IdFine);
            return operation; ;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateFine" };
            var c = (Fine)entity;
            operation.AddIntParam(IDFINE, c.IdFine);
            operation.AddVarcharParam(DESCRIPTION, c.FineDescription);
            operation.AddDateParam(DATE, DateTime.Now);
            operation.AddIntParam(IDCOMPANY, c.Company.IdCompany);
            operation.AddIntParam(IDTERMINAL, c.Terminal.IdTerminal);
            operation.AddIntParam(IDFINETYPE, c.FType.IdType);
            return operation;
        }

        public SqlOperation GetCreateFineTypeStatement(BaseEntity entity)
        {
            throw new NotImplementedException();

        }

        public SqlOperation GetUpdateFinesSettingsStatement(int number)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetAllFinesTypesStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveFineTypeById" };

            var f = (FineType)entity;
            operation.AddIntParam(IDFINETYPE, f.IdType);

            return operation;
        }

        public SqlOperation GetFinesByDateRangeStatement(DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SqlOperation VerifyFinesLimit(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "VerifyCompanyFinesLimit" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetFinesByDateRangeAndCompanyStatement(int ideTerminal, DateTime beginDate, DateTime endDate)
        {
            throw new NotImplementedException();
        }

        public SqlOperation GetFinesByCompanyStatement(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
