using System;
using System.Collections.Generic;
using System.Text;

namespace OpacMauiApp.Models
{
    public class DigitalContMod
    {
        public int? Id { get; set; }

        public string Title { get; set; }

        public string Descr { get; set; }

        public string UrlIfAny { get; set; }

        public string GivenFileName { get; set; }

        public string StoredFileName { get; set; }

        public string AppliedMembCodes { get; set; }

        public string AppliedAccnos { get; set; }
        public int? ContentTypeId { get; set; }  //FK
        public string? ContentType { get; set; }
        public int? AudienceId { get; set; }  //FK
        public int? MemberGroupId { get; set; }
        public string? Audience { get; set; }
        public DateTime? DateSaved { get; set; }
        public DateTime? ValidTill { get; set; } //if applied
        public bool? ForAllUsers { get; set; }
        public string? ForMember { get; set; } //member created and owner
        public int? NoOfFiles { get; set; }
        public string Files { get; set; }
        public int? GroupId { get; set; }
        public string? GroupName { get; set; }
        public int? ProgSubjId { get; set; }
        public string? LibraryName { get; set; }
        public string? District { get; set; }

        public int? SubjectId { get; set; }
        public int? CurrencyCode { get; set; }
        public decimal? exRate { get; set; }
        public string? Thumbnail { get; set; }
        public int? categoryId { get; set; }
        public string? categoryLoadStatus { get; set; }
    }
}
