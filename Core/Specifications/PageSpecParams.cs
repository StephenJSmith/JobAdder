using System;

namespace Core.Specifications
{
  public class PageSpecParams
  {
    private int _maxPageSize = 50;

    public int PageNumber { get; set; } = 1;

    private int _defaultPageSize = 10;

    private int _setPageSize;

    public int PageSize
    {
      get
      {
        var pagesize = (_setPageSize != 0) ? _setPageSize : _defaultPageSize;
        var effectivePageSize = Math.Min(pagesize, _maxPageSize);

        return effectivePageSize;
      }
      set => _setPageSize = value;
    }

    public int PageItems { get; set; }

    public void ApplyConfigurationDefaults(int maxPageSize, int defaultPageSize)
    {
      _maxPageSize = maxPageSize;
      _defaultPageSize = defaultPageSize;
    }

    public int Skip
    {
      get => PageSize * (PageNumber - 1);
    }

    public int Take
    {
      get => PageSize;
    }
  }
}