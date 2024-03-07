using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Dtos.Common
{
    public class PageResultDto<T>
    {
        public IEnumerable<T> Items { get; set; }

        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public int TotalItem { get; set; }
    }
}
