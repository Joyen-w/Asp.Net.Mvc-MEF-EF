using System;
using System.Collections;
using System.Collections.Generic;

namespace DAL.DBContext
{
    public interface IPagedList<out T> : IPagedList, System.Collections.Generic.IEnumerable<T>, System.Collections.IEnumerable
    {
        int Count { get; }
        T this[int index] { get; }
        IPagedList GetMetaData();
    }

    public interface IPagedList
    {
        int PageCount { get; }
        int TotalItemCount { get; }
        int PageIndex { get; }
        int PageSize { get; }
        bool HasPreviousPage { get; }
        bool HasNextPage { get; }
        bool IsFirstPage { get; }
        bool IsLastPage { get; }
        int FirstItemOnPage { get; }
        int LastItemOnPage { get; }
    }
}
