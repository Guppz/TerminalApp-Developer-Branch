using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace COREAPI
{
    public class SanctionManager
    {
        private SanctionCrud CrudFactory;

        public SanctionManager()
        {
            CrudFactory = new SanctionCrud();
        }

        public void Create(Sanction sanction)
        {
            try
            {
                ValidateFields(sanction);
                CrudFactory.Create(sanction);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void CreateSanctionType(SanctionType sanctionType)
        {
            try
            {
                ValidateFields(sanctionType);
                ValidateSanctionTypeCost(sanctionType);

                if (ValidateIsNotExistingType(sanctionType))
                {
                    CrudFactory.CreateSanctionType(sanctionType);
                }
                else
                {
                    throw new BusinessException(3);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Update(Sanction sanction)
        {
            try
            {
                ValidateFields(sanction);
                CrudFactory.Update(sanction);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void Delete(Sanction sanction)
        {
            CrudFactory.Delete(sanction);
        }

        public Sanction RetrieveById(Sanction sanction)
        {
            Sanction NewSanction = CrudFactory.Retrieve<Sanction>(sanction);
            ValidateObject(NewSanction);
            BuildObjects(NewSanction);

            return NewSanction;
        }

        public void ValidateObject(Sanction sanction)
        {
            try
            {
                if (sanction == null)
                {
                    throw new BusinessException(5);
                }
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public List<Sanction> RetrieveAll()
        {
            List<Sanction> LstSanctions = null;

            try
            {
                LstSanctions = CrudFactory.RetrieveAll<Sanction>();
                GenerateList(LstSanctions);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstSanctions;
        }

        public SanctionType RetrieveSanctionTypeById(SanctionType type)
        {
            return CrudFactory.RetrieveSanctionTypeById<SanctionType>(type);
        }

        public List<Sanction> RetrieveSanctionsByType(SanctionType type)
        {
            return CrudFactory.RetrieveSanctionsByType<Sanction>(type);
        }

        public SanctionType RetrieveSanctionTypeByName(SanctionType type)
        {
            return CrudFactory.RetrieveSanctionTypeByName<SanctionType>(type);
        }

        public SanctionType RetrieveSanctionTypeByDescription(SanctionType type)
        {
            return CrudFactory.RetrieveSanctionTypeByDescription<SanctionType>(type);
        }

        public List<SanctionType> RetrieveSanctionsTypes()
        {
            return CrudFactory.RetrieveSanctionsTypes<SanctionType>();
        }

        public void UpdateSanctionType(SanctionType type)
        {
            try
            {
                ValidateFields(type);
                ValidateSanctionTypeCost(type);
                CrudFactory.UpdateSanctionType(type);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }
        }

        public void DeleteSanctionType(SanctionType type)
        {
            CrudFactory.DeleteSanctionType(type);
        }

        public List<Sanction> RetrieveSanctionsByDateRange(DateTime beginDate, DateTime endDate)
        {
            List<Sanction> LstSanctions = null;

            try
            {
                LstSanctions = CrudFactory.RetrieveSanctionsByDateRange<Sanction>(beginDate, endDate);
                GenerateList(LstSanctions);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstSanctions;
        }

        public List<Sanction> RetrieveSanctionsByDateRangeAndCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            List<Sanction> LstSanctions = null;

            try
            {
                LstSanctions = CrudFactory.RetrieveSanctionsByDateRangeAndCompany<Sanction>(beginDate, endDate, idCompany);
                GenerateList(LstSanctions);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstSanctions;
        }

        public List<Sanction> RetrieveSanctionsByCompany(Company company)
        {
            List<Sanction> LstSanctions = null;

            try
            {
                LstSanctions = CrudFactory.RetrieveSanctionsByCompany<Sanction>(company);
                GenerateList(LstSanctions);
            }
            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);
            }

            return LstSanctions;
        }

        public void ValidateFields(BaseEntity entity)
        {
            PropertyInfo[] Props = entity.GetType().GetProperties();

            foreach (PropertyInfo p in Props)
            {
                object valor = p.GetValue(entity, null);

                if (valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }
        }

        public void ValidateSanctionTypeCost(SanctionType sanctionType)
        {
            if (sanctionType.Cost <= 0)
            {
                throw new BusinessException(2);
            }
        }

        public void ValidateDate(DateTime date)
        {
            if (date == default(DateTime))
            {
                throw new BusinessException(1);
            }

            if (date.Date < DateTime.Today)
            {
                throw new BusinessException(4);
            }
        }

        public bool ValidateIsNotExistingType(SanctionType sanctionType)
        {
            bool NotExists = true;

            SanctionType Name = CrudFactory.RetrieveSanctionTypeByName<SanctionType>(sanctionType);
            SanctionType Description = CrudFactory.RetrieveSanctionTypeByDescription<SanctionType>(sanctionType);


            if (Name != null || Description != null)
            {
                NotExists = false;
            }

            return NotExists;
        }

        public void GenerateList(List<Sanction> lstSanctions)
        {
            foreach (Sanction sanction in lstSanctions)
            {
                BuildObjects(sanction);
            }
        }

        public void BuildObjects(Sanction sanction)
        {
            sanction.Terminal = new TerminalCrud().Retrieve<Terminal>(sanction.Terminal);
            sanction.Route = new RouteCrud().Retrieve<Route>(sanction.Route);
            sanction.Type = CrudFactory.RetrieveSanctionTypeById<SanctionType>(sanction.Type);
            sanction.Company = new CompanyCrud().Retrieve<Company>(sanction.Company);
        }
    }
}
