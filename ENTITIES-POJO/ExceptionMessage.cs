using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class ExceptionMessage : BaseEntity
    {
        public int IdMessage { get; set; }
        public string Message { get; set; }

        public ExceptionMessage()
        {

        }
    }
}
