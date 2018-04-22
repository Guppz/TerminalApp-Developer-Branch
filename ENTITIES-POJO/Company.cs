using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Company : BaseEntity
    {
        public int IdCompany { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string CorpIdentification { get; set; }
        public string OwnerName { get; set; }
        public bool Status { get; set; }
        public List<Bus> BusesList { get; set; }
        public List<Route> RoutesList { get; set; }
        public List<Driver> DriversList { get; set; }
        public List<Terminal> TerminalsList { get; set; }

        public Company()
        {

        }
    }
}
