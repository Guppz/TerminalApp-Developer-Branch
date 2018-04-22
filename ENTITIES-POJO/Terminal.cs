using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Terminal : BaseEntity
    {
        public int IdTerminal { get; set; }
        public string Name { get; set; }
        public Location Location { get; set; }

        public Terminal()
        {

        }
    }
}
