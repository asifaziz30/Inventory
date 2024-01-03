using Inventory.Services.Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Service.Models.DTO.Request
{
    public class CreatePurchaseOrderRequest
    {
        public string PurchaseOrderNumber { get; set; }
        public DateTime PurchaseOrderDate { get; set; }
        public Status StatusId { get; set; }
        public IList<PurchaseOrderItemRequest> Items { get; set; }
        public bool IsEditable { get; set; }
        public string UserName { get; set; }
        public int CreatedBy { get; set; } 
    }
    public class UpdatePurchaseOrderRequest : CreatePurchaseOrderRequest
    {
        public long Id { get; set; }
        public int UpdatedBy { get; set; }
        
    }
    public class DeletePurchaseOrderRequest
    {
        public long Id { get; set; }
        public Status StatusId { get; set; }
        public int UpdatedBy { get; set; }
    }
    public class PurchaseOrderItemRequest
    {
        public long PurchaseOrderId { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int Quantity { get; set; }    }
}
