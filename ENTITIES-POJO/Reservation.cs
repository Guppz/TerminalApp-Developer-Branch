using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Reservation : BaseEntity
    {
        public Parking Parking { get; set; }
        public Company Company { get; set; }
        public int Quantity { get; set; }
        public int IdReservation { get; set; }

        public Reservation()
        {

        }
    }
}
