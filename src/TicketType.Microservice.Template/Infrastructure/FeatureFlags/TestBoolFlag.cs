using System.Diagnostics.CodeAnalysis;
using Variant.TicketsShared.LaunchDarklyExtensions;

namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags
{
    [ExcludeFromCodeCoverage]
    internal class TestBoolFlag : BooleanFeatureFlag
    {
        protected override string FlagId => "ticketing-testflag";
        public TestBoolFlag(IBooleanFeatureFlagValueProvider valueProvider) : base(valueProvider)
        {
        }
    }
}
