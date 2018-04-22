using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class ValueListCrud : CrudFactory
    {
        private ValueListMapper Mapper;
       
        public ValueListCrud()
        {
            Mapper = new ValueListMapper();
            Dao = SqlDao.GetInstance();
        }
        public override void Create(BaseEntity entity)
        {
            var location = (ValueListSelect)entity;
            var sqlOperation = Mapper.GetCreateStatement(location);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var user = (ValueListSelect)entity;
            var sqlOperation = Mapper.GetDeleteStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
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
            var user = (ValueListSelect)entity;
            var sqlOperation = Mapper.GetUpdateStatement(user);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveSelect<T>(BaseEntity entity)
        {
            var LstCategoria = new List<T>();

            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            if (LstResult.Count > 0)
            {
                var Objs = Mapper.BuildObjects(LstResult);

                foreach (var c in Objs)
                {
                    LstCategoria.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstCategoria;
        }
    }
}
