using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpacMauiApp.Models
{
    public class JournalIssueMod
    {
        [Key]
        public int? IssueId { get; set; }
        public string MemberCode { get; set; }

        //either of one is applicable below
        public string? ArrivalAccNo { get; set; }  //arrival issue (Non Bind)
        public string? CatalogAccNo { get; set; } //catalog issue binding complete, all issues arrived and catalogued
        public string Journal_no { get; set; }
        public DateTime? IssueDate { get; set; }
        public DateTime? DueDate { get; set; }
        public DateTime? EntDate { get; set; }
        public string IssStatus { get; set; } //Issue, Returned, Lost
        public decimal? FineAmt { get; set; }
        public string? FineCause { get; set; }
        public string? Remark { get; set; }

        public int? JArriveId { get; set; }
        public int? InstId { get; set; }
    }
}
