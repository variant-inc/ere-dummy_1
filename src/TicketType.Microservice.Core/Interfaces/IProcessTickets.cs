using System.Threading.Tasks;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface IProcessTickets
    {
        Task ProcessDriverTickets();
        Task ProcessTractorTickets();
        Task ProcessOrderTickets();
    }
}