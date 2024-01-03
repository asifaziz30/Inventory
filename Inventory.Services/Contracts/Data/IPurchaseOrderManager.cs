using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Inventroy.Service.Models.DTO.Response;
using Kendo.Mvc.UI;

namespace Inventory.Service.Contracts.Data
{
    public interface IPurchaseOrderManager
    {
        Task<string> Add(PurchaseOrder purchaseOrder, CancellationToken cancellationToken);
        Task<short> Update(PurchaseOrder purchaseOrder,  CancellationToken cancellationToken = default);
        Task<bool> Delete(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);
        Task<bool> RemoveItem(PurchaseOrderItem purchaseOrderItem, CancellationToken cancellationToken = default);
        Task<PurchaseOrderResponse> GetById(long id, CancellationToken cancellationToken = default);
        Task<bool> Cancel(PurchaseOrder purchaseOrder, CancellationToken cancellationToken = default);
        Task<bool> Receive(PurchaseOrder id, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, int vendorId, string purchaseOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<PurchaseOrderResponse>> Get(int vendorId, string PONumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<DataSourceResult> GetItems(DataSourceRequest request, long PurchaseOrderId, CancellationToken cancellationToken = default);
        Task<IList<PurchaseOrderItem>> GetItems(long PurchaseOrderId, CancellationToken cancellationToken = default);
    }
}