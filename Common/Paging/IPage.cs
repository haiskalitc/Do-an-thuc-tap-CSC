using System.Collections.Generic;

namespace Common.Paging
{
    public interface IPage<T> where T : class
    {
        int CurrentPage { get; set; }
        int TotalPage { get; set; }
        int PageSize { get; set; }
        int TotalCount { get; set; }
        IEnumerable<T> Entities { get; set; }
    }
}