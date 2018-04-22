using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class SystemParam : BaseEntity
    {
        public int IdSystemParam { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public SystemParamType ParamType { get; set; }

        public SystemParam()
        {

        }
    }
}
