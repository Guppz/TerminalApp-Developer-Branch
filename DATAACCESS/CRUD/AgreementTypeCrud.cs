using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class AgreementTypeCrud: CrudFactory
    {
        private AgreementTypeMapper Mapper;

        public AgreementTypeCrud()
        {
            Mapper = new AgreementTypeMapper();
            Dao = SqlDao.GetInstance();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = Mapper.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstAgreementTypes = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstAgreementTypes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstAgreementTypes;
        }

        public override void Create(BaseEntity entity)
        {
            var agreementType = (AgreementType)entity;
            var sqlOperation = Mapper.GetCreateStatement(agreementType);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var agreementType = (AgreementType)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(agreementType));
        }

        public override void Delete(BaseEntity entity)
        {
            var agreementType = (AgreementType)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(agreementType));
        }
    }
}
