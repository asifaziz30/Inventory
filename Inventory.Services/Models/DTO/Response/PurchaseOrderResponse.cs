using Inventory.Service.Models;

namespace Inventroy.Service.Models.DTO.Response
{
    public class PurchaseOrderResponse : PurchaseOrder
    {
        public short TotalItems { get; set; }
        public string  StatusText { get {return StatusId.ToString(); } }
        public int SerialNumber { get; set; }
    }
}
