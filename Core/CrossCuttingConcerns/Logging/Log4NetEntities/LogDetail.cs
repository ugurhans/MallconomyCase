using System.Collections.Generic;

namespace Core.CrossCuttingConcerns.Logging.Log4NetEntities
{
  public class LogDetail
  {
    public string MethodName { get; set; }
    public List<LogParameter> LogParameters { get; set; }

  }
}
