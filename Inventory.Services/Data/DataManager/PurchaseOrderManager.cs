using Inventory.Service.Contracts.Data;
using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Microsoft.Extensions.Configuration;
using System.Text.Json;
using System.Data;
using KendoUI = Kendo.Mvc.UI;
using Inventory.Service.Models.DTO.Request;
using System.Collections;
using Inventory.Service.Data.DbFactory;
using Inventory.Services.Helper;
using Inventroy.Service.Models.DTO.Response;

namespace Inventory.Service.Data.DataManager
{
    public class PurchaseOrderManager : DbFactoryBase, IPurchaseOrderManager
    {
        public PurchaseOrderManager(IConfiguration config) : base("ConnetionString", config)
        {
        }
        public async Task<string> Add(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return "";
            }

            var PONumber = (await QueryExecutor.QueryAsync<string>(Sqls.CREATE, new
            {
                pPurchaseOrderDate = purchaseOrder.PurchaseOrderDate,
                pItems = JsonSerializer.Serialize(purchaseOrder.Items.GroupBy(d => d.ItemId )
                        .Select(
                            g => new
                            {
                                ItemId = g.First().ItemId,
                                Quantity = g.Sum(s => s.Quantity),
                                Name = g.First().Name,
                                Price = g.First().Price
                            })),
                pStatusId = purchaseOrder.StatusId,
                pUserName = purchaseOrder.UserName,
                pCreatedBy = purchaseOrder.CreatedBy,
                pCreatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();

            return PONumber;
        }
        public async Task<short> Update(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            
             var record=   (await QueryExecutor.QueryAsync<short>(Sqls.UPDATE, new
            {
                pId = purchaseOrder.Id,
                pPurchaseOrderDate = purchaseOrder.PurchaseOrderDate,
                pStatusId = purchaseOrder.StatusId,
                pUserName = purchaseOrder.UserName,
                pItems = JsonSerializer.Serialize(purchaseOrder.Items),
                pUpdatedBy = purchaseOrder.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
            return record;
        }
        public async Task<bool> Cancel(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<bool>(Sqls.CANCEL, new
            {
                pId = purchaseOrder.Id,
                pUserName = purchaseOrder.UserName,
                pCreatedBy = purchaseOrder.UpdatedBy,
                pCreatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
        }
        public async Task<bool> Receive(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<bool>(Sqls.RECEIVE, new
            {
                pId = purchaseOrder.Id,
                pStatusId = Status.Received,
                pUserName = purchaseOrder.UserName,
                pUpdatedBy = purchaseOrder.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
        }
        public async Task<bool> Delete(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<int>(Sqls.DELETE, new
            {
                pId = purchaseOrder.Id,
                pStatusId = Status.Deleted,
                pUserName = purchaseOrder.UserName,
                pUpdatedBy = purchaseOrder.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow
            }, CommandType.StoredProcedure)).FirstOrDefault() > 0;
        }
        public async Task<bool> RemoveItem(PurchaseOrderItem purchaseOrderItem, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return (await QueryExecutor.QueryAsync<int>(Sqls.REMOVE_ITEM, new
            {
                pId = purchaseOrderItem.Id,
                pPurchaseOrderId = purchaseOrderItem.PurchaseOrderId
            }, CommandType.StoredProcedure)).FirstOrDefault() > 0;
        }
        public async Task<KendoUI.DataSourceResult> Get(KendoUI.DataSourceRequest request, int vendorId, string PONumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var kendoPaging = Common.GetKendoPagingRequest(request);

            
          var record= await QueryExecutor.SPQuerySourceResultAsync<PurchaseOrderResponse>(Sqls.GET, new
            {
                pVendorId = vendorId,
                pPurchaseOrderNumber = PONumber,
                pStartDate = startDate.ToString("yyyy-MM-dd"),
                pEndDate = endDate.ToString("yyyy-MM-dd"),
                pageSize = kendoPaging.PageSize,
                pSkip = kendoPaging.Skip,
                orderByColumn = kendoPaging.SortBy,
                orderBy = kendoPaging.SortByDirection
            }, null, request);
            return record;
        }

        public async Task<IEnumerable<PurchaseOrderResponse>> Get(int vendorId, string PONumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }


            var record = await QueryExecutor.QueryAsync<PurchaseOrderResponse>(Sqls.GET_LIST, new
            {
                pVendorId = vendorId,
                pPurchaseOrderNumber = PONumber,
                pStartDate = startDate.ToString("yyyy-MM-dd"),
                pEndDate = endDate.ToString("yyyy-MM-dd"),
            },commandType:CommandType.StoredProcedure);
            return record;
        }
        public async Task<KendoUI.DataSourceResult> GetItems(KendoUI.DataSourceRequest request, long PurchaseOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var kendoPaging = Common.GetKendoPagingRequest(request);
            return (await QueryExecutor.SPQuerySourceResultAsync<PurchaseOrderItem>(Sqls.GET_ITEMS, new
            {
                pPurchaseOrderId = PurchaseOrderId,
                pageSize = kendoPaging.PageSize,
                pSkip = kendoPaging.Skip,
                orderByColumn = kendoPaging.SortBy,
                orderBy = kendoPaging.SortByDirection
            }, null, request));
        }

        public async Task<IList<PurchaseOrderItem>> GetItems(long PurchaseOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            var record= (await QueryExecutor.QueryAsync<PurchaseOrderItem>(Sqls.GET_ALL_PURCHASE_ITEMS, new
            {
                pPurchaseOrderId = PurchaseOrderId,

            })).ToList();
            return record;
        }
        public async Task<PurchaseOrderResponse> GetById(long id, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }

            var result = (await QueryExecutor.QuerySingleAsync<PurchaseOrderResponse, PurchaseOrderItemRequest>(Sqls.GET_BY_ID, new
            {
                pId = id
            }, CommandType.StoredProcedure));

            PurchaseOrderResponse purchaseOrderResponse = new();
            purchaseOrderResponse = result.Item1;
            purchaseOrderResponse.Items = result.Item2.ToList();
            return purchaseOrderResponse;
        }
        private static partial class Sqls
        {
                   public const string GET = @"sp_purchase_order";
              public const string GET_LIST = @"sp_purchase_order_list";
             public const string GET_ITEMS = @"sp_purchase_order_item";
public const string GET_ALL_PURCHASE_ITEMS = @"sp_purchase_order_all_item";            
                public const string CREATE = @"sp_purchase_order_create";
                public const string DELETE = @"sp_purchase_order_delete";
           public const string REMOVE_ITEM = @"sp_purchase_order_item_delete";
             public const string GET_BY_ID = @"sp_purchase_order_by_id";
                public const string UPDATE = @"sp_purchase_order_update";
                public const string CANCEL = @"sp_purchase_order_cancel";
               public const string RECEIVE = @"sp_purchase_order_receive";
        }
    }
}