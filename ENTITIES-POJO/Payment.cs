using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace ENTITIES_POJO
{
    public class Payment : BaseEntity
    {
        public int IdPayment { get; set; }
        public DateTime Date { get; set; }
        public int Amount { get; set; }
        public string Detail { get; set; }
        public Glosary Term { get; set; }
        public User IssuerUser { get; set; }
        public User ReceiverUser { get; set; }
        public Card Card { get; set; }
        public Glosary PaymentType { get; set; }

        public Payment()
        {

        }
    }
}
