using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class CredidCard : BaseEntity
    {
        public int IDCC { get; set; }
        public string credidCardNum { get; set; }
        public User User { get; set; }

        public CredidCard()
        {
        }
    }
}
