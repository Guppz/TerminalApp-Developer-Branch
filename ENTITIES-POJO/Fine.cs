using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Fine : BaseEntity
    {
        public int IdFine { get; set; }
        public DateTime FineDate { get; set; }

        public string FineDateString
        {
            get
            {
                var dateString = FineDate.ToShortDateString();
                return dateString;
            }
        }
        public string FineDescription { get; set; }

        public Company Company { get; set; }
        public Terminal Terminal { get; set; }
        public FineType FType { get; set; }

        public Fine()
        {

        }
    }
}
