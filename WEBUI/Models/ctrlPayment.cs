using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WEBUI.Models.Controls.payment
{
    public class ctrlPayment
    {
        public class CustomViewModel
        {
            
            public string StripeToken { get; set; }
            public string StripeBalance { get; set; }
        }
    }
}