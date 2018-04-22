using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class AgreementManager
    {
        private AgreementCrud CrudFactory;
        private AgreementTypeManager AgreementTypeMng;
        private TerminalManager TerminalMng;

        public AgreementManager()
        {
            CrudFactory = new AgreementCrud();
            AgreementTypeMng = new AgreementTypeManager();
            TerminalMng = new TerminalManager();
        }

        public List<Agreement> RetrieveAll()
        {
            List<Agreement> lt = CrudFactory.RetrieveAll<Agreement>();
            foreach (Agreement a in lt)
            {
                BuildAggregatedObjects(a);
            }
            return lt;
        }

        public Agreement RetrieveById(Agreement agreement)
        {
            Agreement be = null;

            try
            {
                be = CrudFactory.Retrieve<Agreement>(agreement);
                if (be != null)
                {
                    BuildAggregatedObjects(be);
                }
                else
                {
                    // Agreement Not Found.
                    throw new BusinessException(59);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return be;
        }

        public void Create(Agreement agreement)
        {
            try
            {
                var be = CrudFactory.RetrieveByEmailAndType<Agreement>(agreement);
                if (be == null)
                {
                    if (!String.IsNullOrEmpty(agreement.InstituteName) && !String.IsNullOrEmpty(agreement.InstituteEmail))
                    {
                        agreement.Terminal = TerminalMng.RetrieveById(agreement.Terminal);
                        agreement.AgreementType = AgreementTypeMng.RetrieveById(agreement.AgreementType);
                        CrudFactory.Create(agreement);
                    }
                    else
                    {
                        // Both Institute Name and Institute Email are required.
                        throw new BusinessException(60);
                    }
                } else
                {
                    // Email already has an agreement of that type
                    throw new BusinessException(66);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(Agreement agreement)
        {
            Agreement be = null;
            try
            {
                be = CrudFactory.Retrieve<Agreement>(agreement);
                if (be != null)
                {
                    if (!String.IsNullOrEmpty(agreement.InstituteName) && !String.IsNullOrEmpty(agreement.InstituteEmail))
                    {
                        agreement.Terminal = TerminalMng.RetrieveById(agreement.Terminal);
                        agreement.AgreementType = AgreementTypeMng.RetrieveById(agreement.AgreementType);
                        CrudFactory.Update(agreement);
                    }
                    else
                    {
                        // Both Institute Name and Institute Email are required.
                        throw new BusinessException(60);
                    }
                }
                else
                {
                    // Agreement Not Found.
                    throw new BusinessException(59);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Agreement agreement)
        {
            CrudFactory.Delete(agreement);
        }

        private Agreement BuildAggregatedObjects(Agreement pAgreement)
        {
            pAgreement.Terminal = TerminalMng.RetrieveById(pAgreement.Terminal);
            pAgreement.AgreementType = AgreementTypeMng.RetrieveById(pAgreement.AgreementType);

            return pAgreement;
        }

        public Agreement RetrieveByEmailAndType(Agreement agreement)
        {
            Agreement be = null;

            try
            {
                be = CrudFactory.RetrieveByEmailAndType<Agreement>(agreement);
                if (be != null)
                {
                    BuildAggregatedObjects(be);
                }
                else
                {
                    // Agreement Not Found.
                    throw new BusinessException(59);
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
