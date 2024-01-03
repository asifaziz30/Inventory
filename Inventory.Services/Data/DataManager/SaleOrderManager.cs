using Inventory.Service.Contracts.Data;
using Inventory.Service.Models;
using Inventory.Service.Data.DbFactory;
using Inventory.Service.Models.DTO.Response;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data;
using KendoUI = Kendo.Mvc.UI;
using Inventory.Services.Helper;
using Inventory.Service.Models.DTO.Request;

namespace Inventory.Service.Data.DataManager
{
    public class SaleOrderManager : DbFactoryBase, ISaleOrderManager
    {
        private readonly IConfiguration _configuration;
        public SaleOrderManager(IConfiguration config) : base("ConnectionString", config)
        {
            _configuration = config;
        }
        public async Task<string> Add(SaleOrder saleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return "";
            }

            var SONumber = (await QueryExecutor.QueryAsync<string>(Sqls.CREATE, new
            {
                pSaleOrderDate = saleOrder.SaleOrderDate,
                pItems = JsonSerializer.Serialize(saleOrder.Items.GroupBy(d => d.ItemId)
                        .Select(
                            g => new
                            {
                                ItemId = g.First().ItemId,
                                Quantity = g.Sum(s => s.Quantity),
                                Name = g.First().Name,
                                Price = g.First().Price,
                            })),
                pStatusId = saleOrder.StatusId,
                pUserName = saleOrder.UserName,
                pCreatedBy = saleOrder.CreatedBy,
                pCreatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();

            return SONumber;
        }
        public async Task<short> Update(SaleOrder saleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            
             var record=   (await QueryExecutor.QueryAsync<short>(Sqls.UPDATE, new
            {
                pId = saleOrder.Id,
                pSaleOrderDate = saleOrder.SaleOrderDate,
                pStatusId = saleOrder.StatusId,
                pUserName = saleOrder.UserName,
                pItems = JsonSerializer.Serialize(saleOrder.Items.GroupBy(d => d.ItemId)
                        .Select(
                            g => new
                            {
                                ItemId = g.First().ItemId,
                                Quantity = g.Sum(s => s.Quantity),
                                Name = g.First().Name,
                                Price = g.First().Price,
                            })),
                pUpdatedBy = saleOrder.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
            return record;
        }
        public async Task<bool> Cancel(SaleOrder saleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<bool>(Sqls.CANCEL, new
            {
                pId = saleOrder.Id,
                pUserName = saleOrder.UserName,
                pCreatedBy = saleOrder.UpdatedBy,
                pCreatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
        }
        public async Task<bool> Delete(SaleOrder saleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<int>(Sqls.DELETE, new
            {
                pId = saleOrder.Id,
                pStatusId = Status.Deleted,
                pUserName = saleOrder.UserName,
                pUpdatedBy = saleOrder.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow
            }, CommandType.StoredProcedure)).FirstOrDefault() > 0;
        }
        public async Task<bool> RemoveItem(SaleOrderItem saleOrderItem, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<int>(Sqls.REMOVE_ITEM, new
            {
                pId = saleOrderItem.Id,
                pSaleOrderId = saleOrderItem.SaleOrderId
            }, CommandType.StoredProcedure)).FirstOrDefault() > 0;
        }
        public async Task<KendoUI.DataSourceResult> Get(KendoUI.DataSourceRequest request, int vendorId, int customerId, string SONumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var kendoPaging = Common.GetKendoPagingRequest(request);
            var record= (await QueryExecutor.SPQuerySourceResultAsync<SaleOrderResponse>(Sqls.GET, new
            {
                
                pSaleOrderNumber = SONumber.Trim(),
                pStartDate = startDate.ToString("yyyy-MM-dd"),
                pEndDate = endDate.ToString("yyyy-MM-dd"),
                pageSize = kendoPaging.PageSize,
                pSkip = kendoPaging.Skip,
                orderByColumn = kendoPaging.SortBy,
                orderBy = kendoPaging.SortByDirection
            }, null, request));
             return record;
        }

        

        public async Task<KendoUI.DataSourceResult> GetItems(KendoUI.DataSourceRequest request, long saleOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var kendoPaging = Common.GetKendoPagingRequest(request);
            var records= (await QueryExecutor.SPQuerySourceResultAsync<SaleOrderItem>(Sqls.GET_ITEMS, new
            {
                pSaleOrderId = saleOrderId,
                pageSize = kendoPaging.PageSize,
                pSkip = kendoPaging.Skip,
                orderByColumn = kendoPaging.SortBy,
                orderBy = kendoPaging.SortByDirection
            }, null, request));
            return records;
        }

        public  IList<SaleOrderItem> GetItems(long saleOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var record= (QueryExecutor.QueryAsync<SaleOrderItem>(Sqls.GET_ITEMS, new
            {
                pSaleOrderId = saleOrderId,

            }));
            return record.Result.ToList();
        }
        public async Task<SaleOrderResponse> GetById(long id, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var result = (await QueryExecutor.QuerySingleAsync<SaleOrderResponse, SaleOrderItemRequest>(Sqls.GET_BY_ID, new
            {
                pId = id
            }, CommandType.StoredProcedure));

            var saleOrderResponse = new SaleOrderResponse();

            if (result is not null && result.Item1 != null)
            {
                
                saleOrderResponse = result.Item1;
              
            }

            return saleOrderResponse;
        }

       private static partial class Sqls
        {
            public const string GET = @"sp_sale_order";
       public const string GET_LIST = @"sp_sale_order_list";
      public const string GET_ITEMS = @"sp_sale_order_item";
         public const string CREATE = @"sp_sale_order_create";
         public const string DELETE = @"sp_sale_order_delete";
    public const string REMOVE_ITEM = @"sp_sale_order_item_delete";
      public const string GET_BY_ID = @"sp_sale_order_by_id";
         public const string UPDATE = @"sp_sale_order_update";
         public const string CANCEL = @"sp_sale_order_cancel";
        }
    }
}