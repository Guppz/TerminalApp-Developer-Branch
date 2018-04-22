using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace ENTITIES_POJO
{
    public class ParkingBill : BaseEntity
    {
        public int IdParkingBill { get; set; }
        public Reservation Reservation { get; set; }
        public DateTime BeginDate { get; set; }
        public DateTime EndDate { get; set; }
        public int AmountHours { get; set; }
        public int TotalCost { get; set; }
        public Card ParkingCard { get; set; }
        public int IdBill { get; set; }
        public Parking ParkedParking { get; set; }

        public ParkingBill()
        {

        }
    }
}
