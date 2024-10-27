namespace OrderManagementSystem.Model
{
    public class OrderInfo
    {
        public int OrderID { get; set; }
        public string Vendor { get; set; }
        public decimal OrderAmount { get; set; }
        public int OrderNumber { get; set; }
        public string Shop { get; set; }
        public DateTime OrderDate { get; set; }
    }
}
