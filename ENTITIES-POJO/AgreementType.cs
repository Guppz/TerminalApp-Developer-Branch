using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class AgreementType: BaseEntity
    {
        public int IdAgreementType { get; set; }
        public string AgreementTypeName { get; set; }

        public AgreementType()
        {

        }
    }
}