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

      public const string RIBBON_TAB = "Group GSA Tools 2.0.3";
      public const string RIBBONPANEL_ABOUT = "Information";
      public const string RIBBONPANEL_PROJECTMANAGEMENT = "Project Management";
      public const string RIBBONPANEL_PROJECTSETUP = "Project Setup";
      public const string RIBBONPANEL_AUTOMATIONPACKAGE = "Automation Package";

      public const string PULLDOWN_PROJECTCLEANER_NAME = "ProjectCleaner";
      public const string PULLDOWN_PROJECTCLEANER_TEXT = "Project Cleaner";
      public const string PULLDOWN_PROJECTCLEANER_IMAGE = "ProjectCleaner.ico";
      public const string PULLDOWN_PROJECTCLEANER_TOOLTIP = "Quickly delete views, sheets, schedules and RVT/CAD Links";

      public const string BUTTON_DELETEVIEW_NAME = "cmdDeleteView";
      public const string BUTTON_DELETEVIEW_TEXT = "Delete View";
      public const string BUTTON_DELETEVIEW_IMAGE = "DeleteView.png";
      public const string BUTTON_DELETEVIEW_TOOLTIP = "Delete views to cleanup project file";
      public const string BUTTON_DELETEVIEW_LONGDESCRIPTION = "Cannot delete active view\nPlease switch to retain view/sheet/schedule to delete all views or switch to {3D View} to keep {3D View}";

      public const string BUTTON_DELETESHEET_NAME = "cmdDeleteSheet";
      public const string BUTTON_DELETESHEET_TEXT = "Delete Sheet";
      public const string BUTTON_DELETESHEET_IMAGE = "DeleteSheet.png";
      public const string BUTTON_DELETESHEET_TOOLTIP = "Delete sheets to cleanup project file";
      public const string BUTTON_DELETESHEET_LONGDESCRIPTION = "Cannot delete active sheet\nPlease switch to retain view to delete all sheets";

      public const string BUTTON_DELETESCHEDULE_NAME = "cmdDeleteSchedule";
      public const string BUTTON_DELETESCHEDULE_TEXT = "Delete Schedule";
      public const string BUTTON_DELETESCHEDULE_IMAGE = "DeleteSchedule.png";
      public const string BUTTON_DELETESCHEDULE_TOOLTIP = "Delete schedule to cleanup project file";
      public const string BUTTON_DELETESCHEDULE_LONGDESCRIPTION = "Cannot delete active schedule\nPlease switch to retain view to delete all schedules";

      #endregion

   }
}
