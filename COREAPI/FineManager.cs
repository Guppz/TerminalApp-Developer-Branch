using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;

namespace COREAPI
{
    public class FineManager
    {
        private FineCrud CrudFactory;
        private CompanyCrud CompanyCrd;
        private TerminalCrud TerminalCrd;
        private ValueListCrud VLCrud;

        public FineManager()
        {
            CrudFactory = new FineCrud();
            CompanyCrd = new CompanyCrud();
            TerminalCrd = new TerminalCrud();
            VLCrud = new ValueListCrud();

        }

        public void Create(Fine fine)
        {
            try
            {
                CrudFactory.Create(fine);
                VerifyFinesLimit(fine.Company);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void CreateFineType(FineType fineType)
        {
            try
            {
                if (!String.IsNullOrEmpty(fineType.TypeName))
                {

                    ValidateFineTypeFields(fineType);

                    var fineT = CrudFactory.CreateFineType(fineType);

                    var valueList = new ValueListSelect
                    {
                        IdList = "FineType",
                        Value = fineT.IdType.ToString(),
                        Description = fineT.TypeDescription

                    };
                    VLCrud.Create(valueList);
                }
                else
                {

                    throw new BusinessException(53);

                }
            }

            catch (Exception bex)
            {
                ExceptionManager.GetInstance().Process(bex);

            }
        }

        public void Update(Fine fine)
        {
            try
            {
                CrudFactory.Update(fine);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Fine fine)
        {
            try
            {
                CrudFactory.Delete(fine);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

        public void DeleteFineType(FineType fineType)
        {
            try
            {
                CrudFactory.DeleteFineType(fineType);

                var valueList = new ValueListSelect
                {
                    IdList = "FineType",
                    Value = fineType.IdType.ToString(),
                    Description = fineType.TypeName

                };
                VLCrud.Delete(valueList);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

        }

        public Fine RetrieveById(Fine fine)
        {
            Fine f = null;
            try
            {
                f = CrudFactory.Retrieve<Fine>(fine);
                if (f == null)
                {
                    throw new BusinessException(4);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return f;
        }

        public List<Fine> RetrieveAll()
        {
            var Finelst = CrudFactory.RetrieveAll<Fine>();
            foreach (Fine fine in Finelst)
            {
                BuildObjects(fine);

            }

            return Finelst;
        }

        public void UpdateFinesSettings(FineType fineType)
        {
            try
            {
                CrudFactory.UpdateFinesSettings(fineType);
                var valueList = new ValueListSelect
                {
                    IdList = "FineType",
                    Value = fineType.IdType.ToString(),
                    Description = fineType.TypeName

                };
                VLCrud.Update(valueList);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void VerifyFinesLimit(Company company)
        {
            var IsLimit = CrudFactory.VerifyFinesLimit(company);

            if (IsLimit == 1)
            {
                throw new BusinessException(65);
            }
        }

        public List<FineType> RetrieveFinesTypes()
        {
            return CrudFactory.RetrieveFinesTypes<FineType>();
        }

        public List<Fine> RetrieveFinesByDateRange(DateTime beginDate, DateTime endDate)
        {
            throw new System.NotImplementedException();
        }

        public List<Fine> RetrieveFinesByDateRangeAndCompany(DateTime beginDate, DateTime endDate, int idCompany)
        {
            throw new System.NotImplementedException();
        }

        public List<Fine> RetrieveFinesByCompany(int idCompany)
        {
            throw new System.NotImplementedException();
        }
        //VALIDATIONS

        public void ValidateFineFields(Fine fine)
        {
            PropertyInfo[] props = fine.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(fine, null);

                if (valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
                else
                {
                    if (valor.GetType() == typeof(DateTime))
                    {
                        ValidateDate(fine.FineDate);
                    }
                }
            }
        }

        public void ValidateFineTypeFields(FineType fineType)
        {
            PropertyInfo[] props = fineType.GetType().GetProperties();

            foreach (PropertyInfo p in props)
            {
                object valor = p.GetValue(fineType, null);

                if (valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }

            if (fineType.Cost < 0)
            {
                throw new BusinessException(2);
            }
        }

        public void ValidateDate(DateTime date)
        {
            if (date.Date < DateTime.Today)
            {
                throw new BusinessException(4);
            }
        }

        public void ValidateIsNotExistingType(FineType fineType)
        {
            FineType type = CrudFactory.RetrieveFineTypeByName<FineType>(fineType);

            if (type != null)
            {
                throw new BusinessException(3);
            }
        }

        public void BuildObjects(Fine fine)
        {
            fine.Company = CompanyCrd.Retrieve<Company>(fine.Company);
            fine.Terminal = TerminalCrd.Retrieve<Terminal>(fine.Terminal);
            fine.FType = CrudFactory.RetrieveFinesById<FineType>(fine.FType);
        }
    }
}


