namespace Inventory.Service.Models.DTO.Response
{
    public class SaleOrderResponse : SaleOrder
    {
        
        public short TotalItems { get; set; }
        public string  StatusText { get { return StatusId.ToString(); }}

        public int SerialNo { get; set; }
    }
}
