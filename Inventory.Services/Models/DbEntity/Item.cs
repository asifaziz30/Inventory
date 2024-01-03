using Inventory.Service.Models.DTO.Request;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Inventory.Services.Models.DbEntity;
using System.Drawing;
using System.Linq;
using System.Runtime;
using System.Text;
using System.Threading.Tasks;
using Inventory.Services.Helper;

namespace Inventory.Service.Models
{
    public class Item: EntityBase
    {
        [Required(ErrorMessage = "RequiredAttribute")]
        [RegularExpression(RegexConstants.AlphaNumericWithSpecialCharacters, ErrorMessage = "Only Number/Alphabets are allowed")] 
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Description { get; set; }
        public Status StatusId { get; set; } 
    }
}
