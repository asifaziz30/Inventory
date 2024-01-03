using System;
using KendoMvc = Kendo.Mvc;
namespace Inventory.Service.Models.DTO.Response
{
    public class PagingResponse<T> where T : class
    {
        public int TotalRecords { get; set; }
        public int CurrentPageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalPages { get; set; }
        public bool HasNextPage { get; set; }
        public bool HasPreviousPage { get; set; }
        public T Data { get; set; }
        public PagingResponse(T data, int totalRecords, int currentPageNumber, int pageSize)
        {
            Data = data;
            TotalRecords = totalRecords;
            CurrentPageNumber = currentPageNumber;
            PageSize = pageSize;
            // total pages count
            TotalPages = Convert.ToInt32(Math.Ceiling(TotalRecords / (double)pageSize));
            HasNextPage = CurrentPageNumber < TotalPages;
            HasPreviousPage = CurrentPageNumber > 1;
        }
    }

    public class KendoPagingRequest
    {
        public string SortBy { get; set; } = string.Empty;
        public string SortByDirection { get; set; } = string.Empty;
        public int PageSize { get; set; } = 10;
        public int Skip { get; set; } = 0;
        public KendoMvc.FilterDescriptor FilterBy { get; set; }
    }
}
