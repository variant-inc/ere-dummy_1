using Variant.TicketsShared.LaunchDarklyExtensions;

namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags
{
    internal class TestBoolFlag : BooleanFeatureFlag
    {
        protected override string FlagId => "ticketing-testflag";
        public TestBoolFlag(IBooleanFeatureFlagValueProvider valueProvider) : base(valueProvider)
        {
        }
    }
}
