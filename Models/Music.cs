using System;
using System.Collections.Generic;

namespace webdemo.Models
{
    public partial class Music
    {
        public int Id { get; set; }
        public int? Album { get; set; }
        public string? Name { get; set; }
        public string? Artist { get; set; }
        public string? Theloai { get; set; }
        public sbyte? Publish { get; set; }
        public string? Url { get; set; }
        public string? Music1 { get; set; }
        public string? Image { get; set; }
        public string? Memberid { get; set; }
        public int? Viewed { get; set; }
    }
}
