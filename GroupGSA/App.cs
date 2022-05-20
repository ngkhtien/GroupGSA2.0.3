using Autodesk.Revit.UI;
using GroupGSA.Command;
using GroupGSA.Constants;
using GroupGSA.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace GroupGSA
{
   public class App : IExternalApplication
   {
      string path = Assembly.GetExecutingAssembly().Location;
      public Result OnShutdown(UIControlledApplication application)
      {
         return Result.Succeeded;
      }

      public Result OnStartup(UIControlledApplication application)
      {
         CreateRibbonPanel(application);
         return Result.Succeeded;
      }

      private void CreateRibbonPanel(UIControlledApplication application)
      {
         GSAConstraint constraint = new GSAConstraint();
         RibbonUtils ribbonUtils = new RibbonUtils(application.ControlledApplication);

         //if TabName is exist, it will throw Exception
         //In that case, Revit will use the existed Tab instead of creating new one.
         try
         {
            application.CreateRibbonTab(ConstantsAndMessages.RIBBON_TAB);
         }
         catch
         {

         }

         // Create Project Management ribbon panel
         RibbonPanel panelProjectManagement = application.CreateRibbonPanel(ConstantsAndMessages.RIBBON_TAB, ConstantsAndMessages.RIBBONPANEL_PROJECTMANAGEMENT);

         PushButtonData pushButtonData = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_DELETEVIEW_NAME,
            ConstantsAndMessages.BUTTON_DELETEVIEW_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdDeleteView).FullName, ConstantsAndMessages.BUTTON_DELETEVIEW_IMAGE,
            ConstantsAndMessages.BUTTON_DELETEVIEW_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_DELETEVIEW_LONGDESCRIPTION);

         panelProjectManagement.AddItem(pushButtonData);
      }
   }
}
