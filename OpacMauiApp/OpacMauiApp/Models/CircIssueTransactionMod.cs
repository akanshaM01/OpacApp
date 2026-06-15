using System;
using System.Collections.Generic;
using System.Text;

namespace OpacMauiApp.Models
{
    public class CircIssueTransactionMod
    {
        public int? IssueId { get; set; }
        public string? userid { get; set; } //foriegn key
        public string? accno { get; set; } //foreigh key
        public DateTime issuedate { get; set; }
        public DateTime duedate { get; set; }
        public string? status { get; set; }
        public decimal? BBSecurity { get; set; }
        public string? userid1 { get; set; }
        public string? RI { get; set; }
        public int? ModeId { get; set; }
    }
}
