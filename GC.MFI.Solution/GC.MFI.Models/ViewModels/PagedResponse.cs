using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GC.MFI.Models.ViewModels
{
    public class PagedResponse<T> : Response<T>
    {
        public int page { get; set; }
        public int per_page { get; set; }
        public int Total { get; set; }
        public int TotalPages { get; set; }
        public PagedResponse(T data, int page, int per_page, int total, int totalPages)
        {
            this.page = page;
            this.per_page = per_page;
            this.Data = data;
            this.Total = total;
            this.TotalPages = totalPages;
            this.Message = null;
            this.Succeeded = true;
            this.Errors = null;
        }
    }
}
