using System;
using System.Collections.Generic;
using System.Text;

namespace OpacMauiApp.Models
{
    public class ArrivalCmd
    {

        public int? ArrivalId { get; set; }
        public string ordernumber { get; set; }
        public string arrmrnno { get; set; }
        public DateTime? dateFrom { get; set; }
        public DateTime? dateTo { get; set; }
        public bool Uncataloged { get; set; }
    }
    public class BookArrivalMod
    {
        public int? ArriveId { get; set; }

        public string ArrMrnNo { get; set; }
        public DateTime? ArriveDate { get; set; }

        public string OrderNumber { get; set; }
        public bool IsGift { get; set; }

        public string ItemStatus { get; set; }
        public int NoOfCopies { get; set; }

        public string? Remark { get; set; }
        public decimal? invoiceid { get; set; }
        public string? invoicenumber { get; set; }
        public decimal? orderamount { get; set; }
        public DateTime? OrderDate { get; set; }
        public string? vendorid { get; set; }

        public string? vendor { get; set; }
        public string? Accnos { get; set; }
        public string? instituteName { get; set; }

    }
}
