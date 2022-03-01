using System.Collections.Generic;
using System.Threading.Tasks;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core.Interfaces
{
    public interface IGetDataFromApi
    {
        Task<IEnumerable<TractorData>> GetDataFromTractorApi();
        Task<IEnumerable<DriverData>> GetDataFromDriverApi();
        Task<IEnumerable<OrderData>> GetDataFromOrderApi();
    }
}