using Microsoft.AspNetCore.Mvc.ViewFeatures;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Inventory.Services.Helper
{
    public enum Status : short
    {

        Created = 2,
        Active = 3,
        InActive = 4,
        Deleted = 5,
        Cancelled = 11,
        Pending = 14,
        Completed = 15,
        AwaitingApproval = 16,
        Confirmed = 18,
        Received = 19,
        Updated = 20,
        Picked = 21,
    }
}
