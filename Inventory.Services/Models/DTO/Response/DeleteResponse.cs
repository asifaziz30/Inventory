using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Models.DTO.Response
{
    public class DeleteResponse
    {
        public bool Deleted { get; set; } = false;
        public string Message { get; set; } = String.Empty;
    }
}
