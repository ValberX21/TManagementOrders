using TManagementOrders.Domain.Enums;

namespace TManagementOrders.API.Helper
{
    public class Translate
    {
        public string TranslateStatus(StatusOrder? status)
        {
            return status switch
            {
                StatusOrder.NEW => "Novo",
                StatusOrder.PROCESSING => "Em processamento",
                StatusOrder.FINISHED => "Finalizado",
                _ => "Desconhecido"
            };
        }
    }
}
