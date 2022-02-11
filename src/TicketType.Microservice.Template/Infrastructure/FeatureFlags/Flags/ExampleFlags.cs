using System.Diagnostics.CodeAnalysis;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract;

// To create flag: 
// 1. create new FeatureFlag class that inherits BooleanFeatureFlag or StringFeatureFlag class and set FlagId to id created in LaunchDarkly
// 2. register class to DI in FeatureFlagModule
// 3. Inject flag info service to use it (your class inherits property that returns flag value: Value)

// To test flag service that uses feature flag:
// 1. Use FeatureFlagMockFactory (Helpers folder) to get flag object mock

namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags.Flags
{
    [ExcludeFromCodeCoverage]
    // for boolean flag types
    public sealed class YourNewAwesomeFeatureFlag : BooleanFeatureFlag
    {
        // flag id from LaunchDarkly
        protected override string FlagId => "awesome-testflag";

        public YourNewAwesomeFeatureFlag(IBooleanFeatureFlagValueProvider valueProvider) : base(valueProvider)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    // for string flag types
    public sealed class YourNewAwesomeStringFeatureFlag : StringFeatureFlag
    {
        protected override string FlagId => "awesome-string-flag";

        public YourNewAwesomeStringFeatureFlag(IStringFeatureFlagValueProvider valueProvider) : base(valueProvider)
        {
        }
    }
}
