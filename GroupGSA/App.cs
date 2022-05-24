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
      string _assembly = Assembly.GetExecutingAssembly().Location;
      RibbonPanel _pnlProjectManagement;

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

         // Create Ribbon panel
         _pnlProjectManagement = application.CreateRibbonPanel(ConstantsAndMessages.RIBBON_TAB, ConstantsAndMessages.RIBBONPANEL_PROJECTMANAGEMENT);

         CreateButtonInPanelProjectManagement(constraint, ribbonUtils);
      }

      private void CreateButtonInPanelProjectManagement(GSAConstraint constraint, RibbonUtils ribbonUtils)
      {
         PulldownButton projectCleaner = ribbonUtils.CreatePulldownButton(_pnlProjectManagement, "PROJECTCLEANER",
            "Project Cleaner", "tooltip", ConstantsAndMessages.BUTTON_DELETEVIEW_IMAGE);

         PushButtonData pdDeleteView = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_DELETEVIEW_NAME,
            ConstantsAndMessages.BUTTON_DELETEVIEW_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdDeleteView).FullName, ConstantsAndMessages.BUTTON_DELETEVIEW_IMAGE,
            ConstantsAndMessages.BUTTON_DELETEVIEW_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_DELETEVIEW_LONGDESCRIPTION);

         PushButtonData pdDeleteSheet = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_DELETESHEET_NAME,
            ConstantsAndMessages.BUTTON_DELETESHEET_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdDeleteSheet).FullName, ConstantsAndMessages.BUTTON_DELETESHEET_IMAGE,
            ConstantsAndMessages.BUTTON_DELETESHEET_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_DELETESHEET_LONGDESCRIPTION);

         projectCleaner.AddPushButton(pdDeleteView);
         projectCleaner.AddPushButton(pdDeleteSheet);
      }
   }
}
