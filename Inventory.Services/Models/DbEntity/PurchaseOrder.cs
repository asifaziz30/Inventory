//
using Inventory.Service.Models.DTO.Request;
using Inventory.Services.Helper;
using Inventory.Services.Models.DbEntity;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Service.Models
{
    public class PurchaseOrder : EntityBase
    {
        public string PurchaseOrderNumber { get; set; }
        [Required(ErrorMessage = "RequiredAttribute")]
        public DateTime PurchaseOrderDate { get; set; }
        [Required(ErrorMessage = "RequiredAttribute")]
        public IList<PurchaseOrderItemRequest> Items { get; set; }
        public bool IsEditable { get; set; }
        public Status StatusId { get; set; }
        public string UserName { get; set; }
    }
}