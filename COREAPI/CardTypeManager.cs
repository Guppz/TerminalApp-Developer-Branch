using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;

namespace COREAPI
{
    public class CardTypeManager
    {
        private CardTypeCrud CrudFactory;

        public CardTypeManager()
        {
            CrudFactory = new CardTypeCrud();
        }

        public void Create(CardType card)
        {
            var CrudFactory = new CardTypeCrud();
            try
            {
                CrudFactory.Create(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(CardType card)
        {
            var CrudFactory = new CardTypeCrud();
            try
            {
                CrudFactory.Delete(card);
            }
            catch (Exception ex)
            {
                //Warning CS0168  The variable 'ex' is declared but never used    COREAPI C:\Users\AndresMV\Source\Workspaces\ProyectoDos\TerminalApp - Developer - Terminal UI\COREAPI\CardTypeManager.cs    56  Active
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<CardType> RetrieveAll()
        {
            return CrudFactory.RetrieveAll<CardType>();
        }

        public void Update(CardType card)
        {
            try
            {
                var CrudFactory = new CardTypeCrud();
                CrudFactory.Update(card);
            }
            catch (Exception ex)
            {
                //Warning CS0168  The variable 'ex' is declared but never used    COREAPI C:\Users\AndresMV\Source\Workspaces\ProyectoDos\TerminalApp - Developer - Terminal UI\COREAPI\CardTypeManager.cs    56  Active
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public CardType RetrieveByName(CardType card)
        {
            return CrudFactory.Retrieve<CardType>(card);
        }

        public CardType RetrieveByID(CardType card)
        {
            return CrudFactory.RetrieveById<CardType>(card);
        }
    }
}
