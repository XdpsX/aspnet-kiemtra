using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement2.Dtos.Common
{
    public class FilterDto
    {
        [FromQuery(Name = "pageSize")]
        public int PageSize { get; set; } = 2;

        [FromQuery(Name = "pageIndex")]
        public int PageIndex { get; set; } = 1;

        private string _keyword;
        [FromQuery(Name = "keyword")]
        public string? Keyword
        {
            get => _keyword;
            set => _keyword = value?.Trim();
        }

        public int Skip()
        {
            return (PageIndex - 1) * PageSize;
        }
    }
}
