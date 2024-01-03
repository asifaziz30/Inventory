
using Inventory.Services.Helper;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Service.Models.DTO.Request
{
    public class CreateSaleOrderRequest
    {
        [Required(ErrorMessage = "RequiredAttribute")]
        public DateTime SaleOrderDate { get; set; }
        public string SaleOrderNumber { get; set; }
        public Status StatusId { get; set; }
        public IList<SaleOrderItemRequest> Items { get; set; }
        public bool IsEditable { get; set; }
        public string UserName { get; set; }
        public int CreatedBy { get; set; }

}
    public class UpdateSaleOrderRequest : CreateSaleOrderRequest
    {
        public long Id { get; set; }
        public int UpdatedBy { get; set; }

    }
    public class DeleteSaleOrderRequest
    {
        public long Id { get; set; }
        public Status StatusId { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class SaleOrderItemRequest
    {
        public long SaleOrderId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        [DataType(DataType.Currency)]
        public decimal Price { get; set; }
        [Range(1, int.MaxValue, ErrorMessage = "Please enter a value bigger than {1}")]
        public int Quantity { get; set; }


    }

}