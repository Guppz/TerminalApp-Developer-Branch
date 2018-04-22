using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class BusMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string PKIDBUS = "PKIdBus";
        private const string MODEL = "Model";
        private const string BRAND = "Brand";
        private const string YEAR = "ModelYear";
        private const string YEARBus = "Year";
        private const string PLATE = "Plate";
        private const string FKIDCOMPANY = "FKIdCompany";
        private const string FKIDDRIVER = "FKIdDriver";
        private const string REQUIREMENT = "IdRequirement";
        private const string IDBUS = "IdBus";
        private const string ACTIVE = "Active";
        private const string IDCOMPANY = "IdCompany";
        private const string IDDRIVER = "IdDriver";
        private const string EXPIRY = "Expiry";
        private const string SEATS = "Seats";
        private const string STANDING = "Standing";
        private const string CORPIDENTIFICATION = "Identification";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var Driver = new Driver
            {
                CardNumber = GetIntValue(row, FKIDDRIVER)
            };

            var Company = new Company
            {
                IdCompany = GetIntValue(row, FKIDCOMPANY)
            };

            var Bus = new Bus
            {
                IdBus = GetIntValue(row, PKIDBUS),
                Model = GetStringValue(row, MODEL),
                Brand = GetStringValue(row, BRAND),
                Year = GetIntValue(row, YEAR),
                Plate = GetStringValue(row, PLATE),
                Company = Company,
                Driver = Driver,
                Seats = GetIntValue(row, SEATS),
                Standing = GetIntValue(row, STANDING),
                Active = GetIntValue(row, ACTIVE),
            };

            return Bus;
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
            var operation = new SqlOperation { ProcedureName = "CreateBus" };
            var Bus = (Bus)entity;
            var Company = new Company();
            Company.IdCompany = Bus.Company.IdCompany;
            operation.AddVarcharParam(MODEL, Bus.Model);
            operation.AddVarcharParam(BRAND, Bus.Brand);
            operation.AddIntParam(YEARBus, Bus.Year);
            operation.AddVarcharParam(PLATE, Bus.Plate);
            operation.AddIntParam(IDCOMPANY, Company.IdCompany);
            operation.AddIntParam(SEATS, Bus.Seats);
            operation.AddIntParam(STANDING, Bus.Standing);
            return operation;
        }


        public SqlOperation GetCreateStatementForReqXBus(BaseEntity BusEntity , BaseEntity ReqEntity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateBusxReq" };
            var Bus = (Bus)BusEntity;
            var Requirement = (Requirement)ReqEntity;
            operation.AddIntParam(IDBUS, Bus.IdBus);
            operation.AddIntParam(REQUIREMENT, Requirement.IdRequirement);
            operation.AddDateParam(EXPIRY, Requirement.Expiry);
            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteBus" };
            var Bus = (Bus)entity;
            operation.AddIntParam(IDBUS, Bus.IdBus);
            return operation;
        }

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveBusesList" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveBusById" };

            var C = (Bus)entity;
            Operation.AddIntParam(PKIDBUS, C.IdBus);

            return Operation;
        }

        public SqlOperation GetRetriveStatementByPlate(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveBusByPlate" };
            var C = (Bus)entity;
            Operation.AddVarcharParam(PLATE, C.Plate);

            return Operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateBus" };
            var Bus = (Bus)entity;
            var Company = new Company();
            var Driver = new Driver();
            operation.AddIntParam(IDBUS,Bus.IdBus);
            operation.AddVarcharParam(MODEL, Bus.Model);
            operation.AddVarcharParam(BRAND, Bus.Brand);
            operation.AddIntParam(YEARBus, Bus.Year);
            operation.AddIntParam(SEATS, Bus.Seats);
            operation.AddIntParam(STANDING, Bus.Standing);
            return operation;
        }


        public SqlOperation GetUpdateBusStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateBus" };
            var Bus = (Bus)entity;
            var Company = new Company();
            var Driver = new Driver();
            Driver.CardNumber = Bus.Driver.CardNumber;
            Company.IdCompany = Bus.Company.IdCompany;
            operation.AddIntParam(IDBUS, Bus.IdBus);
            operation.AddVarcharParam(MODEL, Bus.Model);
            operation.AddVarcharParam(BRAND, Bus.Brand);
            operation.AddIntParam(YEARBus, Bus.Year);
            operation.AddVarcharParam(PLATE, Bus.Plate);
            operation.AddIntParam(IDCOMPANY, Company.IdCompany);
            operation.AddIntParam(IDDRIVER, Driver.CardNumber);
            operation.AddIntParam(SEATS, Bus.Seats);
            operation.AddIntParam(STANDING, Bus.Standing);
            return operation;
        }

        public SqlOperation GetUpdateDriverStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateBusDriver" };
            var Bus = (Bus)entity;
            var Driver = new Driver();
            Driver.CardNumber = Bus.Driver.CardNumber;
            operation.AddIntParam(IDDRIVER, Driver.CardNumber);
            operation.AddIntParam(IDBUS, Bus.IdBus);
            return operation;
        }
        public SqlOperation GetRetrieveBusesByCompanySatatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveBusesByCompany" };

            var C = (Company)entity;
            Operation.AddIntParam(FKIDCOMPANY, C.IdCompany);

            return Operation;
        }

        public SqlOperation GetRetrieveBusesByCompanyUserSatatement(BaseEntity entity)
        {
            var Operation = new SqlOperation { ProcedureName = "RetrieveBusesByCompanyUser" };
            var C = (Company)entity;
            Operation.AddVarcharParam(CORPIDENTIFICATION, C.CorpIdentification);
            return Operation;
        }
    }
}
