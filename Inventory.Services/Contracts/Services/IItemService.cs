using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;
using Inventory.Service.Models;
using Kendo.Mvc.UI;

namespace Inventory.Service.Contracts.Service
{
    public interface IItemService
    {
        Task<short> Add(CreateItemRequest Item, CancellationToken cancellationToken = default);
        Task<short> Update(UpdateItemRequest Item, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteItemRequest Item, CancellationToken cancellationToken = default);
        Task<IList<ItemResponse>> Get(string name, string code, int vendorId=0, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, string name, string code, int vendorId = 0, CancellationToken cancellationToken = default);
        Task<Item> GetById(int id, CancellationToken cancellationToken = default);
    }
}