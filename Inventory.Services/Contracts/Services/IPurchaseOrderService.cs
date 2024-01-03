using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;
using Inventroy.Service.Models.DTO.Response;

namespace Inventory.Service.Contracts.Service
{
    public interface IPurchaseOrderService
    {
        Task<string> Add(CreatePurchaseOrderRequest purchaseOrder, CancellationToken cancellationToken);
        Task<short> Update(UpdatePurchaseOrderRequest purchaseOrder, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeletePurchaseOrderRequest purchaseOrder, CancellationToken cancellationToken = default);
        Task<bool> RemoveItem(PurchaseOrderItem purchaseOrderItem, CancellationToken cancellationToken = default);
        Task<Inventroy.Service.Models.DTO.Response.PurchaseOrderResponse> GetById(long id, CancellationToken cancellationToken = default);
        Task<bool> Cancel(UpdatePurchaseOrderRequest purchaseOrder, CancellationToken cancellationToken = default);
        Task<bool> Receive(UpdatePurchaseOrderRequest purchaseOrder, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, int vendorId, string purchaseOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<IEnumerable<PurchaseOrderResponse>> Get(int vendorId, string PONumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<DataSourceResult> GetItems(DataSourceRequest request, long PurchaseOrderId, CancellationToken cancellationToken = default);
        Task<IList<PurchaseOrderItem>> GetItems(long PurchaseOrderId, CancellationToken cancellationToken = default);
    }
}