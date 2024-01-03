using Inventory.Service.Data.DbFactory;
using Inventory.Service.Contracts.Data;
using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Microsoft.Extensions.Configuration;
using System.Data;
using Inventory.Services.Helper;
using KendoRequest = Kendo.Mvc.UI;

namespace Inventory.Service.Data.DataManager
{
    public class ItemManager : DbFactoryBase, IItemManager
    {
        public ItemManager(IConfiguration config) : base("Server=localhost:3307;Database=gexp_fulfilment;User Id=root;Password=root;", config)
        {
        }
        public async Task<short> Add(Item model, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            
                 var record=(await QueryExecutor.QueryAsync<short>(Sqls.CREATE_ITEM, new
            {
                pName = model.Name,
                pDescription = model.Description,
                pQuantity = model.Quantity,
                pPrice = model.Price,
                pCreatedBy = model.CreatedBy,
                pCreatedOn = DateTime.UtcNow,
                pStatusId= model.StatusId,
            }, CommandType.StoredProcedure)).FirstOrDefault();
            return record;
        }
        public async Task<bool> Delete(Item delModel, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            else
            {
                await QueryExecutor.QueryAsync<short>(Sqls.DELETE_ITEM, new
                {
                    pId = delModel.Id,
                    pStatusId = delModel.StatusId,
                    pUpdatedBy = delModel.UpdatedBy,
                    pUpdatedOn = DateTime.UtcNow,
                    pUserName=""

                }, CommandType.StoredProcedure);
            }
            return true;
        }
        public async Task<IList<ItemResponse>> Get(string name, string code, int vendorId=0, CancellationToken cancellationToken = default)
        {
            var record = (await QueryExecutor.QueryAsync<ItemResponse>(Sqls.ITEM, new
            {
                pName=name, 
                pCode=code,
                pVendorId= vendorId

            }, CommandType.StoredProcedure)).ToList();
            return record;
        }
        public async Task<KendoRequest.DataSourceResult> Get(KendoRequest.DataSourceRequest request,string name, string code, int vendorId = 0, CancellationToken cancellationToken = default)
        {
            var kendoPaging = Common.GetKendoPagingRequest(request);
            var record = (await QueryExecutor.SPQuerySourceResultAsync<ItemResponse>(Sqls.ITEM_PAGING, new
            {
                pName = name,
                pCode = code,
                pVendorId = vendorId,
                pageSize = kendoPaging.PageSize,
                pSkip = kendoPaging.Skip,
                orderByColumn = kendoPaging.SortBy,
                orderBy = kendoPaging.SortByDirection

            },null,request));
            return record;
        }
        public async Task<Item> GetById(int id, CancellationToken cancellationToken = default)
        {
            return (await QueryExecutor.QuerySingleAsync<Item>(Sqls.ITEM_BY_ID, new
            {
                pId = id
            }, CommandType.StoredProcedure));
        }
        public async Task<short> Update(Item model, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            return (await QueryExecutor.QueryAsync<short>(Sqls.UPDATE_ITEM, new
            {
                pName = model.Name,
                pId=model.Id,
                pDescription = model.Description,
                pQuantity = model.Quantity,
                pPrice = model.Price,
                pUpdatedBy = model.UpdatedBy,
                pUpdatedOn = DateTime.UtcNow,
            }, CommandType.StoredProcedure)).FirstOrDefault();
        }

        

        private static partial class Sqls
        {
                   public const string ITEM = @"sp_Item";
            public const string ITEM_PAGING = @"sp_item_paging";
            public const string CREATE_ITEM = @"sp_Item_create";
            public const string UPDATE_ITEM = @"sp_Item_update";
            public const string DELETE_ITEM = @"sp_Item_delete";
             public const string ITEM_BY_ID = @"sp_Item_by_id";
            
        }
    }
}