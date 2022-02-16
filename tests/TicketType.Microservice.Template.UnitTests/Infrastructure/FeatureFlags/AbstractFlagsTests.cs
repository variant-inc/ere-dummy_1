using Moq;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract;
using Xunit;

namespace TicketType.Microservice.Template.UnitTests.Infrastructure.FeatureFlags
{
    public class AbstractFlagsTests
    {
        public const string BoolFlagId = "bool-flag";
        public const string StringFlagId = "string-flag";

        [Theory]
        [InlineData(true)]
        [InlineData(false)]
        public void TestBooleanFeatureFlag(bool flagValue)
        {
            var valProviderMock = new Mock<IBooleanFeatureFlagValueProvider>();
            valProviderMock.Setup(x => x.GetBooleanFlagValue(BoolFlagId)).Returns(flagValue).Verifiable();

            var testFlag = new TestBooleanFlag(valProviderMock.Object);

            Assert.Equal(flagValue, testFlag.IsOn);
            Assert.Equal(!flagValue, testFlag.IsOff);
            valProviderMock.Verify(x => x.GetBooleanFlagValue(BoolFlagId), Times.Exactly(2));
        }

        [Fact]
        public void TestStringFeatureFlag()
        {
            string flagValue = "some flag val";

            var valProviderMock = new Mock<IStringFeatureFlagValueProvider>();
            valProviderMock.Setup(x => x.GetStringFlagValue(StringFlagId)).Returns(flagValue).Verifiable();

            var testFlag = new TestStringFlag(valProviderMock.Object);

            Assert.Equal(flagValue, testFlag.Value);
            valProviderMock.Verify(x => x.GetStringFlagValue(StringFlagId), Times.Once);
        }

        class TestBooleanFlag : BooleanFeatureFlag
        {
            protected override string FlagId => BoolFlagId;

            public TestBooleanFlag(IBooleanFeatureFlagValueProvider valueProvider) : base(valueProvider)
            {
            }
        }

        class TestStringFlag : StringFeatureFlag
        {
            protected override string FlagId => StringFlagId;

            public TestStringFlag(IStringFeatureFlagValueProvider valueProvider) : base(valueProvider)
            {
            }
        }
    }
}
