using Inventory.Services.Helper;
using Inventory.Services.Models.DTO.Response;

namespace Inventory.Controllers
{

    public class SaleOrderController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        private readonly ISaleOrderService _saleOrderService;
        
        #endregion
        public SaleOrderController(ISaleOrderService saleOrderService,IItemService itemService, IMapper mapper, IConfiguration configuration) : base(configuration)
        {
            _mapper = mapper;
            _saleOrderService = saleOrderService;
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
        public async Task<DataSourceResult> Read([DataSourceRequest] DataSourceRequest request,int customerId, DateTime startDate, DateTime endDate, string saleOrderNumber = "")
        {
            
            var SaleOrder = await _saleOrderService.Get(request,  0, customerId, saleOrderNumber,startDate, endDate);
            return SaleOrder;
        }

        public async Task<DataSourceResult> ReadSaleOrderItems([DataSourceRequest] DataSourceRequest request, long SaleOrderId)
        {
            var SaleOrderItems =  await _saleOrderService.GetItems(request,SaleOrderId);
            
            return SaleOrderItems;
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            ViewBag.ItemId = await _itemService.Get("","",0);
            return View(new CreateSaleOrderRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateSaleOrderRequest createSaleOrderRequest, CancellationToken cancellationToken=default)
        {
            
            createSaleOrderRequest.StatusId = Status.Created;
            createSaleOrderRequest.UserName = UserName;
            createSaleOrderRequest.CreatedBy = UserId;
            var response = await _saleOrderService.Add(createSaleOrderRequest, cancellationToken);
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
            var SaleOrder = await _saleOrderService.GetById(id);
            if (SaleOrder is null)
            {
                return NotFound();
            }
            if (SaleOrder.StatusId == Status.Picked)
            {
                return RedirectToAction("Index");
            }
            ViewBag.ItemId = await _itemService.Get("", "", UserId);
            var updateSaleOrderRequest = _mapper.Map<UpdateSaleOrderRequest>(SaleOrder);
            return View(updateSaleOrderRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateSaleOrderRequest updateSaleOrderRequest)
        {
            updateSaleOrderRequest.UpdatedBy = UserId;
            updateSaleOrderRequest.StatusId = Status.Created;
            updateSaleOrderRequest.UserName = UserName;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v =>
                {
                    return v.Errors;
                });
            }

            var response = await _saleOrderService.Update(updateSaleOrderRequest);
            if(response==1)
               TempData["resp"] = 2;//to distinguish between create and update
            return Json(response);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteSaleOrderRequest request, CancellationToken cancellationToken = default)
        {
            request.UpdatedBy = UserId;
            var deleteResponse = new DeleteResponse();
            if (cancellationToken.IsCancellationRequested)
            {
                deleteResponse.Message = "RequestCancelled";
                return Json(new { deleteResponse });
            }
            var response = await _saleOrderService.Delete(request, cancellationToken);
            
            deleteResponse.Deleted = response;
            return Json(new { Deleted = deleteResponse.Deleted });
        }
        
        [HttpPost]
        
        public  async Task<IActionResult> GetItemForSaleOrder(int Itemid)
        {
            var item = await _itemService.GetById(Itemid);
            CreateSaleOrderRequest request = new CreateSaleOrderRequest();
            return PartialView("_PartialItem",request);
        }
        
        
        
        #region Utilities
        
        [HttpPost]
        public async Task<IActionResult> AddNewItem(int[] ItemIds, int selectedItem)
        {
            var item1 = new ItemResponse
            {
                 Name="Item 1",
                 Price=15,
                 Quantity=44,
                 Id=1,
                 
            };
            var item2 = new ItemResponse
            {
                Name = "Item 2",
                Price = 7,
                Quantity = 5,
                Id = 2,

            }; var item3 = new ItemResponse
            {
                Name = "Item 3",
                Price = 5,
                Quantity = 6,
                Id = 3

            };
            var itemlist = new List<ItemResponse>();
            itemlist.Add(item1);
            itemlist.Add(item2);
            itemlist.Add(item3);

            var items = await _itemService.Get("","",0);
           
                foreach (var item in ItemIds)
                {
                    items.Remove(items.Where(x => x.Id == item).FirstOrDefault());
                }
            //}
            ViewBag.ItemId = items;
            ViewBag.selectedId= selectedItem;
            return PartialView("_PartialItem", new SaleOrderItemRequest());
        }
       
      
        #endregion
    }
}