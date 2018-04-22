using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class LocationManager
    {
        private LocationCrud CrudFactory;
        private ValueListCrud VLCrud;

        public LocationManager()
        {
            CrudFactory = new LocationCrud();
            VLCrud = new ValueListCrud();
        }

        public List<Location> RetrieveAll()
        {
            List<Location> lt = CrudFactory.RetrieveAll<Location>();
            return lt;
        }

        public Location RetrieveById(Location location)
        {
            Location be = null;

            try
            {
                be = CrudFactory.Retrieve<Location>(location);
                if (be == null)
                {
                    // Location Not Found.
                    throw new BusinessException(23);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public void Create(Location location)
        {
            try
            {
                if (!String.IsNullOrEmpty(location.Name)) {
                    var loc = CrudFactory.CreateLocation(location);
                    var valueList = new ValueListSelect
                    {
                        IdList = "Location",
                        Value = loc.IdLocation.ToString(),
                        Description = loc.Name
                    };
                    VLCrud.Create(valueList);

                }
                    
                else
                {
                    throw new BusinessException(24);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Location location)
        {
            Location be = null;
            try
            {
                be = CrudFactory.Retrieve<Location>(location);
                if (be != null)
                {
                    if (!String.IsNullOrEmpty(location.Name))
                    { 

                        CrudFactory.Update(location);
                        var valueList = new ValueListSelect
                        {
                            IdList = "Location",
                            Value = location.IdLocation.ToString(),
                            Description = location.Name
                        };
                        VLCrud.Update(valueList);
                    }
                    else
                    {
                        // Location Name is required.
                        throw new BusinessException(24);
                    }
                }
                else
                {
                    // Location Not Found.
                    throw new BusinessException(23);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Location location)
        {
            CrudFactory.Delete(location);
            var valueList = new ValueListSelect
            {
                IdList = "Location",
                Value = location.IdLocation.ToString(),
                Description = location.Name
            };
            VLCrud.Delete(valueList);

        }

        public Location RetrieveLast()
        {
            Location be = null;

            try
            {
                be = CrudFactory.RetrieveLast<Location>();
                if (be == null)
                {
                    // There are no Locations registered
                    throw new BusinessException(25);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }
    }
}
