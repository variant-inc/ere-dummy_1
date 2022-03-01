using System.Collections.Generic;
using System.Threading.Tasks;
using TicketType.Microservice.Core.Models;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface IPublishMessages
    {
        Task PublishMessageToSnsTopic(List<TicketMessage> tickets);
    }
}