namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract
{
    public interface IBooleanFeatureFlagValueProvider
    {
        bool GetBooleanFlagValue(string flagId);
    }

    public interface IStringFeatureFlagValueProvider
    {
        string GetStringFlagValue(string flagId);
    }
}
