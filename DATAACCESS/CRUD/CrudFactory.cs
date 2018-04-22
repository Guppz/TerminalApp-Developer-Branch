using DATAACCESS.DAO;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public abstract class CrudFactory
    {
        protected SqlDao Dao;

        public abstract void Create(BaseEntity entity);
        public abstract T Retrieve<T>(BaseEntity entity);
        public abstract List<T> RetrieveAll<T>();
        public abstract void Update(BaseEntity entity);
        public abstract void Delete(BaseEntity entity);
    }
}
