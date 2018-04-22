using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace COREAPI
{
    public class DriverManager
    {
        private DriverCrud CrudFactory;
        private CompanyCrud CompanyCrd;

        public DriverManager()
        {
            CrudFactory = new DriverCrud();
            CompanyCrd = new CompanyCrud();

        }

        public void Create(Driver driver)
        {
            try
            {
                var userExist = CrudFactory.UserExist<Driver>(driver);
                if (userExist != null)
                {
                    throw new BusinessException(6);
                }


                ValidateFields(driver);
                CrudFactory.Create(driver);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

        public void Update(Driver driver)
        {
            try
            {
                CrudFactory.Update(driver);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Driver> RetrieveAll()
        {
            var lst = CrudFactory.RetrieveAll<Driver>();

            foreach (Driver driver in lst)
            {
                BuildObjects(driver);

            }

            return lst;
        }

        public void UpdateActivacion(Driver driver)
        {
            try
            {
                driver.Status = ActivationValidarior(driver);
                CrudFactory.Activate(driver);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateDesactivation(Driver driver)
        {
            try
            {
                driver.Status = DesactiveValidarior(driver);
                CrudFactory.Desactive(driver);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Driver RetrieveById(Driver driver)
        {
            Driver d = null;
            try
            {
                d = CrudFactory.Retrieve<Driver>(driver);
                if (d == null)
                {
                    throw new BusinessException(39);

                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return d;

        }

        public void Delete(Driver driver)
        {
            throw new System.NotImplementedException();
        }

        public void AssignDriverToBus(Driver driver)
        {

        }

        public List<Driver> RetrieveDriversByCompany(Company company)
        {
            List<Driver> LstDrivers = null;

            try
            {
                LstDrivers = CrudFactory.RetrieveDriversByCompany<Driver>(company);
                GenerateList(LstDrivers);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstDrivers;
        }

        public void GenerateList(List<Driver> lstDrivers)
        {
            if (lstDrivers.Count > 0)
            {
                foreach (Driver driver in lstDrivers)
                {
                    BuildObjects(driver);
                }
            }
            else
            {
                throw new BusinessException(5);
            }
        }

        public void BuildObjects(Driver driver)
        {
            driver.Company = new CompanyCrud().Retrieve<Company>(driver.Company);
        }
        //Validators
        public void ValidateFields(Driver driver)
        {
            PropertyInfo[] props = driver.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(driver, null);

                if (valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }
        }

        public int ActivationValidarior(Driver driver)
        {
            if (driver.Status == 1)
            {
                throw new BusinessException(40);

            }
            return 1;
        }

        public int DesactiveValidarior(Driver driver)
        {
            if (driver.Status == 0)
            {
                throw new BusinessException(41);

            }
            return 1;
        }

    }
}
