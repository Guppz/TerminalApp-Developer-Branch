using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text.RegularExpressions;

namespace COREAPI
{
    public class SystemParamManager
    {
        private SystemParamCrud CrudFactory;

        public SystemParamManager()
        {
            CrudFactory = new SystemParamCrud();
        }

        public List<SystemParam> RetrieveAll()
        {
            List<SystemParam> LstSystemParams = CrudFactory.RetrieveAll<SystemParam>();
            return LstSystemParams;
        }

        public SystemParam RetrieveById(SystemParam systemParam)
        {
            SystemParam SystemParam = null;

            try
            {
                SystemParam = CrudFactory.Retrieve<SystemParam>(systemParam);

                if (SystemParam == null)
                {
                    throw new BusinessException(26);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }

            return SystemParam;
        }

        public void Create(SystemParam systemParam)
        {
            try
            {
                ValidateFields(systemParam);
                ValidateParamType(systemParam);
                CrudFactory.Create(systemParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Update(SystemParam systemParam)
        {
            try
            {
                ValidateFields(systemParam);
                ValidateParamType(systemParam);
                CrudFactory.Update(systemParam);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(SystemParam systemParam)
        {
            CrudFactory.Delete(systemParam);
        }

        public void ValidateParamType(SystemParam systemParam)
        {
            switch (systemParam.ParamType.IdParamType)
            {
                case 1:
                    if (!Regex.IsMatch(systemParam.Value, @"^([1-9]\d*)(\,\d+)?$"))
                    {
                        throw new BusinessException(54);
                    }

                    break;

                case 2:
                    if (!Regex.IsMatch(systemParam.Value, @"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$"))
                    {
                        throw new BusinessException(55);
                    }

                    break;

                case 3:
                    if (systemParam.Value == "" || Regex.IsMatch(systemParam.Value, @"^([1-9]\d*)(\,\d+)?$") || Regex.IsMatch(systemParam.Value, @"^([1-9]\d*)(\.\d+)?$"))
                    {
                        throw new BusinessException(56);
                    }

                    break;

                default:
                    break;
            }
        }

        public void ValidateFields(SystemParam systemParam)
        {
            PropertyInfo[] Props = systemParam.GetType().GetProperties();

            foreach (PropertyInfo p in Props)
            {
                object Valor = p.GetValue(systemParam, null);

                if (Valor.GetType() == typeof(string))
                {
                    if (string.IsNullOrEmpty(Convert.ToString(Valor)))
                    {
                        throw new BusinessException(1);
                    }
                }
            }
        }

        public List<SystemParam> RetrieveIssuerEmailInfo()
        {
            return CrudFactory.RetrieveIssuerEmailInfo<SystemParam>();
        }

        public SystemParam GetPricePerKm()
        {
            return CrudFactory.GetPricePerKM<SystemParam>();
        }
    }
}
