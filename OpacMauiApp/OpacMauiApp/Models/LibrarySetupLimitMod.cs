using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace OpacMauiApp.Models
{
    public class LibrarySetupLimitMod
    {
        public int? Id { get; set; }
        public string? institutename { get; set; }
        [MaxLength(200)]
        public string? libraryname { get; set; }
        [MaxLength(80)]
        public string? address { get; set; }
        [MaxLength(60)]
        public string? city { get; set; }
        public int? InstId { get; set; }

        public string? Institute { get; set; }
        public string? style { get; set; }
        public bool Sel { get; set; }
        public string? Login { get; set; }
        public string? Password { get; set; }
        public int? Srno { get; set; }
        public string? AuxAddress { get; set; }
        public string? Uniqueid { get; set; }
    }
    public class LibrarySetupLimitMod1
    {
        public int? LibId { get; set; }

        public string LibName { get; set; }
    }
    public class OpacDbsMod
    {
        public int? DataBaseId { get; set; }
        public string ServerName { get; set; }
        public string LoginName { get; set; }
        //        public string DbPassword { get; set; }
        public string DbName { get; set; }
        public DateTime? DateUpdate { get; set; }
        public bool? sel { get; set; }
        public List<Libs>? Libraries { get; set; }
    }
    public class Libs
    {
        public int? LibId { get; set; }
        public string LibName { get; set; }
        public bool sel { get; set; }
    }
}
