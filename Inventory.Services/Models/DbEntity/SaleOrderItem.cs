using Inventory.Service.Models.DTO.Request;
using Inventory.Services.Models.DbEntity;

namespace Inventory.Service.Models
{
    public class SaleOrderItem : EntityBase
    {
        public long SaleOrderId { get; set; }
        public int ItemId { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }



    }
}