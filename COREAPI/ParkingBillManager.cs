using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DATAACCESS.CRUD;
using ENTITIES_POJO;
using Exceptions;

namespace COREAPI
{
    public class ParkingBillManager
    {
        private ParkingCrud PCrud;
        private ParkingBillCrud PBCrud;
        private CardCrud CCrud;
        private PaymentCrud PYCrud;
        private PaymentTerminalCrud PTCrud;

        public ParkingBillManager()
        {
            PCrud = new ParkingCrud();
            PBCrud = new ParkingBillCrud();
            CCrud = new CardCrud();
            PYCrud = new PaymentCrud();
            PTCrud = new PaymentTerminalCrud();
        }
        /// <summary>
        /// Creates a Parking lot Bill
        /// </summary>
        /// <param name="pb"></param>
        public void CreateParkingBill(ParkingBill pb)
        {
            try
            {
                pb.ParkedParking = GetParking(pb, 1);
                if (pb.ParkedParking == null)
                {
                    throw new BusinessException(30);
                }
                if (pb.ParkedParking.AvailableSpaces < pb.ParkedParking.OccupiedSpces + 1)
                {
                    throw new BusinessException(31);
                }
                PBCrud.Create(pb);
                pb.ParkedParking.OccupiedSpces += 1;
                PCrud.ChangeOccupiedSpaces(pb.ParkedParking);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);

            }
        }
        /// <summary>
        /// Updates a ParkingBill 
        /// </summary>
        /// <param name="pb"></param>
        public void UpdateParkingBill(ParkingBill pb)
        {

            try
            {

                ParkingBill exist = PBCrud.RetrieveParkingBillByCard<ParkingBill>(pb.ParkingCard);
                if (exist == null)
                {
                    throw new BusinessException(33);
                }
                exist.ParkedParking = PCrud.Retrieve<Parking>(exist.ParkedParking);
                exist.ParkingCard = pb.ParkingCard;
                exist.EndDate = pb.EndDate;
                exist.AmountHours = Convert.ToInt32(Math.Round(exist.EndDate.Subtract(exist.BeginDate).TotalHours));

                if (exist.AmountHours > 8)
                {

                    exist.AmountHours = (exist.AmountHours / 24 + 1) * 24;
                }

                exist.TotalCost = exist.AmountHours * exist.ParkedParking.RentalCost;

                if (exist.ParkingCard.Balance - exist.TotalCost <= 0)
                {
                    throw new BusinessException(34);

                }

                exist.ParkingCard.Balance = exist.ParkingCard.Balance - exist.TotalCost;

                var payment = GetPayment(exist);
                payment = PYCrud.CreateParkingPayment(payment);

                var paymentTerminal = new PaymentTerminal
                {
                    Amount = exist.TotalCost,
                    PercentageUsed = 0,
                    PaymentGot = payment,
                    TerminalPayed = exist.ParkingCard.Terminal,
                   
                };

                PTCrud.Create(paymentTerminal);
                PBCrud.Update(exist);
                CCrud.UpdateBalance(exist.ParkingCard);
                exist.ParkedParking.OccupiedSpces -= 1;
                PCrud.ChangeOccupiedSpaces(exist.ParkedParking);
            }
            catch (Exception ex)
            {
                ExceptionManager.GetInstance().Process(ex);
            }
        }
        /// <summary>
        /// Builds a payment based on the Parking Bill
        /// </summary>
        /// <param name="exist"></param>
        /// <returns></returns>
        private static Payment GetPayment(ParkingBill exist)
        {
            return new Payment
            {
                Amount = exist.TotalCost,
                Card = exist.ParkingCard,
                Date = exist.EndDate,
                Detail = "Pago de parqueo",
                IssuerUser = exist.ParkingCard.User,
                PaymentType = new Glosary { PkIdTerm = 2 }
            };
        }
        /// <summary>
        /// Get the parking lo from the Parking Bill
        /// </summary>
        /// <param name="parkB"></param>
        /// <param name="type"></param>
        /// <returns></returns>
        public Parking GetParking(ParkingBill parkB, int type)
        {
            var lstParking = PCrud.RetrieveParkingByTerminal<Parking>(parkB.ParkingCard.Terminal);

            foreach (Parking park in lstParking)
            {
                if (park.ParkingType == type)
                {
                    return park;
                }
            }
            return null;
        }
    }
}
