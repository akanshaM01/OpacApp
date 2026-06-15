using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpacMauiApp.Models
{
    public class JournalArrival
    {
        [Key]
        public int? JArriveId { get; set; }
        [MaxLength(60)]
        public string? journal_no { get; set; }

        /******split merge Will Not be in Insert Upate!!!!*******************/
        public int? SplitJrrivalId { get; set; } //if main journal arrival is split, its Jarriveid will be in this collumn
                                                 //   public List<MergedArrivalData>? JSONArr { get; set; } //merged journal arrival list will be stored in this collumn as json string, and will be used for spliting the main journal arrival into multiple journal arrival records when needed.
        public DateTime? date_of_cataloging { get; set; }

        public DateTime? exp_date { get; set; }
        [MaxLength(60)]
        public string? volume { get; set; }
        [MaxLength(100)]
        public string? issues { get; set; }
        [MaxLength(100)]
        public string? parts { get; set; }
        [MaxLength(100)]
        public string? indexes { get; set; }
        [MaxLength(2)]
        public string? Status { get; set; }
        [MaxLength(200)]
        public string? Remarks { get; set; }
        [MaxLength(50)]
        public string? doc_id { get; set; }
        [MaxLength(40)]
        public string? issue_type { get; set; }
        [MaxLength(20)]
        public string? Issued_Status { get; set; } //Issued, Available, Lost, Sold, Writeoff
        [MaxLength(2)]
        public string? Bind_Status { get; set; }
        public DateTime? arr_date { get; set; }
        [MaxLength(30)]
        public string? arr_year { get; set; }
        [MaxLength(100)]
        public string? ISSNNO { get; set; }
        //[MaxLength(2)]
        //public string? accessioned { get; set; }
        [MaxLength(20)]
        public string? accessionnumber { get; set; }
        [MaxLength(20)]
        public string? classno { get; set; }
        [MaxLength(20)]
        public string? bookno { get; set; }

        public int? CurrencyCode { get; set; }
        public decimal? price { get; set; } //both in master and arrival, as price may change at the time of arrival
        [MaxLength(1)]
        public string? Media_Print { get; set; }
        [MaxLength(1)]
        public string? Media_Online { get; set; }
        public DateTime Curr_date { get; set; }
        public DateTime? PublicationDate { get; set; }
        [MaxLength(2)]
        public string? pay_status { get; set; }
        [MaxLength(10)]
        public string? DirectAccessioned { get; set; }
        [MaxLength(10)]
        public string? L_No { get; set; }
        [MaxLength(20)]
        public string? Journal_year { get; set; }
        public int Copy_No { get; set; }
        public int? InstId { get; set; }
        public int? Loc_id { get; set; }
        [NotMapped]
        public string sLocation { get; set; } = "";

        [NotMapped]
        public string? institutename { get; set; }
    }
}
