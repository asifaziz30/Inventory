using Inventory.Service.Models.DTO.Request;
using Inventory.Services.Helper;
using Inventory.Services.Models.DbEntity;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Service.Models
{
   
    public class SaleOrder : EntityBase
    {
        public string SaleOrderNumber { get; set; }
        [Required(ErrorMessage = "RequiredAttribute")]
        public DateTime SaleOrderDate { get; set; }
        public IList<SaleOrderItemRequest> Items { get; set; }
        public bool IsEditable { get; set; }
        public Status StatusId { get; set; }
        public string UserName { get; set; }
    }

   
}