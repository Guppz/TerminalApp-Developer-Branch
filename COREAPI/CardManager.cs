using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;

namespace COREAPI
{
    public class CardManager
    {
        private CardCrud CrudFactory;
        private UserCrud UCrud;
        private TerminalCrud TerminalCrud;
        private CardTypeCrud CtCrud;
        private SystemParamCrud SyParam;
        private PaymentCrud PayCrud;
        private PaymentTerminalCrud PTCrud;

        public CardManager()
        {
            CrudFactory = new CardCrud();
            UCrud = new UserCrud();
            TerminalCrud = new TerminalCrud();
            CtCrud = new CardTypeCrud();
            SyParam = new SystemParamCrud();
            PayCrud = new PaymentCrud();
            PTCrud = new PaymentTerminalCrud();
        }

        public void Create(Card card)
        {
            var crudCard = new CardCrud();

            try
            {
                SystemParam SystemParan = new SystemParam();
                SystemParan.IdSystemParam = 4;
                SystemParan = SyParam.Retrieve<SystemParam>(SystemParan);
                card.Balance = Convert.ToDouble(SystemParan.Value);
                card = typeExperyCard(card);
                cardValidarior(card);
                DateValidator(card);
                notificacionValidarior(card);
                card = CreateNotificasion(card);

                if (card.User.IdUser == 0)
                {
                    User user = new User();
                    user.Identification = card.User.Identification;
                    user = UCrud.RetrieveIdentification<User>(user);
                    card.User = user;
                }
                crudCard.Create(card);
                List<Card> ListCard = new List<Card>();
                ListCard.Add(card);
                checkSentEmail(ListCard);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void Delete(Card card)
        {
            var CrudCard = new CardCrud();
            try
            {
                CrudCard.Delete(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public List<Card> RetrieveAll()
        {
            List<Card> ListCards = CrudFactory.RetrieveAll<Card>();
            var user = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                BuildObjects(c);
            }

            return ListCards;
        }

        public List<Card> RetrieveStudiant(Card card)
        {
            List<Card> ListCards = CrudFactory.RetrieveStudiant<Card>(card);
            var User = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                User.IdUser = c.User.IdUser;
                cardType.IdCardType = c.CrType.IdCardType;
                Terminal.IdTerminal = c.Terminal.IdTerminal;
                c.User = UCrud.Retrieve<User>(User);
                c.CrType = CtCrud.RetrieveById<CardType>(cardType);
                c.Terminal = TerminalCrud.Retrieve<Terminal>(Terminal);
                location.IdLocation = c.Terminal.Location.IdLocation;
                c.Terminal.Location = LocationCrud.Retrieve<Location>(location);
            }
            return ListCards;
        }

        public List<Card> RetrieveStudiantCardDisabled(Card card)
        {
            List<Card> ListCards = CrudFactory.RetrieveStudiantCardDisabled<Card>(card);
            var user = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                user.IdUser = c.User.IdUser;
                cardType.IdCardType = c.CrType.IdCardType;
                Terminal.IdTerminal = c.Terminal.IdTerminal;
                c.User = UCrud.Retrieve<User>(user);
                c.CrType = CtCrud.RetrieveById<CardType>(cardType);
                c.Terminal = TerminalCrud.Retrieve<Terminal>(Terminal);
                location.IdLocation = c.Terminal.Location.IdLocation;
                c.Terminal.Location = LocationCrud.Retrieve<Location>(location);
            }
            return ListCards;
        }

        public void Update(Card card)
        {
            try
            {
                var crudCard = new CardCrud();
                crudCard.Update(card);

            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateBalance(Card card)
        {
            try
            {
                PaymentActivasionValidarior(card);
                MinimumBalanceValidator(card);
                card = CreateNotificasion(card);
                var crudCard = new CardCrud();
                crudCard.UpdateBalance(card);
                Payment payment = new Payment();
                payment.Date = DateTime.Now;
                payment.IssuerUser = card.User;
                payment.Card = card;
                payment.Amount = card.newBalance;
                payment.Detail = "Recarga de saldo del usario";
                Glosary glosary = new Glosary();
                glosary.PkIdTerm = 4;
                payment.PaymentType = glosary;
                payment =PayCrud.CreateParkingPayment(payment);
                PaymentTerminal py = new PaymentTerminal();
                py.PercentageUsed = 0;
                py.TerminalPayed = card.Terminal;
                py.PaymentGot = payment;
          
                PTCrud.Create(py);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateNotificasion(Card card)
        {
            try
            {
                notificacionValidarior(card);
                card = CreateNotificasion(card);
                var crudCard = new CardCrud();
                crudCard.UpdateNoti(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateNotificasionDate(Card card)
        {
            try
            {
                var crudCard = new CardCrud();
                crudCard.Update(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void UpdateActivacion(Card card)
        {
            try
            {
                card = CreateNotificasion(card);
                card.Status = ActivationValidarior(card);
                var crudCard = new CardCrud();

                crudCard.Update(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public void deactivateCard(Card card)
        {
            try
            {

                var crudCard = new CardCrud();
                crudCard.Update(card);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Card RetrieveById(Card card)
        {
            try
            {
                card = CrudFactory.Retrieve<Card>(card);
                if (card != null)
                {
                    BuildObjects(card);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
            return card;
        }

        public List<Card> RetrieveByTerminal(Card card)
        {
            List<Card> ListCards = CrudFactory.RetrieveCardsByTerminal<Card>(card);
            var user = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                BuildObjects(c);
            }

            checkSentEmail(ListCards);
            return ListCards;
        }

        public List<Card> RetrieveUserCards(Card card)
        {
            List<Card> ListCards = CrudFactory.RetrieveUserCards<Card>(card);
            var User = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                CrudFactory.UpdateStatus(c);
                User.IdUser = c.User.IdUser;
                cardType.IdCardType = c.CrType.IdCardType;
                Terminal.IdTerminal = c.Terminal.IdTerminal;
                c.User = UCrud.Retrieve<User>(User);
                c.CrType = CtCrud.RetrieveById<CardType>(cardType);
                c.Terminal = TerminalCrud.Retrieve<Terminal>(Terminal);
                location.IdLocation = c.Terminal.Location.IdLocation;
                c.Terminal.Location = LocationCrud.Retrieve<Location>(location);
                StatusToStatusString(c);
            }

            checkSentEmail(ListCards);
            return ListCards;
        }

        public List<Card> RetrieveUserCardsByTerminal(Card card)
        {
            List<Card> ListCards = CrudFactory.RetrieveUserCardsByTerminal<Card>(card);
            var User = new User();
            var cardType = new CardType();
            var Terminal = new Terminal();
            var location = new Location();
            var LocationCrud = new LocationCrud();

            foreach (Card c in ListCards)
            {
                CrudFactory.UpdateStatus(c);
                User.IdUser = c.User.IdUser;
                cardType.IdCardType = c.CrType.IdCardType;
                Terminal.IdTerminal = c.Terminal.IdTerminal;
                c.User = UCrud.Retrieve<User>(User);
                c.CrType = CtCrud.RetrieveById<CardType>(cardType);
                c.Terminal = TerminalCrud.Retrieve<Terminal>(Terminal);
                location.IdLocation = c.Terminal.Location.IdLocation;
                c.Terminal.Location = LocationCrud.Retrieve<Location>(location);
                StatusToStatusString(c);
            }
            checkSentEmail(ListCards);
            return ListCards;
        }

        public void CardsRequest(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void sendCardsRequest(string fileName)
        {
            throw new System.NotImplementedException();
        }

        public void notificacionValidarior(Card Pcard)
        {
            if (Pcard.DaysForNotification < 0)
            {
                throw new BusinessException(7);
            }
        }

        public int ActivationValidarior(Card Pcard)
        {
            if (Pcard.Status == 1)
            {
                throw new BusinessException(8);

            }
            return 1;
        }

        void PaymentActivasionValidarior(Card pCard)
        {
            if (pCard.Status == 0)
            {
                throw new BusinessException(16);
            }
        }

        void cardValidarior(Card pCard)
        {
            Card userCard = new Card();
            List<Card> ListaCards = RetrieveUserCardsByTerminal(pCard);
            CardType cr = new CardType();

            if (ListaCards.Count == 2)
            {
                throw new BusinessException(9);
            }

            if (ListaCards.Count == 1)
            {
                foreach (Card cards in ListaCards)
                {
                    if (cards.CrType.IdCardType == pCard.CrType.IdCardType)
                    {
                        throw new BusinessException(19);
                    }
                    else if (pCard.CrType.IdCardType == 4)
                    {
                        throw new BusinessException(20);
                    }
                    else if (pCard.CrType.IdCardType == 5)
                    {
                        throw new BusinessException(20);
                    }
                    else if (pCard.CrType.IdCardType == 1)
                    {
                        throw new BusinessException(20);
                    }

                }
            }
        }

        void MinimumBalanceValidator(Card pCard)
        {
            SystemParam SystemParam = new SystemParam();
            SystemParam.IdSystemParam = 3;
            SystemParam = SyParam.Retrieve<SystemParam>(SystemParam);
            if (pCard.newBalance < Convert.ToDouble(SystemParam.Value))
            {
                throw new BusinessException(13);
            }
        }

        void DateValidator(Card pCard)
        {
            DateTime today = DateTime.Now;
            if (pCard.ExpiryDate < today)
            {
                throw new BusinessException(4);
            }
        }

        void checkSentEmail(List<Card> pCard)
        {

            Card newCard = new Card();
            foreach (Card Card in pCard)
            {
                if (Card.Notification.Date == DateTime.Now.Date)
                {
                    String subject = "Notificasion de saldo";
                    SentEmail(Card, subject);
                    newCard = CreateNotificasion(Card);
                    UpdateNotificasionDate(newCard);
                }
                if (Card.ExpiryDate.Date == DateTime.Now.Date)
                {
                    Card.Status = 2;
                    deactivateCard(Card);
                }
            }
        }

        public void SentEmail(Card pCard, string subject)
        {
            string emailBody = CreateBodyEmail(pCard);
            List<SystemParam> IssuerEmailInformation = new SystemParamManager().RetrieveIssuerEmailInfo();

            MailAddress from = new MailAddress(IssuerEmailInformation[0].Value, "Terminal App");
            MailAddress to = new MailAddress(pCard.User.Email, pCard.User.Name);
            MailMessage Mail = new MailMessage(from, to);
            SmtpClient Client = new SmtpClient();
            Client.Port = 587;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Host = "smtp.gmail.com";
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential(IssuerEmailInformation[0].Value, IssuerEmailInformation[1].Value);
            Mail.Subject = subject;
            Mail.Body = emailBody;
            Mail.IsBodyHtml = true;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Client.Send(Mail);
        }

        private string CreateBodyEmail(Card card)
        {
            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string Body;
            String balanceString = card.Balance + "";
            String Gin = card.IdCard;
            basePath = basePath.Replace("TerminalApp", "WEBUI").Replace("WEBUI-", "TerminalApp-");
            string folderPath = "Models\\ControlsHtml\\BalanceTemplate.html";
            string path = basePath + folderPath;
            Body = System.IO.File.ReadAllText(path);
            Body = Body.Replace("{saldo}", balanceString);
            Body = Body.Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));
            Body = Body.Replace("{Gin}", Gin);
            return Body;
        }

        Card CreateNotificasion(Card pCard)
        {
            var d = DateTime.Now;
            pCard.Notification = d.AddDays(pCard.DaysForNotification);
            return pCard;
        }

        public void ReIssueCard(CardReIssue pCardReIssue)
        {
            Card be = null;
            //Card newCard = null;
            var cardCRUD = new CardCrud();

            try
            {
                be = RetrieveById(pCardReIssue.Card);
                if (be != null)
                {
                    if (pCardReIssue.Type == 1)
                    {
                        be.Balance = be.Balance + GetSystemBalance();
                    }

                    cardCRUD.Delete(be);
                    //newCard = cardCRUD.CardReIssue<Card>(be);
                    cardCRUD.Create(be);
                    SendCardDeactivationEmail(be);
                }
                else
                {
                    // Tarjeta no Encontrada || tarjeta no existe.
                    throw new BusinessException(32);
                }
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }

        public Card StatusToStatusString(Card pCard)
        {
            switch (pCard.Status)
            {
                case 0:
                    pCard.StatusString = "Inactiva";
                    break;
                case 1:
                    pCard.StatusString = "Activado";
                    break;
                case 2:
                    pCard.StatusString = "Desactivado";
                    break;
                default:
                    pCard.StatusString = "Desconocido";
                    break;
            }
            return null;
        }

        private void BuildObjects(Card card)
        {
            var terminalMng = new TerminalManager();
            var userMng = new UserManager();

            card.Terminal = terminalMng.RetrieveById(card.Terminal);
            card.User = UCrud.Retrieve<User>(card.User);
            card.CrType = CtCrud.RetrieveById<CardType>(card.CrType);
            StatusToStatusString(card);
        }

        private double GetSystemBalance()
        {
            double balance = 0;

            SystemParam sysParam = new SystemParam();
            sysParam.IdSystemParam = 4;
            sysParam = SyParam.Retrieve<SystemParam>(sysParam);
            balance = Convert.ToDouble(sysParam.Value);

            return balance;
        }

        private void SendCardDeactivationEmail(Card card)
        {
            var subject = "Notificación de Desactivacion de Tarjeta";
            string emailBody = CreateCardDeactivationEmail(card, subject);

            var user = card.User;

            MailMessage Mail = new MailMessage("cobandol@ucenfotec.ac.cr", user.Email);
            SmtpClient Client = new SmtpClient();
            Client.Port = 587;
            Client.DeliveryMethod = SmtpDeliveryMethod.Network;
            Client.UseDefaultCredentials = false;
            Client.Host = "smtp.gmail.com";
            Client.EnableSsl = true;
            Client.Credentials = new NetworkCredential("cobandol@ucenfotec.ac.cr", "12AB34cd");
            Mail.Subject = subject;
            Mail.Body = emailBody;
            Mail.IsBodyHtml = true;
            Mail.BodyEncoding = System.Text.Encoding.UTF8;
            Client.Send(Mail);
        }

        private string CreateCardDeactivationEmail(Card card, string subject)
        {
            var user = card.User;

            string basePath = System.AppDomain.CurrentDomain.BaseDirectory;
            string Body;

            basePath = basePath.Replace("TerminalApp", "WEBUI").Replace("WEBUI-", "TerminalApp-");
            string folderPath = "Models\\ControlsHtml\\CardRecoveryEmailTemplate.html";
            string path = basePath + folderPath;

            Body = System.IO.File.ReadAllText(path);

            Body = Body.Replace("{Name}", user.Name);
            Body = Body.Replace("{LastName}", user.LastName);
            Body = Body.Replace("{Date}", DateTime.Now.ToString("dd/MM/yyyy"));
            Body = Body.Replace("{p1}", "Le informamos que su tarjeta GIN: " + card.IdCard + ", ha sido desactivada.");
            Body = Body.Replace("{p2}", "");

            return Body;
        }
        public Card typeExperyCard(Card card)
        {
            var date = new DateTime();
            if (card.CrType.IdCardType == 1)
            {
                date = new DateTime(2019, 12, 31);
            }
            if (card.CrType.IdCardType == 2)
            {
                date = new DateTime(2018, 12, 31);
            }
            if (card.CrType.IdCardType == 3)
            {
                date = DateTime.Now.AddDays(7);
            }
            if (card.CrType.IdCardType == 4)
            {
                date = new DateTime(2019, 12, 31);
            }
            if (card.CrType.IdCardType == 5)
            {
                date = new DateTime(2019, 12, 31);
            }
            card.ExpiryDate = date;
            return card;
        }


    }
}
