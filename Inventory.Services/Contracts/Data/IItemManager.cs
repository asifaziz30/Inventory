using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;
//using GEXP.Helper.Models.DTO.Response;


namespace Inventory.Service.Contracts.Data
{
    public interface IItemManager
    {
        Task<short> Add(Item Item, CancellationToken cancellationToken = default);
        Task<bool> Delete(Item delRequest, CancellationToken cancellationToken = default);
        Task<Item> GetById(int id, CancellationToken cancellationToken = default);
        Task<short> Update(Item Item, CancellationToken cancellationToken = default);
        Task<IList<ItemResponse>> Get(string name, string code, int vendorId=0, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, string name, string code, int vendorId = 0, CancellationToken cancellationToken = default);
    }
}