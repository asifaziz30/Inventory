using Inventory.Services.Helper;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Inventory.Service.Models.DTO.Request
{
    public class CreateItemRequest
    {
        [Required(ErrorMessage = "RequiredAttribute")]
        [RegularExpression(RegexConstants.AlphaNumericWithSpecialCharacters, ErrorMessage = "Only Number/Alphabets are allowed")]
        [StringLength(150, MinimumLength = 0, ErrorMessage = "InvalidLength150")]

        public string Name { get; set; }
        public decimal Price { get; set; }

        public string Description { get; set; }
        public decimal Quantity { get; set; }
        public Status StatusId { get; set; }

        public string UserName{ get; set; }
        public int CreatedBy { get; set; }
    }
    public class UpdateItemRequest : CreateItemRequest
    {
        public int Id { get; set; }
        public int UpdatedBy { get; set; }
        
    }
    public class DeleteItemRequest
    {
        public int Id { get; set; }
        public Status StatusId { get; set; }
        public int UpdatedBy { get; set; }
    }
}
