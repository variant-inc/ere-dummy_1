using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface IBusinessLogic
    {
        bool IsTractorDataMatchesTheTicketRequirements(TractorData driverData);
        bool IsDriverDataMatchesTheTicketRequirements(DriverData driverData);
        bool IsOrderDataMatchesTheTicketRequirements(OrderData driverData);
    }
}