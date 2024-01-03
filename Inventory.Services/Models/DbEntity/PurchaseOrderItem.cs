using Inventory.Services.Models.DbEntity;

namespace Inventory.Service.Models
{
    public class PurchaseOrderItem : EntityBase
    {
        public long PurchaseOrderId { get; set; }
        public int ItemId { get; set; }
        public float Price { get; set; }
        public string Name { get; set; }
        public int Quantity { get; set; }
    }
}