using AutoMapper;
using Inventory.Service.Contracts.Data;
using Inventory.Service.Contracts.Service;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;
using Inventory.Service.Data.DataManager;
using Inventory.Services.Helper;

namespace Inventory.Service.Services
{
    public class ItemService : IItemService
    {

        private readonly IMapper mapper;
        private readonly IItemManager ItemManager;
        public ItemService(IMapper _mapper, IItemManager _ItemManager)
        {

            mapper = _mapper;
            ItemManager = _ItemManager;
        }
        public async Task<short> Add(CreateItemRequest ItemRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            var Item = mapper.Map<Item>(ItemRequest);
            return await ItemManager.Add(Item, cancellationToken);
        }
        public async Task<short> Update(UpdateItemRequest ItemRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            var Item = mapper.Map<Item>(ItemRequest);
            return await ItemManager.Update(Item);
        }
        public async Task<bool> Delete(DeleteItemRequest delRequest, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var model = mapper.Map<Item>(delRequest);
            model.StatusId = Status.Deleted;
            return await ItemManager.Delete(model, cancellationToken);
        }
        public async Task<IList<ItemResponse>> Get(string name, string code, int vendorId=0, CancellationToken cancellationToken = default)
        {
            return await ItemManager.Get(name,code, vendorId);
        }
        
        public async Task<Item> GetById(int id, CancellationToken cancellationToken = default)
        {
            return await ItemManager.GetById(id, cancellationToken);
        }
        
        public async Task<DataSourceResult> Get(DataSourceRequest request, string name, string code, int vendorId = 0, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await ItemManager.Get(request, name,code,vendorId);

        }

    }
}
