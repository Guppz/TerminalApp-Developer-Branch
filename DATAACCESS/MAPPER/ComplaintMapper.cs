
using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    public class ComplaintMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDCOMPLAINT = "PKIdComplaint";
        private const string DESCRIPTION = "ComplaintDescription";
        private const string DATE = "ComplaintDate";
        private const string IDUSER = "FKIdUser";
        private const string IDBUS = "FKIdBus";
        private const string IDDRIVER = "FKIdDriver";
        private const string IDTERMINAL = "FKIdTerminal";
        private const string IDPARAM = "PKIdParam";
        private const string PARAMVALUE = "ParamValue";
        private const string PARAMNAME = "ParamName";
        private const string DATEINIT = "DateInit";
        private const string DATEEND = "DateEnd";
        private const string IDCOMPANY = "FKIdCompany";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var User = new User
            {
                IdUser = GetIntValue(row, IDUSER)
            };

            var Bus = new Bus
            {
                IdBus = GetIntValue(row, IDBUS)
            };

            var Terminal = new Terminal
            {
                IdTerminal = GetIntValue(row, IDTERMINAL)
            };

            var Driver = new Driver
            {
                CardNumber = GetIntValue(row, IDDRIVER)
            };

            var Company = new Company
            {
                IdCompany = GetIntValue(row, IDCOMPANY)
            };

            var Complaint = new Complaint
            {
                IdComplaint = GetIntValue(row, IDCOMPLAINT),
                Date = GetStringValue(row, DATE),
                Description = GetStringValue(row, DESCRIPTION),
                User = User,
                Terminal = Terminal,
                Driver = Driver,
                Bus = Bus,
                Company = Company
            };

            return Complaint;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var LstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var Complaint = BuildObject(row);
                LstResults.Add(Complaint);
            }

            return LstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "CreateComplaint" };

            var C = (Complaint)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddIntParam(IDBUS, C.Bus.IdBus);
            Operation.AddIntParam(IDTERMINAL, C.Terminal.IdTerminal);
            Operation.AddIntParam(IDDRIVER, C.Driver.CardNumber);
            Operation.AddIntParam(IDUSER, C.User.IdUser);
            Operation.AddDateParam(DATE, DateTime.Now);
            Operation.AddIntParam(IDCOMPANY, C.Company.IdCompany);

            return Operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "DeleteComplaint" };

            var C = (Complaint)entity;
            Operation.AddIntParam(IDCOMPLAINT, C.IdComplaint);

            return Operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveAllComplaint" };

            return Operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintById" };

            var C = (Complaint)entity;
            Operation.AddIntParam(IDCOMPLAINT, C.IdComplaint);

            return Operation;
        }

        public SqlOperation VerifyComplaintsLimit(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "VerifyCompanyComplaintsLimit" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateComplaint" };

            var C = (Complaint)entity;
            Operation.AddVarcharParam(DESCRIPTION, C.Description);
            Operation.AddIntParam(IDBUS, C.Bus.IdBus);
            Operation.AddIntParam(IDTERMINAL, C.Terminal.IdTerminal);
            Operation.AddIntParam(IDDRIVER, C.Driver.CardNumber);
            Operation.AddIntParam(IDUSER, C.User.IdUser);
            Operation.AddDateParam(DATE, DateTime.Now);
            Operation.AddIntParam(IDCOMPLAINT, C.IdComplaint);
            Operation.AddIntParam(IDCOMPANY, C.Company.IdCompany);

            return Operation;
        }

        public SqlOperation GetComplaintsByTerminalStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintsByTerminalId" };

            var C = (Terminal)entity;
            Operation.AddIntParam(IDTERMINAL, C.IdTerminal);

            return Operation;
        }

        public SqlOperation GetComplaintsByDateRange(DateTime beginDate, DateTime endDate)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintsByDateRange" };

            Operation.AddDateParam(DATEINIT, beginDate);
            Operation.AddDateParam(DATEEND, endDate);

            return Operation;
        }

        public SqlOperation GetComplaintsByDateRangeAndTerminal(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintsByDateRangeAndTerminalId" };

            Operation.AddDateParam(DATEINIT, beginDate);
            Operation.AddDateParam(DATEEND, endDate);
            Operation.AddIntParam(IDTERMINAL, idTerminal);

            return Operation;
        }

        public SqlOperation GetComplaintsByCompanyStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintsByCompanyId" };

            var C = (Company)entity;
            Operation.AddIntParam(IDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetComplaintsByDateRangeAndCompany(int idCompany, DateTime beginDate, DateTime endDate)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveComplaintsByDateRangeAndCompanyId" };

            Operation.AddDateParam(DATEINIT, beginDate);
            Operation.AddDateParam(DATEEND, endDate);
            Operation.AddIntParam(IDCOMPANY, idCompany);

            return Operation;
        }

        public SqlOperation GetUpdateSettingsStatement(double number, int idParam, string name)
        {
            var Operation = new SqlOperation { ProcedureName = "UpdateSystemParam" };

            Operation.AddIntParam(IDPARAM, idParam);
            Operation.AddDoubleParam(PARAMVALUE, number);
            Operation.AddVarcharParam(PARAMNAME, name);

            return Operation;
        }
    }
}
