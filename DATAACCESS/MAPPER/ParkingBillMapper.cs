using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.DAO;
using ENTITIES_POJO;

namespace DATAACCESS.MAPPER
{
    class ParkingBillMapper : EntityMapper, ISqlStaments, IObjectMapper
    {

        private const string DB_COL_PKIDBILL = "PKIdBill";
        private const string DB_COL_BEGINDATE = "BeginDateTime";
        private const string DB_COL_ENDDATE = "EndDateTime";
        private const string DB_COL_AMOUNTHOURS = "AmountHours";
        private const string DB_COL_TOTALCOST = "TotalCost";
        private const string DB_COL_FKIDPARKING = "FKIdParking";
        private const string DB_COL_FKIDCARD = "FKIdCard";
        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var parkingBill = new ParkingBill
            {

                IdParkingBill = GetIntValue(row, DB_COL_PKIDBILL),
                BeginDate = GetDateValue(row, DB_COL_BEGINDATE),
                EndDate = GetDateValue(row, DB_COL_ENDDATE),
                AmountHours = GetIntValue(row, DB_COL_AMOUNTHOURS),
                TotalCost = GetIntValue(row, DB_COL_TOTALCOST),
                ParkedParking = new Parking { IdParking = GetIntValue(row, DB_COL_FKIDPARKING) },
                ParkingCard =  new Card {IdCard = GetStringValue(row,DB_COL_FKIDCARD)}
            };

            return parkingBill;
        }

        public List<BaseEntity> BuildObjects(List<Dictionary<string, object>> lstRows)
        {
            var lstResults = new List<BaseEntity>();

            foreach (var row in lstRows)
            {
                var location = BuildObject(row);
                lstResults.Add(location);
            }

            return lstResults;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateParkingBill" };

            var be = (ParkingBill)entity;
            operation.AddDateParam(DB_COL_BEGINDATE, be.BeginDate);
            operation.AddIntParam(DB_COL_FKIDPARKING, be.ParkedParking.IdParking);
            operation.AddVarcharParam(DB_COL_FKIDCARD, be.ParkingCard.IdCard);

            return operation;
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
            var operation = new SqlOperation { ProcedureName = "UpdateParkingBill" };

            var be = (ParkingBill)entity;
            operation.AddIntParam(DB_COL_PKIDBILL, be.IdParkingBill);
            operation.AddDateParam(DB_COL_ENDDATE, be.EndDate);
            operation.AddIntParam(DB_COL_AMOUNTHOURS, be.AmountHours);
            operation.AddIntParam(DB_COL_TOTALCOST, be.TotalCost);

            return operation;
        }

        public SqlOperation GetRetriveByCardStatement(BaseEntity entity)
        {

            var operation = new SqlOperation { ProcedureName = "RetriveParkingBillByCard" };
            var c = (Card)entity;
            operation.AddVarcharParam(DB_COL_FKIDCARD, c.IdCard);

            return operation;
        }
    }
}
