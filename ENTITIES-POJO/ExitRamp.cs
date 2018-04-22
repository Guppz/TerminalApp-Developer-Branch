using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class ExitRamp : BaseEntity
    {
        public int IdExitRamp { get; set; }
        public string Name { get; set; }
        public Terminal Terminal { get; set; }
        public Travel CurrentTravel { get; set; }
        public Travel NextTravel { get; set; }
        public Route Route { get; set; }
        public List<Travel> TravelList { get; set; }

        public ExitRamp()
        {

        }
    }
}
