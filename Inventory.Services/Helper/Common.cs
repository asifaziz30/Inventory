using Kendo.Mvc.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KendoMvc = Kendo.Mvc;
using Inventory.Service.Models.DTO.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

namespace Inventory.Services.Helper
{

    public static class Common
    {
        public static IConfiguration Configuration { get; }

        static Common() 
        {
            Configuration = new ConfigurationBuilder()
                       .SetBasePath(Directory.GetCurrentDirectory())
                       .AddJsonFile("appsettings.json")
                       //.AddJsonFile($"appsettings.{environment}.json")
                       .Build();
        }
        public static int[] GetGridPageSizes()
        {
            return Configuration.GetSection("GEXP_Grids:Parent:PageSizes").Value.Split(',').Select(int.Parse).ToArray();
            
        }
        public static KendoPagingRequest GetKendoPagingRequest(DataSourceRequest request)
        {
            if (request is null)
            {
                return new KendoPagingRequest();
            }
            var sort = request.Sorts is not null && request.Sorts.Count > 0 ? request.Sorts[0] : null;
            var sortBy = sort is not null ? sort.Member : FormFields.Id;
            var kendoSortDirection = sort is not null ? (sort.SortDirection) : KendoMvc.ListSortDirection.Ascending;
            var sortDirection = kendoSortDirection is KendoMvc.ListSortDirection.Ascending ? ListSortDirection.Ascending : ListSortDirection.Descending;
            var filterBy = request.Filters is not null && request.Filters.Count > 0 ? request.Filters[0] : null;
            request.Page = request.Page is 0 ? 1 : request.Page;
            return new KendoPagingRequest
            {
                PageSize = request.PageSize,
                Skip = (request.Page - 1) * request.PageSize,
                SortBy = sortBy,
                SortByDirection = sortDirection,
                FilterBy = (filterBy is not null) ? (KendoMvc.FilterDescriptor)filterBy : new KendoMvc.FilterDescriptor(),
            };
        }
    }
    public static class FormFields
    {
        public const string Id = "Id";
        public const string Name = "Name";
        public const string Description = "Description";
        public const string Address = "Address";
    }
    public static class ListSortDirection
    {
        public const string Ascending = "asc";
        public const string Descending = "desc";
    }

    
}
