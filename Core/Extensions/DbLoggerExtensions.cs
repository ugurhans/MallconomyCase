using Core.CrossCuttingConcerns.Logging.UgurhansLogger.Entities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;

namespace Core.Extensions
{
  public static class DbLoggerExtensions
  {
    public static ILoggingBuilder AddDbLogger(this ILoggingBuilder builder, Action<DbLoggerOptions> configure)
    {
      builder.Services.Configure(configure);
      return builder;
    }
  }
}
