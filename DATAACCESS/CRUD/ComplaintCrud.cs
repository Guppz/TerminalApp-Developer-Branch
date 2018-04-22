using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class ComplaintCrud : CrudFactory
    {
        private ComplaintMapper Mapper;

        public ComplaintCrud()
        {
            Mapper = new ComplaintMapper();
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
                var Objs = (Complaint)Mapper.BuildObject(Dic);
                return (T)Convert.ChangeType(Objs, typeof(T));
            }

            return default(T);
        }

        public int CreateComplaint(BaseEntity entity)
        {
            var x = Dao.ExecuteQueryProcedure(Mapper.GetCreateStatement(entity));

            return 1;
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

        public void UpdateComplaintsSettings(double number, int idParam, string name)
        {
            Dao.ExecuteProcedure(Mapper.GetUpdateSettingsStatement(number, idParam, name));
        }

        public List<T> RetrieveComplaintsByTerminal<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetComplaintsByTerminalStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveComplaintsByCompany<T>(BaseEntity entity)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetComplaintsByCompanyStatement(entity));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveComplaintsByDateRange<T>(DateTime beginDate, DateTime endDate)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetComplaintsByDateRange(beginDate, endDate));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveComplaintsByDateRangeAndCompany<T>(DateTime beginDate, DateTime endDate, int idCompany)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetComplaintsByDateRangeAndCompany(idCompany, beginDate, endDate));

            return GenerateList<T>(LstResults);
        }

        public List<T> RetrieveComplaintsByDateRangeAndTerminal<T>(int idTerminal, DateTime beginDate, DateTime endDate)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.GetComplaintsByDateRangeAndTerminal(idTerminal, beginDate, endDate));

            return GenerateList<T>(LstResults);
        }

        public List<T> GenerateList<T>(List<Dictionary<string, object>> lstResults)
        {
            var LstComplaints = new List<T>();

            if (lstResults.Count > 0)
            {
                var Objs = Mapper.BuildObjects(lstResults);
                foreach (var c in Objs)
                {
                    LstComplaints.Add((T)Convert.ChangeType(c, typeof(T)));
                }
            }

            return LstComplaints;
        }

        public int VerifyComplaintsLimit(Company company)
        {
            var LstResults = Dao.ExecuteQueryProcedure(Mapper.VerifyComplaintsLimit(company));
            return (int)LstResults[0]["Result"];

        }
    }
}
