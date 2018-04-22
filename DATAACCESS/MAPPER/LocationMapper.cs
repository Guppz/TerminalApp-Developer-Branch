using DATAACCESS.DAO;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.MAPPER
{
    public class LocationMapper : EntityMapper, ISqlStaments, IObjectMapper
    {
        private const string IDLOCATION = "PKIdLocation";
        private const string NAME = "LocationName";
        private const string LATITUDE = "Latitude";
        private const string LONGITUDE = "Longitude";
        private const string IDROUTE = "FKIdRoute";

        public BaseEntity BuildObject(Dictionary<string, object> row)
        {
            var location = new Location
            {
                IdLocation = GetIntValue(row, IDLOCATION),
                Name = GetStringValue(row, NAME),
                Latitude = GetStringValue(row, LATITUDE),
                Longitude = GetStringValue(row, LONGITUDE)
            };

            return location;
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

        public SqlOperation GetRetrieveAllStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveAllLocation" };
            return operation;
        }

        public SqlOperation GetRetriveStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveLocationById" };

            var be = (Location)entity;
            operation.AddIntParam(IDLOCATION, be.IdLocation);

            return operation;
        }
        public SqlOperation GetRetriveByRouteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveLocationByRoute" };

            var be = (Route)entity;
            operation.AddIntParam(IDROUTE, be.IdRoute);

            return operation;
        }

        public SqlOperation GetRetriveLastStatement()
        {
            var operation = new SqlOperation { ProcedureName = "RetrieveLastLocation" };
            return operation;
        }

        public SqlOperation AddLocationToRoute(Location location, Route route)
        {
            var operation = new SqlOperation { ProcedureName = "AddLocationToRoute" };

            operation.AddIntParam(IDLOCATION, location.IdLocation);
            operation.AddIntParam(IDROUTE, route.IdRoute);

            return operation;
        }

        public SqlOperation GetCreateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "CreateLocation" };

            var C = (Location)entity;
            operation.AddVarcharParam(NAME, C.Name);
            operation.AddVarcharParam(LATITUDE, C.Latitude);
            operation.AddVarcharParam(LONGITUDE, C.Longitude);

            return operation;
        }

        public SqlOperation GetUpdateStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "UpdateLocation" };

            var be = (Location)entity;
            operation.AddIntParam(IDLOCATION, be.IdLocation);
            operation.AddVarcharParam(NAME, be.Name);
            operation.AddVarcharParam(LATITUDE, be.Latitude);
            operation.AddVarcharParam(LONGITUDE, be.Longitude);

            return operation;
        }

        public SqlOperation GetDeleteStatement(BaseEntity entity)
        {
            var operation = new SqlOperation { ProcedureName = "DeleteLocation" };

            var be = (Location)entity;
            operation.AddIntParam(IDLOCATION, be.IdLocation);

            return operation;
        }
    }
}
