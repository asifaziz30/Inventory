using Inventory.Service.Models.DTO.Request;
using Inventory.Services.Helper;
using Inventory.Services.Models.DTO.Response;
using System;
using System.Threading;
using Microsoft.Extensions.Configuration;


namespace Inventory.Controllers
{

    public class ItemController : BaseController
    {
        #region Fields
        private readonly IMapper _mapper;
        private readonly IItemService _itemService;
        #endregion
        public ItemController(IItemService itemService, IMapper mapper, IConfiguration configuration) : base(configuration)
        {
            _mapper = mapper;
            _itemService = itemService;
            
        }
        public IActionResult Index()
        {
           //TempData["resp"] = "1";
            if (TempData["resp"] == null)
                TempData["resp"] = "";
            return View();
        }
        [HttpPost]
        public async Task<DataSourceResult> Read([DataSourceRequest] DataSourceRequest request,string name,string code)
         {

            ViewBag.pagesize=Common.GetGridPageSizes();
            if (name != null)
                name = name.Trim();
            if (code != null)
                code = code.Trim();
            var items = await _itemService.Get(request, name,code);
            return items;
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View(new CreateItemRequest());
        }
        [HttpPost]
        public async Task<IActionResult> Create(CreateItemRequest createItemRequest, CancellationToken cancellationToken=default)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v =>
                {
                    return v.Errors;
                });
                foreach (var error in errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }
            }
            createItemRequest.UserName = UserName;
            createItemRequest.CreatedBy = UserId;
            var response = await _itemService.Add(createItemRequest, cancellationToken);
            TempData["resp"] = (int)response;
            if (response > 0)
            {
                return RedirectToAction(nameof(Index), "Item");
            }
            if (response == -1)
            {
                ModelState.TryAddModelError("AlreadyExists", "");
            }
            return View();
        }
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            if (id is 0)
            {
                return NotFound();
            }
            var updateItem = await _itemService.GetById(id);
            if (updateItem is null)
            {
                return NotFound();
            }
            var updateItemRequest = _mapper.Map<UpdateItemRequest>(updateItem);
            return View(updateItemRequest);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(UpdateItemRequest request)
        {
            request.UpdatedBy = UserId;
            request.UserName = UserName;
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v =>
                {
                    return v.Errors;
                });
            }

            var response = await _itemService.Update(request);
            if(response==1)
            TempData["resp"] = 2;//to distinguish between create and update
            if (response > 0)
            {
                return RedirectToAction(nameof(Index));
            }
            if (response == -1)
            {
                ModelState.AddModelError("NameAlreadyExists", "");
            }
            ModelState.AddModelError("", "Some error occured");
            return View(request);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(DeleteItemRequest request, CancellationToken cancellationToken = default)
        {
            request.UpdatedBy = UserId;
            var deleteResponse = new DeleteResponse();
            if (cancellationToken.IsCancellationRequested)
            {
                deleteResponse.Message = "RequestCancelled";
                return Json(new { deleteResponse });
            }
            var response = await _itemService.Delete(request, cancellationToken);
            
            deleteResponse.Deleted = response;
            return Json(new { Deleted = deleteResponse.Deleted });
        }
        
    }
}