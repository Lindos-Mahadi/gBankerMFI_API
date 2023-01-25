using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models
{
    public class PaginationFilter<T> where T:class
    {
        public int pageNum { get; set; }
        public int pageSize { get; set; }
        public Expression<Func<T, bool>> search { get; set; }
        public PaginationFilter()
        {
            this.pageNum = 1;
            this.pageSize = 10;
            search = (x => true);
        }
        public PaginationFilter(int page, int per_page, Expression<Func<T, bool>> search)
        {
            this.pageNum = page < 1 ? 1 : page;
            this.pageSize = per_page > 10 ? 10 : per_page;
            this.search = search;
        }
    }
}
