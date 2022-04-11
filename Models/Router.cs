using System;
using System.Collections.Generic;

namespace webdemo.Models
{
    public partial class Router
    {
        public int Id { get; set; }
        public int? Objectid { get; set; }
        public string? Module { get; set; }
        public string? Url { get; set; }
    }
}
