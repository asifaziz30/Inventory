using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;

namespace Inventory.Service.Contracts.Service
{
    public interface ISaleOrderService
    {
        Task<string> Add(CreateSaleOrderRequest saleOrder, CancellationToken cancellationToken);
        Task<short> Update(UpdateSaleOrderRequest saleOrder, CancellationToken cancellationToken = default);
        Task<bool> Delete(DeleteSaleOrderRequest saleOrder, CancellationToken cancellationToken = default);
        Task<bool> RemoveItem(SaleOrderItem saleOrderItem, CancellationToken cancellationToken = default);
        Task<SaleOrderResponse> GetById(long id, CancellationToken cancellationToken = default);
        Task<bool> Cancel(UpdateSaleOrderRequest saleOrder, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, int vendorId, int customerId, string saleOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<DataSourceResult> GetItems(DataSourceRequest request, long saleOrderId, CancellationToken cancellationToken = default);
        IList<SaleOrderItem> GetItems(long saleOrderId, CancellationToken cancellationToken = default);
    }
}