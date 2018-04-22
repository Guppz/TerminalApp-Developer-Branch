using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Requirement : BaseEntity
    {
        public int IdRequirement { get; set; }
        public string Name { get; set; }
        public DateTime Expiry {get; set;}
        public Requirement()
        { 
        }
    }
}
