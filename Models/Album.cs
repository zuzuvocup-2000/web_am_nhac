using System;
using System.Collections.Generic;

namespace webdemo.Models
{
    public partial class Album
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Url { get; set; }
        public sbyte? Publish { get; set; }
        public string? Image { get; set; }
        public int? Memberid { get; set; }
    }
}
