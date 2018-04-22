using System;
using System.Collections.Generic;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;

namespace DATAACCESS.CRUD
{
    public class TerminalCrud : CrudFactory
    {
        private TerminalMapper Mapper;

        public TerminalCrud()
        {
            Mapper = new TerminalMapper();
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
            var lstTerminals = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveAllStatement());
            var dic = new Dictionary<string, object>();
            if (lstResult.Count > 0)
            {
                var objs = Mapper.BuildObjects(lstResult);
                foreach (var c in objs)
                {
                    lstTerminals.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return lstTerminals;
        }

        public override void Create(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            var sqlOperation = Mapper.GetCreateStatement(terminal);

            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            Dao.ExecuteProcedure(Mapper.GetUpdateStatement(terminal));
        }

        public override void Delete(BaseEntity entity)
        {
            var terminal = (Terminal)entity;
            Dao.ExecuteProcedure(Mapper.GetDeleteStatement(terminal));
        }

        public List<String> RetrieveProfitsReport(int idTerminal)
        {
            throw new System.NotImplementedException();
        }

        public List<T> RetrieveCompaniesByTerminal<T>(BaseEntity entity)
        {
            var LstCompanies = new List<T>();

            var LstResult = Dao.ExecuteQueryProcedure(Mapper.GetRetrieveCompaniesByTerminalStatement(entity));

            if (LstResult.Count > 0)
            {
                var objs = new CompanyMapper().BuildObjects(LstResult);
                foreach (var c in objs)
                {
                    LstCompanies.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }
            return LstCompanies;
        }
    }
}
