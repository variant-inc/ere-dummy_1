namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract
{
    public abstract class StringFeatureFlag
    {
        public virtual string Value => _valueProvider.GetStringFlagValue(FlagId);

        protected abstract string FlagId { get; }
        private readonly IStringFeatureFlagValueProvider _valueProvider;

        protected StringFeatureFlag(IStringFeatureFlagValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }
    }
}
