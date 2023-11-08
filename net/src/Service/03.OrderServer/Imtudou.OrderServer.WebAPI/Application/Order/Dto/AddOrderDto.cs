namespace Imtudou.OrderServer.WebAPI.Application.Order.Dto
{
    public class AddOrderDto
    {
        public string OrderID { get; set; }
        public string ProductID { get; set; }
        public string UserID { get; set; }
        public int OrderNum { get; set; }
    }
}
