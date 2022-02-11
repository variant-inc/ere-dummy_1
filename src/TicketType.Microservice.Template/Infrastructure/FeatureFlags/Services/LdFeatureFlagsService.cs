using LaunchDarkly.Sdk;
using LaunchDarkly.Sdk.Server;
using Microsoft.Extensions.Logging;
using LaunchDarkly.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using TicketType.Microservice.Template.Infrastructure.FeatureFlags.Abstract;

namespace TicketType.Microservice.Template.Infrastructure.FeatureFlags.Services
{
    [ExcludeFromCodeCoverage]
    public class LdFeatureFlagsService : IBooleanFeatureFlagValueProvider, IStringFeatureFlagValueProvider
    {
        private static LdClient _client;
        private static User _user;
        private static Dictionary<string, LdValue> _flagValues;
        private static object _lock = new object();

        private readonly ILogger<LdFeatureFlagsService> _logger;

        public LdFeatureFlagsService(ILogger<LdFeatureFlagsService> logger)
        {
            _logger = logger;
        }

        public static void Initialize(string key, string userName, ILoggerFactory loggerFactory)
        {
            _user = User.Builder(userName).Build();

            var config = Configuration.Builder(key)
                .Logging(LdMicrosoftLogging.Adapter(loggerFactory))
                .StartWaitTime(TimeSpan.FromSeconds(20))
                .Build();

            _client = new LdClient(config);

            LoadFlags();
            RegisterToFlagChanges();

            Test(loggerFactory.CreateLogger<LdFeatureFlagsService>());
        }

        //printing deature flag value for testing purposes
        //to be removed after LD code tested on every environment
        private static void Test(ILogger<LdFeatureFlagsService> logger)
        {
            logger.LogWarning("----LaunchDarkly Test Start----");
            if (_flagValues.TryGetValue("ticketing-testflag", out var val))
            {
                logger.LogWarning($"LaunchDarkly ticketing-testflag value: {val}");
            }
            else
            {
                logger.LogWarning("LaunchDarkly pulling ticketing-testflag value failed");
            }
            logger.LogWarning("----LaunchDarkly Test End----");
        }

        public bool GetBooleanFlagValue(string flagId)
        {
            var flagValue = GetFlagValue(flagId);
            return flagValue == LdValue.Null ? false : flagValue.AsBool;
        }

        public string GetStringFlagValue(string flagId)
        {
            var flagValue = GetFlagValue(flagId);
            return flagValue == LdValue.Null ? string.Empty : flagValue.AsString;
        }

        private LdValue GetFlagValue(string flagId)
        {
            lock (_lock)
            {
                if (_flagValues.TryGetValue(flagId, out var value))
                {
                    if (value == LdValue.Null) _logger.LogError($"LaunchDarkly - value for flag: {flagId} is null");
                    return value;
                }
                else
                {
                    _logger.LogError($"LaunchDarkly - requested value for flag: {flagId} not found");
                    return LdValue.Null;
                }
            }
        }

        private static void RegisterToFlagChanges()
        {
            _client.FlagTracker.FlagChanged += (sender, ev) => UpdateFlagValue(ev.Key);
        }

        private static void UpdateFlagValue(string key)
        {
            lock (_lock)
            {
                if (!_flagValues.ContainsKey(key)) return;
                var newValue = _client.JsonVariation(key, _user, LdValue.Null);
                _flagValues[key] = newValue;
            }
        }

        private static void LoadFlags()
        {
            var flagsState = _client.AllFlagsState(_user);
            _flagValues = new Dictionary<string, LdValue>(flagsState.ToValuesJsonMap());
        }
    }
}
