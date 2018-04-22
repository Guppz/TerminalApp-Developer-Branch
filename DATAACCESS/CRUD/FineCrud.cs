using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class FineCrud : CrudFactory
    {
        private FineMapper Mapper;
        private FineTypeMapper MapperTypeFine;

        public FineCrud()
        {
            MapperTypeFine = new FineTypeMapper();
            Mapper = new FineMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var sqlOperation = Mapper.GetCreateStatement(entity);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var fine = (Fine)entity;
            var sqlOperation = Mapper.GetDeleteStatement(fine);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public  void DeleteFineType(BaseEntity entity)
        {
            var fineType = (FineType)entity;
            var sqlOperation = MapperTypeFine.GetDeleteStatement(fineType);
            Dao.ExecuteProcedure(sqlOperation);
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

        public  T RetrieveFinesById<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(MapperTypeFine.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = MapperTypeFine.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveAllFines<T>()
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(MapperTypeFine.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = MapperTypeFine.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstCards.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstCards;
        }

        public List<T> RetrieveFinesTypes<T>()
        {
            var LstTypes = new List<T>();

            var LstResults = Dao.ExecuteQueryProcedure(MapperTypeFine.GetRetrieveAllStatement());

            if (LstResults.Count > 0)
            {
                var Objs = MapperTypeFine.BuildObjects(LstResults);
                foreach (var c in Objs)
                {
                    LstTypes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return LstTypes;
        }

        public override List<T> RetrieveAll<T>()
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
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

        public  List<T> RetrieveAllFineComboBox<T>()
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.RetrieveAllFineComboBox());
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

        public override void Update(BaseEntity entity)
        {
            var fine = (Fine)entity;
            var sqlOperation = Mapper.GetUpdateStatement(fine);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public int VerifyFinesLimit(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(Mapper.VerifyFinesLimit(entity));
            var IsLimit = (int)lstResult[0]["Result"];

            return IsLimit;
        }

        public FineType CreateFineType(BaseEntity entity)
        {
            var fineT = (FineType)entity;
            var sqlOperation = MapperTypeFine.GetCreateStatement(entity);
            var lstResult = Dao.ExecuteQueryProcedure(sqlOperation);
            fineT.IdType = (int)lstResult[0]["PKIdFineType"];

            return fineT;
        }
      
        public T RetrieveFineTypeByName<T>(BaseEntity entity)
        {
            var lstResult = Dao.ExecuteQueryProcedure(MapperTypeFine.GetRetriveStatement(entity));
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                dic = lstResult[0];
                var objs = MapperTypeFine.BuildObject(dic);
                return (T)Convert.ChangeType(objs, typeof(T));
            }
            return default(T);
        }

        public void UpdateFinesSettings(BaseEntity entity)
        {
            var fineType = (FineType)entity;
            var sqlOperation = MapperTypeFine.GetUpdateStatement(fineType);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public List<T> RetrieveFinesByDateRange<T>(DateTime beginDate, DateTime endDate)
        {
            throw new System.NotImplementedException();
        }

        public List<T> RetrieveFinesByDateRangeAndCompany<T>(DateTime beginDate, DateTime endDate, int idCompany)
        {
            throw new System.NotImplementedException();
        }

        public List<T> RetrieveFinesByCompany<T>(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }
    }




}
