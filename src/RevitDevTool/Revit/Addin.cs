using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autodesk.Revit.UI;
using RevitDevTool.Theme;
using RevitDevTool.Utils;
using RevitDevTool.View;
using Serilog.Core;

namespace RevitDevTool.Revit
{
  public class Addin : IExternalApplication
  {
    /// <summary>
    /// 程序集路径
    /// </summary>
    private static string _assemblyPath = Assembly.GetExecutingAssembly().Location;

    public Result OnStartup(UIControlledApplication application)
    {
      LoadDependencies();
      AddButton(application);
      AddDockable(application);

      return Result.Succeeded;
    }

    /// <summary>
    /// 加载依赖项
    /// </summary>
    private void LoadDependencies()
    {
      foreach (var depend in Directory.GetFiles(Path.GetDirectoryName(_assemblyPath)!, "*.dll"))
      {
        Assembly.LoadFrom(depend);
      }
    }

    private void AddButton(UIControlledApplication application)
    {
      RibbonPanel rvtRibbonPanel = null;
      if (application.GetRibbonPanels().Any(x => x.Name == "RevitDevTools"))
      {
        rvtRibbonPanel = application.GetRibbonPanels().Find(x => x.Name == "RevitDevTools");
      }
      else
      {
        rvtRibbonPanel = application.CreateRibbonPanel("RevitDevTools");
      }

      PushButtonData data = new PushButtonData("TraceLog",
                                               "TraceLog",
                                               Addin._assemblyPath,
                                               "RevitDevTool.Revit.Command.TraceCommand")
      {
        LargeImage = ImageUtils.GetResourceImage("Images/log.png"),
        LongDescription = "Display trace data",
      };

      rvtRibbonPanel.AddItem(data);

      #region 添加新的命令

      PushButtonData data1 = new PushButtonData("HelloWorldCommand",
                                                "HelloWorld",
                                                Addin._assemblyPath,
                                                "RevitDevTool.Revit.Command.HelloWorldCommand")
      {
        LargeImage = ImageUtils.GetResourceImage("Images/log.png"),
        LongDescription = "测试命令，展示该命令下的日志",
      };

      rvtRibbonPanel.AddItem(data1);

      #endregion 添加新的命令
    }

    private void AddDockable(UIControlledApplication application)
    {
      DockablePaneRegisterUtils.Register<TraceLog>(Resource.TraceGuid, application);
    }

    public Result OnShutdown(UIControlledApplication application)
    {
      return Result.Succeeded;
    }
  }
}