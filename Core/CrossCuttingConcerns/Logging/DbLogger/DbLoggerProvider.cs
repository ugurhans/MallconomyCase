using Core.CrossCuttingConcerns.Logging.UgurhansLogger.Entities;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Core.CrossCuttingConcerns.Logging.UgurhansLogger
{
  [ProviderAlias("Database")]
  public class DbLoggerProvider : ILoggerProvider
  {
    public readonly DbLoggerOptions Options;

    public DbLoggerProvider(IOptions<DbLoggerOptions> _options)
    {
      Options = _options.Value; // Stores all the options.
    }

    /// <summary>
    /// Creates a new instance of the db logger.
    /// </summary>
    /// <param name="categoryName"></param>
    /// <returns></returns>
    public ILogger CreateLogger(string categoryName)
    {
      return new DbLogger(this);
    }

    public void Dispose()
    {
    }
  }
}
