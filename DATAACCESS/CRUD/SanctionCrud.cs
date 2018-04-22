using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class SanctionCrud : CrudFactory
    {
        private SanctionMapper Mapper;
        private SanctionTypeMapper SancTypeMapper;

        public SanctionCrud()
        {
            Mapper = new SanctionMapper();
            SancTypeMapper = new SanctionTypeMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetCreateStatement(entity));
        }

        public override void Delete(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(entity));
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));
            var Dic = new Dictionary<string, object>();

            if (LstResult.Count > 0)
            {
                Dic = LstResult[0];
                var Objs = (Sanction)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public override List<T> RetrieveAll<T>()
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());

            return GenerateList<T>(LstResults);
        }

        public override void Update(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(entity));
        }

        public void CreateSanctionType(BaseEntity entity)
        {
            Dao.ExecuteProcedure(SancTypeMapper.GetCreateStatement(entity));
        }

        public void UpdateSanctionType(BaseEntity entity)
        {
            Dao.ExecuteProcedure(SancTypeMapper.GetUpdateStatement(entity));
        }

        public void DeleteSanctionType(BaseEntity entity)
        {
            Dao.ExecuteProcedure(SancTypeMapper.GetDeleteStatement(entity));
        }

        public T RetrieveSanctionTypeByName<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(SancTypeMapper.GetRetriveByNameStatement(entity));
            return BuildSanctionTypeObject<T>(LstResult);
        }

        public T RetrieveSanctionTypeByDescription<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(SancTypeMapper.GetRetriveByDescriptionStatement(entity));
            return BuildSanctionTypeObject<T>(LstResult);
        }

        public T RetrieveSanctionTypeById<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(SancTypeMapper.GetRetriveStatement(entity));

            return BuildSanctionTypeObject<T>(LstResult);
        }

        public T BuildSanctionTypeObject<T>(List<Dictionary<string, object>> lstResults)
        {
            var Dic = new Dictionary<string, object>();

            if (lstResults.Count > 0)
            {
                Dic = lstResults[0];
                var Objs = SancTypeMapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public List<T> RetrieveSanctionsTypes<T>()
        {
            var LstTypes = new List<T>();

            var LstResults = Dao.ExecuteQueryProcedure(SancTypeMapper.GetRetrieveAllStatement());

            if (LstResults.Count > 0)
            {
                var Objs = SancTypeMapper.BuildObjects(LstResults);
                foreach (var c in Objs)
                {
                    LstTypes.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstTypes;
        }

        public List<T> RetrieveSanctionsByDateRange<T>(DateTime beginDate, DateTime endDate)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetSanctionsByDateRangeStatement(beginDate, endDate));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveSanctionsByDateRangeAndCompany<T>(DateTime beginDate, DateTime endDate, int idCompany)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetSanctionsByDateRangeAndCompanyStatement(beginDate, endDate, idCompany));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveSanctionsByCompany<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetSanctionsByCompanyStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveSanctionsByType<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetSanctionsByTypeStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstSanctions = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstSanctions.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstSanctions;
        }
    }
}
