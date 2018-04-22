using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class View : BaseEntity
    {
        public int IdView { get; set; }
        public string ViewName { get; set; }
        public string ViewGroup { get; set; }
        public string ViewPage { get; set; }

        public View()
        {

        }
    }
}
