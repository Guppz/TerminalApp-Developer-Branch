using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class PaymentTerminal : BaseEntity
    {
        public int IdPaymentTerminal { get; set; }
        public int Amount { get; set; }
        public double PercentageUsed { get; set; }
        public Terminal TerminalPayed { get; set; }
        public Payment PaymentGot { get; set; }

        public PaymentTerminal() {


        }
    }
}
