
using System;
using System.Linq;
using Serilog.Core;
using Serilog.Events;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using Microsoft.Extensions.Configuration;
using BD.Core.Logging.Models;

namespace BD.Core.Logging.Enrichers
{
    public class CustomEnricher : ILogEventEnricher
    {
        private readonly IEnumerable<string> _excludeProperties;
        private readonly IEnumerable<string> _maskProperties;
        private readonly string _regEx;

        public CustomEnricher(IConfigurationRoot configuration)
        {
            if (configuration == null) throw new ArgumentNullException(nameof(configuration));

            _excludeProperties = configuration.GetSection("Serilog:MaskingProperties:Exclude")
                .AsEnumerable().Select(e => e.Value).OfType<string>();
            _maskProperties = configuration.GetSection("Serilog:MaskingProperties:Mask")
                .AsEnumerable().Select(e => e.Value).OfType<string>();
            _regEx = configuration.GetSection("Serilog:MaskingProperties:RegEx").Value;
        }

        public void Enrich(LogEvent logEvent, ILogEventPropertyFactory propertyFactory)
        {
            #region ADD OR UPDATE PROPERTIES

            var level = Level.DEBUG;

            if (logEvent.Level == LogEventLevel.Information)
            {
                level = Level.INFO;
            }
            else if (logEvent.Level == LogEventLevel.Warning)
            {
                level = Level.WARN;
            }
            else if (logEvent.Level == LogEventLevel.Error)
            {
                level = Level.ERROR;
            }
            else if (logEvent.Level == LogEventLevel.Fatal)
            {
                level = Level.CRITICAL;
            }

            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
                Model.LEVEL_NAME, level.ToString()));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
                Model.LEVEL, (byte)level));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
                Model.DATE_TIME, logEvent.Timestamp.UtcDateTime));
            logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
                Model.EPOCH_TIMESTAMP_NS, (logEvent.Timestamp.ToUnixTimeMilliseconds() * 1000000)));

            #endregion

            #region REMOVE PROPERTIES IF ANY

            foreach (var property in _excludeProperties)
            {
                logEvent.RemovePropertyIfPresent(property);
            }

            #endregion

            #region MASK PROPERTIES

            if (_maskProperties.Any())
            {
                var maskDictionary = new Dictionary<string, string>();

                foreach (var property in logEvent.Properties.Where(prop =>
                             _maskProperties.Contains(prop.Key, StringComparer.CurrentCultureIgnoreCase) &&
                             prop.Value.GetType() == typeof(ScalarValue)))
                {
                    var scalarValue = (ScalarValue)property.Value;

                    if (scalarValue.Value is string input)
                    {
                        maskDictionary.Add(property.Key, Regex.Replace(input,
                            _regEx, m => new string('*', m.Length)));
                    }
                }

                foreach (var (key, value) in maskDictionary)
                {
                    logEvent.AddOrUpdateProperty(propertyFactory.CreateProperty(
                           key, value));
                }
            }
            #endregion
        }
    }
}