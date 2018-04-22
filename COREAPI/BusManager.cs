using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class BusManager
    {
        private BusCrud CrudFactory;
        private TravelCrud TravelCrud;
        private CompanyCrud CompanyCrud;
        public DriverCrud DriverCrud;
        public RequirementCrud ReqCrud;

        public BusManager()
        {
            TravelCrud = new TravelCrud();
            CrudFactory = new BusCrud();
            CompanyCrud = new CompanyCrud();
            DriverCrud = new DriverCrud();
            ReqCrud = new RequirementCrud();
        }

        public void Create(Bus bus)
        {
            var crudBus = new BusCrud();
            try
            {
                validateBlankSpace(bus);
                validatePlate(bus);
                validateRequirements(bus);
                crudBus.Create(bus);
                Bus newBus = crudBus.RetrieveByPlate<Bus>(bus);
                foreach (Requirement requirement in bus.RequirementsPerBus)
                {
                    crudBus.CreateBusXReq(newBus, requirement);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Bus bus)
        {
            var crudBus = new BusCrud();
            try
            {
                crudBus.Delete(bus);
                ReqCrud.Delete(bus);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Bus> RetrieveAll()
        {
            List<Bus> ListBus = CrudFactory.RetrieveAll<Bus>();
            foreach (Bus Bus in ListBus)
            {
                Bus.Company = new CompanyCrud().Retrieve<Company>(Bus.Company);
                Bus.Driver = new DriverCrud().Retrieve<Driver>(Bus.Driver);
                Bus.RequirementsPerBus = new RequirementCrud().RetrieveAllREQ<Requirement>(Bus);
                CheckRequirements(Bus);
                ActiveString(Bus);
            }
            return ListBus;
        }

        public void Update(Bus bus)
        {
            var reqCrud = new RequirementCrud();
            var crudBus = new BusCrud();
            try
            {
                validateBlankSpace(bus);
                validateRequirements(bus);
                crudBus.Update(bus);
                List<Requirement> LisReq = bus.RequirementsPerBus;
                foreach (Requirement r in LisReq)
                {
                    reqCrud.Update(bus, r);
                }


            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateDriver(Bus bus)
        {
            var crudBus = new BusCrud();
            try
            {
                crudBus.UpdateDriver(bus);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Bus RetrieveById(Bus bus)
        {
            List<Bus> ListBus = new List<Bus>();
            try
            {
                bus = CrudFactory.Retrieve<Bus>(bus);
                ListBus.Add(bus);
                foreach (Bus Bus in ListBus)
                {
                    Bus.Company = new CompanyCrud().Retrieve<Company>(Bus.Company);
                    Bus.Driver = new DriverCrud().Retrieve<Driver>(Bus.Driver);
                    Bus.RequirementsPerBus = new RequirementCrud().RetrieveAllREQ<Requirement>(Bus);
                }

            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return ListBus[0];
        }
        //Travel
        public List<Travel> RetrieveTravelsByBus(Bus bus)
        {
            List<Bus> ListBus = CrudFactory.RetrieveTravelsByBus<Bus>(bus);
            return null;
        }

        public void Delete(Travel travel)
        {
            throw new System.NotImplementedException();
        }

        public List<Bus> RetrieveBusesByCompany(Company company)
        {
            List<Bus> LstBuses = null;

            try
            {
                LstBuses = CrudFactory.RetrieveBusesByCompany<Bus>(company);
                foreach (Bus Bus in LstBuses)
                {
                    Bus.Company = new CompanyCrud().Retrieve<Company>(Bus.Company);
                    Bus.Driver = new DriverCrud().Retrieve<Driver>(Bus.Driver);
                    Bus.RequirementsPerBus = new RequirementCrud().RetrieveAllREQ<Requirement>(Bus);
                    CheckRequirements(Bus);
                    ActiveString(Bus);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstBuses;
        }

        public List<Bus> RetrieveBusesByCompanyUser(Company company)
        {
            List<Bus> LstBuses = null;

            try
            {
                LstBuses = CrudFactory.RetrieveBusesByCompanyUser<Bus>(company);
                foreach (Bus Bus in LstBuses)
                {
                    Bus.Company = new CompanyCrud().Retrieve<Company>(Bus.Company);
                    Bus.Driver = new DriverCrud().Retrieve<Driver>(Bus.Driver);
                    Bus.RequirementsPerBus = new RequirementCrud().RetrieveAllREQ<Requirement>(Bus);
                    CheckRequirements(Bus);
                    ActiveString(Bus);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstBuses;
        }

        public void BuildObjects(Bus bus)
        {
            bus.Company = new CompanyCrud().Retrieve<Company>(bus.Company);
            bus.Driver = new DriverCrud().Retrieve<Driver>(bus.Driver);
        }

        public void validateBlankSpace(Bus bus)
        {
            if (bus.Model == "")
            {
                throw new BusinessException(1);
            }
            if (bus.Plate == "")
            {
                throw new BusinessException(1);
            }
            if (bus.Year < 2000)
            {
                throw new BusinessException(42);
            }
            if (bus.Brand == "")
            {
                throw new BusinessException(1);
            }
        }

        public void validateRequirements(Bus bus)
        {
            List<Requirement> ListRequirements = bus.RequirementsPerBus;
            DateTime Year = DateTime.Now;

            if (ListRequirements.Count > 0)
            {
                if (ListRequirements[0].Expiry.Year <= Year.Year)
                {
                    throw new BusinessException(43);
                }
                if (ListRequirements[1].Expiry.Year <= Year.Year)
                {
                    throw new BusinessException(44);
                }
                if (ListRequirements[2].Expiry <= Year)
                {
                    throw new BusinessException(45);
                }
            }
        }

        public void validatePlate(Bus bus)
        {
            var CrudBus = new BusCrud();
            Bus oldBus = CrudBus.RetrieveByPlate<Bus>(bus);

            if (oldBus != null)
            {
                throw new BusinessException(46);
            }
        }

        public void CheckRequirements(Bus bus)
        {
            List<Requirement> ListRequirements = bus.RequirementsPerBus;
            DateTime Year = DateTime.Now;
            if (ListRequirements.Count > 0)
            {
                if (ListRequirements[0].Expiry.Year <= Year.Year)
                {
                    ReqCrud.UpdateReq(bus, ListRequirements[0]);
                }
                if (ListRequirements[1].Expiry <= Year)
                {
                    ReqCrud.UpdateReq(bus, ListRequirements[1]);
                }
                if (ListRequirements[2].Expiry <= Year)
                {
                    ReqCrud.UpdateReq(bus, ListRequirements[2]);
                }
            }

        }
        public void ActiveString(Bus bus)
        {
            if (bus.Active == 1)
            {
                bus.ActiveString = "Activo";
            }
            if (bus.Active == 2)
            {
                bus.ActiveString = "Requerimiento desactualizado";
            }
        }
    }
}
