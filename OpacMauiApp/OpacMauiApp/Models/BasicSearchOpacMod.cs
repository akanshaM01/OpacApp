using System;
using System.Collections.Generic;
using System.Text;

namespace OpacMauiApp.Models
{
    public class BasicSearchOpacMod
    {
        public int? sno { get; set; }
        public long ctrl_no { get; set; }
        public string? Control001 { get; set; }

        public string Title { get; set; }
        public string Author { get; set; }
        public string Publisher { get; set; }
        public string Subject { get; set; }
        public string Isbn { get; set; }

        public string CallNo { get; set; }
        public int? NoCopies { get; set; }
        public int? LibId { get; set; }
        public string? LibName { get; set; }

    }
    public class BasicSearchReqAdo
    {
        public bool OnTitle { get; set; }
        public string? Title { get; set; }
        public bool OnAuthor { get; set; }
        public string? Author { get; set; }
        public bool OnPublisher { get; set; }
        public string? Publisher { get; set; }
        public bool OnKeyword { get; set; }
        public string? Keywords { get; set; }
        public bool OnSubject { get; set; }
        public string? Subject { get; set; }
        public bool OnLocation { get; set; }
        public string? Location { get; set; }
        public bool OnIsbn { get; set; }
        public string? Isbn { get; set; }
        public bool OnClassNo { get; set; }
        public string? ClassNo { get; set; }
        public int? publyearFr { get; set; }
        public int? publyearTo { get; set; }
        public int? LangId { get; set; }
        public int? ItemStatId { get; set; }
        public string? PublPlace { get; set; }
        //  public List<int?> libIds { get; set; }
        // public List<int> DBIds { get; set; } = new List<int>();
        //public int? DataBase { get; set; }
        public Int16 SetNo { get; set; } = 1;
        public List<DbsLibs> DatabasesLibs { get; set; }

    }
    public class DbsLibs
    {
        public int? DatabaseId { get; set; }
        public List<int?>? libIds { get; set; }
    }
    public class TitleAuthAICmd
    {
        public string Title { get; set; }
        public string Author { get; set; }
        public string TitleAuthor { get; set; }

    }
}
