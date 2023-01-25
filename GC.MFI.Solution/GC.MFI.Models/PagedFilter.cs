using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models
{
    public class PagedFilter
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public string search { get; set; }
        public PagedFilter()
        {
            this.pageNum = 1;
            this.pageSize = 10;
            search = search;
        }
        public PagedFilter(int page, int per_page, string search)
        {
            this.pageNum = page < 1 ? 1 : page;
            this.pageSize = per_page > 10 ? 10 : per_page;
            this.search = search;
        }
    }
}
