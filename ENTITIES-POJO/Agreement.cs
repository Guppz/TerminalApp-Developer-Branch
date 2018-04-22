using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Agreement: BaseEntity
    {
        public int IdAgreement { get; set; }
        public string InstituteName { get; set; }
        public string InstituteEmail { get; set; }
        public Terminal Terminal { get; set; }
        public AgreementType AgreementType { get; set; }

        public Agreement()
        {

        }
    }
}