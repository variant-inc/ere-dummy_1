using System.Collections.Generic;
using TicketType.Microservice.Core.Models;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface ICreateTickets
    {
        List<TicketMessage> CreateTicketsFromTractorData(IEnumerable<TractorData> tractorData);
        List<TicketMessage> CreateTicketsFromDriverData(IEnumerable<DriverData> driverData);
        List<TicketMessage> CreateTicketsFromOrderData(IEnumerable<OrderData> orderData);
    }
}