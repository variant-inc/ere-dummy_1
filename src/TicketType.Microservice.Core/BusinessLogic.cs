using System.Diagnostics.CodeAnalysis;
using TicketType.Microservice.Core.Interfaces;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core
{
    [ExcludeFromCodeCoverage]
    public class BusinessLogic : IBusinessLogic
    {
        public bool IsTractorDataMatchesTheTicketRequirements(TractorData driverData)
        {
            return false;
        }

        public bool IsDriverDataMatchesTheTicketRequirements(DriverData driverData)
        {
            return false;
        }

        public bool IsOrderDataMatchesTheTicketRequirements(OrderData driverData)
        {
            return false;
        }
    }
}