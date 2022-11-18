using ClosedXML.Excel;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Core.Utilities.Helpers
{
  public class ExcelHelper : IExcelHelper
  {
    public IDataResult<FileStream> GetExcel<T>(List<T> list, string collectionName, string userName, string subpath)
    {
      if (list.Count == 0 || list == null)
      {
        return new ErrorDataResult<FileStream>("Veri Bulunamadı");
      }
      var workbook = new XLWorkbook();
      workbook.AddWorksheet("sheetName");
      var ws = workbook.Worksheet("sheetName");

      int row = 2;
      int column = 1;

      var nameList = list[0].GetType().GetProperties();

      foreach (var name in nameList)
      {
        ws.Cell(1, column.ToString()).Value = name.Name;
        column++;
      }
      column = 1;
      foreach (var item in list)
      {
        foreach (PropertyInfo prop in item.GetType().GetProperties())
        {
          var type = Nullable.GetUnderlyingType(prop.PropertyType) ?? prop.PropertyType;
          if (type == typeof(decimal) || type == typeof(DateTime))
          {
            ws.Cell(row, column).Value = prop.GetValue(item, null);
            column++;
          }
          else
          {
            ws.Cell(row, column).Value = prop.GetValue(item, null)?.ToString();
            column++;
          }
        }
        column = 1;
        row++;
      }

      string fileGuid = GuidHelper.CreateGuid();
      subpath += @$"{collectionName}/{userName.Trim()}/";
      DirectoryInfo di = Directory.CreateDirectory(subpath);
      System.IO.FileStream fs = new FileStream(@$"{subpath}{fileGuid}.xlsx", FileMode.OpenOrCreate, FileAccess.ReadWrite);

      string fileName = fs.Name;
      fs.Close();
      workbook.SaveAs(fileName);
      return new SuccessDataResult<FileStream>(fs);
    }
  }

}
