using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Location : BaseEntity
    {
        public int IdLocation { get; set; }
        public string Name { get; set; }
        public string Latitude { get; set; }
        public string Longitude { get; set; }

        public Location()
        {

        }
    }
}
