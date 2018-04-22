using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class UserPassword : BaseEntity
    {
        public int IdPassword { get; set; }
        public string Password { get; set; }
        public DateTime DateExpiry { get; set; }
        
        public UserPassword() {


        }
    }
}
