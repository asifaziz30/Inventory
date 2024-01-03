using AutoMapper;
using Inventory.Service.Models;
using Inventory.Service.Contracts.Data;
using Inventory.Service.Contracts.Service;
using Inventory.Service.Models.DTO.Request;
using Inventory.Service.Models.DTO.Response;
using Kendo.Mvc.UI;
using Inventory.Services.Helper;

namespace Inventory.Service.Services
{
    public class SaleOrderService : ISaleOrderService
    {
        private readonly IMapper _mapper;
        private readonly ISaleOrderManager _saleOrderManager;
        public SaleOrderService(IMapper mapper, ISaleOrderManager saleOrderManager)
        {
            _mapper = mapper;
            _saleOrderManager = saleOrderManager;
        }
        public async Task<string> Add(CreateSaleOrderRequest request,  CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return "";
            }
            var saleOrder = _mapper.Map<SaleOrder>(request);
            if (saleOrder is null)
            {
                return "";
            }
            return await _saleOrderManager.Add(saleOrder, cancellationToken);
        }
        public async Task<short> Update(UpdateSaleOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return 0;
            }
            var saleOrder = _mapper.Map<SaleOrder>(request);
            if (saleOrder is null)
            {
                return 0;
            }
            return await _saleOrderManager.Update(saleOrder, cancellationToken);
        }

        public async Task<bool> Delete(DeleteSaleOrderRequest deleteSaleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var saleOrder = _mapper.Map<SaleOrder>(deleteSaleOrder);
            if (saleOrder is null)
            {
                return false;
            }
            saleOrder.StatusId = Status.Deleted;
            return await _saleOrderManager.Delete(saleOrder, cancellationToken);
        }
        public async Task<bool> RemoveItem(SaleOrderItem deleteSaleOrder, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            return await _saleOrderManager.RemoveItem(deleteSaleOrder, cancellationToken);
        }
        public async Task<bool> Cancel(UpdateSaleOrderRequest request, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return false;
            }
            var saleOrder = _mapper.Map<SaleOrder>(request);
            if (saleOrder is null)
            {
                return false;
            }
            return await _saleOrderManager.Cancel(saleOrder);
        }
       
       
        public async Task<DataSourceResult> Get(DataSourceRequest request,int vendorId, int customerId, string saleOrderNumber, DateTime startDate, DateTime endDate, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _saleOrderManager.Get(request, vendorId, customerId, saleOrderNumber, startDate, endDate, cancellationToken);
        }

        
        public async Task<DataSourceResult> GetItems(DataSourceRequest request, long SaleOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _saleOrderManager.GetItems(request, SaleOrderId, cancellationToken);
        }
        public  IList<SaleOrderItem> GetItems(long SaleOrderId, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return  _saleOrderManager.GetItems(SaleOrderId, cancellationToken);
        }
        public async Task<SaleOrderResponse> GetById(long id, CancellationToken cancellationToken = default)
        {
            if (cancellationToken.IsCancellationRequested)
            {
                return null;
            }
            return await _saleOrderManager.GetById(id, cancellationToken);
        }

     }
}