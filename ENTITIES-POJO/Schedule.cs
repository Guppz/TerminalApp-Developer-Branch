using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Schedule : BaseEntity
    {
        public int IdSchedule { get; set; }
        public int Available { get; set; }
        public Route Route { get; set; }
        public string Day { get; set; }
        public string DepartHour { get; set; }
        public string AvailableStatus { get; set; }
        public int DepartHourAsMinutes { get; set; }

        public Schedule()
        {

        }
    }
}
