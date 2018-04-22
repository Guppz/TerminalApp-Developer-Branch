using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;


namespace COREAPI
{
    public class CredidCardManager
    {

        private CredidCardCrud CrudFactory;
        private UserCrud UCrud;

        public CredidCardManager()
        {
            CrudFactory = new CredidCardCrud();
            UCrud = new UserCrud();
        }

        public void Create(CredidCard card)
        {
            var crudCard = new CredidCardCrud();
            try
            {
                crudCard.Create(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(CredidCard card)
        {
            var CrudCard = new CredidCardCrud();
            try
            {
                CrudCard.Delete(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<CredidCard> RetrieveAll(CredidCard CC)
        {
            List<CredidCard> ListCards = CrudFactory.RetrieveUserCredidCards<CredidCard>(CC);
            var user = new User();

            foreach (CredidCard c in ListCards)
            {
                user.IdUser = c.User.IdUser;
                c.User = UCrud.Retrieve<User>(user);

            }
            return ListCards;
        }
    }
}
