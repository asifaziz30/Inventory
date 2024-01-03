using Inventory.Services.Helper;
using Inventory.Services.Models.DTO.Response;
using System.Threading;

namespace Inventory.Controllers
{

    public class PurchaseOrderController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        private readonly IPurchaseOrderService _purchaseOrderService;



        #endregion
        public PurchaseOrderController(IPurchaseOrderService purchaseOrderService, IItemService itemService,  IMapper mapper, IConfiguration configuration) : base(configuration)
        {
            _mapper = mapper;
            _purchaseOrderService = purchaseOrderService;
            _itemService = itemService;
        }
        public async Task<IActionResult> Index()
        {
            //TempData["resp"] = "1";
            if (TempData["resp"] == null)
                TempData["resp"] = "";
            return View();
        }
        [HttpPost]
        public async Task<DataSourceResult> Read([DataSourceRequest] DataSourceRequest request, int vendorId, string purchaseOrderNumber, DateTime startDate, DateTime endDate)
        {
            if (purchaseOrderNumber != null)
                purchaseOrderNumber = purchaseOrderNumber.Trim();
            if (purchaseOrderNumber == null)
                purchaseOrderNumber = "";

            var purchaseOrder = await _purchaseOrderService.Get(request, 0, purchaseOrderNumber, startDate, endDate);
            return purchaseOrder;
        }

        public async Task<DataSourceResult> ReadPurchaseOrderItems([DataSourceRequest] DataSourceRequest request, long purchaseOrderId)
        {
            var purchaseOrderItems = await _purchaseOrderService.GetItems(request, purchaseOrderId);
            return purchaseOrderItems;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            try
            {
                // ViewBag.ItemId = await _itemService.Get("", "", UserId);
                return View(new CreatePurchaseOrderRequest());
            }
            catch (Exception ex)
            {

                var test = ex.Message;
                return View(new CreatePurchaseOrderRequest());
            }

        }
        [HttpPost]
        public async Task<IActionResult> Create(CreatePurchaseOrderRequest createPurchaseOrderRequest, CancellationToken cancellationToken = default)
        {

            createPurchaseOrderRequest.StatusId = Status.Created;
            createPurchaseOrderRequest.UserName = UserName;
            createPurchaseOrderRequest.CreatedBy = UserId;
            var response = await _purchaseOrderService.Add(createPurchaseOrderRequest, cancellationToken);
            TempData["resp"] = response;

            return Json(string.IsNullOrEmpty(response) ? 0 : 1);
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id is 0)
            {
                return NotFound();
            }
            var purchaseOrder = await _purchaseOrderService.GetById(id);
            if (purchaseOrder is null)
            {
                return NotFound();
            }

           // ViewBag.ItemId = await _itemService.Get("", "", UserId);
            var updatePurchaseOrderRequest = _mapper.Map<UpdatePurchaseOrderRequest>(purchaseOrder);
            return View(updatePurchaseOrderRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdatePurchaseOrderRequest updatePurchaseOrderRequest)
        {
            updatePurchaseOrderRequest.UpdatedBy = UserId;
            updatePurchaseOrderRequest.StatusId = Status.Created;
            //request.UserName = UserName;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v =>
                {
                    return v.Errors;
                });
            }

            var response = await _purchaseOrderService.Update(updatePurchaseOrderRequest);
            if (response == 1)
                TempData["resp"] = 2;//to distinguish between create and update
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeletePurchaseOrderRequest request, CancellationToken cancellationToken = default)
        {
            request.UpdatedBy = UserId;
            var deleteResponse = new DeleteResponse();
            if (cancellationToken.IsCancellationRequested)
            {
                deleteResponse.Message = "RequestCancelled";
                return Json(new { deleteResponse });
            }
            var response = await _purchaseOrderService.Delete(request, cancellationToken);

            deleteResponse.Deleted = response;
            return Json(new { deleteResponse.Deleted });
        }
       

       

        [HttpGet]
        public async Task<IActionResult> GetItemForPurchaseOrder(int Itemid)
        {
            var item = await _itemService.GetById(Itemid);
            CreatePurchaseOrderRequest request = new CreatePurchaseOrderRequest();
            return PartialView("_PartialItem", request);
        }
        [HttpPost]
        public async Task<bool> ReceivePurChaseOrder(int purchaseOrderId)
        {
            var updatePurchaseOrder = new UpdatePurchaseOrderRequest();
            updatePurchaseOrder.Id = Convert.ToInt32(purchaseOrderId);
            updatePurchaseOrder.UserName = UserName;
            updatePurchaseOrder.UpdatedBy = UserId;
            updatePurchaseOrder.StatusId = Status.Received;
            return await _purchaseOrderService.Receive(updatePurchaseOrder);

        }
        #region Utilities
        [HttpPost]
        public async Task<IActionResult> AddNewItem(int[] ItemIds, int selectedItem)
        {
            var items = await _itemService.Get("", "", UserId);
            foreach (var item in ItemIds)
            {
                items.Remove(items.Where(x => x.Id == item).First());
            }
            ViewBag.ItemId = items;
            ViewBag.selectedId = selectedItem;
            return PartialView("_PartialItem", new PurchaseOrderItemRequest());
        }
        
        #endregion
    }
}