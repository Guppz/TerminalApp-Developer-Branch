using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class CompanyCrud : CrudFactory
    {
        private CompanyMapper Mapper;
       
        public CompanyCrud()
        {
            Mapper = new CompanyMapper();
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

        public override List<T> RetrieveAll<T>()
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());

            return GenerateList<T>(LstResults);
        }

        public override void Update(BaseEntity entity)
        {
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(entity));
        }

        public List<Complaint> RetrieveProfitsReport(BaseEntity entity)
        {
            throw new System.NotImplementedException();
        }

        public List<T> RetrieveCompaniesByTerminal<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetCompaniesByTerminalStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public override T Retrieve<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public void AddCompanyToTerminal(BaseEntity entity)
        {
            Dao.ExecuteQueryProcedure(Mapper.GetAddCompanyToTerminalStatement(entity));
        }

        public T RetrieveCompanyByName<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByNameStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public T RetrieveCompanyByEmail<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByEmailStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public T RetrieveCompanyByInfo<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByInfoStatement(entity));

            return BuildObject<T>(LstResult);
        }
        

        public T RetrieveCompanyByCorpId<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetriveByCorpIdStatement(entity));

            return BuildObject<T>(LstResult);
        }
        public T RetrieveCompanyByUser<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetCompanyByUser(entity));

            return BuildObject<T>(LstResult);
        }
        public T RetrieveCompanyByTerminalId<T>(BaseEntity entity)
        {
            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveByTerminalIdStatement(entity));

            return BuildObject<T>(LstResult);
        }

        public T BuildObject<T>(List<Dictionary<string, object>> lstResults)
        {
            var Dic = new Dictionary<string, object>();

            if (lstResults.Count > 0)
            {
                Dic = lstResults[0];
                var Objs = Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstCompanies = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstCompanies.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstCompanies;
        }
    }
}

