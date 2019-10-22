using System;
using System.Collections.Generic;

namespace Models
{
    public partial class LibyanTenders
    {
        public long TenderId { get; set; }
        public string Title { get; set; }
        public string Field { get; set; }
        public DateTime CreatedOn { get; set; }
    }
}
