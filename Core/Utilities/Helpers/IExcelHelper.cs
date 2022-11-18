#nullable enable
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ClosedXML.Excel;
using Core.Utilities.Results;

namespace Core.Utilities.Helpers
{
  public interface IExcelHelper
  {
    public IDataResult<FileStream> GetExcel<T>(List<T> list, string collectionName, string userName, string subPath);
  }
}
