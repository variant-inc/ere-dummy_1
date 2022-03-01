using System.Threading.Tasks;
using Variant.TicketsShared.Messaging.Interfaces;

namespace TicketType.Microservice.Core
{
    public interface IDataHandler
    {
        Task ManageChecklistAsync(string topicName, IEntityApiChecklist checklist);
    }
}