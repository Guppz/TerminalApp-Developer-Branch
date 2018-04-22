using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class FineType : BaseEntity
    {
        public int IdType { get; set; }
        public string TypeDescription { get; set; }
        public double Cost { get; set; }
        public string TypeName { get; set; }

        public FineType()
        {

        }
    }
}
