using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ENTITIES_POJO
{
    public class Card : BaseEntity
    {
        public string IdCard { get; set; }
        public DateTime ExpiryDate { get; set; }
        public DateTime Notification { get; set; }
        public double Balance { get; set; }
        public int Status { get; set; }
        public String StatusString { get; set; }
        public int DaysForNotification { get; set; }
        public int newBalance { get; set; }
        public User User { get; set; }
        public Terminal Terminal { get; set; }
        public CardType CrType { get; set; }
        public Agreement agreement { get; set; }

        public Card()
        {
        }
    }
}
