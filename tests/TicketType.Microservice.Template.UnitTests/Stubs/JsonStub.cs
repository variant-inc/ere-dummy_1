namespace TicketType.Microservice.Template.UnitTests.Stubs
{
    public static class JsonStub
    {
        public static string GetGoodJson()
        {
            return @"{""message"":""Is the loneliest number"",""Other"":""Blah blah blah""}";
        }

        public static string GetBadJson()
        {
            return @"{""message""""Is the loneliest number""}";
        }
    }
}