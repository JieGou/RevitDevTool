using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using RevitDevTool.Model;
using Serilog;

namespace RevitDevTool.Revit.Command
{
  /// <remarks>
  /// The "HelloWorld" external command. The class must be Public.
  /// </remarks>
  [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
  public class HelloWorldCommand : IExternalCommand
  {
    // The main Execute method (inherited from IExternalCommand) must be public
    public Autodesk.Revit.UI.Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
      UIDocument uiDoc = commandData.Application.ActiveUIDocument;
      Document doc = uiDoc.Document;

      if (Global.IsStarted)
      {
        Global.Logger.Information("Starting ALKIS-Import");
        Global.Logger.Information($"Finished importing layer {"Test"}");
      }

      TaskDialog.Show("提示", "Hello World!");

      Random random = new Random();
      var num = random.Next(0, 1);

      if (num == 0)
      {
        if (Global.IsStarted)
        {
          Global.Logger.Error("这是错误消息!");
        }

        return Result.Failed;
      }

      return Result.Succeeded;
    }
  }
}