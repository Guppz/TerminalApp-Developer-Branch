using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class CompanyMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDCOMPANY = "PKIdCompany";
        private const string NAME = "CompanyName";
        private const string CORPID = "CorpIdentification";
        private const string OWNER = "OwnerName";
        private const string EMAIL = "Email";
        private const string IDTERMINAL = "FKIdTerminal";
        private const string FKIDUSER = "FKIDUSER";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY),
                Name = GetStringValue(row, NAME),
                CorpIdentification = GetStringValue(row, CORPID),
                OwnerName = GetStringValue(row,OWNER),
                Email = GetStringValue(row,EMAIL)
            };

            return Company;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Sanction = BuildObject(row);
                LstResults.Add(Sanction);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateCompany" };

            var C = (Company)entity;
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddVarcharParam(CORPID, C.CorpIdentification);
            Operation.AddVarcharParam(OWNER, C.OwnerName);
            Operation.AddVarcharParam(EMAIL, C.Email);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteCompany" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompaniesList" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyById" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetCompaniesByTerminalStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompaniesByTerminal" };

            var C = (Terminal)entity;
            Operation.AddIntParam(IDTERMINAL, C.IdTerminal);

            return Operation;
        }

        public SqlOperation GetRetriveByNameStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyByName" };

            var C = (Company)entity;
            Operation.AddVarcharParam(NAME, C.Name);

            return Operation;
        }

        public SqlOperation GetRetriveByEmailStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyByEmail" };

            var C = (Company)entity;
            Operation.AddVarcharParam(EMAIL, C.Email);

            return Operation;
        }

        public SqlOperation GetRetriveByCorpIdStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyByCorpId" };

            var C = (Company)entity;
            Operation.AddVarcharParam(CORPID,C.CorpIdentification);

            return Operation;
        }

        public SqlOperation GetRetriveByInfoStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyByInfo" };

            var C = (Company)entity;
            Operation.AddVarcharParam(CORPID, C.CorpIdentification);
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddVarcharParam(EMAIL, C.Email);

            return Operation;
        }

        public SqlOperation GetRetrieveByTerminalIdStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveCompanyByTerminalId" };

            var C = (Company)entity;
            Operation.AddIntParam(IDTERMINAL, C.TerminalsList[0].IdTerminal);
            Operation.AddVarcharParam(CORPID, C.CorpIdentification);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateCompany" };

            var C = (Company)entity;
            Operation.AddVarcharParam(NAME, C.Name);
            Operation.AddVarcharParam(CORPID, C.CorpIdentification);
            Operation.AddVarcharParam(OWNER, C.OwnerName);
            Operation.AddVarcharParam(EMAIL, C.Email);
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetAddCompanyToTerminalStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "AddCompanyToTerminal" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);
            Operation.AddIntParam(IDTERMINAL, C.TerminalsList[0].IdTerminal);

            return Operation;
        }

        public SqlOperation GetCompanyByUser(BaseEntity entity)
        {

            var Operation = new SqlOperation { ProcedureName = "GetUserCompany" };

            var C = (User)entity;
            Operation.AddIntParam(FKIDUSER, C.IdUser);
           

            return Operation;
        }
    }
}
