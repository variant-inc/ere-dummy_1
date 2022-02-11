namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract
{
    public abstract class BooleanFeatureFlag
    {
        public virtual bool IsOn => Value;
        public virtual bool IsOff => !Value;

        private bool Value => _valueProvider.GetBooleanFlagValue(FlagId);
        private readonly IBooleanFeatureFlagValueProvider _valueProvider;
        protected abstract string FlagId { get; }

        protected BooleanFeatureFlag(IBooleanFeatureFlagValueProvider valueProvider)
        {
            _valueProvider = valueProvider;
        }
    }
}
