using System;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class CredidCardCrud : CrudFactory
    {
        private CredidCardMapper Mapper;

        public CredidCardCrud()
        {
            Mapper = new CredidCardMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = Mapper.GetCreateStatement(entity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveUserCredidCards<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetUserCardsStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCards.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return lstCards;
        }

        public override void Delete(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            throw new NotImplementedException();
        }

        public override List<T> RetrieveAll<T>()
        {
            throw new NotImplementedException();
        }

        public override void Update(BaseEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
