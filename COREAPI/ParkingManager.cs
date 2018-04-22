using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class ParkingManager
    {
        private ParkingCrud CrudFactory;
        private TerminalCrud TCrud;

        public ParkingManager()
        {
            CrudFactory = new ParkingCrud();
            TCrud = new TerminalCrud();
        }
        /// <summary>
        /// Creates a Parking lot
        /// </summary>
        /// <param name="parking"></param>
        public void Create(Parking parking)
        {
            try
            {
                var ParkingList = RetriveParkingsByTerminal(parking.Terminal);

                foreach (Parking park in ParkingList)
                {
                    if (park.ParkingType == parking.ParkingType)
                    {
                        throw new BusinessException(17);

                    }
                }

                CrudFactory.Create(parking);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }
        /// <summary>
        /// Deletes an parking 
        /// </summary>
        /// <param name="parking"></param>
        public void Delete(Parking parking)
        {
            CrudFactory.Delete(parking);
        }
        /// <summary>
        /// Retrives all parking
        /// </summary>
        /// <returns></returns>
        public List<Parking> RetrieveAll()
        {
            var lstParking = CrudFactory.RetrieveAll<Parking>();
            foreach (var park in lstParking)
            {
                park.Terminal = TCrud.Retrieve<Terminal>(park.Terminal);
                if (park.ParkingType == 1)
                {
                    park.ParkingTypeName = "Parqueo usuarios";
                }
                else
                {
                    park.ParkingTypeName = "Parqueo buses";
                }
            }
            return lstParking;
        }
        /// <summary>
        /// Updates  Parking
        /// </summary>
        /// <param name="parking"></param>
        public void Update(Parking parking)
        {
            try
            {
                bool exist = false;
                var ParkingList = CrudFactory.RetrieveParkingByTerminal<Parking>(parking.Terminal);
                foreach (Parking park in ParkingList)
                {
                    if (park.ParkingType == parking.ParkingType)
                    {
                        exist = true;
                    }
                }

                if (ParkingList.Count == 0 || !exist)
                {
                    throw new BusinessException(18);
                }

                CrudFactory.Update(parking);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Parking RetrieveById(int idParking)
        {
            throw new System.NotImplementedException();
        }
        /// <summary>
        /// Retrieve a parkin By terminal
        /// </summary>
        /// <param name="term"></param>
        /// <returns> the terminal´s Parking lot</returns>
        public List<Parking> RetriveParkingsByTerminal(Terminal term)
        {
            return CrudFactory.RetrieveParkingByTerminal<Parking>(term);
        }

        public ParkingBill GenerateParkingBill(int idReservation)
        {
            throw new System.NotImplementedException();
        }
    }
}
