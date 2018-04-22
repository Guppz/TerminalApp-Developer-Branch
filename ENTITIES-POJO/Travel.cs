using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Travel : BaseEntity
    {
        public int IdTravel { get; set; }
        public string Date { get; set; }
        public Schedule Schedule { get; set; }
        public Bus Bus { get; set; }
        //public Driver Driver { get; set; }
        public Route Route { get; set; }
        public int PassengersOnboard { get; set; }

        public Travel()
        {

        }
    }
}
