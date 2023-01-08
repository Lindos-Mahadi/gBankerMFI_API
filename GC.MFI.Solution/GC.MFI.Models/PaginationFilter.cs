using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models
{
    public class PaginationFilter
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public string search { get; set; }
        public PaginationFilter()
        {
            this.page = 1;
            this.per_page = 10;
            this.search = string.Empty;
        }
        public PaginationFilter(int page, int per_page, string search)
        {
            this.page = page < 1 ? 1 : page;
            this.per_page = per_page > 10 ? 10 : per_page;
            this.search = search;
        }
    }
}
