using System.Collections.Generic;

namespace Common.Paging
{
    public class Page<T> : IPage<T> where T : class
    {
        public int CurrentPage { get; set; }
        public int TotalPage { get; set; }
        public int PageSize { get; set; }
        public int TotalCount { get; set; }
        public IEnumerable<T> Entities { get; set; }
        public Page() { }

        public Page(int currentPage, int pageSize, int totalCount)
        {
            CurrentPage = currentPage;
            TotalCount = totalCount;
            PageSize = pageSize;
            PageRecalculator();
        }

        public Page(int currentPage, int pageSize, int totalCount, IEnumerable<T> entities)
            : this(currentPage, pageSize, totalCount)
        {
            Entities = entities;
        }
        public void PageRecalculator()
        {
            TotalPage = TotalCount / PageSize;
            if (TotalPage * PageSize < TotalCount) ++TotalPage;
            if (TotalPage < 1) TotalPage = 1;
            if (CurrentPage < 1) CurrentPage = 1;
            if (CurrentPage > TotalPage) CurrentPage = TotalPage;
        }
    }
}