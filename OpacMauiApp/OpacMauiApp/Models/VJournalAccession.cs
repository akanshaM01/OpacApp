using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace OpacMauiApp.Models
{
    public class VJournalAccession
    {

        public int JArriveId { get; set; }
        public string? accessionnumber { get; set; }
        public DateTime? arr_date { get; set; }
        public string? publication_Date { get; set; }
        public string? Issued_Status { get; set; }
        public string? doc_id { get; set; }
        public string? L_No { get; set; }
        public string? volume { get; set; }
        public string? issues { get; set; }
        public string? parts { get; set; }
        public int? Copy_No { get; set; }
        public int? Loc_id { get; set; }
        public string? ClassNo { get; set; }
        public string? BookNo { get; set; }
        public int? CurrencyCode { get; set; }
        public decimal? price { get; set; }
        public DateTime? date_of_cataloging { get; set; }

        public string journal_no { get; set; }
        public string? subscription_no { get; set; }
        public string? journal_title { get; set; }
        public string? title_abbreviation { get; set; }
        public string? departmentname { get; set; }
        public DateTime? entry_date { get; set; }
        public string? issn { get; set; }

        public int? Journal_Id { get; set; }
        public int? total_volume { get; set; }
        public int? issue_per_volume { get; set; }
        public int? part_per_issue { get; set; }
        public string? frequency { get; set; }
        public string? Journal_status { get; set; }
        public DateTime? PublicationDate { get; set; }
        public int? NOC { get; set; }

        public string? publisher { get; set; }
        public string? agent { get; set; }

        public string? PublisherCode { get; set; }
        public string? firstname { get; set; }
        public string? peraddress { get; set; }
        public string? percity { get; set; }

        public string? vendorname { get; set; }
        public string? VendorAddress { get; set; }
        public string? AgentCity { get; set; }
        [NotMapped]
        public bool ShowMore { get; set; }
    }
    public class AccnSearchAdv
    {
        public string? Accno { get; set; }
        public string? Title { get; set; }
        public string? Dept { get; set; }
        public string? Keyword { get; set; }
        public string? Issn { get; set; }
        public string? Location { get; set; }
        public string? publisher { get; set; }
        public string? Agent { get; set; }
        public string? TitleDiff { get; set; }
        public string? PublisherDiff { get; set; }
        public string? AgentDiff { get; set; }
        public string? DeptDiff { get; set; }

        public DateTime? EntryDateFrom { get; set; }
        public DateTime? EntryDateTo { get; set; }
        public int Skip { get; set; } = 0;
        public int Take { get; set; } = 50;
    }
}
