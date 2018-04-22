
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class RolePerView : BaseEntity
    {
        public int IdView { get; set; }
        public int IdRole { get; set; }

        public RolePerView()
        {

        }
    }
}
