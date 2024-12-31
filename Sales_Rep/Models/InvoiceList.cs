using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Sales_Rep.Models
{
    public class InvoiceList
    {
        public string InvoiceNo { get; set; }
        public string Pcode { get; set; }

        public string Prdouct { get; set; }

        public double Price { get; set; }
        public double Discount { get; set; }
        public string Sell_Rep { get; set; }
    }
}