using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.DB;
using RevitDevTool.Theme;
using RevitDevTool.Utils;
using RevitDevTool.View;
using Autodesk.Revit.Attributes;

namespace RevitDevTool.Revit.Command
{
  [Transaction(TransactionMode.Manual)]
  public class TraceCommand : IExternalCommand
  {
    public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
    {
      DockablePaneId dockablePaneId = new DockablePaneId(new Guid(Resource.TraceGuid));

      var dockablePane = commandData.Application.GetDockablePane(dockablePaneId);

      if (DockablePane.PaneIsRegistered(dockablePaneId))
      {
        if (dockablePane.IsShown())
        {
          dockablePane.Hide();
        }
        else
        {
          dockablePane.Show();
        }
      }
      else
      {
        DockablePaneRegisterUtils.Register<TraceLog>(Resource.TraceGuid, commandData.Application);

        dockablePane.Show();
      }

      return Result.Succeeded;
    }
  }
}