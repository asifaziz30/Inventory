using AutoMapper;
using Inventory.Service.Models;
using Inventory.Service.Contracts.Data;
using Inventory.Service.Contracts.Service;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;
using System.Collections;
using Inventory.Services.Helper;
using Inventroy.Service.Models.DTO.Response;

namespace Inventory.Service.Services
{
    public class PurchaseOrderService : IPurchaseOrderService
    {
        private readonly IMapper _mapper;
        private readonly IPurchaseOrderManager _purchaseOrderManager;
        public PurchaseOrderService(IMapper mapper, IPurchaseOrderManager purchaseOrderManager)
        {
            _mapper = mapper;
            _purchaseOrderManager = purchaseOrderManager;
        }
        public async Task<string> Add(CreatePurchaseOrderRequest request,  CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return "";
            }
            var PurchaseOrder = _mapper.Map<PurchaseOrder>(request);
            if (PurchaseOrder is null)
            {
                return "";
            }
            return await _purchaseOrderManager.Add(PurchaseOrder, cancellationToken);
        }
        public async Task<short> Update(UpdatePurchaseOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            var PurchaseOrder = _mapper.Map<PurchaseOrder>(request);
            if (PurchaseOrder is null)
            {
                return 0;
            }
            return await _purchaseOrderManager.Update(PurchaseOrder, cancellationToken);
        }
        public async Task<bool> Delete(DeletePurchaseOrderRequest deletePurchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var PurchaseOrder = _mapper.Map<PurchaseOrder>(deletePurchaseOrder);
            if (PurchaseOrder is null)
            {
                return false;
            }
            PurchaseOrder.StatusId = Status.Deleted;
            return await _purchaseOrderManager.Delete(PurchaseOrder, cancellationToken);
        }
        public async Task<bool> RemoveItem(PurchaseOrderItem deletePurchaseOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return await _purchaseOrderManager.RemoveItem(deletePurchaseOrder, cancellationToken);
        }
        public async Task<bool> Cancel(UpdatePurchaseOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var PurchaseOrder = _mapper.Map<PurchaseOrder>(request);
            if (PurchaseOrder is null)
            {
                return false;
            }
            return await _purchaseOrderManager.Cancel(PurchaseOrder);
        }
        public async Task<bool> Receive(UpdatePurchaseOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var PurchaseOrder = _mapper.Map<PurchaseOrder>(request);
            if (PurchaseOrder is null)
            {
                return false;
            }
            return await _purchaseOrderManager.Receive(PurchaseOrder);
        }
        public async Task<DataSourceResult> Get(DataSourceRequest request, int vendorId, string purchaseOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _purchaseOrderManager.Get(request, vendorId, purchaseOrderNumber, startDate, endDate, cancellationToken);
        }
        public async Task<IEnumerable<PurchaseOrderResponse>> Get(int vendorId, string purchaseOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _purchaseOrderManager.Get(vendorId, purchaseOrderNumber, startDate, endDate, cancellationToken);
        }
        public async Task<DataSourceResult> GetItems(DataSourceRequest request, long PurchaseOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _purchaseOrderManager.GetItems(request, PurchaseOrderId, cancellationToken);
        }
        public async Task<IList<PurchaseOrderItem>> GetItems(long PurchaseOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _purchaseOrderManager.GetItems(PurchaseOrderId, cancellationToken);
        }
        public async Task<PurchaseOrderResponse> GetById(long id, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _purchaseOrderManager.GetById(id, cancellationToken);
        }
    }
}