using System;

namespace TicketType.Microservice.Template.Infrastructure
{
    public class BatchProcessFailedException : ApplicationException
    {
        //thrown when whole batch process fails
        public BatchProcessFailedException() : base(message: "Exception generation batch process failed")
        {

        }
    }
}
