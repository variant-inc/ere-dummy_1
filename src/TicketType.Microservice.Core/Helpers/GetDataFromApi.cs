using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using TicketType.Microservice.Core.Interfaces;
using Variant.TicketsShared.DataSource.Driver;
using Variant.TicketsShared.DataSource.Order.Abstract;
using Variant.TicketsShared.DataSource.Order.RequestModels;
using Variant.TicketsShared.DataSource.Order.ResponseModels;
using Variant.TicketsShared.DataSource.Tractor;
using Variant.TicketsShared.DataSource.Tractor.RequestModels;
using Variant.TicketsShared.DataSource.Tractor.ResponseModels;

namespace TicketType.Microservice.Core.Helpers
{
    public class GetDataFromApi : IGetDataFromApi
    {
        private readonly ILogger _logger;
        private readonly ITractorData _tractorData;
        private readonly IDriversData _driversData;
        private readonly IOrderData _orderData;

        public GetDataFromApi(ILogger<GetDataFromApi> logger, ITractorData tractorData, IDriversData driversData, 
            IOrderData orderData)
        {
            _logger = logger;
            _tractorData = tractorData;
            _driversData = driversData;
            _orderData = orderData;
        }
        
        public async Task<IEnumerable<TractorData>> GetDataFromTractorApi()
        {
            try
            {
                TractorSearchParams tractorSearchParams = null;
                var tractorData = await _tractorData.SearchTractors(tractorSearchParams);
                return tractorData;
            }
            catch
            {
                throw new DataException("Error Fetching Tractors Data");
            }

        }
        
        public async Task<IEnumerable<DriverData>> GetDataFromDriverApi()
        {
            try
            {
                SearchDriverParams searchParameters = null;
                var driverData = await _driversData.SearchDrivers(searchParameters);
                return driverData;
            }
            catch
            {
                throw new DataException("Error Fetching Drivers Data");
            }
        }
        
        public async Task<IEnumerable<OrderData>> GetDataFromOrderApi()
        {
            try
            {
                TractorCompositeIdSearchParams searchParameters = null;
                var orderData = await _orderData.SearchOrdersByTractorData(searchParameters);
                return orderData;
            }
            catch
            {
                throw new DataException("Error Fetching Order Data");
            }
        }
    }
}