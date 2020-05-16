using System.Collections.Generic;

namespace Core.Helpers
{
    public class Pagination<T> where T : class
    {
    public Pagination(int pageNumber, int pageSize, int count, IReadOnlyList<T> items)
    {
      PageNumber = pageNumber;
      PageSize = pageSize;
      Count = count;
      Items = items;
    }

    public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int Count { get; set; }
        public IReadOnlyList<T> Items { get; set; }
    }
}