using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Models.DbEntity
{
    public class EntityBase
    {
        public EntityBase() => CreatedOn = DateTime.UtcNow;
        public long Id { get; set; }
        public string UserName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public int UpdatedBy { get; set; }
        public DateTime? UpdatedOn { get; set; }
    }
}
