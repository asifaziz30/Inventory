using Inventory.Service.Models;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;

namespace Inventory.Service.Contracts.Data
{
    public interface ISaleOrderManager
    {
        Task<string> Add(SaleOrder saleOrder, CancellationToken cancellationToken);
        Task<short> Update(SaleOrder saleOrder,  CancellationToken cancellationToken = default);
        Task<bool> Delete(SaleOrder saleOrder, CancellationToken cancellationToken = default);
        Task<bool> RemoveItem(SaleOrderItem saleOrderItem, CancellationToken cancellationToken = default);
        Task<SaleOrderResponse> GetById(long id, CancellationToken cancellationToken = default);
        Task<bool> Cancel(SaleOrder saleOrder, CancellationToken cancellationToken = default);
        Task<DataSourceResult> Get(DataSourceRequest request, int vendorId, int customerId, string saleOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default);
        Task<DataSourceResult> GetItems(DataSourceRequest request, long saleOrderId, CancellationToken cancellationToken = default);
        IList<SaleOrderItem> GetItems(long saleOrderId, CancellationToken cancellationToken = default);

    }
}