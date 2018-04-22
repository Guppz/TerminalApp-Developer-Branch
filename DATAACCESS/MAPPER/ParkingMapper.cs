using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class ParkingMapper : EntityMapper,ISqlStaments, IObjectMapper
    {
        private const string IDPARKING = "PKIdParking";
        private const string TYPE = "ParkingType";
        private const string AVAILABLESPACES = "AvailableSpaces";
        private const string OCCUPIEDSPACES = "OccupiedSpaces";
        private const string SPACERENTALCOST = "SpaceRentalCost";
        private const string IDTERMINAL = "PKIdTerminal";
        private const string FKIDTERMINAL = "FKIdTerminal";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var parking = new Parking
            {
                IdParking = GetIntValue(row, IDPARKING),
                ParkingType = GetIntValue(row, TYPE),
                AvailableSpaces = GetIntValue(row, AVAILABLESPACES),
                OccupiedSpces = GetIntValue(row, OCCUPIEDSPACES),
                RentalCost = GetIntValue(row, SPACERENTALCOST),
                Terminal = new Terminal { IdTerminal = GetIntValue(row, FKIDTERMINAL) }
            };

            return parking;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var customer = BuildObject(row);
                lstResults.Add(customer);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateParking" };
            var c = (Parking)entity;


            operation.AddIntParam(TYPE, c.ParkingType);
            operation.AddIntParam(AVAILABLESPACES, c.AvailableSpaces);
            operation.AddIntParam(OCCUPIEDSPACES, c.OccupiedSpces);
            operation.AddIntParam(SPACERENTALCOST, c.RentalCost);
            operation.AddIntParam(FKIDTERMINAL, c.Terminal.IdTerminal);

            return operation;
        }

       

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllParking" };
         

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateParking" };
            var c = (Parking)entity;

            operation.AddIntParam(IDPARKING, c.IdParking);
            operation.AddIntParam(TYPE, c.ParkingType);
            operation.AddIntParam(AVAILABLESPACES, c.AvailableSpaces);
            operation.AddIntParam(OCCUPIEDSPACES, c.OccupiedSpces);
            operation.AddIntParam(SPACERENTALCOST, c.RentalCost);
            operation.AddIntParam(FKIDTERMINAL, c.Terminal.IdTerminal);

            return operation;
        }

        public SqlOperation GetParkingBillStatement(int idReserevation)
        {
            throw new System.NotImplementedException();
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteParking" };
            var c = (Parking)entity;

            operation.AddIntParam(IDPARKING, c.IdParking);
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveParkingById" };
            var c = (Parking)entity;

            operation.AddIntParam(IDPARKING, c.IdParking);
    

            return operation;
        }

        public SqlOperation GetParkingByTerminal(BaseEntity entity) {

            var operation = new SqlOperation { ProcedureName = "GetParkingsByTerminal" };
            var c = (Terminal)entity;
            operation.AddIntParam(IDTERMINAL, c.IdTerminal);
            return operation;
        }

        public SqlOperation AddOcuppiedSpaces(BaseEntity entity) {
            var operation = new SqlOperation { ProcedureName = "AddOrSubStracOcupiedSpaces" };
            var c = (Parking)entity;
            operation.AddIntParam(IDPARKING, c.IdParking);
            operation.AddIntParam(OCCUPIEDSPACES, c.OccupiedSpces);

            return operation;
        }
    }
}
