using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Parking : BaseEntity
    {
        public int IdParking { get; set; }
        public int ParkingType { get; set; }
        public string ParkingTypeName { get; set; }
        public int AvailableSpaces { get; set; }
        public int OccupiedSpces { get; set; }
        public int RentalCost { get; set; }
        public Terminal Terminal { get; set; }

        public Parking()
        {

        }
    }
}
