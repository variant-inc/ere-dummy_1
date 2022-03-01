using System.Threading.Tasks;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface IProcessTickets
    {
        Task ProcessDriverTickets(string topicName);
        Task ProcessTractorTickets(string topicName);
        Task ProcessOrderTickets(string topicName);
    }
}