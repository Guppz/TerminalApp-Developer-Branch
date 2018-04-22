using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class AgreementTypeManager
    {
        private AgreementTypeCrud CrudFactory;

        public AgreementTypeManager()
        {
            CrudFactory = new AgreementTypeCrud();
        }

        public List<AgreementType> RetrieveAll()
        {
            List<AgreementType> lt = CrudFactory.RetrieveAll<AgreementType>();
            return lt;
        }

        public AgreementType RetrieveById(AgreementType agreementType)
        {
            AgreementType be = null;

            try
            {
                be = CrudFactory.Retrieve<AgreementType>(agreementType);
                if (be == null)
                {
                    // Agreement Type Not Found.
                    throw new BusinessException(57);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public void Create(AgreementType agreementType)
        {
            try
            {
                if (!String.IsNullOrEmpty(agreementType.AgreementTypeName))
                    CrudFactory.Create(agreementType);
                else
                {
                    // Agreement Type Name is required.
                    throw new BusinessException(58);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(AgreementType agreementType)
        {
            AgreementType be = null;
            try
            {
                be = CrudFactory.Retrieve<AgreementType>(agreementType);
                if (be != null)
                {
                    if (!String.IsNullOrEmpty(agreementType.AgreementTypeName))
                        CrudFactory.Update(agreementType);
                    else
                    {
                        // Agreement Type Name is required.
                        throw new BusinessException(58);
                    }
                }
                else
                {
                    // Agreement Type Not Found.
                    throw new BusinessException(57);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(AgreementType agreementType)
        {
            CrudFactory.Delete(agreementType);
        }
    }
}
