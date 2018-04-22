using System;
using DATAACCESS.DAO;
using DATAACCESS.MAPPER;
using ENTITIES_POJO;
using System.Collections.Generic;

namespace DATAACCESS.CRUD
{
    public class CardCrud : CrudFactory
    {
        private CardMapper Mapper;

        public CardCrud()
        {
            Mapper = new CardMapper();
            Dao = SqlDao.GetInstance();
        }

        public override void Create(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetCreateStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public  void CreateCardAgreement(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetCreateStatementAgreement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }
        public  void UpdateStatus(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetUpdateSatutsStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Update(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetUpdateStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }
        public  void UpdateNoti(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetUpdateNotiStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }
        public  void UpdateBalance(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetUpdateBalanceStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
        }

        public override void Delete(BaseEntity entity)
        {
            var card = (Card)entity;
            var sqlOperation = Mapper.GetDeleteStatement(card);
            Dao.ExecuteProcedure(sqlOperation);
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

        public List<T> RetrieveStudiant<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetStudiantStatement(entity));
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

        public List<T> RetrieveStudiantCardDisabled<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetStudiantCardDisabledStatement(entity));
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


        public List<T> RetrieveUserCards<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetUserCarsStatement(entity));
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

        public List<T> RetrieveUserCardsByTerminal<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetUserCardsByTerminalStatement(entity));
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

        public List<T> RetrieveById<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetUserCarsStatement(entity));
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

        public List<T> RetrieveCardsByTerminal<T>(BaseEntity entity)
        {
            var lstCards = new List<T>();

            var lstResult = Dao.ExecuteQueryProcedure(Mapper.GetCardsByTerminalStatement(entity));
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

        public T CardReIssue<T>(BaseEntity entity)
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
    }
}
