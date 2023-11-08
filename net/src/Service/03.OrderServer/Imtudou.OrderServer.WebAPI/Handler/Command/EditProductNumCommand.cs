using MediatR;

namespace Imtudou.OrderServer.WebAPI.Handler.Command
{
    public class EditProductNumCommand : INotification
    {
        public string ProductID { get; set; }
    }
}
