using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class SystemParamType : BaseEntity
    {
        public int IdParamType { get; set; }
        public string ParamTypeName { get; set; }

        public SystemParamType()
        {

        }
    }
}
