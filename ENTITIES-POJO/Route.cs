using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Route : BaseEntity
    {
        public int IdRoute { get; set; }
        public string Name { get; set; }
        public double Distance { get; set; }
        public double EstimatedTime { get; set; }
        public double Price { get; set; }
        public int Status { get; set; }

        public string StatusString
        {
            get
            {
                if (Status == 1)
                {
                    return "Activa";
                }
                else
                {
                    return "Inactiva";
                }
            }
        }

        public Company RouteCompany { get; set; }
        public Terminal RouteTerminal { get; set; }
        public List<Schedule> SchedulesList { get; set; }
        public List<Location> BusStops { get; set; }

        public Route()
        {

        }
    }
}
