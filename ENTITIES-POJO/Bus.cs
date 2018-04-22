using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Bus : BaseEntity
    {
        public int IdBus { get; set; }
        public string Plate { get; set; }
        public string Model { get; set; }
        public string Brand { get; set; }
        public int Year { get; set; }
        public int Seats { get; set; }
        public int Standing { get; set; }
        public int Active { get; set; }
        public String ActiveString { get; set; }
        public Company Company { get; set;}
        public Driver Driver { get; set; }
        public List<Requirement> RequirementsPerBus { get; set; }
        public Bus()
        {
        }
    }
}
