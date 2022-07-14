using System.Windows.Media;

namespace GroupGSA.Constants
{
   public static class ConstantsAndMessages
   {
      public const string DLL_NAME = "GroupGSA.dll";

      #region Ribbons

      public const string LARGE_IMAGE = "GSA.ico";
      public const string SMALL_IMAGE = "icon_16x16.png";
      public const string SMALL_IMAGE_ON = "On_16x16.png";
      public const string SMALL_IMAGE_OFF = "Off_16x16.png";

      public const string RIBBON_TAB = "GroupGSA Tools 3.0.0";
      public const string RIBBONPANEL_ABOUT = "Group GSA";
      public const string RIBBONPANEL_PROJECTMANAGEMENT = "Project Management";
      public const string RIBBONPANEL_PROJECTSETUP = "Project Setup";
      public const string RIBBONPANEL_AUTOMATIONPACKAGE = "Automation Package";

      public const string BUTTON_ABOUT_NAME = "cmdAbout";
      public const string BUTTON_ABOUT_TEXT = "About";
      public const string BUTTON_ABOUT_IMAGE = "GSA.ico";
      public const string BUTTON_ABOUT_TOOLTIP = "About Group GSA Tools 3.0.0";
      public const string BUTTON_ABOUT_LONGDESCRIPTION = "Click to read information about Group GSA Tools 3.0.0";

      public const string PULLDOWN_PROJECTCLEANER_NAME = "ProjectCleaner";
      public const string PULLDOWN_PROJECTCLEANER_TEXT = "Project Cleaner";
      public const string PULLDOWN_PROJECTCLEANER_IMAGE = "ProjectCleaner.ico";
      public const string PULLDOWN_PROJECTCLEANER_TOOLTIP = "Quickly delete views, sheets, schedules and RVT/CAD Links";

      public const string BUTTON_DELETEVIEW_NAME = "cmdDeleteView";
      public const string BUTTON_DELETEVIEW_TEXT = "Purge View";
      public const string BUTTON_DELETEVIEW_IMAGE = "DeleteView.png";
      public const string BUTTON_DELETEVIEW_TOOLTIP = "Purge views to cleanup project file";
      public const string BUTTON_DELETEVIEW_LONGDESCRIPTION = "Cannot delete active view\nPlease switch to retain sheet/schedule to delete all views or switch to {3D View} to keep {3D View}";

      public const string BUTTON_DELETESHEET_NAME = "cmdDeleteSheet";
      public const string BUTTON_DELETESHEET_TEXT = "Purge Sheet";
      public const string BUTTON_DELETESHEET_IMAGE = "DeleteSheet.png";
      public const string BUTTON_DELETESHEET_TOOLTIP = "Purge sheets to cleanup project file";
      public const string BUTTON_DELETESHEET_LONGDESCRIPTION = "Cannot delete active sheet\nPlease switch to retain view to delete all sheets";

      public const string BUTTON_DELETESCHEDULE_NAME = "cmdDeleteSchedule";
      public const string BUTTON_DELETESCHEDULE_TEXT = "Purge Schedule";
      public const string BUTTON_DELETESCHEDULE_IMAGE = "DeleteSchedule.png";
      public const string BUTTON_DELETESCHEDULE_TOOLTIP = "Purge schedule to cleanup project file";
      public const string BUTTON_DELETESCHEDULE_LONGDESCRIPTION = "Cannot delete active schedule\nPlease switch to retain view to delete all schedules";

      public const string BUTTON_DELETELINK_NAME = "cmdDeleteLink";
      public const string BUTTON_DELETELINK_TEXT = "Purge Link";
      public const string BUTTON_DELETELINK_IMAGE = "DeleteLink.png";
      public const string BUTTON_DELETELINK_TOOLTIP = "Purge link to cleanup project file";
      public const string BUTTON_DELETELINK_LONGDESCRIPTION = "";

      #endregion

   }
}
