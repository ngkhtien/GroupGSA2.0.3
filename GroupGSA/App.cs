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
      RibbonPanel _pnlGroupGSA;

      public Result OnShutdown(UIControlledApplication application)
      {
         return Result.Succeeded;
      }

      /// <summary>
      /// Event when open Revit
      /// </summary>
      /// <param name="application"></param>
      /// <returns></returns>
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

         _pnlGroupGSA = application.CreateRibbonPanel(ConstantsAndMessages.RIBBON_TAB, ConstantsAndMessages.RIBBONPANEL_ABOUT);
         _pnlProjectManagement = application.CreateRibbonPanel(ConstantsAndMessages.RIBBON_TAB, ConstantsAndMessages.RIBBONPANEL_PROJECTMANAGEMENT);

         CreateButtonPanelGroupGSA(constraint, ribbonUtils);
         CreateButtonInPanelProjectManagement(constraint, ribbonUtils);
      }

      private void CreateButtonPanelGroupGSA(GSAConstraint constraint, RibbonUtils ribbonUtils)
      {
         PushButtonData pdAbout = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_ABOUT_NAME,
            ConstantsAndMessages.BUTTON_ABOUT_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdAbout).FullName, ConstantsAndMessages.BUTTON_ABOUT_IMAGE,
            ConstantsAndMessages.BUTTON_ABOUT_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_ABOUT_LONGDESCRIPTION);

         _pnlGroupGSA.AddItem(pdAbout);
      }

      /// <summary>
      /// Create button in panel Project Management
      /// </summary>
      /// <param name="constraint"></param>
      /// <param name="ribbonUtils"></param>
      private void CreateButtonInPanelProjectManagement(GSAConstraint constraint, RibbonUtils ribbonUtils)
      {
         PulldownButton projectCleaner = ribbonUtils.CreatePulldownButton(_pnlProjectManagement, 
            ConstantsAndMessages.PULLDOWN_PROJECTCLEANER_NAME, ConstantsAndMessages.PULLDOWN_PROJECTCLEANER_TEXT, 
            ConstantsAndMessages.PULLDOWN_PROJECTCLEANER_TOOLTIP, ConstantsAndMessages.PULLDOWN_PROJECTCLEANER_IMAGE);

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

         PushButtonData pdDeleteSchedule = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_DELETESCHEDULE_NAME,
            ConstantsAndMessages.BUTTON_DELETESCHEDULE_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdDeleteSchedule).FullName, ConstantsAndMessages.BUTTON_DELETESCHEDULE_IMAGE,
            ConstantsAndMessages.BUTTON_DELETESCHEDULE_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_DELETESCHEDULE_LONGDESCRIPTION);

         PushButtonData pdDeleteLink = ribbonUtils.CreatePushButtonData(ConstantsAndMessages.BUTTON_DELETELINK_NAME,
            ConstantsAndMessages.BUTTON_DELETELINK_TEXT, ConstantsAndMessages.DLL_NAME,
            typeof(CmdDeleteLink).FullName, ConstantsAndMessages.BUTTON_DELETELINK_IMAGE,
            ConstantsAndMessages.BUTTON_DELETELINK_TOOLTIP, constraint.HelperPath,
            ConstantsAndMessages.BUTTON_DELETELINK_LONGDESCRIPTION);

         projectCleaner.AddPushButton(pdDeleteView);
         projectCleaner.AddPushButton(pdDeleteSheet);
         projectCleaner.AddPushButton(pdDeleteSchedule);
         projectCleaner.AddPushButton(pdDeleteLink);
      }
   }
}
